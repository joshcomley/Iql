using System;

namespace Iql.Entities.SpecialTypes
{
    public abstract class ConfigurableSpecialTypeDefinition<T> : SpecialTypeDefinition, IConfigurable<T>
        where T : ConfigurableSpecialTypeDefinition<T>
    {
        protected ConfigurableSpecialTypeDefinition(IProperty idProperty) : base(idProperty)
        {
        }

        public T Configure(Action<T> configure)
        {
            if (configure != null)
            {
                configure((T) this);
            }
            return (T) this;
        }
    }
}