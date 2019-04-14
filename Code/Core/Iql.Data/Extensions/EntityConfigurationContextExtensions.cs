using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Data.Lists;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Entities.Relationships;

namespace Iql.Data.Extensions
{
    public static class EntityConfigurationContextExtensions
    {
        public static T EnsureTypedEntity<T>(
            this IEntityConfigurationBuilder context,
            object entity, bool convertRelationships)
            where T : class
        {
            return (T)context.EnsureTypedEntityByType(entity, typeof(T), convertRelationships);
        }

        public static Dictionary<Type, IList> EnsureTypedResult(this IEntityConfigurationBuilder context, Dictionary<Type, IList> data)
        {
            var dic = new Dictionary<Type, IList>();
            foreach (var entry in data)
            {
                dic.Add(entry.Key, context.EnsureTypedListByType(entry.Value, entry.Key, null, null, false));
            }

            return dic;
        }


        public static object EnsureTypedEntityByType(this IEntityConfigurationBuilder context, object entity, Type type, bool convertRelationships)
        {
            if (entity != null && entity.GetType() != type /* prevent infinite recursion */)
            {
                var entityConfiguration = context.GetEntityByType(type);
                var typedEntity = Activator.CreateInstance(type);
                foreach (var property in entityConfiguration.Properties)
                {
                    if (!convertRelationships &&
                        property.Relationship != null &&
                        property.Relationship.ThisEnd.Property == property)
                    {
                        continue;
                    }
                    //var instanceValue = typedEntity.GetPropertyValue(property.Name);
                    var remoteValue = property.TypeDefinition.EnsureValueType(entity.GetPropertyValue(property));

                    if (remoteValue != null)
                    {
                        var isEnum = property.TypeDefinition.Type.IsEnum;
#if !TypeScript
                        if (!isEnum)
                        {
                            var underlyingType = Nullable.GetUnderlyingType(property.TypeDefinition.Type);
                            if (underlyingType != null && underlyingType.IsEnum)
                            {
                                isEnum = true;
                            }
                        }
#endif
                        if (isEnum && remoteValue is string)
                        {
                            try
                            {
                                remoteValue = Enum.Parse(property.TypeDefinition.Type, remoteValue as string);
                            }
                            catch
                            {
                                remoteValue = 0;
                            }
                        }
                        typedEntity.SetPropertyValue(property, remoteValue);
                    }
                }

                if (convertRelationships)
                {
                    foreach (var relationship in entityConfiguration.Relationships)
                    {
                        var isSource = relationship.Source.EntityConfiguration == entityConfiguration;
                        var propertyName = isSource
                            ? relationship.Source.Property
                            : relationship.Target.Property;
                        if (isSource)
                        {
                            switch (relationship.Kind)
                            {
                                case RelationshipKind.OneToMany:
                                case RelationshipKind.OneToOne:
                                    typedEntity.SetPropertyValue(propertyName,
                                        context.EnsureTypedEntityByType(
                                            entity.GetPropertyValue(propertyName),
                                            relationship.Target.Type,
                                            convertRelationships
                                        ));
                                    break;
                                case RelationshipKind.ManyToMany:
                                    typedEntity.SetPropertyValue(propertyName,
                                        context.EnsureTypedListByType((IEnumerable)entity.GetPropertyValue(propertyName), relationship.Source.Type, entity, relationship.Target.Type, convertRelationships));
                                    break;
                            }
                        }
                        else
                        {
                            switch (relationship.Kind)
                            {
                                case RelationshipKind.OneToOne:
                                    typedEntity.SetPropertyValue(propertyName,
                                        context.EnsureTypedEntityByType(
                                            entity.GetPropertyValue(propertyName),
                                            relationship.Source.Type,
                                            convertRelationships)
                                    );
                                    break;
                                case RelationshipKind.OneToMany:
                                case RelationshipKind.ManyToMany:
                                    typedEntity.SetPropertyValue(propertyName,
                                        context.EnsureTypedListByType(
                                            (IEnumerable)entity.GetPropertyValue(propertyName),
                                            relationship.Target.Type,
                                            entity,
                                            relationship.Source.Type,
                                            convertRelationships));
                                    break;
                            }
                        }
                    }
                }
                entity = typedEntity;
            }
            return entity;
        }

        public static IList<T> EnsureTypedList<T>(
            this IEntityConfigurationBuilder context,
            IEnumerable responseData, bool forceNotNull = false)
            where T : class
        {
            return (IList<T>)context.EnsureTypedListByType(responseData, typeof(T), null, null, false, forceNotNull);
        }

        public static IList EnsureTypedListByType(
            this IEntityConfigurationBuilder context,
            IEnumerable responseData, Type type, object owner = null, Type childType = null, bool convertRelationships = false, bool forceNotNull = false)
        {
            IList list = null;
            if (responseData != null || forceNotNull)
            {
                if (childType == null)
                {
                    list = (IList)Activator.CreateInstance(typeof(DbList<>).MakeGenericType(type));
                }
                else
                {
                    list = (IList)Activator.CreateInstance(typeof(RelatedList<,>).MakeGenericType(childType, type), new object[] { owner });
                }
            }
            if (responseData != null)
            {
                foreach (var entity in responseData)
                {
                    var typedEntity = context.EnsureTypedEntityByType(entity, childType ?? type, convertRelationships);
                    list.Add(typedEntity);
                }
            }
            return list;
        }
    }
}