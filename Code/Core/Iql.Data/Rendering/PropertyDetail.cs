using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Evaluation;
using Iql.Data.Extensions;
using Iql.Entities;
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

        public bool IsCollection => IsEntityConfiguration || IsPropertyCollection || IsBasicProperty;
        public bool IsPropertyCollection => PropertyAsPropertyCollection != null;
        public bool IsPropertyGroup => PropertyAsPropertyGroup != null;
        public bool IsCoreProperty => PropertyAsCoreProperty != null;
        public bool IsEntityConfiguration => PropertyAsEntityConfiguration != null;

        public bool IsHorizontalPropertyCollection => IsPropertyCollection &&
                                                      PropertyAsPropertyCollection.ContentAlignment ==
                                                      ContentAlignment.Horizontal;

        public bool IsSimpleProperty => PropertyAsSimpleProperty != null;//IsPropertyGroup && PropertyAsPropertyGroup.Kind.HasFlag(PropertyKind.Property);
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
            Type entityType,
            object user,
            Type userType,
            IDataContext dataContext,
            DisplayConfiguration configuration,
            SnapshotOrdering ordering = SnapshotOrdering.Default,
            bool appendNonConfiguredProperties = true,
            PermissionsEvaluationSession permissionsEvaluationSession = null,
            InferredValueEvaluationSession inferredValueEvaluationSession = null
        )
        {
            permissionsEvaluationSession = permissionsEvaluationSession ?? new PermissionsEvaluationSession();
            inferredValueEvaluationSession = inferredValueEvaluationSession ??
                                             new InferredValueEvaluationSession(permissionsEvaluationSession.Session);
            var canEditAltersPosition = true;
            var canEdit = !IsSimpleProperty || !await inferredValueEvaluationSession.IsReadOnlyAsync(PropertyAsSimpleProperty, entity, dataContext);
            var canShow = CanShow(entity, dataContext, configuration);
            var canShowReason = SnapshotReasonKind.Configuration;
            var canEditReason = SnapshotReasonKind.Configuration;
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
                    ? entityConfiguration.BuildDisplayConfiguration(configuration, appendNonConfiguredProperties)
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
                if (property.Kind.HasFlag(PropertyKind.Property) && property.Kind.HasFlag(PropertyKind.Count))
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
                    var propertySnapshot = await child.GetSnapshotAsync(entity, entityType, user, userType, dataContext, configuration, ordering, appendNonConfiguredProperties, permissionsEvaluationSession);
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
                            var readOnly = new List<EntityPropertySnapshot>();
                            var readOnlyRelationshipCollections = new List<EntityPropertySnapshot>();
                            var nonReadOnlyRelationshipCollections = new List<EntityPropertySnapshot>();
                            var nonReadOnly = new List<EntityPropertySnapshot>();
                            for (var i = 0; i < kids.Length; i++)
                            {
                                var kid = kids[i];
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
                                .Concat(nonReadOnlyRelationshipCollections).Concat(nonReadOnly)).ToArray();

                            break;
                        }
                }
            }

            if (canEdit && kids.Any())
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

        private bool CanShow(object entity,
            IDataContext dataContext,
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