﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Crud.Operations;
using Iql.Data.Evaluation;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Entities.InferredValues;
using Iql.Entities.Validation.Validation;

namespace Iql.Data.Extensions
{
    public static class DataContextExtensions
    {
        internal static void ForMatchingDataContexts(this IDataContext sourceDataContext,
            Action<IDataContext> action)
        {
            var dataContextsDealtWith = new Dictionary<IDataContext, IDataContext>();
            foreach (var dataContext in DataContext.AllDataContexts)
            {
                if (!dataContextsDealtWith.ContainsKey(dataContext))
                {
                    dataContextsDealtWith.Add(dataContext, dataContext);
                    if (dataContext.SynchronicityKey == sourceDataContext.SynchronicityKey && dataContext.EntityConfigurationContext == sourceDataContext.EntityConfigurationContext)
                    {
                        action(dataContext);
                    }
                }
            }
        }

        public static bool? IsEntityNew3(this IDataContext dataContext, object entity
#if TypeScript
            , Type entityType
#endif
        )
        {
#if !TypeScript
            var entityType = entity.GetType();
#endif
            var entityState = dataContext.GetEntityState(entity
#if TypeScript
                , entityType
#endif
                );
            return entityState?.IsNew;
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

        public static CompositeKey EntityKey(this IDataContext dataContext, object entity, Type entityType)
        {
            return dataContext.GetEntityState(entity
#if TypeScript
                , entityType
#endif
            ).LocalKey;
        }

        public static bool EntityHasKey(this IDataContext dataContext, object entity, Type entityType)
        {
            var entityConfiguration = dataContext.EntityConfigurationContext.GetEntityByType(entityType);
            foreach (var keyProperty in entityConfiguration.Key.Properties)
            {
                if (entity.GetPropertyValue(keyProperty).IsDefaultValue(keyProperty.TypeDefinition))
                {
                    return false;
                }
            }
            return true;
        }

        public static List<IPropertyState> EntityNonNullProperties(this IEntityConfigurationBuilder entityConfigurationBuilder, object entity, bool includeNonEditableKey = false)
        {
            var entityConfiguration = entityConfigurationBuilder.GetEntityByType(entity.GetType());
            var properties = new List<IPropertyState>();
            for (var i = 0; i < entityConfiguration.Properties.Count; i++)
            {
                var property = entityConfiguration.Properties[i];
                if (property.TypeDefinition.Kind == IqlType.Collection)
                {
                    continue;
                }

                var propertyValue = entity.GetPropertyValue(property);
                if (propertyValue != null || !property.TypeDefinition.Nullable &&
                    !property.Kind.HasFlag(IqlPropertyKind.Relationship) &&
                    (!property.Kind.HasFlag(IqlPropertyKind.Key) || property.EntityConfiguration.Key.CanWrite ||
                     includeNonEditableKey))
                {
                    var state = new PropertyState(property, null);
                    properties.Add(state);
                    state.RemoteValue = null;
                    state.LocalValue = propertyValue;
                }
            }

            return properties;
        }
    }
}