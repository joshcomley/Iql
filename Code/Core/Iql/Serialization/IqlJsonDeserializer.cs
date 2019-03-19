using System;

namespace Iql.Serialization
{
    public class IqlJsonDeserializer
    {
        public static IqlExpression DeserializeJson(string json)
        {
            var instance = new IqlJsonDeserializerInstance<IqlExpression>(json);
            return instance.Deserialize();
        }

        public static TIql DeserializeJsonAs<TIql>(string json)
            where TIql : IqlExpression
        {
            var instance = new IqlJsonDeserializerInstance<TIql>(json);
            return instance.Deserialize();
        }

        public static object EnsureValueType(object value, Type type, bool nullable = true)
        {
            if (Equals(value, null))
            {
                if (nullable)
                {
                    return null;
                }

                //if (!property.Nullable)
                //{
                //    return property.Type.DefaultValue();
                //}
                return null;
            }

            if (type == typeof(DateTime) && !(value is DateTime))
            {
                if (value is Int64)
                {
                    return new DateTime((long)value);
                }

                return DateTime.Parse(value.ToString());
            }

            if (type == typeof(Boolean) && !(value is Boolean))
            {
                return Boolean.Parse(value.ToString());
            }

#if !TypeScript
            if (type == typeof(object))
            {
                return value;
            }

            var underlyingType = Nullable.GetUnderlyingType(type) ?? type;
            if (underlyingType.IsEnum)
            {
                return Enum.ToObject(underlyingType, value);
            }
            return Convert.ChangeType(value, underlyingType);
#else
            if (type == typeof(String) && !(value is String))
            {
                return value.ToString();
            }

            if (type == typeof(Int32) && !(value is Int32) && !(value is Double))
            {
                return Convert.ToDouble(value.ToString());
            }

            return value;
#endif
        }
    }
}