using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace Iql.Entities.Extensions
{
    public static class IqlQueryableObjectExtensions
    {
        public static PropertyInfo[] GetProperties(this object value)
        {
            return value == null
                ? new PropertyInfo[] { }
                : value.GetType().GetProperties().ToArray();
        }

        public static bool IsDefaultValue(this object value, ITypeDefinition type)
        {
            if (type.ToIqlType() == IqlType.Date)
            {
                if (Equals(value, null))
                {
                    return true;
                }
                var defaultDate = (DateTimeOffset)type.DefaultValue();
                var date = (DateTimeOffset) value;
                return defaultDate.Ticks == date.Ticks;
            }
            return Equals(value, null) || Equals(type.DefaultValue(), value);
        }

        public static object[] GetPropertyValues<T>(this T obj, IProperty[] properties)
        {
            var arr = new object[properties.Length];
            for (var i = 0; i < properties.Length; i++)
            {
                //arr[i] = property.PropertyInfo.GetValue(obj);
                arr[i] = properties[i].GetValue(obj);
                i++;
            }

            return arr;
        }

        public static object GetPropertyValue<T>(this T obj, IProperty property)
        {
            //return property.PropertyInfo.GetValue(obj);
            return property.GetValue(obj);
        }

        public static T GetPropertyValueAs<T>(this object obj, IProperty property)
        {
            //return (T) property.PropertyInfo.GetValue(obj);
            return (T)property.GetValue(obj);
        }

        public static void SetPropertyValue<T>(this T obj, IProperty property, object value)
        {
            //property.PropertyInfo.SetValue(obj, value);
            property.SetValue(obj, value);
        }

        public static void SetPropertyValues<T>(this T obj, CompositeKey compositeKey)
        {
            foreach (var key in compositeKey.Keys)
            {
                obj.GetType().GetRuntimeProperty(key.Name).SetValue(obj, key.Value);
            }
        }

        public static bool IsArray(this object obj)
        {
            return obj is IEnumerable && !(obj is string);
        }
    }
}