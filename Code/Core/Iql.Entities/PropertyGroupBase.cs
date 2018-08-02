using Iql.Entities.Rules;
using Iql.Entities.Rules.Display;
using System;
using System.Linq;
using Iql.Entities.Extensions;

namespace Iql.Entities
{
    public abstract class SimplePropertyGroupBase<T> : PropertyGroupBase<T>, ISimpleProperty
        where T : IConfigurable<T>
    {
        protected SimplePropertyGroupBase(IEntityConfiguration entityConfiguration, string key) : base(entityConfiguration, key)
        {
        }

        public PropertyReadKind ReadKind { get; set; } = PropertyReadKind.Display;
        public PropertyEditKind EditKind { get; set; } = PropertyEditKind.Edit;
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
            ReadKind = PropertyReadKind.Display;
            EditKind = PropertyEditKind.Hidden;
            return this;
        }

        public ISimpleProperty SetHidden()
        {
            EditKind = PropertyEditKind.Hidden;
            ReadKind = PropertyReadKind.Hidden;
            return this;
        }
    }

    public abstract class PropertyGroupBase<T> : MetadataBase, IPropertyGroup, IConfigurable<T>
        where T : IConfigurable<T>
    {
        protected IEntityConfiguration _entityConfiguration;
        public string Key { get; set; }
        public string GroupName => this.ResolveGroupName();

        public abstract PropertyKind Kind { get; set; }

        public virtual IEntityConfiguration EntityConfiguration => _entityConfiguration = _entityConfiguration ?? GetGroupProperties().Where(p => p != null).Select(p => p.EntityConfiguration).FirstOrDefault();

        public IRuleCollection<IBinaryRule> ValidationRules { get; set; }
        public IRuleCollection<IDisplayRule> DisplayRules { get; set; }
        public abstract IPropertyGroup[] GetGroupProperties();

        protected PropertyGroupBase(IEntityConfiguration entityConfiguration, string key)
        {
            _entityConfiguration = entityConfiguration;
            Key = key;
        }

        public T Configure(Action<T> configure)
        {
            configure((T)(object)this);
            return (T)(object)this;
        }
    }
}