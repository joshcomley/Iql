using Iql.Entities.Events;
using Iql.Events;

namespace Iql.Entities
{
    public interface ISimpleProperty : IPropertyGroup
    {
        EventEmitter<ValueChangedEvent<PropertyEditKind>> EditKindChanged { get; }
        EventEmitter<ValueChangedEvent<PropertyReadKind>> ReadKindChanged { get; }
        PropertyReadKind ReadKind { get; set; }
        PropertyEditKind EditKind { get; set; }
        bool IsReadOnly { get; }
        bool MarkedReadOnly { get; set; }
        bool HasReadOnly { get; }
        bool IsHiddenFromRead { get; }
        bool IsHiddenFromEdit { get; }
        bool IsInternal { get; }
        bool SupportsInlineEditing { get; set; }
        bool PromptBeforeEdit { get; set; }
        string Placeholder { get; set; }
        bool Sortable { get; set; }
        ISimpleProperty SetReadOnlyAndHidden();
        ISimpleProperty SetReadOnly();
        ISimpleProperty SetHidden();
        ISimpleProperty ResolvePrimaryProperty();
    }
}