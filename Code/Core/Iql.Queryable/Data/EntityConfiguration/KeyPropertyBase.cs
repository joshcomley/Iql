using System;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public class KeyPropertyBase : IKeyProperty
    {
        public string Name { get; }
        public Type Type { get; }

        public KeyPropertyBase(string name, Type type)
        {
            Name = name;
            Type = type;
        }
    }
}