using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.Crud.Operations.Results;
using Iql.Data.DataStores.NestedSets;
using Iql.Entities;
using Iql.Events;

namespace Iql.Data.DataStores
{
    public abstract class DataStore : IDataStore
    {
        public string Name { get; set; }
        public abstract string SerializeStateToJson();

        public EventEmitter<DataSetRetrievedEvent> DataSetRetrieved { get; }

        protected DataStore(string name)
        {
            Name = name;
            DataSetRetrieved = new EventEmitter<DataSetRetrievedEvent>();
        }

        public virtual IEntityConfigurationBuilder EntityConfigurationBuilder { get; set; }

        public virtual INestedSetsProviderBase NestedSetsProviderForType(Type type)
        {
            return null;
        }

        public virtual INestedSetsProvider<T> NestedSetsProviderFor<T>()
        {
            return (INestedSetsProvider<T>)NestedSetsProviderForType(typeof(T));
        }

        public virtual Task<AddEntityResult<TEntity>> PerformAddAsync<TEntity>(
            QueuedAddEntityOperation<TEntity> operation) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual Task<UpdateEntityResult<TEntity>> PerformUpdateAsync<TEntity>(
            QueuedUpdateEntityOperation<TEntity> operation) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual Task<DeleteEntityResult<TEntity>> PerformDeleteAsync<TEntity>(
            QueuedDeleteEntityOperation<TEntity> operation) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual Task<FlattenedGetDataResult<TEntity>> PerformGetAsync<TEntity>(
            QueuedGetDataOperation<TEntity> operation)
            where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual Task<FlattenedGetDataResult<TEntity>> PerformCountAsync<TEntity>(
            QueuedGetDataOperation<TEntity> operation) 
            where TEntity : class
        {
            throw new NotImplementedException();
        }

        protected int FindEntityIndex<TEntity>(
            IEntityConfigurationBuilder builder,
            Type entityType,
            TEntity clone,
            IList<TEntity> data) where TEntity : class
        {
            return Entity.FindIndexOfEntityInSetByEntity(
                builder,
                clone,
                data
            );
        }

        protected int FindEntityIndexByKey<TEntity>(
            Type entityType,
            CompositeKey key,
            IList<TEntity> data) where TEntity : class
        {
            return Entity.FindIndexOfEntityByKey(
                data,
                key
            );
        }
    }
}