using Iql.Entities.Rules;
using Iql.Entities.Rules.Display;
using System;

namespace Iql.Entities
{
    public abstract class PropertyGroupBase<T> : MetadataBase, IPropertyGroup, IConfigurableProperty<T>
        where T : IConfigurableProperty<T>
    {
        public abstract PropertyKind Kind { get; set; }
        public virtual IEntityConfiguration EntityConfiguration { get; }
        public IRuleCollection<IBinaryRule> ValidationRules { get; set; }
        public IRuleCollection<IDisplayRule> DisplayRules { get; set; }
        public abstract IPropertyGroup[] GetGroupProperties();

        protected PropertyGroupBase(IEntityConfiguration entityConfiguration)
        {
            EntityConfiguration = entityConfiguration;
        }

        public T Configure(Action<T> configure)
        {
            configure((T)(object)this);
            return (T)(object)this;
        }
    }
}