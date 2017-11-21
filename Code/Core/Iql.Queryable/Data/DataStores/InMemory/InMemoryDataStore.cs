using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Crud.Operations.Queued;
using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Data.Tracking;

namespace Iql.Queryable.Data.DataStores.InMemory
{
    public class InMemoryDataStore : DataStore
    {
        public InMemoryDataStore(IQueryableAdapterBase queryableAdapter)
        {
            QueryableAdapter = queryableAdapter;
        }

        public IQueryableAdapterBase QueryableAdapter { get; }

        public override Task<AddEntityResult<TEntity>> PerformAdd<TEntity>(
            QueuedAddEntityOperation<TEntity> operation)
        {
            var data = operation.Operation.DataContext.GetConfiguration<InMemoryDataStoreConfiguration>()
                .GetSource<TEntity>();
            data.Add(operation.Operation.Entity.Clone());
            operation.Result.Success = true;
            return Task.FromResult(operation.Result);
        }

        private static IList<TEntity> DataSet<TEntity>(ICrudOperation operation)
        {
            return operation.DataContext.GetConfiguration<InMemoryDataStoreConfiguration>().GetSource<TEntity>();
        }

        private int FindEntityIndexFromOperation<TEntity>(EntityCrudOperation<TEntity> operation) where TEntity : class
        {
            return FindEntityIndex(
                operation.EntityType,
                operation.Entity,
                DataSet<TEntity>(operation)
            );
        }

        public override Task<UpdateEntityResult<TEntity>> PerformUpdate<TEntity>(
            QueuedUpdateEntityOperation<TEntity> operation)
        {
            var index = FindEntityIndexFromOperation(operation.Operation);
            if (index != -1)
            {
                DataSet<TEntity>(operation.Operation)[index] = operation.Operation.Entity;
            }
            return Task.FromResult(operation.Result);
        }

        public override Task<DeleteEntityResult<TEntity>> PerformDelete<TEntity>(
            QueuedDeleteEntityOperation<TEntity> operation)
        {
            var index = FindEntityIndexFromOperation(operation.Operation);
            if (index != -1)
            {
                DataSet<TEntity>(operation.Operation).RemoveAt(0);
            }
            return Task.FromResult(operation.Result);
        }

        public override Task<GetDataResult<TEntity>> PerformGet<TEntity>(QueuedGetDataOperation<TEntity> operation)
        {
            //var data = operation.Operation.DataContext.GetConfiguration<InMemoryDataStoreConfiguration>()
            //    .GetSource<TEntity>();
            var q = operation.Operation.Queryable.ToQueryWithAdapterBase(QueryableAdapter, DataContext);
            //var q = operation.Operation.Queryable.ToQueryWithAdapter(new JavaScriptQueryableAdapter());
            operation.Result.Data = (DbList<TEntity>) q.ToList();
            //var localQuery = new JavaScriptQuery<TEntity>(
            //    operation.Operation.Queryable,
            //    operation.Operation.DataContext);
            //operation.Result.Data = localQuery.ToList();
            return Task.FromResult(operation.Result);
        }
    }
}