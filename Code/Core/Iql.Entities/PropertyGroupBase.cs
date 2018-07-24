using Iql.Entities.Rules;
using Iql.Entities.Rules.Display;
using System;

namespace Iql.Entities
{
    public abstract class PropertyGroupBase<T> : MetadataBase, IPropertyGroup
        where T : PropertyGroupBase<T>
    {
        public virtual IEntityConfiguration EntityConfiguration { get; }
        public IRuleCollection<IBinaryRule> ValidationRules { get; set; }
        public IRuleCollection<IDisplayRule> DisplayRules { get; set; }
        public abstract IPropertyGroup[] GetProperties();

        protected PropertyGroupBase(IEntityConfiguration entityConfiguration)
        {
            EntityConfiguration = entityConfiguration;
        }

        public T Configure(Action<T> configure)
        {
            configure((T)this);
            return (T)this;
        }
    }
}