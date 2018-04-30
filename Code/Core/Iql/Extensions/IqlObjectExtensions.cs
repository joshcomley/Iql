using System.Reflection;

namespace Iql.Extensions
{
    public static class IqlObjectExtensions
    {
        public static object GetPropertyValueByName<T>(this T obj, string propertyName)
        {
            return obj.GetType().GetRuntimeProperty(propertyName).GetValue(obj);
        }

        public static T GetPropertyValueByNameAs<T>(this object obj, string propertyName)
        {
            return (T)obj.GetType().GetRuntimeProperty(propertyName).GetValue(obj);
        }

        public static void SetPropertyValueByName<T>(this T obj, string propertyName, object value)
        {
            obj.GetType().GetRuntimeProperty(propertyName).SetValue(obj, value);
        }
    }
}