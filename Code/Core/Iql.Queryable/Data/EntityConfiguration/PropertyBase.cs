using System;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public class PropertyBase : IProperty
    {
        public string Name { get; }
        public Type Type { get; }
        public bool IsCollection { get; }
        public Type DeclaringType { get; }

        public PropertyBase(string name, Type type, bool isCollection, Type declaringType)
        {
            Name = name;
            Type = type;
            IsCollection = isCollection;
            DeclaringType = declaringType;
        }
    }
}