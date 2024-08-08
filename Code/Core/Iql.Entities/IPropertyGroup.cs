using Iql.Entities.Events;
using Iql.Entities.Rules;
using Iql.Entities.Rules.Display;
using Iql.Entities.Rules.Relationship;
using Iql.Events;

namespace Iql.Entities
{
    public interface IPropertyGroup : IPropertyContainer
    {
        bool CanWriteSet { get; }
        bool CanWrite { get; set; }
        bool ForceDecision { get; set; }
        bool PromptBeforeEdit { get; set; }
        string Placeholder { get; set; }
        bool Sortable { get; set; }
        bool Matches(params string[] names);
        EventEmitter<ValueChangedEvent<IqlPropertyEditKind, IPropertyGroup>> EditKindChanged { get; }
        EventEmitter<ValueChangedEvent<IqlPropertyReadKind, IPropertyGroup>> ReadKindChanged { get; }
        IPropertyGroup SetReadOnlyAndHidden();
        IPropertyGroup SetReadOnly();
        IPropertyGroup SetEditorReadOnly();
        IPropertyGroup SetHidden();
        bool IsHiddenFromRead { get; }
        bool IsHiddenFromEdit { get; }
        ReadOnlyEditDisplayKind ReadOnlyEditDisplayKind { get; set; }
        ReadOnlyEditDisplayKind ResolvedReadOnlyEditDisplayKind { get; }
        IqlPropertyReadKind ReadKind { get; set; }
        IqlPropertyEditKind EditKind { get; set; }
        IPropertyGroup PropertyGroup { get; }
        string GroupKey { get; }
        string Key { get; set; }
        IqlPropertyKind Kind { get; set; }
        IRuleCollection<IRelationshipRule> RelationshipFilterRules { get; set; }
        IRuleCollection<IBinaryRule> ValidationRules { get; set; }
        IRuleCollection<IDisplayRule>? DisplayRules { get; set; }
    }
}