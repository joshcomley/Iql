using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Iql.OData.Queryable;
using Iql.OData.Queryable.Applicators;
using Iql.Queryable;
using Iql.Queryable.Data;
using Iql.Queryable.Data.Crud;
using Iql.Queryable.Data.Crud.Operations.Queued;
using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Data.Http;
using Iql.Queryable.Data.Validation;
using Iql.Queryable.Operations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Iql.OData.Data
{
#pragma warning disable IDE1006 // Naming Styles
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class ODataErrorResult
    {
        public ODataError error { get; set; }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class ODataError
    {
        public string code { get; set; }
        public string message { get; set; }
        public string target { get; set; }
        public ODataError[] details { get; set; }
    } // ReSharper disable once ClassNeverInstantiated.Local
#pragma warning restore IDE1006 // Naming Styles

    //internal class ODataGetResult<T>
    //{
    //    public int? Count { get; set; }

    //    // ReSharper disable once InconsistentNaming
    //    public T Value { get; set; }
    //}

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

        public override async Task<FlattenedGetDataResult<TEntity>> PerformGet<TEntity>(
            QueuedGetDataOperation<TEntity> operation)
        {
            var configuration = GetConfiguration();
            var http = configuration.HttpProvider;
            var entitySetUri = ResolveEntitySetUri<TEntity>();

            var oDataQuery =
                operation.Operation.Queryable.ToQueryWithAdapterBase(
                    QueryableAdapter,
                    DataContext,
                    null,
                    null) as IODataQuery;
            var queryString = oDataQuery.ToODataQuery();
            var fullQueryUri = $"{entitySetUri}{queryString}";

            var httpResult = await http.Get(fullQueryUri);
            operation.Result.Success = httpResult.Success;
            if (operation.Operation.IsSingleResult)
            {
                var odataResultRoot = JObject.Parse(httpResult.ResponseData);
                ParseObj(odataResultRoot);
                var oDataGetResult =
                    odataResultRoot.ToObject<TEntity>();
                //JsonConvert.DeserializeObject<TEntity>(httpResult.ResponseData);
                var flattened = DataContext.EntityConfigurationContext.FlattenObjectGraph(
                  oDataGetResult,
                  typeof(TEntity));
                operation.Result.Data = flattened;
                operation.Result.Root = new[] { oDataGetResult }.ToList();
            }
            else
            {
                var odataResultRoot = JObject.Parse(httpResult.ResponseData);
                ParseObj(odataResultRoot);
                var countToken = odataResultRoot["Count"];
                var count = countToken?.ToObject<int?>();
                var values = odataResultRoot["value"].ToObject<TEntity[]>();
                var flattened = DataContext.EntityConfigurationContext.FlattenObjectGraphs(
                    typeof(TEntity),
                    values);
                operation.Result.Data = flattened;
                operation.Result.Root = values.ToList();
                //operation.Result.Data =
                //    oDataGetResult.Value;
                operation.Result.TotalCount = count;
            }
            return operation.Result;
        }

        private void ParseObj(object jvalue)
        {
            if (jvalue is JArray)
            {
                foreach (var child in (JArray)jvalue)
                {
                    ParseObj(child);
                }
            }
            else if (jvalue is JObject)
            {
                var jobj = (JObject)jvalue;
                foreach (var prop in jobj.Properties().ToArray())
                {
                    var value = jobj[prop.Name];
                    const string odataKey = "@odata.";
                    if (prop.Name.Contains(odataKey))
                    {
                        var index = prop.Name.IndexOf(odataKey);
                        var odataName = prop.Name.Substring(index + odataKey.Length);
                        var before = prop.Name.Substring(0, index);
                        switch (odataName)
                        {
                            case "count":
                                odataName = "Count";
                                break;
                        }
                        jobj[before + odataName] = value;
                        jobj.Remove(prop.Name);
                    }
                    ParseObj(value);
                }
            }
        }

        public override async Task<AddEntityResult<TEntity>> PerformAdd<TEntity>(
            QueuedAddEntityOperation<TEntity> operation)
        {
            var configuration = GetConfiguration();
            var http = configuration.HttpProvider;
            var entitySetUri = ResolveEntitySetUri<TEntity>();
            var json = JsonSerializer.Serialize(operation.Operation.Entity, operation.Operation.DataContext);
            var httpResult = await http.Post(entitySetUri, new HttpRequest(json));
            operation.Result.RemoteEntity = JsonConvert.DeserializeObject<TEntity>(httpResult.ResponseData);
            operation.Result.Success = httpResult.Success;
            ParseValidation(operation.Result, operation.Operation.Entity, httpResult.ResponseData);
            return operation.Result;
        }

        public override async Task<UpdateEntityResult<TEntity>> PerformUpdate<TEntity>(
            QueuedUpdateEntityOperation<TEntity> operation)
        {
            var configuration = GetConfiguration();
            var http = configuration.HttpProvider;
            var entityUri = ResolveEntityUri(operation.Operation.Entity);
            var properties = new List<string>();
            foreach (var property in operation.Operation.EntityState.ChangedProperties)
            {
                properties.Add(property.Property.Name);
            }
            foreach (var key in DataContext.EntityConfigurationContext.GetEntity<TEntity>().Key.Properties)
            {
                properties.Add(key.Name);
            }
            var json = JsonSerializer.Serialize(
                operation.Operation.Entity,
                DataContext,
                operation.Operation.EntityState.ChangedProperties.ToArray());
            var httpResult = await http.Put(entityUri, new HttpRequest(json));
            //var remoteEntity = JsonConvert.DeserializeObject<TEntity>(result.ResponseData);
            operation.Result.Success = httpResult.Success;
            ParseValidation(operation.Result, operation.Operation.Entity, httpResult.ResponseData);
            return operation.Result;
        }

        public override async Task<DeleteEntityResult<TEntity>> PerformDelete<TEntity>(
            QueuedDeleteEntityOperation<TEntity> operation)
        {
            var configuration = GetConfiguration();
            var http = configuration.HttpProvider;
            var entityUri = ResolveEntityUri(operation.Operation.Entity);
            var httpResult = await http.Delete(entityUri);
            operation.Result.Success = httpResult.Success;
            ParseValidation(operation.Result, operation.Operation.Entity, httpResult.ResponseData);
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
            var compositeKey = new CompositeKey(key.Properties.Count);
            for (var i = 0; i < key.Properties.Count; i++)
            {
                var p = key.Properties[i];
                compositeKey.Keys[i] = new KeyValue(p.Name, entity.GetPropertyValueByName(p.Name), null);
            }
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

        #region Validation
        private void ParseValidation(IEntityCrudResult result, object entity, string responseData)
        {
            if (!result.Success)
            {
                var errorResult = JsonConvert.DeserializeObject<ODataErrorResult>(responseData);
                var error = errorResult?.error;
                if (error != null)
                {
                    var entityType = entity.GetType();
                    var entityValidationResult = new EntityValidationResult(entityType);
                    foreach (var detail in error.details)
                    {
                        if (string.IsNullOrWhiteSpace(detail.target))
                        {
                            entityValidationResult.AddFailure(detail.message);
                        }
                        else
                        {
                            var path = detail.target.Split('.');
                            var currentEntityType = entityType;
                            EntityValidationResult currentError = entityValidationResult;
                            for (var i = 0; i < path.Length; i++)
                            {
                                var pathPart = path[i];
                                if (pathPart.EndsWith("]"))
                                {
                                    int index;
                                    var openBracketIndex = pathPart.IndexOf("[");
                                    pathPart = pathPart.Substring(0, pathPart.Length - 1);
                                    var property = pathPart.Substring(0, openBracketIndex);
                                    index = Convert.ToInt32(pathPart.Substring(openBracketIndex + 1));
                                    var relationship = FindRelationship(currentEntityType, property);
                                    if (relationship != null)
                                    {
                                        var relationshipEntityType = relationship.Type;

                                        RelationshipCollectionValidationResult relationshipCollectionValidationResult = null;
                                        foreach (var existingCollectionResult in currentError
                                            .RelationshipCollectionValidationResults)
                                        {
                                            if (existingCollectionResult.PropertyName == property)
                                            {
                                                relationshipCollectionValidationResult = existingCollectionResult;
                                                break;
                                            }
                                        }
                                        if (relationshipCollectionValidationResult == null)
                                        {
                                            relationshipCollectionValidationResult = new RelationshipCollectionValidationResult(
                                                relationshipEntityType, currentEntityType, property);
                                            currentError.RelationshipCollectionValidationResults.Add(
                                                relationshipCollectionValidationResult);
                                        }
                                        var relationshipCollectionEntityValidationResult =
                                            new EntityValidationResult(relationshipEntityType);
                                        if (!relationshipCollectionValidationResult.RelationshipValidationResults
                                            .ContainsKey(index))
                                        {
                                            var relationshipValidationResult = new RelationshipValidationResult(
                                                relationshipEntityType, currentEntityType,
                                                relationshipCollectionEntityValidationResult, property);
                                            relationshipCollectionValidationResult.RelationshipValidationResults.Add(index,
                                                relationshipValidationResult);
                                            currentError = relationshipValidationResult.EntityValidationResult;
                                        }
                                        else
                                        {
                                            currentError = relationshipCollectionValidationResult
                                                .RelationshipValidationResults[index].EntityValidationResult;
                                        }
                                        currentEntityType = relationshipEntityType;
                                    }
                                    else
                                    {
                                        AddPropertyError(currentError, property, currentEntityType, detail);
                                    }
                                }
                                else
                                {
                                    var property = pathPart;
                                    var relationship = FindRelationship(currentEntityType, property);
                                    if (relationship != null)
                                    {
                                        var relationshipEntityType = relationship.Type;
                                        var relationshipValidationResult = new RelationshipValidationResult(
                                            relationshipEntityType, currentEntityType, currentError, property);
                                        currentError.RelationshipValidationResults.Add(relationshipValidationResult);
                                        relationshipValidationResult.EntityValidationResult =
                                            new EntityValidationResult(relationshipEntityType);
                                        currentError = relationshipValidationResult.EntityValidationResult;
                                        currentEntityType = relationshipEntityType;
                                    }
                                    else
                                    {
                                        AddPropertyError(currentError, property, currentEntityType, detail);
                                    }
                                }
                            }
                        }
                    }
                    result.RootEntityValidationResult = entityValidationResult;
                }
            }
        }

        private static void AddPropertyError(EntityValidationResult currentError, string property,
            Type currentEntityType, ODataError detail)
        {
            var propertyError = currentError.PropertyValidationResults.FirstOrDefault(p => p.PropertyName == property);
            if (propertyError == null)
            {
                propertyError = new PropertyValidationResult(currentEntityType, property);
                currentError.AddPropertyValidationResult(propertyError);
            }
            propertyError.AddFailure(detail.message);
        }

        private IRelationshipDetail FindRelationship(Type entityType, string property)
        {
            var entityConfiguration = DataContext.EntityConfigurationContext.GetEntityByType(entityType);
            foreach (var relationship in entityConfiguration.Relationships)
            {
                var end = relationship.Source.Configuration == entityConfiguration
                    ? relationship.Source
                    : relationship.Target;
                if (end.Property.Name == property)
                {
                    return end;
                }
            }
            return null;
        }
        #endregion Validation
    }
}