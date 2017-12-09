using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Queryable.Data.EntityConfiguration;
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
                var collection = Activator.CreateInstance(obj.GetType()) as IList;
                clonedObjects.Add(obj, collection);
                var oldCollection = obj as IEnumerable;
                foreach (var item in oldCollection)
                {
                    collection.Add(item.CloneInternal(dataContext, entityType, cloneRelationships, clonedObjects));
                }
                return collection;
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
                        clone.SetPropertyValue(property.Name, obj.GetPropertyValue(property.Name));
                        break;
                    case PropertyKind.Relationship:
                        if (cloneRelationships != RelationshipCloneMode.DoNotClone)
                        {
                            var value = obj.GetPropertyValue(property.Name);
                            if (value != null)
                            {
                                if (!property.IsCollection)
                                {
                                    clone.SetPropertyValue(
                                        property.Name,
                                        cloneRelationships == RelationshipCloneMode.KeysOnly
                                        ? CloneKeysOnly(dataContext, property, value)
                                        : value.CloneInternal(dataContext, property.Type, cloneRelationships, clonedObjects)
                                    );
                                }
                                else
                                {
                                    IList newValue;
                                    if (value is IRelatedList)
                                    {
                                        newValue = Activator.CreateInstance(value.GetType(), new object[] { clone, property.Name, null }) as IList;
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
                                                : item.CloneInternal(dataContext, property.Type, cloneRelationships,
                                                    clonedObjects));
                                        }
                                    }
                                    clone.SetPropertyValue(
                                        property.Name,
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
                    newValue.SetPropertyValue(relationshipProperty.Name,
                        value.GetPropertyValue(relationshipProperty.Name));
                }
            }
            return newValue;
        }
    }
}