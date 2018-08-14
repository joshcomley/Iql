﻿using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Data.Context;
using Iql.Data.Crud.Operations;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Extensions;
using Iql.OData.Extensions;
using Iql.Queryable.Extensions;
using Newtonsoft.Json.Linq;

namespace Iql.OData.Json
{
    public static class JsonSerializer
    {
        public static string NormalizeDate(this DateTimeOffset offset)
        {
            var timezoneOffsetInHours = -offset.Offset.TotalHours; //UTC minus local time
            var sign = timezoneOffsetInHours >= 0 ? '+' : '-';
            var iso =
                $"{offset.Year.PadLeft('0', 4)}-{offset.Month.PadLeft('0', 2)}-{offset.Day.PadLeft('0', 2)}T{offset.Hour.PadLeft('0', 2)}:{offset.Minute.PadLeft('0', 2)}:{offset.Second.PadLeft('0', 2)}.{offset.Millisecond}{sign}00:00";
            return iso;
        }

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
                if (dataContext.IsEntityNew(entity, entity.GetType()) != false)
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
                if (property.Property.Kind.HasFlag(PropertyKind.Count) || property.Property.Kind.HasFlag(PropertyKind.Relationship))
                {
                    continue;
                }
                if (property.Property.TypeDefinition.IsCollection)
                {
                    obj[property.Property.Name] = new JArray(propertyValue);
                }
                else
                {
                    if (property.Property.TypeDefinition.Type == typeof(DateTimeOffset))
                    {
                        if (propertyValue == null && !property.Property.TypeDefinition.Nullable)
                        {
                            obj[property.Property.Name] = "0001-01-01T00:00:00.0+00:00";
                        }
                        else
                        {
                            obj[property.Property.Name] = new JValue(((DateTimeOffset)propertyValue).NormalizeDate());
                        }
                    }
                    else if (property.Property.TypeDefinition.ConvertedFromType == KnownPrimitiveTypes.Guid && !property.Property.TypeDefinition.Nullable && propertyValue == null)
                    {
                        obj[property.Property.Name] = "00000000-0000-0000-0000-000000000000";
                    }
                    else
                    {
                        obj[property.Property.Name] = new JValue(propertyValue);
                    }
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