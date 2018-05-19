using System;
using Iql.Serialization;

namespace Iql.Data.Configuration
{
    public class TypeDetail : ITypeDefinition
    {
        public Type Type { get; }
        public bool Nullable { get; }
        public Type DeclaringType { get; }
        public IqlType Kind { get; }

        public object EnsureValueType(object value)
        {
            return IqlJsonDeserializer.EnsureValueType(value, Type, Nullable);
        }

        public string ConvertedFromType { get; }
        public Type ElementType { get; }
        public bool IsCollection { get; }


        public TypeDetail(Type type, bool nullable, Type declaringType, string convertedFromType, Type elementType, bool isCollection, IqlType kind)
        {
            Type = type;
            Nullable = nullable;
            DeclaringType = declaringType;
            ConvertedFromType = convertedFromType;
            ElementType = elementType;
            IsCollection = isCollection;
            Kind = kind;
        }
    }
}