using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Iql.Queryable.Data;
using Iql.Queryable.Data.Crud.Operations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using TypeSharp.Extensions;

namespace Iql.OData.Data
{
    [DoNotConvert]
    public static class JsonSerializer
    {
        public static string Serialize(object entity, 
            IDataContext dataContext,
            params PropertyChange[] properties)
        {
            var obj = Serialize(dataContext, entity, properties);
            return obj.ToString();
        }

        private static JObject Serialize(IDataContext dataContext, object entity, IEnumerable<PropertyChange> properties)
        {
            var obj = new JObject();
            var propertyChanges = properties as PropertyChange[] ?? properties.ToArray();
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
                                array.Add(Serialize(dataContext, item, property.EnumerableChangedProperties[i]));
                            }
                            else
                            {
                                array.Add(Serialize(dataContext, item, new PropertyChange[]{}));
                            }
                            i++;
                        }
                        obj[property.Property.Name] = array;
                    }
                    else
                    {
                        obj[property.Property.Name] = Serialize(dataContext, entity.GetPropertyValue(property.Property.Name), property.ChildChangedProperties);
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