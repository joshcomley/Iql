using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Extensions;
using Iql.Serialization;

namespace Iql.Data
{
    public static class EntityCloner
    {
        public static T CloneAs<T>(this T obj,
            IEntityConfigurationBuilder entityConfigurationBuilder, 
            Type entityType,
            Dictionary<object, object> mergeMap = null)
            where T : class
        {
            return (T)obj.Clone(entityConfigurationBuilder, entityType, mergeMap);
        }

        public static object Clone(this object obj, 
            IEntityConfigurationBuilder entityConfigurationBuilder, 
            Type entityType, 
            Dictionary<object, object> mergeMap = null)
        {
            return obj.CloneInternal(entityConfigurationBuilder, entityType, new Dictionary<object, object>(), mergeMap);
        }

        private static List<T> CloneList<T>(
            this List<T> list,
            EntityConfigurationBuilder entityConfigurationBuilder,
            Type entityType,
            Dictionary<object, object> clonedObjects,
            Dictionary<object, object> mergeMap)
        {
            var newList = new List<T>();
            clonedObjects.Add(list, newList);
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < list.Count; i++)
            {
                var clonedItem = list[i].CloneInternal(entityConfigurationBuilder, entityType, clonedObjects, mergeMap);
                newList.Add((T)clonedItem);
            }
            return newList;
        }

        static EntityCloner()
        {
            CloneListMethod = typeof(EntityCloner).GetMethod(
                nameof(CloneList),
                BindingFlags.NonPublic | BindingFlags.Static);
        }

        private static MethodInfo CloneListMethod { get; set; }

        private static object CloneInternal(
            this object obj,
            IEntityConfigurationBuilder entityConfigurationBuilder,
            Type entityType,
            Dictionary<object, object> clonedObjects,
            Dictionary<object, object> mergeMap)
        {
            if (obj == null)
            {
                return null;
            }
            if (clonedObjects.ContainsKey(obj))
            {
                return clonedObjects[obj];
            }
            if (obj.IsArray())
            {
#if !TypeScript
                var listType = (obj as IList).GetListType();
                var listElementType = listType.GetGenericArguments().First();
#else
                var listType = typeof(IList);
                var listElementType = typeof(object);
#endif
                var newList = CloneListMethod
                        .InvokeGeneric(null,
                        new[]
                        {
                            obj,
                            entityConfigurationBuilder,
                            entityType,
                            clonedObjects,
                            mergeMap
                        },
                        listElementType);
                return newList;
            }

            object clone;
            if (mergeMap != null && mergeMap.ContainsKey(obj))
            {
                clone = mergeMap[obj];
            }
            else
            {
                clone = Activator.CreateInstance(entityType);
                if (mergeMap != null)
                {
                    mergeMap.Add(obj, clone);
                }
            }
            clonedObjects.Add(obj, clone);
            var entityConfiguration = entityConfigurationBuilder.GetEntityByType(entityType);
            for (var i = 0; i < entityConfiguration.Properties.Count; i++)
            {
                var property = entityConfiguration.Properties[i];
                if (property.Kind.HasFlag(PropertyKind.Key) ||
                    property.Kind.HasFlag(PropertyKind.Primitive) ||
                    property.Kind.HasFlag(PropertyKind.RelationshipKey))
                {
                    var propertyValue = obj.GetPropertyValue(property);
                    if (propertyValue.ClaimsToBeIql())
                    {
                        propertyValue = (propertyValue as IqlExpression).EnsureIsIql();
                    }
                    clone.SetPropertyValue(property, propertyValue);
                }
            }

            return clone;
        }
    }
}