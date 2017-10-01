using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Iql.OData.Queryable;
using Iql.Queryable;
using Iql.Queryable.Data;
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
        // ReSharper disable once ClassNeverInstantiated.Local
        class ODataGetResult<T>
        {
            // ReSharper disable once InconsistentNaming
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
            operation.Result.RemoteEntity = JsonConvert.DeserializeObject<TEntity>(httpResult.ResponseData);
            operation.Result.Success = httpResult.Success;
            return operation.Result;
        }

        public override async Task<UpdateEntityResult<TEntity>> PerformUpdate<TEntity>(
            QueuedUpdateEntityOperation<TEntity> operation)
        {
            var configuration = GetConfiguration();
            var http = configuration.HttpProvider;
            var entityUri = ResolveEntityUri(operation.Operation.Entity);
            var result = await http.Put(entityUri, new HttpRequest<TEntity>(operation.Operation.Entity));
            //var remoteEntity = JsonConvert.DeserializeObject<TEntity>(result.ResponseData);
            operation.Result.Success = result.Success;
            return operation.Result;
        }

        public override async Task<DeleteEntityResult<TEntity>> PerformDelete<TEntity>(
            QueuedDeleteEntityOperation<TEntity> operation)
        {
            var configuration = GetConfiguration();
            var http = configuration.HttpProvider;
            var entityUri = ResolveEntityUri(operation.Operation.Entity);
            var result = await http.Delete(entityUri);
            operation.Result.Success = result.Success;
            return operation.Result;
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

        private string ResolveEntityUri<TEntity>(TEntity entity) where TEntity : class
        {
            var configuration = GetConfiguration();
            var entitySetName = configuration.GetEntitySetName<TEntity>();
            var apiUriBase = configuration.ApiUriBase;
            if (!apiUriBase.EndsWith("/"))
            {
                apiUriBase += "/";
            }
            var key = this.DataContext.EntityConfigurationContext.GetEntity<TEntity>().Key;
            var keyValue = entity.GetPropertyValue(key.Properties.First().PropertyName);
            var entityUri = $"{apiUriBase}{entitySetName}({keyValue})";
            return entityUri;
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