using System.Threading.Tasks;
using Iql.Data.Crud;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.Crud.Operations.Results;
using Iql.Data.DataStores;
using Iql.Data.DataStores.InMemory;
using Iql.Entities;

namespace Iql.Tests.Context
{
    public class OfflinableInMemoryDataStore : InMemoryDataStore
    {
        public OfflinableInMemoryDataStore(EntityConfigurationBuilder entityConfigurationBuilder, IOfflineDataStore offlineDataStore = null) : base(entityConfigurationBuilder, offlineDataStore)
        {

        }

        public bool IsOffline { get; set; }

        public override async Task<FlattenedGetDataResult<TEntity>> PerformGetAsync<TEntity>(QueuedGetDataOperation<TEntity> operation)
        {
            if (IsOffline)
            {
                return Offline(operation.Result);
            }
            return await base.PerformGetAsync(operation);
        }

        public override async Task<UpdateEntityResult<TEntity>> PerformUpdateAsync<TEntity>(QueuedUpdateEntityOperation<TEntity> operation)
        {
            if (IsOffline)
            {
                return Offline(operation.Result);
            }
            return await base.PerformUpdateAsync(operation);
        }

        public override async Task<DeleteEntityResult<TEntity>> PerformDeleteAsync<TEntity>(QueuedDeleteEntityOperation<TEntity> operation)
        {
            if (IsOffline)
            {
                return Offline(operation.Result);
            }
            return await base.PerformDeleteAsync(operation);
        }

        private T Offline<T>(T result)
            where T : ICrudResult
        {
            result.RequestStatus = RequestStatus.Offline;
            result.Success = false;
            return result;
        }
    }
}