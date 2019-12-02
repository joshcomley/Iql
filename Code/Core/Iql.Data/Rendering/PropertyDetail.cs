using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Evaluation;
using Iql.Data.Extensions;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Entities.NestedSets;
using Iql.Entities.Permissions;
using Iql.Entities.PropertyGroups.Dates;
using Iql.Entities.PropertyGroups.Files;
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

            var kind = property.ResolveKind();
            Kind = kind;
        }

        public string Kind { get; set; }
        private static bool MappingDelayedInitialized;
        private static Dictionary<IPropertyContainer, PropertyDetail> MappingDelayed;
        private static Dictionary<IPropertyContainer, PropertyDetail> Mapping { get { if(!MappingDelayedInitialized) { MappingDelayedInitialized = true; MappingDelayed =             new Dictionary<IPropertyContainer, PropertyDetail>(); } return MappingDelayed; } set { MappingDelayedInitialized = true; MappingDelayed = value; } }
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

        public bool IsCollection => IsEntityConfiguration || IsPropertyCollection || IsBasicProperty;
        public bool IsPropertyCollection => PropertyAsPropertyCollection != null;
        public bool IsPropertyGroup => PropertyAsPropertyGroup != null;
        public bool IsCoreProperty => PropertyAsCoreProperty != null;
        public bool IsEntityConfiguration => PropertyAsEntityConfiguration != null;

        public bool IsHorizontalPropertyCollection => IsPropertyCollection &&
                                                      PropertyAsPropertyCollection.ContentAlignment ==
                                                      ContentAlignment.Horizontal;

        public bool IsSimpleProperty => PropertyAsSimpleProperty != null;//IsPropertyGroup && PropertyAsPropertyGroup.Kind.HasFlag(IqlPropertyKind.Property);
        public bool IsBasicProperty => IsPropertyGroup && PropertyAsPropertyGroup.Kind.HasFlag(IqlPropertyKind.Property);

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
            Type entityType,
            object user,
            Type userType,
            IDataContext dataContext,
            DisplayConfiguration configuration,
            SnapshotOrdering ordering = SnapshotOrdering.Default,
            bool doNotAppendNonConfiguredProperties = false,
            PermissionsEvaluationSession permissionsEvaluationSession = null,
            InferredValueEvaluationSession inferredValueEvaluationSession = null,
            Dictionary<IPropertyContainer, IqlPropertyEditKind> editKindOverrides = null,
            bool isInitialize = false
        )
        {
            permissionsEvaluationSession = permissionsEvaluationSession ?? new PermissionsEvaluationSession();
            inferredValueEvaluationSession = inferredValueEvaluationSession ??
                                             new InferredValueEvaluationSession(permissionsEvaluationSession.Session);
            var canEditAltersPosition = true;
            //var isInferredReadOnly =
            //    await inferredValueEvaluationSession.IsReadOnlyAsync(PropertyAsSimpleProperty, entity, dataContext);
            var canEdit = true;
            var canShow = true;
            if (IsPropertyGroup && PropertyAsPropertyGroup.IsHiddenFromEdit)
            {
                canEdit = false;
                canShow = false;
            }
            if(canEdit)
            {
                var isInferredReadOnly = await inferredValueEvaluationSession.IsReadOnlyWithDataContextAsync(
                    Property, 
                    entity,
                    dataContext, 
                    isInitialize);
                canEdit = !isInferredReadOnly;
            }
            canShow = canShow && (entity == null || CanShow(entity, configuration));
            var canShowReason = SnapshotReasonKind.Configuration;
            var canEditReason = SnapshotReasonKind.Configuration;
            if (editKindOverrides != null && editKindOverrides.ContainsKey(Property))
            {
                var overrideKind = editKindOverrides[Property];
                switch (overrideKind)
                {
                    case IqlPropertyEditKind.Display:
                        canEdit = false;
                        break;
                    case IqlPropertyEditKind.Hidden:
                        canEdit = false;
                        canShow = false;
                        break;
                }
            }
            if (IsPropertyGroup && (canShow || canEdit))
            {
                var permissions = await permissionsEvaluationSession.GetUserPermissionAsync(
                    PropertyAsPropertyGroup.EntityConfiguration.Builder.PermissionManager,
                    PropertyAsPropertyGroup,
                    dataContext,
                    user,
                    userType,
                    entity,
                    entityType,
                    dataContext,
                    dataContext.EntityConfigurationContext);
                switch (permissions)
                {
                    case IqlUserPermission.Read:
                        canEditReason = SnapshotReasonKind.Permissions;
                        canEdit = false;
                        break;
                    case IqlUserPermission.None:
                        canEditReason = SnapshotReasonKind.Permissions;
                        canShowReason = SnapshotReasonKind.Permissions;
                        canEdit = false;
                        canShow = false;
                        break;
                }
            }

            if (!canEdit && canShow)
            {
                var propertyGroup = PropertyAsPropertyGroup;
                if (propertyGroup != null)
                {
                    if (propertyGroup.ResolvedReadOnlyEditDisplayKind == ReadOnlyEditDisplayKind.Hide)
                    {
                        canShow = false;
                    }
                }
            }
            var entityConfiguration = Property as IEntityConfiguration;
            var allProperties =
                entityConfiguration != null
                    ? entityConfiguration.BuildDisplayConfiguration(configuration, doNotAppendNonConfiguredProperties, true)
                    : Property.GetGroupProperties();
            var manuallyAddedCount =
                entityConfiguration == null || configuration == null || configuration.AutoGenerated ? 0 : configuration.Properties.Count;
            // If there is no property order configuration, just use all simple properties from this type
            if (entityConfiguration != null && (allProperties == null || allProperties.Length == 0))
            {
                allProperties = entityConfiguration.Properties.ToArray();
            }
            var properties = new List<IPropertyGroup>();
            for (var i = 0; i < allProperties.Length; i++)
            {
                var property = allProperties[i];
                if (property.Kind.HasFlag(IqlPropertyKind.Property) && property.Kind.HasFlag(IqlPropertyKind.Count))
                {
                    continue;
                }
                //var simpleProperty = property as IProperty;
                properties.Add(property);
            }
            // ReSharper disable once ConvertClosureToMethodGroup
            var childProperties = properties.Select(_ => For(_)).ToArray();
            var children = new List<EntityPropertySnapshot>();
            var wasManuallyAdded = new Dictionary<EntityPropertySnapshot, bool>();
            for (var i = 0; i < childProperties.Length; i++)
            {
                var child = childProperties[i];
                if (child.Property != Property)
                {
                    var propertySnapshot = await child.GetSnapshotAsync(entity, entityType, user, userType, dataContext, configuration, ordering, doNotAppendNonConfiguredProperties, permissionsEvaluationSession, null, editKindOverrides);
                    wasManuallyAdded.Add(propertySnapshot, manuallyAddedCount - 1 >= i);
                    children.Add(propertySnapshot);
                }
            }

            var kids = children.ToArray();
            if (ordering != SnapshotOrdering.Default)
            {
                switch (ordering)
                {
                    case SnapshotOrdering.ReadOnlyFirst:
                    case SnapshotOrdering.ReadOnlyLast:
                        {
                            var readOnly = new List<EntityPropertySnapshot>();
                            var nonReadOnly = new List<EntityPropertySnapshot>();
                            for (var i = 0; i < kids.Length; i++)
                            {
                                var kid = kids[i];
                                if (!kid.CanEdit && kid.CanEditReason == SnapshotReasonKind.Configuration && (!wasManuallyAdded.ContainsKey(kid) || wasManuallyAdded[kid] == false))
                                {
                                    readOnly.Add(kid);
                                }
                                else
                                {
                                    nonReadOnly.Add(kid);
                                }
                            }

                            kids =
                                (ordering == SnapshotOrdering.ReadOnlyFirst
                                    ? readOnly.Concat(nonReadOnly)
                                    : nonReadOnly.Concat(readOnly)).ToArray();
                            break;
                        }
                    case SnapshotOrdering.Standard:
                        {
                            if (configuration.AutoGenerated && configuration.Kind == DisplayConfigurationKind.Read)
                            {
                                kids = PrioritizeForReading(kids).ToArray();
                            }
                            else
                            {
                                var readOnly = new List<EntityPropertySnapshot>();
                                var readOnlyRelationshipCollections = new List<EntityPropertySnapshot>();
                                var nonReadOnlyRelationshipCollections = new List<EntityPropertySnapshot>();
                                var nonReadOnly = new List<EntityPropertySnapshot>();
                                var manuallyAdded = new List<EntityPropertySnapshot>();
                                for (var i = 0; i < kids.Length; i++)
                                {
                                    var kid = kids[i];
                                    if (wasManuallyAdded.ContainsKey(kid) && wasManuallyAdded[kid] == true)
                                    {
                                        manuallyAdded.Add(kid);
                                        continue;
                                    }
                                    if (kid.CanEdit || kid.CanEditReason == SnapshotReasonKind.Permissions)
                                    {
                                        if (kid.IsRelationshipTarget)
                                        {
                                            nonReadOnlyRelationshipCollections.Add(kid);
                                        }
                                        else
                                        {
                                            nonReadOnly.Add(kid);
                                        }
                                    }
                                    else
                                    {
                                        if (kid.IsRelationshipTarget)
                                        {
                                            readOnlyRelationshipCollections.Add(kid);
                                        }
                                        else
                                        {
                                            readOnly.Add(kid);
                                        }
                                    }
                                }
                                kids = (readOnly.Concat(readOnlyRelationshipCollections)
                                    .Concat(nonReadOnlyRelationshipCollections).Concat(manuallyAdded).Concat(nonReadOnly)).ToArray();
                            }


                            break;
                        }
                }
            }

            if (canEdit &&
                kids.Any() &&
                !Property.IsTypeGroup)
            {
                canEdit = kids.Any(_ => _.CanEdit);
            }
            return new EntityPropertySnapshot(
                this,
                Kind,
                canShow,
                canShowReason,
                canEdit,
                canEditReason,
                kids);
        }

        private static IEnumerable<EntityPropertySnapshot> PrioritizeForReading(IEnumerable<EntityPropertySnapshot> properties)
        {
            if (properties == null)
            {
                return properties;
            }
            var propertiesArray = properties as EntityPropertySnapshot[] ?? properties.ToArray();
            double increment = 0.0001;
            return propertiesArray.OrderByWithIndex((property, i) =>
            {
                double index = (i + 1) * increment;
                var order = property.Property.ResolveDisplayOrderKey() + index;
                return order;
            });
        }

        private bool CanShow(object entity,
            DisplayConfiguration displayConfiguration)
        {
            if (IsCoreProperty)
            {
                var hidden = displayConfiguration?.Kind == DisplayConfigurationKind.Edit
                    ? PropertyAsCoreProperty.IsHiddenFromEdit
                    : PropertyAsCoreProperty.IsHiddenFromRead;
                if (hidden)
                {
                    return false;
                }
            }

            if (IsSimpleProperty)
            {
                var property = PropertyAsPropertyGroup;
                if (property.DisplayRules == null)
                {
                    return true;
                }
                var displayRulesAll = property.DisplayRules.All.ToArray();
                if (!displayRulesAll.Any())
                {
                    return true;
                }

                for (var i = 0; i < displayRulesAll.Length; i++)
                {
                    var rule = displayRulesAll[i];
                    if (!rule.Run(entity))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}