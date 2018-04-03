using System.Collections;
using System.Linq;
using System.Reflection;
using Iql.Queryable.Data;
using Iql.Queryable.Data.EntityConfiguration;

namespace Iql.Queryable.Extensions
{
    public static class IqlObjectExtensions
    {
        public static object[] GetPropertyValues<T>(this T obj, IProperty[] properties)
        {
            var arr = new object[properties.Length];
            for(var i = 0; i < properties.Length; i++)
            {
                //arr[i] = property.PropertyInfo.GetValue(obj);
                arr[i] = properties[i].PropertyGetter(obj);
                i++;
            }

            return arr;
        }

        public static object GetPropertyValue<T>(this T obj, IProperty property)
        {
            //return property.PropertyInfo.GetValue(obj);
            return property.PropertyGetter(obj);
        }

        public static T GetPropertyValueAs<T>(this object obj, IProperty property)
        {
            //return (T) property.PropertyInfo.GetValue(obj);
            return (T)property.PropertyGetter(obj);
        }

        public static void SetPropertyValue<T>(this T obj, IProperty property, object value)
        {
            //property.PropertyInfo.SetValue(obj, value);
            property.PropertySetter(obj, value);
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