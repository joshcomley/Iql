using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.Crud.Operations.Results;
using Iql.Data.DataStores.NestedSets;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Entities.Events;

namespace Iql.Data.DataStores
{
    public interface IDataStore
    {
        EventEmitter<DataSetRetrievedEvent> DataSetRetrieved { get; }
        EntityConfigurationBuilder EntityConfigurationBuilder { get; set; }
        IOfflineDataStore OfflineDataStore { get; set; }
        //DbList<T> TrackGetDataResult<T>(FlattenedGetDataResult<T> response) where T : class;
        INestedSetsProviderBase NestedSetsProviderForType(Type type);
        INestedSetsProvider<T> NestedSetsProviderFor<T>();

        Task<AddEntityResult<TEntity>> PerformAddAsync<TEntity>(QueuedAddEntityOperation<TEntity> operation)
            where TEntity : class;

        //EntityState<TEntity> Update<TEntity>(TEntity entity)
        //    where TEntity : class;

        Task<UpdateEntityResult<TEntity>> PerformUpdateAsync<TEntity>(QueuedUpdateEntityOperation<TEntity> operation)
            where TEntity : class;

        Task<DeleteEntityResult<TEntity>> PerformDeleteAsync<TEntity>(QueuedDeleteEntityOperation<TEntity> operation)
            where TEntity : class;

        Task<FlattenedGetDataResult<TEntity>> PerformGetAsync<TEntity>(QueuedGetDataOperation<TEntity> operation)
            where TEntity : class;
    }

    public class DataSetRetrievedEvent
    {
        public IFlattenedGetDataResult Data { get; }

        public DataSetRetrievedEvent(IFlattenedGetDataResult data)
        {
            Data = data;
        }
    }
}