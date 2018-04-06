using System.Threading.Tasks;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Crud.Operations.Queued;
using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Data.Tracking;

namespace Iql.Queryable.Data.DataStores.Dynamic
{
    public class DynamicDataStore : DataStore
    {
        protected IDataStore InternalDataStore;

        protected IDataStore GetDataStore()
        {
            InternalDataStore.DataContext = DataContext;
            //this.internalDataStore.tracking2 = this.getTracking();
            //this.internalDataStore.entityType = this.entityType;
            return InternalDataStore;
        }

        public override TrackingSetCollection Tracking => InternalDataStore.Tracking;

        public override async Task<AddEntityResult<TEntity>> PerformAdd<TEntity>(
            QueuedAddEntityOperation<TEntity> operation)
        {
            return await GetDataStore().PerformAdd(operation);
        }

        public override async Task<UpdateEntityResult<TEntity>> PerformUpdate<TEntity>(
            QueuedUpdateEntityOperation<TEntity> operation)
        {
            return await GetDataStore().PerformUpdate(operation);
        }

        public override async Task<DeleteEntityResult<TEntity>> PerformDelete<TEntity>(
            QueuedDeleteEntityOperation<TEntity> operation)
        {
            return await GetDataStore().PerformDelete(operation);
        }

        public override async Task<FlattenedGetDataResult<TEntity>> PerformGet<TEntity>(
            QueuedGetDataOperation<TEntity> operation)
        {
            return await GetDataStore().PerformGet(operation);
        }

        public override async Task<SaveChangesResult> SaveChangesAsync(SaveChangesOperation operation)
        {
            //InternalDataStore.SetQueue(GetQueue());
            //SetQueue(new List<IQueuedOperation>());
            return await GetDataStore().SaveChangesAsync(operation);
        }
    }
}