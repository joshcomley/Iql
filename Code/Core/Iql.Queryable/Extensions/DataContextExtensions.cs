using System;
using System.Collections.Generic;
using Iql.Queryable.Data;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Extensions
{
    public static class DataContextExtensions
    {
        public static bool? IsEntityNew(this IDataContext dataContext, object entity, Type entityType)
        {
            var entityState = dataContext.GetEntityState(entity
#if TypeScript
                , entityType
#endif
                );
            return entityState?.IsNew;
        }

        public static CompositeKey EntityKey(this IDataContext dataContext, object entity, Type entityType)
        {
            return dataContext.GetEntityState(entity
#if TypeScript
                , entityType
#endif
            ).Key;
        }

        public static bool EntityHasKey(this IDataContext dataContext, object entity, Type entityType)
        {
            var entityConfiguration = dataContext.EntityConfigurationContext.GetEntityByType(entityType);
            foreach (var keyProperty in entityConfiguration.Key.Properties)
            {
                if (entity.GetPropertyValue(keyProperty.PropertyName).IsDefaultValue())
                {
                    return false;
                }
            }
            return true;
        }
        //public static List<PropertyChange> ChangedProperties<T>(this IDataContext dataContext, T entity) where T : class
        //{
        //    if (dataContext.IsEntityNew(entity))
        //    {
        //        return dataContext.EntityNonNullProperties(entity);
        //    }
        //    var entityConfiguration = dataContext.EntityConfigurationContext.GetEntity<T>();
        //    foreach (var property in entityConfiguration.Properties)
        //    {

        //    }
        //}

        public static List<PropertyChange> EntityNonNullProperties(this IDataContext dataContext, object entity)
        {
            var entityConfiguration = dataContext.EntityConfigurationContext.GetEntityByType(entity.GetType());
            var properties = new List<PropertyChange>();
            foreach (var property in entityConfiguration.Properties)
            {
                var propertyValue = entity.GetPropertyValue(property.Name);
                if (propertyValue != null)
                {
                    properties.Add(new PropertyChange(property, null, propertyValue, null));
                }
            }
            return properties;
        }
    }
}