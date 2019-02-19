using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.Crud.Operations.Results;

namespace Iql.Data.DataStores
{
    public interface IOfflineDataStore : IDataStore
    {
        Task ClearStateAsync();
        Task SaveStateAsync();
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