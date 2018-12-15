using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Extensions;
using Iql.Entities;
using Iql.Entities.NestedSets;
using Iql.Entities.PropertyGroups.Dates;
using Iql.Entities.Relationships;

namespace Iql.Data.Rendering
{
    public class PropertyDetail
    {
        private PropertyDetail(IPropertyContainer property)
        {
            Property = property;
            PropertyAsPropertyCollection = property as PropertyCollection;
            PropertyAsRelationship = property as RelationshipDetailBase;
            PropertyAsPropertyGroup = property as IPropertyGroup;
            PropertyAsCoreProperty = property as IProperty;
            PropertyAsSimpleProperty = property as ISimpleProperty;
            PropertyAsDateRange = property as IDateRange;
            PropertyAsNestedSet = property as INestedSet;
            PropertyAsEntityConfiguration = property as IEntityConfiguration;

            var kind = ResolveKind(property, out var canShow);
            Kind = kind;
            CanShow = canShow;
        }

        public bool CanShow { get; set; }

        public string Kind { get; set; }
        private static readonly Dictionary<IPropertyContainer, PropertyDetail> Mapping =
            new Dictionary<IPropertyContainer, PropertyDetail>();
        public static PropertyDetail For(IPropertyContainer property)
        {
            if (property == null)
            {
                return null;
            }
            if (!Mapping.ContainsKey(property))
            {
                Mapping[property] = new PropertyDetail(property);
            }
            return Mapping[property];
        }
        private static string ResolveKind(IPropertyContainer property, out bool canShow)
        {
            var kind = "unknown";
            canShow = true;
            var prop = property as PropertyBase;
            var rel = property as RelationshipDetailBase;
            if (prop != null)
            {
                switch (prop.TypeDefinition.Kind)
                {
                    case IqlType.Enum:
                        kind = "enum";
                        break;
                    case IqlType.String:
                        kind = "string";
                        break;
                    case IqlType.Date:
                        kind = "date";
                        break;
                    case IqlType.Boolean:
                        kind = "boolean";
                        break;
                    case IqlType.Integer:
                    case IqlType.Decimal:
                        kind = "number";
                        break;
                    case IqlType.GeometryPolygon:
                    case IqlType.GeographyPolygon:
                        kind = "geopolygon";
                        break;
                    case IqlType.GeometryPoint:
                    case IqlType.GeographyPoint:
                        kind = "geopoint";
                        break;
                }

                if (prop.Kind.HasFlag(PropertyKind.Key))
                {
                    kind = "key";
                    canShow = false;
                }
                else if (prop.Kind.HasFlag(PropertyKind.RelationshipKey))
                {
                    kind = "relationship-key";
                    canShow = false;
                }
                else if (prop.Kind == PropertyKind.Relationship)
                {
                    kind = "relationship";
                }
            }
            else if (rel != null && rel.RelationshipSide == RelationshipSide.Target)
            {
                kind = "relationship-target";
            }
            else if (rel != null && rel.RelationshipSide == RelationshipSide.Source)
            {
                kind = "relationship-source";
            }
            else
            {
                kind = "group";
            }

            return kind;
        }

        public bool IsCollection => IsEntityConfiguration || IsPropertyCollection || IsBasicProperty;
        public bool IsPropertyCollection => PropertyAsPropertyCollection != null;
        public bool IsPropertyGroup => PropertyAsPropertyGroup != null;
        public bool IsCoreProperty => PropertyAsCoreProperty != null;
        public bool IsEntityConfiguration => PropertyAsEntityConfiguration != null;

        public bool IsHorizontalPropertyCollection => IsPropertyCollection &&
                                                      PropertyAsPropertyCollection.ContentAlignment ==
                                                      ContentAlignment.Horizontal;

        public bool IsSimpleProperty => IsPropertyGroup && PropertyAsPropertyGroup.Kind.HasFlag(PropertyKind.Property);
        public bool IsBasicProperty => IsPropertyGroup && PropertyAsPropertyGroup.Kind.HasFlag(PropertyKind.Property);

        public bool IsGeographic => IsCoreProperty && PropertyAsCoreProperty.TypeDefinition != null &&
                                    PropertyAsCoreProperty.TypeDefinition.Type == typeof(IqlPointExpression);

        public bool IsNestedSet => PropertyAsNestedSet != null;
        public bool IsDateRange => PropertyAsDateRange != null;

        public bool IsTargetRelationship =>
            IsRelationship && PropertyAsRelationship.RelationshipSide == RelationshipSide.Target;

        public bool IsSourceRelationship =>
            IsRelationship && PropertyAsRelationship.RelationshipSide == RelationshipSide.Source;

        public bool IsRelationship => PropertyAsRelationship != null;

        public ISimpleProperty PropertyAsSimpleProperty { get; }
        public IEntityConfiguration PropertyAsEntityConfiguration { get; }
        public PropertyCollection PropertyAsPropertyCollection { get; }
        public IPropertyGroup PropertyAsPropertyGroup { get; }
        public IProperty PropertyAsCoreProperty { get; }
        public INestedSet PropertyAsNestedSet { get; }
        public IDateRange PropertyAsDateRange { get; }
        public RelationshipDetailBase PropertyAsRelationship { get; }

        public IPropertyContainer Property { get; set; }

        public async Task<EntityPropertySnapshot> GetSnapshotAsync(
            object entity, 
            IDataContext dataContext
            )
        {
            var canEdit = !IsSimpleProperty || await PropertyAsCoreProperty.IsReadOnlyAsync(entity, dataContext);

            var entityConfiguration = Property as IEntityConfiguration;
            var allProperties =
                entityConfiguration != null
                    ? entityConfiguration.GetDisplayConfiguration(DisplayConfigurationKind.Edit)
                    : Property.GetGroupProperties();
            // If there is no property order configuration, just use all simple properties from this type
            if (entityConfiguration != null && (allProperties == null || allProperties.Length == 0))
            {
                allProperties = entityConfiguration.Properties.ToArray();
            }
            var properties = new List<IPropertyGroup>();
            var filteredProperties = new List<IPropertyGroup>();
            for (var i = 0; i < allProperties.Length; i++)
            {
                var property = allProperties[i];
                var rel = property as RelationshipDetailBase;
                if (property.Kind.HasFlag(PropertyKind.Property))
                {
                    if (property.Kind.HasFlag(PropertyKind.Count))
                    {
                        continue;
                    }
                    var simpleProperty = property as IProperty;
                    if (simpleProperty != null &&
                        simpleProperty.Relationship != null &&
                        simpleProperty.Relationship.ThisIsTarget &&
                        await simpleProperty.Relationship.OtherEnd.Property.IsReadOnlyAsync(entity, dataContext))
                    {
                        continue;
                    }
                }
                else if (rel != null && rel.RelationshipSide == RelationshipSide.Target && !rel.AllowInlineEditing)
                {
                    continue;
                }

                properties.Add(property);
            }

            for (var i = 0; i < properties.Count; i++)
            {
                var propertyGroup = properties[i];
                var detail = For(properties[i]);
                if (detail == null)
                {
                    continue;
                }

                var relationshipDetail = propertyGroup as RelationshipDetailBase;
                if (detail.Kind == "group" &&
                    relationshipDetail != null &&
                    relationshipDetail.Property.EditKind != PropertyEditKind.Edit)
                {
                    continue;
                }

                if (detail.CanShow)
                {
                    filteredProperties.Add(propertyGroup);
                }
            }
            // ReSharper disable once ConvertClosureToMethodGroup
            var childProperties = filteredProperties.Select(_=> For(_)).ToArray();
            var children = new List<EntityPropertySnapshot>();
            for (var i = 0; i < childProperties.Length; i++)
            {
                var child = childProperties[i];
                if (child.Property != Property)
                {
                    children.Add(await child.GetSnapshotAsync(entity, dataContext));
                }
            }

            return new EntityPropertySnapshot(
                this,
                Kind,
                CanShow,
                canEdit,
                children.ToArray());
        }
    }
}