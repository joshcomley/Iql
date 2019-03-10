using Iql.Entities.Events;
using Iql.Events;

namespace Iql.Entities
{
    public abstract class SimplePropertyGroupBase<T> : PropertyGroupBase<T>, ISimpleProperty
        where T : IConfigurable<T>
    {
        public bool MarkedReadOnly { get; set; }
        public virtual bool IsInternal => false;
        public virtual bool IsReadOnly => HasReadOnly;
        public virtual bool IsHiddenFromEdit =>
            IsInternal || EditKind == PropertyEditKind.Hidden;

        public virtual bool IsHiddenFromRead =>
            IsInternal || ReadKind == PropertyReadKind.Hidden;

        public virtual bool HasReadOnly
        {
            get
            {
                if (MarkedReadOnly)
                {
                    return true;
                }

                if (Kind.HasFlag(PropertyKind.Key) && !Kind.HasFlag(PropertyKind.RelationshipKey))
                {
                    return true;
                }

                return false;
            }
        }
        private PropertyEditKind _editKind = PropertyEditKind.Edit;
        private PropertyReadKind _readKind = PropertyReadKind.Display;

        protected SimplePropertyGroupBase(IEntityConfiguration entityConfiguration, string key) : base(entityConfiguration, key)
        {
        }

        private EventEmitter<ValueChangedEvent<PropertyEditKind>> _editKindChanged;

        public EventEmitter<ValueChangedEvent<PropertyEditKind>> EditKindChanged => _editKindChanged =
            _editKindChanged ?? new EventEmitter<ValueChangedEvent<PropertyEditKind>>();

        private EventEmitter<ValueChangedEvent<PropertyReadKind>> _readKindChanged;

        public EventEmitter<ValueChangedEvent<PropertyReadKind>> ReadKindChanged => _readKindChanged =
            _readKindChanged ?? new EventEmitter<ValueChangedEvent<PropertyReadKind>>();

        public virtual PropertyReadKind ReadKind
        {
            get => _readKind;
            set
            {
                var oldValue = _readKind;
                _readKind = value;
                if (oldValue != value && _readKindChanged != null)
                {
                    ReadKindChanged.Emit(() => new ValueChangedEvent<PropertyReadKind>(oldValue, value));
                }
            }
        }

        public virtual PropertyEditKind EditKind
        {
            get => _editKind;
            set
            {
                var oldValue = _editKind;
                _editKind = value;
                if (oldValue != value && _editKindChanged != null)
                {
                    EditKindChanged.Emit(() => new ValueChangedEvent<PropertyEditKind>(oldValue, value));
                }
            }
        }

        public bool SupportsInlineEditing { get; set; } = true;
        public bool PromptBeforeEdit { get; set; } = false;
        public string Placeholder { get; set; }
        public bool Sortable { get; set; } = true;
        public ISimpleProperty SetReadOnlyAndHidden()
        {
            SetReadOnly().SetHidden();
            return this;
        }

        public ISimpleProperty SetReadOnly()
        {
            MarkedReadOnly = true;
            return this;
        }

        public ISimpleProperty SetHidden()
        {
            EditKind = PropertyEditKind.Hidden;
            ReadKind = PropertyReadKind.Hidden;
            return this;
        }

        public virtual ISimpleProperty ResolvePrimaryProperty()
        {
            return this;
        }
    }
}