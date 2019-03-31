using Iql.Entities.Events;
using Iql.Entities.Rules;
using Iql.Entities.Rules.Display;
using Iql.Entities.Rules.Relationship;
using Iql.Events;

namespace Iql.Entities
{
    public interface IPropertyGroup : IPropertyContainer, IUserPermission
    {
        bool CanWriteSet { get; }
        bool CanWrite { get; set; }
        bool ForceDecision { get; set; }
        bool PromptBeforeEdit { get; set; }
        string Placeholder { get; set; }
        bool Sortable { get; set; }
        bool Matches(params string[] names);
        EventEmitter<ValueChangedEvent<PropertyEditKind>> EditKindChanged { get; }
        EventEmitter<ValueChangedEvent<PropertyReadKind>> ReadKindChanged { get; }
        IPropertyGroup SetReadOnlyAndHidden();
        IPropertyGroup SetReadOnly();
        IPropertyGroup SetEditorReadOnly();
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