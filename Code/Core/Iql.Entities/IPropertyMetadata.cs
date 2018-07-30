using System.Collections.Generic;
using System.Linq.Expressions;
using Iql.Entities.Relationships;
using Iql.Entities.Rules;
using Iql.Entities.Rules.Display;
using Iql.Entities.Rules.Relationship;

namespace Iql.Entities
{
    public interface IPropertyMetadata : IPropertyGroup
    {
        IRuleCollection<IRelationshipRule> RelationshipFilterRules { get; set; }
        IEnumerable<IRelationship> Relationships { get; }
        ITypeDefinition TypeDefinition { get; set; }
        string Placeholder { get; set; }
        PropertySearchKind SearchKind { get; set; }
        bool ReadOnly { get; set; }
        bool Hidden { get; set; }
        bool Sortable { get; set; }
        bool Searchable { get; set; }
        bool? Nullable { get; set; }
        LambdaExpression InferredWith { get; set; }
        IqlPropertyPath GetInferredWithPath();
    }
}