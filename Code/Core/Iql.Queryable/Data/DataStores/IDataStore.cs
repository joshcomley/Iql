using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Crud.Operations.Queued;
using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Data.Tracking;

namespace Iql.Queryable.Data.DataStores
{
    public interface IDataStore
    {
        List<IQueuedOperation> Queue { get; set; }
        IDataContext DataContext { get; set; }
        TrackingSetCollection GetTracking();

        AddEntityResult<TEntity> Add<TEntity>(AddEntityOperation<TEntity> operation)
            where TEntity : class, IEntity;

        Task<AddEntityResult<TEntity>> PerformAdd<TEntity>(QueuedAddEntityOperation<TEntity> operation)
            where TEntity : class, IEntity;

        UpdateEntityResult<TEntity> Update<TEntity>(UpdateEntityOperation<TEntity> operation)
            where TEntity : class, IEntity;

        Task<UpdateEntityResult<TEntity>> PerformUpdate<TEntity>(QueuedUpdateEntityOperation<TEntity> operation)
            where TEntity : class, IEntity;

        DeleteEntityResult<TEntity> Delete<TEntity>(DeleteEntityOperation<TEntity> operation)
            where TEntity : class, IEntity;

        Task<DeleteEntityResult<TEntity>> PerformDelete<TEntity>(QueuedDeleteEntityOperation<TEntity> operation)
            where TEntity : class, IEntity;

        Task<GetDataResult<TEntity>> Get<TEntity>(GetDataOperation<TEntity> operation)
            where TEntity : class, IEntity;

        Task<GetDataResult<TEntity>> PerformGet<TEntity>(QueuedGetDataOperation<TEntity> operation)
            where TEntity : class, IEntity;

        Task<SaveChangesResult> SaveChanges(SaveChangesOperation operation);

        IEnumerable<IQueuedOperation> GetChanges(bool reset = false);
    }
}