using System;
using System.Collections.Generic;
using Iql.Queryable.Data;
using Iql.Queryable.Data.Crud.Operations;

namespace Iql.Queryable.Extensions
{
    public static class DataContextExtensions
    {
        public static bool IsEntityNew(this IDataContext dataContext, object entity, Type entityType)
        {
            var entityConfiguration = dataContext.EntityConfigurationContext.GetEntityByType(entityType);
            foreach (var keyProperty in entityConfiguration.Key.Properties)
            {
                var value = entity.GetPropertyValue(keyProperty.PropertyName);
                if (Equals(value, null) ||
                    Equals(value, 0) ||
                    Equals(value, "") ||
                    Equals(value, new DateTime()))
                {
                    return true;
                }
            }
            return false;
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
                    properties.Add(new PropertyChange(property));
                }
            }
            return properties;
        }
    }
}