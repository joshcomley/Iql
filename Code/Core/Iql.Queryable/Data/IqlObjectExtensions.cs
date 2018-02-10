using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data
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

        public static object GetPropertyValueByName<T>(this T obj, string propertyName)
        {
            return obj.GetType().GetRuntimeProperty(propertyName).GetValue(obj);
        }

        public static T GetPropertyValueByNameAs<T>(this object obj, string propertyName)
        {
            return (T)obj.GetType().GetRuntimeProperty(propertyName).GetValue(obj);
        }

        public static void SetPropertyValue<T>(this T obj, IProperty property, object value)
        {
            //property.PropertyInfo.SetValue(obj, value);
            property.PropertySetter(obj, value);
        }

        public static void SetPropertyValueByName<T>(this T obj, string propertyName, object value)
        {
            obj.GetType().GetRuntimeProperty(propertyName).SetValue(obj, value);
        }

        public static void SetPropertyValues<T>(this T obj, CompositeKey compositeKey)
        {
            foreach (var key in compositeKey.Keys)
            {
                obj.GetType().GetRuntimeProperty(key.Name).SetValue(obj, key.Value);
            }
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