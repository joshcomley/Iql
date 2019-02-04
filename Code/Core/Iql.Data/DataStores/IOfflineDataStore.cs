using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.Crud.Operations.Results;

namespace Iql.Data.DataStores
{
    public interface IOfflineDataStore : IDataStore
    {
        void SynchroniseData(Dictionary<Type, IList> data);
        Task<AddEntityResult<TEntity>> ScheduleAddAsync<TEntity>(QueuedAddEntityOperation<TEntity> operation)
            where TEntity : class;
        Task<UpdateEntityResult<TEntity>> ScheduleUpdateAsync<TEntity>(QueuedUpdateEntityOperation<TEntity> operation)
            where TEntity : class;
        Task<DeleteEntityResult<TEntity>> ScheduleDeleteAsync<TEntity>(QueuedDeleteEntityOperation<TEntity> operation)
            where TEntity : class;
    }
}