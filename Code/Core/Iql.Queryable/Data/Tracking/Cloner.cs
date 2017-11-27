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
    [DoNotConvert]
    public static class Cloner
    {
        public static T CloneAs<T>(this T obj, IDataContext dataContext, Type entityType)
            where T : class
        {
            return (T) obj.Clone(dataContext, entityType);
        }

        public static object Clone(this object obj, IDataContext dataContext, Type entityType)
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
                        if (!property.IsCollection)
                        {
                            var value = obj.GetPropertyValue(property.Name);
                            if (value != null)
                            {
                                var newValue = Activator.CreateInstance(property.Relationship.OtherEnd.Type);
                                var relationshipTypeConfiguration = dataContext.EntityConfigurationContext.GetEntityByType(property.Relationship.OtherEnd.Type);
                                foreach (var relationshipProperty in relationshipTypeConfiguration.Properties)
                                {
                                    if (relationshipProperty.Kind == PropertyKind.Key)
                                    {
                                        newValue.SetPropertyValue(relationshipProperty.Name, value.GetPropertyValue(relationshipProperty.Name));
                                    }
                                }
                                clone.SetPropertyValue(property.Name, newValue);
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
    }
}