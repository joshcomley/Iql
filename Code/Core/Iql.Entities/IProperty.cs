using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Iql.Entities.Events;
using Iql.Entities.Geography;
using Iql.Entities.NestedSets;
using Iql.Entities.PropertyGroups.Dates;
using Iql.Entities.PropertyGroups.Files;
using Iql.Entities.ValueResolvers;

namespace Iql.Entities
{
    public interface ISimpleProperty : IPropertyGroup
    {
        EventEmitter<ValueChangedEvent<PropertyEditKind>> EditKindChanged { get; }
        EventEmitter<ValueChangedEvent<PropertyReadKind>> ReadKindChanged { get; }
        PropertyReadKind ReadKind { get; set; }
        PropertyEditKind EditKind { get; set; }
        bool SupportsInlineEditing { get; set; }
        bool PromptBeforeEdit { get; set; }
        string Placeholder { get; set; }
        bool Sortable { get; set; }
        ISimpleProperty SetReadOnlyAndHidden();
        ISimpleProperty SetReadOnly();
        ISimpleProperty SetHidden();
        ISimpleProperty ResolvePrimaryProperty();
    }

    public interface IProperty : IPropertyMetadata, IConfigurable<IProperty>
    {
        ISimpleProperty PropertyGroup { get; }
        IDateRange DateRange { get; }
        INestedSet NestedSet { get; }
        IGeographicPoint GeographicPoint { get; }
        IFile File { get; }
        bool IsLongitudeProperty { get; }
        bool IsLatitudeProperty { get; }
        bool IsLongitudeOrLatitudeProperty { get; }
        bool IsTitleProperty { get; }
        bool IsPreviewProperty { get; }
        bool IsSubTitleProperty { get; }
#if !TypeScript
        PropertyInfo PropertyInfo { get; set; }
#endif
        EntityRelationship Relationship { get; set; }
        List<EntityRelationship> RelationshipSources { get; set; }
        //IProperty CountRelationship { get; }
        List<object> Helpers { get; set; }
        Func<object, object> GetValue { get; }
        Func<object, object, object> SetValue { get; }
        Dictionary<string, object> CustomInformation { get; }
        IProperty SetNullable(bool nullable = true);
        IProperty IsInferredWithExpression(LambdaExpression expression);
    }
}