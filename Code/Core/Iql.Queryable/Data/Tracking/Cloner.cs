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
    public static class Cloner
    {
        public static T CloneAs<T>(this T obj, IDataContext dataContext, Type entityType, bool cloneRelationships = true)
            where T : class
        {
            return (T)obj.Clone(dataContext, entityType, cloneRelationships);
        }

        public static object Clone(this object obj, IDataContext dataContext, Type entityType, bool cloneRelationships = true)
        {
            if (obj == null)
            {
                return null;
            }
            if (obj.IsArray())
            {
                var collection = Activator.CreateInstance(obj.GetType()) as IList;
                var oldCollection = obj as IEnumerable;
                foreach (var item in oldCollection)
                {
                    collection.Add(item.Clone(dataContext, entityType));
                }
                return collection;
            }
            var clone = Activator.CreateInstance(entityType);
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
                        if (cloneRelationships)
                        {
                            var value = obj.GetPropertyValue(property.Name);
                            if (value != null)
                            {
                                if (!property.IsCollection)
                                {
                                    clone.SetPropertyValue(
                                        property.Name,
                                        CloneKeysOnly(dataContext, property, value)
                                    );
                                }
                                else
                                {
                                    IList newValue;
                                    if (value is IRelatedList)
                                    {
                                        newValue = Activator.CreateInstance(value.GetType(), new object[] { value.GetPropertyValue(nameof(IRelatedList.Owner)), null }) as IList;
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
                                            newValue.Add(CloneKeysOnly(dataContext, property, item));
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