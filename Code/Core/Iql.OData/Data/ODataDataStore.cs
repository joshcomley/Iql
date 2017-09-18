using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Iql.OData.Queryable;
using Iql.Queryable;
using Iql.Queryable.Data.Crud.Operations.Queued;
using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.Http;

namespace Iql.OData.Data
{
    public class ODataDataStore : DataStore
    {
        public ODataDataStore(IQueryableAdapterBase queryableAdapter = null)
        {
            QueryableAdapter = queryableAdapter ?? new ODataQueryableAdapter();
        }

        public IQueryableAdapterBase QueryableAdapter { get; }

        public ODataConfiguration GetConfiguration()
        {
            return DataContext.GetConfiguration<ODataConfiguration>();
        }

        public IHttpProvider GetHttp()
        {
            return GetConfiguration().HttpProvider;
        }

        public override Task<AddEntityResult<TEntity>> PerformAdd<TEntity>(
                QueuedAddEntityOperation<TEntity> operation)
        {
            throw new NotImplementedException();
            return null;
        }

        public override Task<UpdateEntityResult<TEntity>> PerformUpdate<TEntity>(
            QueuedUpdateEntityOperation<TEntity> operation)
        {
            throw new NotImplementedException();
            return null;
        }

        public override Task<DeleteEntityResult<TEntity>> PerformDelete<TEntity>(
            QueuedDeleteEntityOperation<TEntity> operation)
        {
            throw new NotImplementedException();
            return null;
        }

        public override async Task<GetDataResult<TEntity>> PerformGet<TEntity>(QueuedGetDataOperation<TEntity> operation)
        {
            var configuration = GetConfiguration();
            var oDataQuery = operation.Operation.Queryable.ToQueryWithAdapterBase(QueryableAdapter, DataContext) as IODataQuery;
            var http = configuration.HttpProvider;
            var apiUriBase = configuration.ApiUriBase;
            if (!apiUriBase.EndsWith("/"))
            {
                apiUriBase += "/";
            }
            var entitySetName = configuration.GetEntitySetName<TEntity>();
            var queryString = oDataQuery.ToODataQuery();
            var uri = apiUriBase + entitySetName + "/?" + queryString;
            var httpResult = await http.Get<IEnumerable<TEntity>>(uri);
            operation.Result.Data = httpResult.ResponseData.ToList();
            operation.Result.Success = true;
            return operation.Result;
        }

        public Task<ODataResult<TResult>> PostOnEntityInstance<TEntity, TResult>(TEntity entity,
            object payload) where TEntity : class
        {
            throw new NotImplementedException();
            return null;
        }

        public Task<ODataResult<TResult>> GetOnEntityInstance<TEntity, TResult>(TEntity entity,
            object payload) where TEntity : class
        {
            throw new NotImplementedException();
            return null;
        }
    }
}