using System;
using System.Threading.Tasks;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.Crud.Operations.Results;
using Iql.Data.DataStores.NestedSets;
using Iql.Entities;
using Iql.Events;

namespace Iql.Data.DataStores
{
    public interface IDataStore
    {
        string Name { get; set; }
        string SerializeStateToJson();
        EventEmitter<DataSetRetrievedEvent> DataSetRetrieved { get; }
        IEntityConfigurationBuilder EntityConfigurationBuilder { get; set; }
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

        Task<FlattenedGetDataResult<TEntity>> PerformCountAsync<TEntity>(QueuedGetDataOperation<TEntity> operation)
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