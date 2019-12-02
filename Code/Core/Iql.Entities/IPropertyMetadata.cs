using System.Collections.Generic;
using System.Linq.Expressions;
using Iql.Entities.InferredValues;
using Iql.Entities.Relationships;

namespace Iql.Entities
{
    public interface IPropertyMetadata : ISimpleProperty, IUserPermission
    {
        IqlCanTranslateKind CanTranslate { get; set; }
        string CanTranslateProperty { get; set; }
        IEnumerable<IRelationship> Relationships { get; }
        ITypeDefinition TypeDefinition { get; set; }
        bool AutoSearchKind { get; set; }
        IqlPropertySearchKind SearchKind { get; set; }
        string PropertyName { get; set; }
        bool Searchable { get; set; }
        bool? Nullable { get; set; }
        void SetInferredWithExpression(LambdaExpression value, bool onlyIfNew = false, InferredValueKind kind = InferredValueKind.Always, bool canOverride = false, params string[] onlyWhenPropertyChanges);
        IList<IInferredValueConfiguration> InferredValueConfigurations { get; set; }
    }
}