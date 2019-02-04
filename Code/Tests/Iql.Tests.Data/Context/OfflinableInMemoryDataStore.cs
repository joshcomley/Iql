using System.Threading.Tasks;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.Crud.Operations.Results;
using Iql.Data.DataStores;
using Iql.Data.DataStores.InMemory;

namespace Iql.Tests.Context
{
    public class OfflinableInMemoryDataStore : InMemoryDataStore
    {
        public OfflinableInMemoryDataStore(IDataStore offlineDataStore = null) : base(offlineDataStore)
        {

        }

        public bool IsOffline { get; set; }

        public override async Task<FlattenedGetDataResult<TEntity>> PerformGetAsync<TEntity>(QueuedGetDataOperation<TEntity> operation)
        {
            if (IsOffline)
            {
                return new FlattenedGetDataResult<TEntity>(null, operation.Operation, false, RequestStatus.Offline);
            }
            return await base.PerformGetAsync(operation);
        }
    }
}