using System;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Iql.Extensions
{
    public static class IqlObjectExtensions
    {
        public static object GetPropertyValueByName<T>(this T obj, string propertyName)
        {
            if (obj is JToken)
            {
                var value = (obj as JToken)[propertyName];
                if (value is JValue)
                {
                    return (value as JValue).Value;
                }

                return value;
            }

            var type = obj.GetType();
#if !TypeScript
            if (!type.IsClass || type == typeof(string))
            {
                return null;
            }
#endif
            var property = type.GetRuntimeProperty(propertyName);
            if(property == null)
            {
#if !TypeScript
                return type.GetRuntimeField(propertyName)?.GetValue(obj);
#else
                return null;
#endif
            }
            return property.GetValue(obj);
        }

        public static T GetPropertyValueByNameAs<T>(this object obj, string propertyName)
        {
            return (T)obj.GetPropertyValueByName(propertyName);
        }

        public static object SetPropertyValueByName<T>(this T obj, string propertyName, object value)
        {
            if (obj == null)
            {
                throw new Exception(
                    $"Cannot set property \"{propertyName}\" on null object.");
            }
            if (obj is JToken)
            {
                (obj as JToken)[propertyName] = (JToken)((value is JToken) ? value : new JValue(value));
            }
            else
            {
                var runtimeProperty = obj.GetType().GetRuntimeProperty(propertyName);
                if (runtimeProperty == null)
                {
#if TypeScript
                    var fullName = obj.GetType().Name;
#else
                    var fullName = obj.GetType().FullName;
#endif
                    throw new Exception(
                        $"Unable to find property \"{propertyName}\" on object of type \"{fullName}\".");
                }
                runtimeProperty.SetValue(obj, value);
            }

            return value;
        }
    }
}