using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Crud;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.Crud.Operations.Results;
using Iql.Data.DataStores;
using Iql.Data.Extensions;
using Iql.Data.Http;
using Iql.Data.Methods;
using Iql.Entities;
using Iql.Entities.Relationships;
using Iql.Entities.Validation.Validation;
using Iql.Extensions;
using Iql.OData.Extensions;
using Iql.OData.IqlToODataExpression.Parsers;
using Iql.OData.Json;
using Iql.OData.Methods;
using Iql.Queryable.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = Iql.OData.Json.JsonSerializer;

namespace Iql.OData
{
    public class ODataDataStore : DataStore
    {
        private ODataConfiguration _configuration;

        public ODataConfiguration Configuration
        {
            get => _configuration ?? DataContext?.GetConfiguration<ODataConfiguration>();
            set => _configuration = value;
        }

        public IHttpProvider GetHttp()
        {
            return Configuration.HttpProvider;
        }

        public virtual ODataDataMethodRequest<TResult> MethodWithResponse<TResult>(
            IEnumerable<ODataParameter> parameters,
            ODataMethodType methodType,
            ODataMethodScope methodScope,
            string nameSpace,
            string name,
            Type entityType,
            Type responseElementType
        )
        {
            var uri = GetMethodUri(parameters, methodScope, methodType, nameSpace, name, entityType);
            var request = new ODataDataMethodRequest<TResult>(
                this,
                uri,
                async () =>
                {
                    var httpResult = await GetMethodHttpResult(methodType, uri, parameters);
                    var dataMethodResult = new DataMethodResult<TResult>(httpResult.Success);
                    var isCollectionResult = typeof(TResult).IsEnumerableType();
                    if (DataContext.EntityConfigurationContext.IsEntityType(responseElementType))
                    {
                        var flattenedResponse = await ParseODataEntityResponseByTypeAsync(responseElementType, httpResult, isCollectionResult);
                        var dbList = TrackGetDataResultByType(responseElementType, flattenedResponse);
                        var list = dbList.ToList(responseElementType);
                        dataMethodResult.Data = (TResult)(isCollectionResult
                            ? list
                            : (list.Count == 0 ? null : list[0]));
                    }
                    else
                    {
                        if (isCollectionResult)
                        {
                            var collectionResult = await GetODataCollectionResponseByTypeAsync(responseElementType, httpResult);
                            dataMethodResult.Data = (TResult)collectionResult.Items;
                        }
                        else
                        {
                            var oDataGetResult = await GetODataSingleResultAsync<TResult>(httpResult);
                            dataMethodResult.Data = oDataGetResult;
                        }

                    }
                    return dataMethodResult;
                });
            return request;
        }

        public virtual ODataMethodRequest Method(
            IEnumerable<ODataParameter> parameters,
            ODataMethodType methodType,
            ODataMethodScope methodScope,
            string nameSpace,
            string name,
            Type entityType
        )
        {
            var uri = GetMethodUri(parameters, methodScope, methodType, nameSpace, name, entityType);
            var request = new ODataMethodRequest(
                this,
                uri,
                async () =>
                {
                    var httpResult = await GetMethodHttpResult(methodType, uri, parameters);
                    //var flattenedResponse = ParseODataResponseByType(responseType, httpResult, false);
                    //var dbList = TrackGetDataResultByType(responseType, flattenedResponse);
                    return new MethodResult(httpResult.Success);
                });
            return request;
        }

        private async Task<IHttpResult> GetMethodHttpResult(ODataMethodType methodType, string uri, IEnumerable<ODataParameter> parameters)
        {
            var http = GetHttp();
            IHttpResult httpResult = null;
            switch (methodType)
            {
                case ODataMethodType.Action:
                    HttpRequest httpRequest = null;
                    if (parameters != null && parameters.Any())
                    {
                        var jobject = new JObject();
                        foreach (var parameter in parameters)
                        {
                            if (parameter.Value != null)
                            {
                                jobject[parameter.Name] =
                                    DataContext.EntityConfigurationContext.GetEntityByType(parameter.ValueType) == null
                                        ? JToken.FromObject(parameter.Value)
                                        : JToken.Parse(JsonSerializer.Serialize(parameter.Value, DataContext, DataContext.EntityNonNullProperties(parameter.Value).ToArray()));
                            }
                            else
                            {
                                jobject[parameter.Name] = null;
                            }
                        }
                        var body = JsonConvert.SerializeObject(jobject);
                        httpRequest = new HttpRequest(body);
                    }
                    httpResult = await http.Post(uri, httpRequest);
                    break;
                case ODataMethodType.Function:
                    httpResult = await http.Get(uri);
                    break;
            }

            return httpResult;
        }

        public string GetMethodUri(
            IEnumerable<ODataParameter> parameters,
            ODataMethodScope methodScope,
            ODataMethodType methodType,
            string nameSpace,
            string name,
            Type entityType)
        {
            var baseUri = Configuration.ApiUriBase;
            var bindingParameterName = "bindingParameter";
            switch (methodScope)
            {
                case ODataMethodScope.Collection:
                    baseUri = Configuration.ResolveEntitySetUriByType(entityType);
                    break;
                case ODataMethodScope.Entity:
                    var bindingParameter = parameters.Single(p => p.Name == bindingParameterName);
                    var entityState = DataContext.GetEntityState(bindingParameter.Value);
                    var compositeKey = entityState == null
                        ? DataContext.EntityConfigurationContext.GetEntityByType(entityType).GetCompositeKey(bindingParameter.Value)
                        : entityState.CurrentKey;
                    baseUri = ResolveEntityUriByType(compositeKey, bindingParameter.ValueType);
                    break;
                case ODataMethodScope.Global:
                    nameSpace = "";
                    break;
            }

            if (baseUri.EndsWith("/"))
            {
                baseUri = baseUri.Substring(0, baseUri.Length - 1);
            }

            baseUri = baseUri + "/";
            if (!string.IsNullOrWhiteSpace(nameSpace))
            {
                baseUri += nameSpace + ".";
            }

            baseUri += name;
            if (methodType == ODataMethodType.Function)
            {
                var otherParameters = parameters.Where(p => p.Name != bindingParameterName).ToArray();
                if (otherParameters.Any())
                {
                    baseUri += "(";
                    baseUri += string.Join(",", otherParameters.Select(p => $"{p.Name}={ODataLiteralParser.ODataEncode(p.Value)}"));
                    baseUri += ")";
                }
            }

            return baseUri;
        }

        public override async Task<FlattenedGetDataResult<TEntity>> PerformGetAsync<TEntity>(
            QueuedGetDataOperation<TEntity> operation)
        {
            var fullQueryUri = await operation.Operation.Queryable.ResolveODataUriFromQueryAsync(Configuration);
            return await PerformGetInternalAsync(operation, fullQueryUri);
        }

        private async Task<FlattenedGetDataResult<TEntity>> PerformGetInternalAsync<TEntity>(
            QueuedGetDataOperation<TEntity> operation,
            string fullQueryUri) where TEntity : class
        {
            var configuration = Configuration;
            var http = configuration.HttpProvider;
            var httpResult = await http.Get(fullQueryUri);
            var result = operation.Result;
            var isSingleResult = operation.Operation.IsSingleResult;
            await ParseODataEntityResponseAsync(httpResult, !isSingleResult, result);
            return result;
        }

        protected async Task<IFlattenedGetDataResult> ParseODataEntityResponseByTypeAsync(
            Type entityType,
            IHttpResult httpResult,
            bool isCollection,
            IFlattenedGetDataResult result = null)
        {
            return await (Task<IFlattenedGetDataResult>)GetType().GetMethod(nameof(ParseODataEntityResponseAsync))
                .InvokeGeneric(this, new object[] { httpResult, isCollection, result }, entityType);
        }

        protected async Task<FlattenedGetDataResult<TEntity>> ParseODataEntityResponseAsync<TEntity>(
            IHttpResult httpResult,
            bool isCollection,
            FlattenedGetDataResult<TEntity> result = null)
            where TEntity : class
        {
            result = result ?? new FlattenedGetDataResult<TEntity>(
                         new Dictionary<Type, IList>(),
                         new GetDataOperation<TEntity>(null, DataContext),
                         false);
            result.Success = httpResult.Success;
            if (!result.IsSuccessful())
            {
                return result;
            }

            if (isCollection)
            {
                var collectionResult = await GetODataCollectionResponseAsync<TEntity>(httpResult);
                var flattened = DataContext.EntityConfigurationContext.FlattenObjectGraphs(
                    typeof(TEntity),
                    collectionResult.Items);
                result.Data = flattened;
                result.Root = collectionResult.Items.ToList();
                result.TotalCount = collectionResult.TotalCount;
            }
            else
            {
                var oDataGetResult = await GetODataSingleResultAsync<TEntity>(httpResult);
                //JsonConvert.DeserializeObject<TEntity>(httpResult.ResponseData);
                var flattened = DataContext.EntityConfigurationContext.FlattenObjectGraph(
                    oDataGetResult,
                    typeof(TEntity));
                result.Data = flattened;
                result.Root = new[] { oDataGetResult }.ToList();
            }

            return result;
        }

        private static async Task<TEntity> GetODataSingleResultAsync<TEntity>(IHttpResult httpResult)
        {
            var json = await httpResult.GetResponseTextAsync();
            var odataResultRoot = JObject.Parse(json);
            ParseObj(odataResultRoot);
            var oDataGetResult =
                odataResultRoot.ToObject<TEntity>();
            return oDataGetResult;
        }

        private static async Task<IODataCollectionResult> GetODataCollectionResponseByTypeAsync(Type entityType, IHttpResult httpResult)
        {
            return await (Task<IODataCollectionResult>)typeof(ODataDataStore).GetMethod(nameof(GetODataCollectionResponseAsync))
                .InvokeGeneric(
                    null,
                    new object[] { httpResult },
                    entityType
                );
        }

        private static async Task<ODataCollectionResult<TEntity>> GetODataCollectionResponseAsync<TEntity>(IHttpResult httpResult)
        {
            var odataResultRoot = JObject.Parse(await httpResult.GetResponseTextAsync());
            ParseObj(odataResultRoot);
            var countToken = odataResultRoot["Count"];
            var count = countToken?.ToObject<int?>();
            var values = odataResultRoot["value"].ToObject<TEntity[]>();
            return new ODataCollectionResult<TEntity>(values, count);
        }

        private static void ParseObj(object jvalue)
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

        public override async Task<AddEntityResult<TEntity>> PerformAddAsync<TEntity>(
            QueuedAddEntityOperation<TEntity> operation)
        {
            var configuration = Configuration;
            var http = configuration.HttpProvider;
            var entitySetUri = Configuration.ResolveEntitySetUri<TEntity>();
            var json = JsonSerializer.Serialize(operation.Operation.Entity, operation.Operation.DataContext);
            var httpResult = await http.Post(entitySetUri, new HttpRequest(json));
            var responseData = await httpResult.GetResponseTextAsync();
            if (httpResult.Success)
            {
                operation.Result.RemoteEntity = JsonConvert.DeserializeObject<TEntity>(responseData);
            }
            operation.Result.Success = httpResult.Success;
            ParseValidation(operation.Result, operation.Operation.Entity, responseData);
            return operation.Result;
        }

        public override async Task<UpdateEntityResult<TEntity>> PerformUpdateAsync<TEntity>(
            QueuedUpdateEntityOperation<TEntity> operation)
        {
            var configuration = Configuration;
            var http = configuration.HttpProvider;
            var entityUri = ResolveEntityUri(operation.Operation.Entity);
            var properties = new List<string>();
            foreach (var property in operation.Operation.EntityState.GetChangedProperties())
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
                operation.Operation.EntityState.GetChangedProperties().ToArray());
            var httpResult = await http.Put(entityUri, new HttpRequest(json));
            //var remoteEntity = JsonConvert.DeserializeObject<TEntity>(result.ResponseData);
            operation.Result.Success = httpResult.Success;
            ParseValidation(operation.Result, operation.Operation.Entity, await httpResult.GetResponseTextAsync());
            return operation.Result;
        }

        public override async Task<DeleteEntityResult<TEntity>> PerformDeleteAsync<TEntity>(
            QueuedDeleteEntityOperation<TEntity> operation)
        {
            var configuration = Configuration;
            var http = configuration.HttpProvider;
            var entityUri = ResolveEntityUriByType(operation.Operation.Key, typeof(TEntity));
            var httpResult = await http.Delete(entityUri);
            operation.Result.Success = httpResult.Success;
            ParseValidation(operation.Result, operation.Operation.Entity, await httpResult.GetResponseTextAsync());
            return operation.Result;
        }

        public string ResolveEntityUri<TEntity>(TEntity entity) where TEntity : class
        {
            var compositeKey = DataContext.GetEntityState(entity).KeyBeforeChanges();
            return ResolveEntityUriByType(compositeKey, typeof(TEntity));
        }

        public string ResolveEntityUriByType(CompositeKey compositeKey, Type entityType)
        {
            var configuration = Configuration;
            var entitySetName = configuration.GetEntitySetNameByType(entityType);
            var apiUriBase = configuration.ApiUriBase;
            if (!apiUriBase.EndsWith("/"))
            {
                apiUriBase += "/";
            }
            //var compositeKey = new CompositeKey(key.Properties.Count);
            //for (var i = 0; i < key.Properties.Count; i++)
            //{
            //    var p = key.Properties[i];
            //    compositeKey.Keys[i] = new KeyValue(p.Name, entity.GetPropertyValueByName(p.Name), null);
            //}

            var entityUri = $"{apiUriBase}{entitySetName}({FormatKey(compositeKey)})";
            return entityUri;
        }
        public static string FormatKey(CompositeKey key)
        {
            string keyString;
            if (key.Keys.Length == 1)
            {
                keyString = GetKeyValue(key.Keys.Single());
            }
            else
            {
                var keys = key.Keys.Select(k => k.Name + "=" + GetKeyValue(k));
                keyString = string.Join(",", keys);
            }
            return keyString;
        }

        private static string GetKeyValue(KeyValue key)
        {
            if (key.Value is string || key.Value is Guid || key.Value is Guid? ||
                (key.ValueType != null &&
                 (key.ValueType.Type == typeof(string) || key.ValueType.Type == typeof(Guid))))
            {
                return $"\'{key.Value}\'";
            }

            return key.Value.ToString();
        }

        #region Validation
        private void ParseValidation<TEntity>(IEntityCrudResult result, TEntity entity, string responseData) where TEntity : class
        {
            if (!result.Success)
            {
                ODataErrorResult errorResult = null;
                try
                {
                    errorResult = JsonConvert.DeserializeObject<ODataErrorResult>(responseData);
                }
                catch (Exception e)
                {
                    var entityValidationResult = new EntityValidationResult<TEntity>(entity);
                    entityValidationResult.ValidationFailures.Add(new ValidationError("", responseData));
                    result.EntityValidationResults.Add("", entityValidationResult);
                }
                var error = errorResult?.error;
                if (error != null)
                {
                    var entityType = entity.GetType();
                    var entityValidationResult = new EntityValidationResult<TEntity>(entity);
                    foreach (var detail in error.details)
                    {
                        if (string.IsNullOrWhiteSpace(detail.target))
                        {
                            entityValidationResult.AddFailure(detail.code, detail.message);
                        }
                        else
                        {
                            var path = detail.target.Split('.');
                            var currentEntityType = entityType;
                            EntityValidationResult<TEntity> currentError = entityValidationResult;
                            for (var i = 0; i < path.Length; i++)
                            {
                                var pathPart = path[i];
                                if (pathPart.EndsWith("]"))
                                {
                                    int index;
                                    var openBracketIndex = pathPart.IndexOf("[");
                                    pathPart = pathPart.Substring(0, pathPart.Length - 1);
                                    var propertyName = pathPart.Substring(0, openBracketIndex);
                                    var property = DataContext.EntityConfigurationContext.EntityType<TEntity>()
                                        .FindProperty(propertyName);
                                    index = Convert.ToInt32(pathPart.Substring(openBracketIndex + 1));
                                    var relationship = FindRelationship(currentEntityType, property.Name);
                                    if (relationship != null)
                                    {
                                        var relationshipEntityType = relationship.Type;

                                        RelationshipCollectionValidationResult<TEntity> relationshipCollectionValidationResult = null;
                                        foreach (var existingCollectionResult in currentError
                                            .RelationshipCollectionValidationResults)
                                        {
                                            if (existingCollectionResult.Property == property)
                                            {
                                                relationshipCollectionValidationResult = existingCollectionResult;
                                                break;
                                            }
                                        }
                                        if (relationshipCollectionValidationResult == null)
                                        {
                                            relationshipCollectionValidationResult = new RelationshipCollectionValidationResult<TEntity>(
                                                relationshipEntityType, entity, property);
                                            currentError.RelationshipCollectionValidationResults.Add(
                                                relationshipCollectionValidationResult);
                                        }
                                        var relationshipCollectionEntityValidationResult =
                                            new EntityValidationResult<TEntity>(entity);
                                        var match = relationshipCollectionValidationResult.RelationshipValidationResults
                                            .FirstOrDefault(r => r.Index == index);
                                        if (match == null)
                                        {
                                            var relationshipValidationResult = new RelationshipValidationResult<TEntity>(
                                                relationshipEntityType, entity,
                                                relationshipCollectionEntityValidationResult, property);
                                            relationshipCollectionValidationResult.RelationshipValidationResults
                                            .Add(new RelationshipCollectionValidationResultItem<TEntity>(relationshipValidationResult, index));
                                            currentError = relationshipValidationResult.EntityValidationResult;
                                        }
                                        else
                                        {
                                            currentError = match.ValidationResult.EntityValidationResult;
                                        }
                                        currentEntityType = relationshipEntityType;
                                    }
                                    AddPropertyError(currentError, property, entity, detail);
                                }
                                else
                                {
                                    var propertyName = pathPart;
                                    var property = DataContext.EntityConfigurationContext.EntityType<TEntity>()
                                        .FindProperty(propertyName);
                                    var relationship = FindRelationship(currentEntityType, property.Name);
                                    if (relationship != null)
                                    {
                                        var relationshipEntityType = relationship.Type;
                                        var relationshipValidationResult = new RelationshipValidationResult<TEntity>(
                                            relationshipEntityType, entity, currentError, property);
                                        currentError.RelationshipValidationResults.Add(relationshipValidationResult);
                                        relationshipValidationResult.EntityValidationResult =
                                            new EntityValidationResult<TEntity>(entity);
                                        AddPropertyError(currentError, property, entity, detail);
                                        currentError = relationshipValidationResult.EntityValidationResult;
                                        currentEntityType = relationshipEntityType;
                                    }
                                    else
                                    {
                                        AddPropertyError(currentError, property, entity, detail);
                                    }
                                }
                            }
                        }
                    }

                    result.EntityValidationResults =
                        result.EntityValidationResults ?? new Dictionary<object, IEntityValidationResult>();
                    if (!result.EntityValidationResults.ContainsKey(entity))
                    {
                        result.EntityValidationResults.Add(entity, entityValidationResult);
                    }
                    result.RootEntityValidationResult = entityValidationResult;
                }
            }
        }

        private void AddPropertyError<TEntity>(EntityValidationResult<TEntity> currentError, IProperty property,
            TEntity currentEntity, ODataError detail)
        {
            var propertyError = currentError.PropertyValidationResults.FirstOrDefault(p => p.Property == property);
            if (propertyError == null)
            {
                propertyError = currentError.RelationshipValidationResults.FirstOrDefault(p => p.Property == property);
                if (propertyError == null)
                {
                    propertyError = new PropertyValidationResult<TEntity>(currentEntity, property);
                    currentError.AddPropertyValidationResult(propertyError);
                }
            }
            propertyError.AddFailure(detail.code, detail.message);
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