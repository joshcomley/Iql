﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iql.Extensions;
using Iql.Queryable.Data;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using TypeSharp.Extensions;

namespace Iql.OData.Data
{
    public static class JsonSerializer
    {
        public static string Serialize(object entity, 
            IDataContext dataContext,
            params IPropertyState[] properties)
        {
            var obj = SerializeInternal(dataContext, entity, properties);
            return obj.ToString();
        }

        private static JObject SerializeInternal(IDataContext dataContext, object entity, IEnumerable<IPropertyState> properties)
        {
            var obj = new JObject();
            if (properties == null)
            {
                properties = new PropertyState[] { };
            }
            var propertyChanges = properties as PropertyState[] ?? properties.ToArray();
            if (!propertyChanges.Any())
            {
                if (dataContext.IsEntityNew(entity, entity.GetType()) == true)
                {
                    propertyChanges = dataContext.EntityNonNullProperties(entity).ToArray();
                }
            }
            foreach (var property in propertyChanges)
            {
                //if (property.ChildChangedProperties.Any() || property.Property.ElementType.IsClass &&
                //    !typeof(string).IsAssignableFrom(property.Property.ElementType))
                //{
                //    var memberType = entity.GetPropertyValue(property.Property).GetType();
                //    if (typeof(IEnumerable).IsAssignableFrom(memberType))
                //    {
                //        var enumerable = (IEnumerable)entity.GetPropertyValue(property.Property);
                //        var array = new JArray();
                //        var i = 0;
                //        foreach (var item in enumerable)
                //        {
                //            if (property.EnumerableChangedProperties.ContainsKey(i))
                //            {
                //                array.Add(SerializeInternal(dataContext, item, property.EnumerableChangedProperties[i]));
                //            }
                //            else
                //            {
                //                array.Add(SerializeInternal(dataContext, item, null));
                //            }
                //            i++;
                //        }
                //        obj[property.Property.Name] = array;
                //    }
                //    else
                //    {
                //        obj[property.Property.Name] = SerializeInternal(dataContext, entity.GetPropertyValue(property.Property), property.ChildChangedProperties);
                //    }
                //}
                //else
                //{
                //    obj[property.Property.Name] = new JValue(entity.GetPropertyValue(property.Property));
                //}
                //if (property.Property.IsCollection)
                //{
                //    propertyValue = (propertyValue as IList).ToArray(property.Property.ElementType);
                //}
                var propertyValue = entity.GetPropertyValue(property.Property);
                if (property.Property.IsCollection)
                {
                    obj[property.Property.Name] = new JArray(propertyValue);
                }
                else
                {
                    obj[property.Property.Name] = new JValue(propertyValue);
                }
            }
            var entityConfiguration = dataContext.EntityConfigurationContext.GetEntityByType(entity.GetType());
            foreach (var key in entityConfiguration.Key.Properties)
            {
                if (propertyChanges.Any(p => p.Property.Name == key.Name))
                {
                    continue;
                }
                obj[key.Name] = new JValue(entity.GetPropertyValueByName(key.Name));
            }
            return obj;
        }
    }
}