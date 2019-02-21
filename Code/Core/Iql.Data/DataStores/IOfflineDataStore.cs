using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.Serialization;
using Iql.Data.Tracking;

namespace Iql.Data.DataStores
{
    public interface IOfflineDataStore : IDataStore
    {
        Task ResetAsync();
        Task<bool> RestoreStateAsync(IPersistState persistState);
        Task<bool> ClearStateAsync(IPersistState persistState);
        Task<bool> SaveStateAsync(IPersistState persistState);
        Task<bool> RestoreStateFromJsonAsync(string json);
        Task<bool> RestoreStateFromSetsAsync(DeserializedEntitySets sets);
        IList[] AllDataSets();

        IList<T> DataSet<T>()
            where T : class;
        IList DataSetByType(Type type);
        void Clear();
        void SynchroniseData(Dictionary<Type, IList> data);
        Task ApplyAddAsync<TEntity>(QueuedAddEntityOperation<TEntity> operation)
            where TEntity : class;
        Task ApplyUpdateAsync<TEntity>(QueuedUpdateEntityOperation<TEntity> operation, IPropertyState[] changedProperties)
            where TEntity : class;
        Task ApplyDeleteAsync<TEntity>(QueuedDeleteEntityOperation<TEntity> operation)
            where TEntity : class;
    }
}