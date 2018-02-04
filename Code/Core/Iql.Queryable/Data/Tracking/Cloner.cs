using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Extensions;
using TypeSharp.Extensions;
#if !TypeScript
using Iql.Queryable.Data.Tracking.Cloning;
#endif

namespace Iql.Queryable.Data.Tracking
{
    public enum RelationshipCloneMode
    {
        DoNotClone,
        KeysOnly,
        Full
    }
    public static class Cloner
    {
        public static T CloneAs<T>(this T obj, IDataContext dataContext, Type entityType, RelationshipCloneMode cloneRelationships)
            where T : class
        {
            return (T)obj.Clone(dataContext, entityType, cloneRelationships);
        }

        public static object Clone(this object obj, IDataContext dataContext, Type entityType,
            RelationshipCloneMode cloneRelationships)
        {
            return obj.CloneInternal(dataContext, entityType, cloneRelationships, new Dictionary<object, object>());
        }

        public static List<T> CloneList<T>(this List<T> list, IDataContext dataContext, Type entityType, RelationshipCloneMode cloneRelationships, Dictionary<object, object> clonedObjects)
        {
            var newList = new List<T>();
            clonedObjects.Add(list, newList);
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < list.Count; i++)
            {
                var clonedItem = list[i].CloneInternal(dataContext, entityType, cloneRelationships, clonedObjects);
                newList.Add((T)clonedItem);
            }
            return newList;
        }

        static Cloner()
        {
            CloneListMethod = typeof(Cloner).GetMethod(
                nameof(CloneList), 
                BindingFlags.Public | BindingFlags.Static);
        }

        public static MethodInfo CloneListMethod { get; set; }

        private static object CloneInternal(this object obj, IDataContext dataContext, Type entityType, RelationshipCloneMode cloneRelationships, Dictionary<object, object> clonedObjects)
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
                var listType = (obj as IList).GetListType();
                var listElementType = listType.GetGenericArguments().First();
                var newList = CloneListMethod
                    .MakeGenericMethod(listElementType)
                    .Invoke(null, new object[]
                    {
                        obj,
                        dataContext,
                        entityType,
                        cloneRelationships,
                        clonedObjects
                    });
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
            var clone = Activator.CreateInstance(entityType);
            clonedObjects.Add(obj, clone);
            var entityConfiguration = dataContext.EntityConfigurationContext.GetEntityByType(entityType);
            foreach (var property in entityConfiguration.Properties)
            {
                switch (property.Kind)
                {
                    case PropertyKind.Key:
                    case PropertyKind.Primitive:
                    case PropertyKind.RelationshipKey:
                        var propertyValue = obj.GetPropertyValue(property);
                        clone.SetPropertyValue(property, propertyValue);
                        break;
                    case PropertyKind.Relationship:
                        if (cloneRelationships != RelationshipCloneMode.DoNotClone)
                        {
                            var value = obj.GetPropertyValue(property);
                            if (value != null)
                            {
                                if (!property.IsCollection)
                                {
                                    clone.SetPropertyValue(
                                        property,
                                        cloneRelationships == RelationshipCloneMode.KeysOnly
                                        ? CloneKeysOnly(dataContext, property, value)
                                        : value.CloneInternal(dataContext, property.ElementType, cloneRelationships, clonedObjects)
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
                                        foreach (var item in oldValue)
                                        {
                                            newValue.Add(cloneRelationships == RelationshipCloneMode.KeysOnly
                                                ? CloneKeysOnly(dataContext, property, item)
                                                : item.CloneInternal(dataContext, property.ElementType, cloneRelationships,
                                                    clonedObjects));
                                        }
                                    }
                                    clone.SetPropertyValue(
                                        property,
                                        newValue
                                    );
                                }
                            }
                        }
                        break;
                }
            }
            return clone;
            //#if TypeScript
            //            return obj;
            //#else
            //            return obj.Copy();
            //#endif
        }

        private static object CloneKeysOnly(IDataContext dataContext, IProperty property, object value)
        {
            var newValue = Activator.CreateInstance(property.Relationship.OtherEnd.Type);
            var relationshipTypeConfiguration =
                dataContext.EntityConfigurationContext.GetEntityByType(property.Relationship
                    .OtherEnd.Type);
            foreach (var relationshipProperty in relationshipTypeConfiguration.Properties)
            {
                if (relationshipProperty.Kind == PropertyKind.Key)
                {
                    newValue.SetPropertyValue(relationshipProperty,
                        value.GetPropertyValue(relationshipProperty));
                }
            }
            return newValue;
        }
    }
}