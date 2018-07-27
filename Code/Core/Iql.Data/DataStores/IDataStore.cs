using System;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.Crud.Operations.Results;
using Iql.Data.DataStores.NestedSets;
using Iql.Data.Relationships;
using Iql.Data.Tracking;
using Iql.Data.Tracking.State;

namespace Iql.Data.DataStores
{
    public interface IDataStore
    {
        INestedSetsProviderBase NestedSetsProviderForType(Type type);
        INestedSetsProvider<T> NestedSetsProviderFor<T>();
        DataTracker DataTracker { get; }
        IQueuedOperation[] GetChanges();
        IQueuedOperation[] GetUpdates();
        IDataContext DataContext { get; set; }
        IRelationshipObserver RelationshipObserver { get; }
        TrackingSetCollection Tracking { get; }

        EntityState<TEntity> Add<TEntity>(TEntity entity)
            where TEntity : class;

#if !TypeScript
        IEntityStateBase Add(object entity);
        IEntityStateBase Delete(object entity);
#endif

        Task<AddEntityResult<TEntity>> PerformAddAsync<TEntity>(QueuedAddEntityOperation<TEntity> operation)
            where TEntity : class;

        //EntityState<TEntity> Update<TEntity>(TEntity entity)
        //    where TEntity : class;

        Task<UpdateEntityResult<TEntity>> PerformUpdateAsync<TEntity>(QueuedUpdateEntityOperation<TEntity> operation)
            where TEntity : class;

        EntityState<TEntity> Delete<TEntity>(TEntity entity)
            where TEntity : class;

        Task<DeleteEntityResult<TEntity>> PerformDeleteAsync<TEntity>(QueuedDeleteEntityOperation<TEntity> operation)
            where TEntity : class;

        Task<GetDataResult<TEntity>> GetAsync<TEntity>(GetDataOperation<TEntity> operation)
            where TEntity : class;

        Task<FlattenedGetDataResult<TEntity>> PerformGetAsync<TEntity>(QueuedGetDataOperation<TEntity> operation)
            where TEntity : class;

        Task<SaveChangesResult> SaveChangesAsync(SaveChangesOperation operation);
    }
}