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
            return type.GetRuntimeProperty(propertyName)?.GetValue(obj);
        }

        public static T GetPropertyValueByNameAs<T>(this object obj, string propertyName)
        {
            return (T)obj.GetPropertyValueByName(propertyName);
        }

        public static object SetPropertyValueByName<T>(this T obj, string propertyName, object value)
        {
            if (obj is JToken)
            {
                (obj as JToken)[propertyName] = (JToken)((value is JToken) ? value : new JValue(value));
            }
            else
            {
                obj.GetType().GetRuntimeProperty(propertyName).SetValue(obj, value);
            }

            return value;
        }
    }
}