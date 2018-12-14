using System.Collections.Generic;
using System.Linq.Expressions;
using Iql.Entities.InferredValues;
using Iql.Entities.Relationships;

namespace Iql.Entities
{
    public enum PropertyReadKind
    {
        Display = 1,
        Hidden
    }

    public enum PropertyEditKind
    {
        Edit = 1,
        New,
        NewAndPromptForEdit,
        Display,
        Hidden
    }

    public interface IPropertyMetadata : ISimpleProperty
    {
        IEnumerable<IRelationship> Relationships { get; }
        ITypeDefinition TypeDefinition { get; set; }
        PropertySearchKind SearchKind { get; set; }
        string PropertyName { get; set; }
        bool Searchable { get; set; }
        bool? Nullable { get; set; }
        void SetInferredWithExpression(LambdaExpression value, bool onlyIfNew = false, InferredValueMode mode = InferredValueMode.Always, bool canOverride = false);
        IList<IInferredValueConfiguration> InferredValueConfigurations { get; set; }
    }
}