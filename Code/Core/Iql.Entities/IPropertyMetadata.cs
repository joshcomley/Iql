using System.Collections.Generic;
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
        bool HasMediaKey { get; }
        IMediaKey MediaKey { get; set; }
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