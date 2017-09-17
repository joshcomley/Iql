using System.Reflection;

namespace Iql.Queryable.Data
{
    public static class ObjectExtensions
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
    }
}