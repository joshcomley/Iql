using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iql.Queryable.Data;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Extensions;
using Newtonsoft.Json.Linq;
using TypeSharp.Extensions;

namespace Iql.OData.Data
{
    //[DoNotConvert]
    public static class JsonSerializer
    {
        public static string Serialize(object entity, 
            IDataContext dataContext,
            params PropertyChange[] properties)
        {
            var obj = SerializeInternal(dataContext, entity, properties);
            return obj.ToString();
        }

        private static JObject SerializeInternal(IDataContext dataContext, object entity, IEnumerable<PropertyChange> properties)
        {
            var obj = new JObject();
            if (properties == null)
            {
                properties = new PropertyChange[] { };
            }
            var propertyChanges = properties as PropertyChange[] ?? properties.ToArray();
            if (!propertyChanges.Any())
            {
                if (dataContext.IsEntityNew(entity))
                {
                    propertyChanges = dataContext.EntityNonNullProperties(entity).ToArray();
                }
            }
            foreach (var property in propertyChanges)
            {
                if (property.ChildChangedProperties.Any() || property.Property.Type.IsClass &&
                    !typeof(string).IsAssignableFrom(property.Property.Type))
                {
                    var memberType = entity.GetPropertyValue(property.Property.Name).GetType();
                    if (typeof(IEnumerable).IsAssignableFrom(memberType))
                    {
                        var enumerable = (IEnumerable)entity.GetPropertyValue(property.Property.Name);
                        var array = new JArray();
                        var i = 0;
                        foreach (var item in enumerable)
                        {
                            if (property.EnumerableChangedProperties.ContainsKey(i))
                            {
                                array.Add(SerializeInternal(dataContext, item, property.EnumerableChangedProperties[i]));
                            }
                            else
                            {
                                array.Add(SerializeInternal(dataContext, item, null));
                            }
                            i++;
                        }
                        obj[property.Property.Name] = array;
                    }
                    else
                    {
                        obj[property.Property.Name] = SerializeInternal(dataContext, entity.GetPropertyValue(property.Property.Name), property.ChildChangedProperties);
                    }
                }
                else
                {
                    obj[property.Property.Name] = new JValue(entity.GetPropertyValue(property.Property.Name));
                }
            }
            var entityConfiguration = dataContext.EntityConfigurationContext.GetEntityByType(entity.GetType());
            foreach (var key in entityConfiguration.Key.Properties)
            {
                if (propertyChanges.Any(p => p.Property.Name == key.PropertyName))
                {
                    continue;
                }
                obj[key.PropertyName] = new JValue(entity.GetPropertyValue(key.PropertyName));
            }
            return obj;
        }
    }
}