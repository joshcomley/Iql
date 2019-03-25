using Iql.Entities.Events;
using Iql.Entities.Rules;
using Iql.Entities.Rules.Display;
using Iql.Entities.Rules.Relationship;
using Iql.Events;

namespace Iql.Entities
{
    public interface IPropertyGroup : IPropertyContainer
    {
        bool ForceDecision { get; set; }
        bool SupportsInlineEditing { get; set; }
        bool PromptBeforeEdit { get; set; }
        string Placeholder { get; set; }
        bool Sortable { get; set; }
        bool Matches(params string[] names);
        bool IsReadOnly { get; }
        bool MarkedReadOnly { get; set; }
        bool HasReadOnly { get; }
        EventEmitter<ValueChangedEvent<PropertyEditKind>> EditKindChanged { get; }
        EventEmitter<ValueChangedEvent<PropertyReadKind>> ReadKindChanged { get; }
        IPropertyGroup SetReadOnlyAndHidden();
        IPropertyGroup SetReadOnly();
        IPropertyGroup SetHidden();
        bool IsHiddenFromRead { get; }
        bool IsHiddenFromEdit { get; }
        PropertyReadKind ReadKind { get; set; }
        PropertyEditKind EditKind { get; set; }
        IPropertyGroup ResolvePrimaryProperty();
        IPropertyGroup PropertyGroup { get; }
        string GroupKey { get; }
        string Key { get; set; }
        PropertyKind Kind { get; set; }
        IRuleCollection<IRelationshipRule> RelationshipFilterRules { get; set; }
        IRuleCollection<IBinaryRule> ValidationRules { get; set; }
        IRuleCollection<IDisplayRule> DisplayRules { get; set; }
    }
}