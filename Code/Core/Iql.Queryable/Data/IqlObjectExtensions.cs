using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Iql.Queryable.Data
{
    public static class IqlObjectExtensions
    {
        public static object GetPropertyValue<T>(this T obj, string propertyName)
        {
            return obj.GetType().GetRuntimeProperty(propertyName).GetValue(obj);
        }

        public static T GetPropertyValueAs<T>(this object obj, string propertyName)
        {
            return (T) obj.GetType().GetRuntimeProperty(propertyName).GetValue(obj);
        }

        public static void SetPropertyValue<T>(this T obj, string propertyName, object value)
        {
            obj.GetType().GetRuntimeProperty(propertyName).SetValue(obj, value);
        }

#if !TypeScript
        public static void SetFieldValue<T>(this T obj, string propertyName, object value)
        {
            obj.GetType().GetRuntimeFields().Single(f => f.Name == propertyName).SetValue(obj, value);
        }
#endif
        public static bool IsArray(this object obj)
        {
            return obj is IEnumerable && !(obj is string);
        }
    }
}