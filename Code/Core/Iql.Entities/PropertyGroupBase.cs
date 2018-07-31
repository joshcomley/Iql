using Iql.Entities.Rules;
using Iql.Entities.Rules.Display;
using System;
using System.Linq;
using Iql.Entities.Extensions;

namespace Iql.Entities
{
    public abstract class PropertyGroupBase<T> : MetadataBase, IPropertyGroup, IConfigurable<T>
        where T : IConfigurable<T>
    {
        public string Key { get; set; }
        public string GroupName => this.ResolveGroupName();

        public abstract PropertyKind Kind { get; set; }
        public virtual IEntityConfiguration EntityConfiguration { get; }
        public IRuleCollection<IBinaryRule> ValidationRules { get; set; }
        public IRuleCollection<IDisplayRule> DisplayRules { get; set; }
        public abstract IPropertyGroup[] GetGroupProperties();

        protected PropertyGroupBase(IEntityConfiguration entityConfiguration, string key)
        {
            EntityConfiguration = entityConfiguration;
            Key = key;
        }

        public T Configure(Action<T> configure)
        {
            configure((T)(object)this);
            return (T)(object)this;
        }
    }
}