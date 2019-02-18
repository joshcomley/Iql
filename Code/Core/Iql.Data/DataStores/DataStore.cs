using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.Crud.Operations.Results;
using Iql.Data.DataStores.NestedSets;
using Iql.Entities;
using Iql.Entities.Events;

namespace Iql.Data.DataStores
{
    public abstract class DataStore : IDataStore
    {
        private EntityConfigurationBuilder _entityConfigurationBuilder;
        private IOfflineDataStore _offlineDataStore;
        public abstract string SerializeEntitiesToJson();

        public EventEmitter<DataSetRetrievedEvent> DataSetRetrieved { get; }
        public DataStore(IOfflineDataStore offlineDataStore = null)
        {
            OfflineDataStore = offlineDataStore;
            DataSetRetrieved = new EventEmitter<DataSetRetrievedEvent>();
        }

        public EntityConfigurationBuilder EntityConfigurationBuilder
        {
            get => _entityConfigurationBuilder;
            set
            {
                _entityConfigurationBuilder = value;
                if (OfflineDataStore != null)
                {
                    OfflineDataStore.EntityConfigurationBuilder = value;
                }
            }
        }

        public IOfflineDataStore OfflineDataStore
        {
            get => _offlineDataStore;
            set
            {
                _offlineDataStore = value;
                if (value != null)
                {
                    value.EntityConfigurationBuilder = EntityConfigurationBuilder;
                }
            }
        }

        public virtual INestedSetsProviderBase NestedSetsProviderForType(Type type)
        {
            return null;
        }

        public virtual INestedSetsProvider<T> NestedSetsProviderFor<T>()
        {
            return (INestedSetsProvider<T>)NestedSetsProviderForType(typeof(T));
        }

        public virtual async Task<AddEntityResult<TEntity>> PerformAddAsync<TEntity>(
            QueuedAddEntityOperation<TEntity> operation) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual async Task<UpdateEntityResult<TEntity>> PerformUpdateAsync<TEntity>(
            QueuedUpdateEntityOperation<TEntity> operation) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual async Task<DeleteEntityResult<TEntity>> PerformDeleteAsync<TEntity>(
            QueuedDeleteEntityOperation<TEntity> operation) where TEntity : class
        {
            throw new NotImplementedException();
        }
        
        public virtual async Task<FlattenedGetDataResult<TEntity>> PerformGetAsync<TEntity>(
            QueuedGetDataOperation<TEntity> operation)
            where TEntity : class
        {
            throw new NotImplementedException();
        }

        protected int FindEntityIndex<TEntity>(
            Type entityType,
            TEntity clone,
            IList<TEntity> data) where TEntity : class
        {
            return Entity.FindIndexOfEntityInSetByEntity(
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