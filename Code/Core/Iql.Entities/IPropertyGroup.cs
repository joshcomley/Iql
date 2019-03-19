using Iql.Entities.Rules;
using Iql.Entities.Rules.Display;
using Iql.Entities.Rules.Relationship;

namespace Iql.Entities
{
    public interface IPropertyGroup : IPropertyContainer
    {
        string GroupKey { get; }
        string Key { get; set; }
        PropertyKind Kind { get; set; }
        IRuleCollection<IRelationshipRule> RelationshipFilterRules { get; set; }
        IRuleCollection<IBinaryRule> ValidationRules { get; set; }
        IRuleCollection<IDisplayRule> DisplayRules { get; set; }
    }
}