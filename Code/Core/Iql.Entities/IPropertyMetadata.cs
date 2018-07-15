using System.Collections.Generic;
using Iql.Entities.Relationships;
using Iql.Entities.Rules;
using Iql.Entities.Rules.Display;
using Iql.Entities.Rules.Relationship;

namespace Iql.Entities
{
    public interface IPropertyMetadata : IMetadata
    {
        IRuleCollection<IBinaryRule> ValidationRules { get; set; }
        IRuleCollection<IDisplayRule> DisplayRules { get; set; }
        IRuleCollection<IRelationshipRule> RelationshipFilterRules { get; set; }
        IEnumerable<IRelationship> Relationships { get; }
        ITypeDefinition TypeDefinition { get; set; }
        IMediaKey MediaKey { get; }
        string Placeholder { get; set; }
        PropertyKind Kind { get; set; }
        PropertySearchKind SearchKind { get; set; }
        bool ReadOnly { get; set; }
        bool Hidden { get; set; }
        bool Sortable { get; set; }
        bool Searchable { get; set; }
        bool? Nullable { get; set; }
    }
}