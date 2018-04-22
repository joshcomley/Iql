using System;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public class TypeDetail : ITypeDefinition
    {
        public Type Type { get; }
        public bool Nullable { get; }
        public Type DeclaringType { get; }
        public IqlType Kind { get; }

        public object EnsureValueType(object value)
        {
            if (Equals(value, null))
            {
                if (Nullable)
                {
                    return null;
                }

                //if (!property.Nullable)
                //{
                //    return property.Type.DefaultValue();
                //}
                return null;
            }

            if (Type == typeof(DateTime) && !(value is DateTime))
            {
                if (value is Int64)
                {
                    return new DateTime((long)value);
                }

                return DateTime.Parse(value.ToString());
            }

            if (Type == typeof(Boolean) && !(value is Boolean))
            {
                return Boolean.Parse(value.ToString());
            }

#if !TypeScript
            return Convert.ChangeType(value, Type);
#else
            if (Type == typeof(String) && !(value is String))
            {
                return value.ToString();
            }

            if (Type == typeof(Int32) && !(value is Int32) && !(value is Double))
            {
                return Convert.ToDouble(value.ToString());
            }

            return value;
#endif
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