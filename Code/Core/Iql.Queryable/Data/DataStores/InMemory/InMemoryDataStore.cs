﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Crud.Operations.Queued;
using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.Tracking;

namespace Iql.Queryable.Data.DataStores.InMemory
{
    public class InMemoryDataStore : DataStore
    {
        public InMemoryDataStore(IQueryableAdapterBase queryableAdapter)
        {
            QueryableAdapter = queryableAdapter;
        }

        public IQueryableAdapterBase QueryableAdapter { get; }

        public override Task<AddEntityResult<TEntity>> PerformAdd<TEntity>(
            QueuedAddEntityOperation<TEntity> operation)
        {
            //var flattened =
            //    operation.Operation.DataContext.EntityConfigurationContext.FlattenObjectGraph(
            //        operation.Operation.Entity, typeof(TEntity));
            //object rootClone = null;
            //foreach (var entity in flattened)
            //{
            //    var data = operation.Operation.DataContext.GetConfiguration<InMemoryDataStoreConfiguration>()
            //        .GetSourceByType(entity.EntityType);
            //    var clone = entity.Entity.CloneAs(DataContext, entity.EntityType, RelationshipCloneMode.DoNotClone);
            //    var configuration = operation.Operation.DataContext.EntityConfigurationContext.GetEntityByType(
            //        entity.EntityType);
            //    foreach (var key in configuration.Key.Properties)
            //    {
            //        var property = configuration.FindProperty(key.PropertyName);
            //        if (property.Kind == PropertyKind.Key)
            //        {
            //            if (property.Type == typeof(int))
            //            {
            //                clone.SetPropertyValue(key.PropertyName, NextIdInteger(data, property));
            //            }
            //            else if (property.Type == typeof(string))
            //            {
            //                clone.SetPropertyValue(key.PropertyName, NextIdString(data, property));
            //            }
            //        }
            //    }
            //    data.Add(clone);
            //    if (entity.Entity == operation.Operation.Entity)
            //    {
            //        rootClone = clone;
            //    }
            //}


            var data = operation.Operation.DataContext.GetConfiguration<InMemoryDataStoreConfiguration>()
                .GetSourceByType(operation.Operation.EntityType);
            var clone = operation.Operation.Entity.CloneAs(DataContext, operation.Operation.EntityType, RelationshipCloneMode.DoNotClone);
            var configuration = operation.Operation.DataContext.EntityConfigurationContext.GetEntityByType(
                operation.Operation.EntityType);
            foreach (var key in configuration.Key.Properties)
            {
                var property = configuration.FindProperty(key.PropertyName);
                if (property.Kind == PropertyKind.Key)
                {
                    if (property.Type == typeof(int))
                    {
                        clone.SetPropertyValue(key.PropertyName, NextIdInteger(data, property));
                    }
                    else if (property.Type == typeof(string))
                    {
                        clone.SetPropertyValue(key.PropertyName, NextIdString(data, property));
                    }
                }
            }
            data.Add(clone);
            operation.Result.Success = true;
            operation.Result.RemoteEntity = clone;
            return Task.FromResult(operation.Result);
        }

        public int NextIdInteger(IList data, IProperty property)
        {
            int max = 0;
            foreach (var existingEntity in data)
            {
                var value = (int)existingEntity.GetPropertyValue(property.Name);
                if(value > max)
                {
                    max = value;
                }
            }
            return ++max;
        }

        public string NextIdString(IList data, IProperty property)
        {
            return Guid.NewGuid().ToString();
        }

        private static IList<TEntity> DataSet<TEntity>(ICrudOperation operation)
        {
            return operation.DataContext.GetConfiguration<InMemoryDataStoreConfiguration>().GetSource<TEntity>();
        }

        private int FindEntityIndexFromOperation<TEntity>(EntityCrudOperation<TEntity> operation) where TEntity : class
        {
            return FindEntityIndex(
                operation.EntityType,
                operation.Entity,
                DataSet<TEntity>(operation)
            );
        }

        public override Task<UpdateEntityResult<TEntity>> PerformUpdate<TEntity>(
            QueuedUpdateEntityOperation<TEntity> operation)
        {
            var index = FindEntityIndexFromOperation(operation.Operation);
            if (index != -1)
            {
                DataSet<TEntity>(operation.Operation)[index] = operation.Operation.Entity;
            }
            return Task.FromResult(operation.Result);
        }

        public override Task<DeleteEntityResult<TEntity>> PerformDelete<TEntity>(
            QueuedDeleteEntityOperation<TEntity> operation)
        {
            var index = FindEntityIndexFromOperation(operation.Operation);
            if (index != -1)
            {
                DataSet<TEntity>(operation.Operation).RemoveAt(0);
            }
            return Task.FromResult(operation.Result);
        }

        public override Task<GetDataResult<TEntity>> PerformGet<TEntity>(QueuedGetDataOperation<TEntity> operation)
        {
            //var data = operation.Operation.DataContext.GetConfiguration<InMemoryDataStoreConfiguration>()
            //    .GetSource<TEntity>();
            var q = operation.Operation.Queryable.ToQueryWithAdapterBase(QueryableAdapter, DataContext);
            //var q = operation.Operation.Queryable.ToQueryWithAdapter(new JavaScriptQueryableAdapter());
            operation.Result.Data = (DbList<TEntity>) q.ToList();
            //var localQuery = new JavaScriptQuery<TEntity>(
            //    operation.Operation.Queryable,
            //    operation.Operation.DataContext);
            //operation.Result.Data = localQuery.ToList();
            return Task.FromResult(operation.Result);
        }
    }
}