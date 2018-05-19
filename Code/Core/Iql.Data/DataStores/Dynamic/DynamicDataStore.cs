using System.Threading.Tasks;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.Crud.Operations.Results;
using Iql.Data.Tracking;

namespace Iql.Data.DataStores.Dynamic
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

        public override async Task<AddEntityResult<TEntity>> PerformAddAsync<TEntity>(
            QueuedAddEntityOperation<TEntity> operation)
        {
            return await GetDataStore().PerformAddAsync(operation);
        }

        public override async Task<UpdateEntityResult<TEntity>> PerformUpdateAsync<TEntity>(
            QueuedUpdateEntityOperation<TEntity> operation)
        {
            return await GetDataStore().PerformUpdateAsync(operation);
        }

        public override async Task<DeleteEntityResult<TEntity>> PerformDeleteAsync<TEntity>(
            QueuedDeleteEntityOperation<TEntity> operation)
        {
            return await GetDataStore().PerformDeleteAsync(operation);
        }

        public override async Task<FlattenedGetDataResult<TEntity>> PerformGetAsync<TEntity>(
            QueuedGetDataOperation<TEntity> operation)
        {
            return await GetDataStore().PerformGetAsync(operation);
        }

        public override async Task<SaveChangesResult> SaveChangesAsync(SaveChangesOperation operation)
        {
            //InternalDataStore.SetQueue(GetQueue());
            //SetQueue(new List<IQueuedOperation>());
            return await GetDataStore().SaveChangesAsync(operation);
        }
    }
}