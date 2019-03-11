using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Iql.Data.Context;
using Iql.Data.Lists;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Extensions;

#if !TypeScript

#endif

namespace Iql.Data
{
    public enum RelationshipCloneMode
    {
        DoNotClone,
        KeysOnly,
        Full
    }
    public static class Cloner
    {
        public static T CloneAs<T>(this T obj, IEntityConfigurationBuilder entityConfigurationBuilder, Type entityType, RelationshipCloneMode cloneRelationships, Dictionary<object, object> cloneMap = null, Dictionary<object, object> mergeMap = null)
            where T : class
        {
            return (T)obj.Clone(entityConfigurationBuilder, entityType, cloneRelationships, cloneMap, mergeMap);
        }

        public static object Clone(this object obj, IEntityConfigurationBuilder entityConfigurationBuilder, Type entityType,
            RelationshipCloneMode cloneRelationships = RelationshipCloneMode.DoNotClone, Dictionary<object, object> cloneMap = null, Dictionary<object, object> mergeMap = null)
        {
            var clonedObjects = cloneMap ?? new Dictionary<object, object>();
            return obj.CloneInternal(entityConfigurationBuilder, entityType, cloneRelationships, clonedObjects, mergeMap);
        }

        private static List<T> CloneList<T>(
            this List<T> list, 
            EntityConfigurationBuilder entityConfigurationBuilder, 
            Type entityType, 
            RelationshipCloneMode cloneRelationships,
            Dictionary<object, object> clonedObjects,
            Dictionary<object, object> mergeMap)
        {
            var newList = new List<T>();
            clonedObjects.Add(list, newList);
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < list.Count; i++)
            {
                var clonedItem = list[i].CloneInternal(entityConfigurationBuilder, entityType, cloneRelationships, clonedObjects, mergeMap);
                newList.Add((T)clonedItem);
            }
            return newList;
        }

        static Cloner()
        {
            CloneListMethod = typeof(Cloner).GetMethod(
                nameof(CloneList), 
                BindingFlags.NonPublic | BindingFlags.Static);
        }

        private static MethodInfo CloneListMethod { get; set; }

        private static object CloneInternal(
            this object obj, 
            IEntityConfigurationBuilder entityConfigurationBuilder, 
            Type entityType, 
            RelationshipCloneMode cloneRelationships, 
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
                        new []
                        {
                            obj,
                            entityConfigurationBuilder,
                            entityType,
                            cloneRelationships,
                            clonedObjects,
                            mergeMap
                        },
                        listElementType);
                return newList;
                //var collection = Activator.CreateInstance(obj.GetType()) as IList;
                //clonedObjects.Add(obj, collection);
                //var oldCollection = obj as IEnumerable;
                //foreach (var item in oldCollection)
                //{
                //    var clonedItem = item.CloneInternal(dataContext, entityType, cloneRelationships, clonedObjects);
                //    collection.Add(clonedItem);
                //}
                //return collection;
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
                    clone.SetPropertyValue(property, propertyValue);
                }else if (property.Kind.HasFlag(PropertyKind.Relationship))
                {
                    if (cloneRelationships != RelationshipCloneMode.DoNotClone)
                    {
                        var value = obj.GetPropertyValue(property);
                        if (value != null)
                        {
                            if (!property.TypeDefinition.IsCollection)
                            {
                                clone.SetPropertyValue(
                                    property,
                                    cloneRelationships == RelationshipCloneMode.KeysOnly
                                        ? CloneKeysOnly(entityConfigurationBuilder, property, value)
                                        : value.CloneInternal(entityConfigurationBuilder, property.TypeDefinition.ElementType, cloneRelationships,
                                            clonedObjects, mergeMap)
                                );
                            }
                            else
                            {
                                IList newValue;
                                if (value is IRelatedList)
                                {
                                    newValue = (value as IRelatedList).NewEmptyClone(clone);
                                }
                                else
                                {
                                    newValue = Activator.CreateInstance(value.GetType()) as IList;
                                }

                                var oldValue = value as IList;
                                if (oldValue != null && newValue != null)
                                {
                                    for (var j = 0; j < oldValue.Count; j++)
                                    {
                                        var item = oldValue[j];
                                        newValue.Add(cloneRelationships == RelationshipCloneMode.KeysOnly
                                            ? CloneKeysOnly(entityConfigurationBuilder, property, item)
                                            : item.CloneInternal(entityConfigurationBuilder, property.TypeDefinition.ElementType,
                                                cloneRelationships,
                                                clonedObjects,
                                                mergeMap));
                                    }
                                }

                                clone.SetPropertyValue(
                                    property,
                                    newValue
                                );
                            }
                        }
                    }
                }
            }

            return clone;
            //#if TypeScript
            //            return obj;
            //#else
            //            return obj.Copy();
            //#endif
        }

        private static object CloneKeysOnly(IEntityConfigurationBuilder entityConfigurationBuilder, IProperty property, object value)
        {
            var newValue = Activator.CreateInstance(property.Relationship.OtherEnd.Type);
            var relationshipTypeConfiguration =
                entityConfigurationBuilder.GetEntityByType(property.Relationship
                    .OtherEnd.Type);
            for (var i = 0; i < relationshipTypeConfiguration.Properties.Count; i++)
            {
                var relationshipProperty = relationshipTypeConfiguration.Properties[i];
                if (relationshipProperty.Kind.HasFlag(PropertyKind.Key))
                {
                    newValue.SetPropertyValue(relationshipProperty,
                        value.GetPropertyValue(relationshipProperty));
                }
            }

            return newValue;
        }
    }
}