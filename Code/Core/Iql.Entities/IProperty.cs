using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Iql.Entities.Geography;
using Iql.Entities.InferredValues;
using Iql.Entities.NestedSets;
using Iql.Entities.PropertyGroups.Dates;
using Iql.Entities.PropertyGroups.Files;
using Iql.Entities.ValueResolvers;

namespace Iql.Entities
{
    public interface IProperty : IPropertyMetadata, IConfigurable<IProperty>, IPropertyMetadataProvider
    {
        bool IsPersistenceKey { get; }
        bool HasInferredWith { get; }
        bool HasInferredWithCondition { get; }
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
        EntityRelationship RelationshipDirect { get; }
        List<EntityRelationship> RelationshipSources { get; set; }
        //IProperty CountRelationship { get; }
        List<object> Helpers { get; set; }
        Func<object, object> GetValue { get; }
        Func<object, object, object> SetValue { get; }
        Dictionary<string, object> CustomInformation { get; }
        IProperty SetNullable(bool nullable = true);
        IProperty IsInferredWithExpression(LambdaExpression expression, bool onlyIfNew = false, InferredValueKind kind = InferredValueKind.Always, bool canOverride = false);
    }
}