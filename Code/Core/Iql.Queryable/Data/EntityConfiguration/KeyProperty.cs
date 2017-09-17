using System;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public class KeyProperty<TProperty> : IKeyProperty
    {
        public KeyProperty(string name)
        {
            Name = name;
            Type = typeof(TProperty);
        }

        public Type Type { get; set; }

        public string Name { get; set; }
    }
}