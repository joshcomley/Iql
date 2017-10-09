using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Iql.OData.Queryable;
using Iql.OData.Queryable.Applicators;
using Iql.Queryable;
using Iql.Queryable.Data;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Crud.Operations.Queued;
using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.Http;
using Iql.Queryable.Operations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Iql.OData.Data
{
    // ReSharper disable once ClassNeverInstantiated.Local
    class ODataGetResult<T>
    {
        // ReSharper disable once InconsistentNaming
        public T value { get; set; }
    }

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
            var entityConfiguration =
                operation.Operation.DataContext.EntityConfigurationContext.GetEntityByType(operation.Operation
                    .EntityType);
            var properties = new List<PropertyChange>();
            foreach (var property in entityConfiguration.Properties)
            {
                var propertyValue = operation.Operation.Entity.GetPropertyValue(property.Name);
                if (propertyValue != null)
                {
                    properties.Add(new PropertyChange(property));
                }
            }
            var json = JsonSerializer.Serialize(operation.Operation.Entity, operation.Operation.DataContext, properties.ToArray());
            var httpResult = await http.Post(entitySetUri, new HttpRequest(json));
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
            var properties = new List<string>();
            foreach (var property in operation.Operation.ChangedProperties)
            {
                properties.Add(property.Property.Name);
            }
            foreach (var key in DataContext.EntityConfigurationContext.GetEntity<TEntity>().Key.Properties)
            {
                properties.Add(key.PropertyName);
            }
            var json = JsonSerializer.Serialize(
                operation.Operation.Entity, 
                DataContext,
                operation.Operation.ChangedProperties.ToArray());
            var result = await http.Put(entityUri, new HttpRequest(json));
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
            var key = DataContext.EntityConfigurationContext.GetEntity<TEntity>().Key;
            var compositeKeyProperties =
                key.Properties.Select(p => new KeyValue(p.PropertyName, entity.GetPropertyValue(p.PropertyName)));
            var compositeKey = new CompositeKey();
            compositeKey.Keys.AddRange(compositeKeyProperties);
            var entityUri = $"{apiUriBase}{entitySetName}({WithKeyOperationApplicatorOData.FormatKey(compositeKey)})";
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