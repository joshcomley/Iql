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
using Iql.Queryable.Operations;
using Newtonsoft.Json;

namespace Iql.OData.Data
{
    public class ODataDataStore : DataStore
    {
        class ODataGetResult<T>
        {
            public T value { get; set; }
        }
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

        public override async Task<GetDataResult<TEntity>> PerformGet<TEntity>(QueuedGetDataOperation<TEntity> operation)
        {
            var configuration = GetConfiguration();
            var http = configuration.HttpProvider;
            var entitySetUri = ResolveEntitySetUri<TEntity>();

            var oDataQuery = operation.Operation.Queryable.ToQueryWithAdapterBase(QueryableAdapter, DataContext) as IODataQuery;
            var queryString = oDataQuery.ToODataQuery();
            var fullQueryUri = $"{entitySetUri}{queryString}";

            var httpResult = await http.Get(fullQueryUri);
            var singleResult = operation.Operation.Queryable.Operations.Any(o => o is WithKeyOperation);
            operation.Result.Success = httpResult.Success;
            if (singleResult)
            {
                var oDataGetResult =
                    JsonConvert.DeserializeObject<TEntity>(httpResult.ResponseData);
                operation.Result.Data =
                    new List<TEntity>(new []{ oDataGetResult });
            }
            else
            {
                var oDataGetResult = JsonConvert.DeserializeObject<ODataGetResult<List<TEntity>>>(httpResult.ResponseData);
                operation.Result.Data =
                    oDataGetResult.value;
            }
            return operation.Result;
        }

        public override async Task<AddEntityResult<TEntity>> PerformAdd<TEntity>(
                QueuedAddEntityOperation<TEntity> operation)
        {
            var configuration = GetConfiguration();
            var http = configuration.HttpProvider;
            var entitySetUri = ResolveEntitySetUri<TEntity>();
            var httpResult = await http.Post(entitySetUri, new HttpRequest<TEntity>(operation.Operation.Entity));
            operation.Result.RemoteEntity = null;//httpResult.ResponseData;
            operation.Result.Success = httpResult.Success;
            return operation.Result;
        }

        public override Task<UpdateEntityResult<TEntity>> PerformUpdate<TEntity>(
            QueuedUpdateEntityOperation<TEntity> operation)
        {
            throw new NotImplementedException();
        }

        public override Task<DeleteEntityResult<TEntity>> PerformDelete<TEntity>(
            QueuedDeleteEntityOperation<TEntity> operation)
        {
            var configuration = GetConfiguration();
            var http = configuration.HttpProvider;
            var entitySetUri = ResolveEntitySetUri<TEntity>();
            throw new NotImplementedException();
        }

        private string ResolveEntitySetUri<TEntity>()
        {
            var configuration = GetConfiguration();
            var entitySetName = configuration.GetEntitySetName<TEntity>();
            var apiUriBase = configuration.ApiUriBase;
            if (!apiUriBase.EndsWith("/"))
            {
                apiUriBase += "/";
            }
            var entitySetUri = $"{apiUriBase}{entitySetName}";
            return entitySetUri;
        }

        public Task<ODataResult<TResult>> PostOnEntityInstance<TEntity, TResult>(TEntity entity,
            object payload) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public Task<ODataResult<TResult>> GetOnEntityInstance<TEntity, TResult>(TEntity entity,
            object payload) where TEntity : class
        {
            throw new NotImplementedException();
        }
    }
}