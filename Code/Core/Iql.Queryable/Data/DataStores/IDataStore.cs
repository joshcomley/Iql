using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Crud.Operations.Queued;
using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Data.Crud.State;
using Iql.Queryable.Data.Tracking;
using Iql.Queryable.Native;

namespace Iql.Queryable.Data.DataStores
{
    public interface IDataStore
    {
        IEnumerable<IQueuedOperation> GetQueue();
        IDataContext DataContext { get; set; }
        IRelationshipObserver RelationshipObserver { get; }
        TrackingSetCollection GetTracking();

        EntityState<TEntity> Add<TEntity>(TEntity entity)
            where TEntity : class;

#if !TypeScript
        IEntityStateBase Add(object entity);
        IEntityStateBase Delete(object entity);
#endif

        Task<AddEntityResult<TEntity>> PerformAdd<TEntity>(QueuedAddEntityOperation<TEntity> operation)
            where TEntity : class;

        //EntityState<TEntity> Update<TEntity>(TEntity entity)
        //    where TEntity : class;

        Task<UpdateEntityResult<TEntity>> PerformUpdate<TEntity>(QueuedUpdateEntityOperation<TEntity> operation)
            where TEntity : class;

        EntityState<TEntity> Delete<TEntity>(TEntity entity)
            where TEntity : class;

        Task<DeleteEntityResult<TEntity>> PerformDelete<TEntity>(QueuedDeleteEntityOperation<TEntity> operation)
            where TEntity : class;

        Task<GetDataResult<TEntity>> Get<TEntity>(GetDataOperation<TEntity> operation)
            where TEntity : class;

        Task<FlattenedGetDataResult<TEntity>> PerformGet<TEntity>(QueuedGetDataOperation<TEntity> operation)
            where TEntity : class;

        Task<SaveChangesResult> SaveChanges(SaveChangesOperation operation);

        IEnumerable<IQueuedOperation> GetChanges();
    }
}