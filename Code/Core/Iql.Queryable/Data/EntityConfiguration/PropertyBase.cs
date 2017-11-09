using System;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public class PropertyBase : IProperty
    {
        public string Name { get; }
        public Type Type { get; }
        public bool IsCollection { get; }
        public Type DeclaringType { get; }
        public string ConvertedFromType { get; }

        public PropertyBase(string name, Type type, bool isCollection, Type declaringType, string convertedFromType)
        {
            Name = name;
            Type = type;
            IsCollection = isCollection;
            DeclaringType = declaringType;
            ConvertedFromType = convertedFromType;
        }
    }
}