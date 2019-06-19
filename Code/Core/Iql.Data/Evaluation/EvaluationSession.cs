using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Data.Context;
#if TypeScript
using Iql.Data.DataStores.InMemory;
#endif
using Iql.Data.IqlToIql;
using Iql.Data.Types;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Entities.Services;
using Iql.Extensions;
using Iql.Parsing.Evaluation;
using Iql.Parsing.Types;

namespace Iql.Data.Evaluation
{
    public class EvaluationSession : IEvaluationSession
    {
        public bool EnforceLatest { get; set; }
        public EvaluationCacheMode CacheMode { get; set; }
        public IExpressionConverter Converter { get; set; }

        public EvaluationSession(
            bool enforceLatest = false,
            EvaluationCacheMode cacheMode = EvaluationCacheMode.All,
            IExpressionConverter converter = null)
        {
            EnforceLatest = enforceLatest;
            CacheMode = cacheMode;
            Converter = converter ?? IqlConverter.Instance;
        }

        public IEvaluationSession Session => this;

        private readonly Dictionary<string, object> _cachedEntities = new Dictionary<string, object>();
        //private readonly Dictionary<string, object> _cachedExcludedEntities = new Dictionary<string, object>();

        //public void ExcludeFromCache(IEntityConfiguration entityConfiguration, CompositeKey compositeKey, object entity)
        //{
        //    AddToDictionary(entityConfiguration, compositeKey, entity, _cachedExcludedEntities);
        //}

        public GetCachedEntityResult GetCachedEntity(IEntityConfiguration entityConfiguration, object compositeKeyOrEntity)
        {
            var compositeKey = CompositeKey.Ensure(compositeKeyOrEntity, entityConfiguration);
            var compositeKeyLookup = $"{entityConfiguration.Name}::{compositeKey.AsLegacyKeyString(true)}";
            if (_cachedEntities.ContainsKey(compositeKeyLookup))
            {
                return new GetCachedEntityResult(true, _cachedEntities[compositeKeyLookup]);
            }
            return new GetCachedEntityResult(false, null);
        }

        public void SetCachedEntity(IEntityConfiguration entityConfiguration, CompositeKey compositeKey, object entity)
        {
            AddToDictionary(entityConfiguration, compositeKey, entity, _cachedEntities);
        }

        private static void AddToDictionary(IEntityConfiguration entityConfiguration, CompositeKey compositeKey, object entity,
            Dictionary<string, object> dictionary)
        {
            var compositeKeyLookup = $"{entityConfiguration.Name}::{compositeKey.AsLegacyKeyString(true)}";
            if (dictionary.ContainsKey(compositeKeyLookup))
            {
                dictionary[compositeKeyLookup] = entity;
            }
            else
            {
                dictionary.Add(compositeKeyLookup, entity);
            }
        }

        public Task<IqlObjectEvaluationResult> EvaluateExpressionAsync<T>(
            Expression<Func<T, object>> expression,
            T entity,
            ITypeResolver typeResolver,
            IServiceProviderProvider serviceProviderProvider)
            where T : class
        {
            return EvaluateLambdaAsync(expression, entity, typeResolver, serviceProviderProvider, typeof(T));
        }

        public async Task<IqlPropertyPathEvaluationResult> EvaluateAsync(
            IqlPropertyPath propertyPath,
            object entity,
            IDataContext dataContext,
            bool populate)
        {
            return await EvaluateCustomAsync(propertyPath, entity, dataContext, populate);
        }

        public async Task<IqlPropertyPathEvaluationResult> EvaluateCustomAsync(
            IqlPropertyPath propertyPath,
            object entity,
            IIqlDataEvaluator dataEvaluator,
            bool populate,
            Dictionary<object, object> replacements = null,
            bool? trackResults = null,
            string rootName = null,
            Func<object, string, Task<object>> propertyValueResolverAsync = null)
        {
            IContextEvaluator contextEvaluator = null;
            if (entity is IContextEvaluator ce)
            {
                contextEvaluator = ce;
            }
            var evaluationResult = new IqlPropertyPathEvaluationResult(
                false,
                entity,
                propertyPath,
                new IqlPropertyPathEvaluated[] { });

            if (entity == null)
            {
                evaluationResult.Success = propertyPath.Parent == null;
                if (evaluationResult.Success)
                {
                    evaluationResult.Results = new IqlPropertyPathEvaluated[]
                    {
                        new IqlPropertyPathEvaluated(
                            evaluationResult,
                            propertyPath,
                            null,
                            null,
                            propertyPath.PropertyPath.Length,
                            0)
                    };
                }
                return evaluationResult;
            }

            var success = true;
            var results = new List<IqlPropertyPathEvaluated>();
            object result = entity;
            if (contextEvaluator != null && rootName != null)
            {
                var contextResult = contextEvaluator.ResolveVariable(rootName);
                if (!contextResult.Success)
                {
                    throw new Exception($@"Unable to find variable ""{rootName}"".");
                }
                result = contextResult.Value;
            }
            for (var i = 0; i < propertyPath.PropertyPath.Length; i++)
            {
                var part = propertyPath.PropertyPath[i];
                if (result != null && replacements != null && replacements.ContainsKey(result))
                {
                    result = replacements[result];
                }
                var parent = result;
                if (result == null)
                {
                    break;
                }

                if (part.PropertyName != null)
                {
                    if (contextEvaluator != null)
                    {
                        var contextResult = contextEvaluator.ResolveProperty(result, part.PropertyName);
                        if (!contextResult.Success)
                        {
                            throw new Exception($@"Unable to find property ""{part.PropertyName}"".");
                        }

                        result = contextResult.Value;
                    }
                    else
                    {
                        if(propertyValueResolverAsync != null)
                        {
                            result = await propertyValueResolverAsync(result, part.PropertyName);
                        }
                        else
                        {
                            result = result.GetPropertyValueByName(part.PropertyName);
                        }
                    }
                }
                if (part.Property != null && part.Property.Kind.HasFlag(PropertyKind.Relationship))
                {
                    var key = part.Property.EntityProperty().Relationship.ThisEnd.GetCompositeKey(parent, true);
                    result = await dataEvaluator.GetEntityByKeyAsync(
                        part.Property.EntityProperty().Relationship.OtherEnd.EntityConfiguration,
                        key,
                        new string[] { },
                        trackResults == true);
                    if (populate && part.Property.GetValue(parent) != result)
                    {
                        part.Property.SetValue(parent, result);
                    }
                    results.Add(new IqlPropertyPathEvaluated(
                        evaluationResult,
                        part,
                        parent,
                        result,
                        propertyPath.PropertyPath.Length,
                        i));

                    if (result == null)
                    {
                        success = i == propertyPath.PropertyPath.Length - 1;
                        break;
                    }
                }
                else
                {
                    results.Add(new IqlPropertyPathEvaluated(
                        evaluationResult,
                        part,
                        parent,
                        result,
                        propertyPath.PropertyPath.Length,
                        i));
                }
            }

            evaluationResult.Success = success;
            evaluationResult.Results = results.ToArray();
            return evaluationResult;
        }

        public async Task<T> EvaluateAsAsync<T>(IqlPropertyPath propertyPath, object entity, IDataContext dataContext, bool populate)
        {
            var result = await EvaluateAsync(propertyPath, entity, dataContext, populate);
            return (T)result.Value;
        }

        public async Task<IqlObjectEvaluationResult> EvaluateLambdaAsync(
            LambdaExpression expression,
            object entity,
            ITypeResolver typeResolver,
            IServiceProviderProvider serviceProviderProvider,
            Type entityType = null,
            IContextEvaluator contextEvaluator = null
            )
        {
            var iql = Converter.ConvertLambdaExpressionToIqlByType(expression, typeResolver, entityType).Expression;
            var processResult = await ProcessIqlExpressionAsync(iql, entityType, typeResolver, serviceProviderProvider, contextEvaluator);
            foreach (var item in processResult.lookup.Keys.ToArray())
            {
                processResult.lookup[item] = item.Evaluate(entity) ?? ResolveNull(item, processResult.propertyExpressions.First(_ => _.Expression == item.Expression));
            }
            var value = await FinaliseAsync(
                entity,
                entityType,
                serviceProviderProvider,
                typeResolver,
                processResult.lookup,
                iql,
                processResult.propertyExpressions);
            return new IqlObjectEvaluationResult(processResult.Success, value);
        }

        public Task<IqlObjectEvaluationResult> EvaluateExpressionWithDbAsync<T>(
            Expression<Func<T, object>> expression,
            T entity,
            IDataContext dataContext,
            IContextEvaluator contextEvaluator = null)
            where T : class
        {
            return EvaluateLambdaWithDbAsync(expression, entity, dataContext, typeof(T), contextEvaluator);
        }

        public async Task<IqlObjectEvaluationResult> EvaluateLambdaWithDbAsync(
            LambdaExpression expression,
            object entity,
            IDataContext dataContext,
            Type entityType = null,
            IContextEvaluator contextEvaluator = null
            )
        {
            entityType = entityType ?? entity.GetType();
            var iql = Converter.ConvertLambdaExpressionToIqlByType(expression, dataContext.EntityConfigurationContext, entityType).Expression;
            return await EvaluateIqlAsync(
                iql,
                entity,
                dataContext,
                contextEvaluator,
                entityType);
        }

        public Task<IqlExpressonEvaluationResult> EvaluateIqlAsync(
            IqlExpression expression,
            object entity,
            IDataContext dataContext,
            IContextEvaluator contextEvaluator = null,
            Type contextType = null,
            ITypeResolver typeResolver = null,
            bool? trackResults = null)
        {
            return EvaluateIqlCustomAsync(
                expression,
                entity,
                contextEvaluator,
                (IServiceProviderProvider)dataContext ?? DataContext.FindBuilderForEntityType(contextType),
                dataContext,
                typeResolver ?? dataContext.EntityConfigurationContext, contextType,
                false,
                trackResults);
        }

        public async Task<IqlExpressonEvaluationResult> EvaluateIqlPathAsync(
            IqlExpression expression,
            object context,
            IDataContext dataContext,
            Type contextType,
            IContextEvaluator contextEvaluator = null,
            ITypeResolver typeResolver = null,
            bool populatePath = false,
            bool? trackResults = null
            )
        {
            var value = await EvaluateIqlCustomAsync(
                expression,
                context,
                contextEvaluator,
                dataContext?.EntityConfigurationContext ?? DataContext.FindBuilderForEntityType(contextType),
                dataContext,
                typeResolver ?? dataContext.EntityConfigurationContext,
                contextType, 
                populatePath,
                trackResults);
            value.Result = value.Result is IqlPropertyPathEvaluationResult
                ? (value.Result as IqlPropertyPathEvaluationResult).Value
                : value.Result;
            return value;
        }

        private class ExpandGroupDefinition
        {
            public IEntityConfiguration EntityConfiguration { get; }
            public object Entity { get; }
            public List<string> ExpandPaths { get; } = new List<string>();

            public ExpandGroupDefinition(IEntityConfiguration entityConfiguration, object entity)
            {
                EntityConfiguration = entityConfiguration;
                Entity = entity;
            }
        }

        public static object ResolveNull(IqlPropertyPath propertyPath, IqlFlattenedExpression flattenedExpression)
        {
            if (IsStringConcatenation(flattenedExpression))
            {
                return "";
            }

            return null;
        }

        private static bool IsStringConcatenation(IqlFlattenedExpression flattenedExpression)
        {
            if (flattenedExpression.Expression.Kind == IqlExpressionKind.Condition)
            {
                return false;
            }

            if (flattenedExpression.Expression.Kind == IqlExpressionKind.Add)
            {
                var add = flattenedExpression.Expression as IqlAddExpression;
                if (add.Left.ReturnType == IqlType.String || add.Right.ReturnType == IqlType.String)
                {
                    return true;
                }

                var ancestors = flattenedExpression.Ancestors.ToList();
                ancestors.Add(flattenedExpression);
                return IsStringConcatenation(
                           new IqlFlattenedExpression(add.Left, ancestors.ToArray())) ||
                       IsStringConcatenation(
                           new IqlFlattenedExpression(add.Right, ancestors.ToArray()));
            }

            if (flattenedExpression.Ancestors.Any())
            {
                return IsStringConcatenation(flattenedExpression.Ancestors[flattenedExpression.Ancestors.Length - 1]);
            }
            return false;
        }

        //private static IqlExpression ProcessLambdaExpression(LambdaExpression expression, object entity, Type entityType,
        //    IDataContext dataContext, out IqlPropertyExpression[] propertyExpressions, out Dictionary<IqlPropertyPath, object> lookup)
        //{
        //    entityType = entityType ?? entity.GetType();
        //    var iql = Converter.ConvertLambdaExpressionToIqlByType(expression, entityType).Expression;
        //    return ProcessIqlExpression(iql, entityType, dataContext, out propertyExpressions, out lookup);
        //}

        private async Task<object> FinaliseAsync(object entity,
            Type contextType,
            IServiceProviderProvider serviceProviderProvider,
            ITypeResolver typeResolver,
            Dictionary<IqlPropertyPath, object> lookup,
            IqlExpression iql,
            IqlFlattenedExpression[] propertyExpressions)
        {
            typeResolver = typeResolver ?? new TypeResolver();
            var expressionResultLookup = new Dictionary<IqlExpression, object>();
            foreach (var item in lookup)
            {
                expressionResultLookup.Add(item.Key.Expression, item.Value);
            }

            var processedIql = iql.ReplaceWith((context, iqlExpression) =>
            {
                if (propertyExpressions.Any(_ => _.Expression == iqlExpression))
                {
                    var value = expressionResultLookup[iqlExpression];
                    if (value is IqlPropertyPathEvaluationResult)
                    {
                        value = (value as IqlPropertyPathEvaluationResult).Value;
                    }
                    return new IqlLiteralExpression(value);
                }

                return iqlExpression;
            });
            //var resolvedType = typeResolver.FindTypeByType(contextType);
            //processedIql = (await processedIql.ProcessAsync(
            //    resolvedType,
            //    typeResolver,
            //    serviceProviderProvider,
            //    true)).Result;
            var processedLambda = Converter.ConvertIqlToLambdaExpression(processedIql, typeResolver);
            var compiledLambda = processedLambda.Compile();
            object result = null;
            if (iql.Kind == IqlExpressionKind.Lambda && (iql as IqlLambdaExpression)?.Parameters?.Any() == true)
            {
                result = compiledLambda.DynamicInvoke(entity
#if TypeScript
                , new InMemoryContext<object>(null)
#endif
                );
            }
            else
            {
                result = compiledLambda.DynamicInvoke(
#if TypeScript
                new InMemoryContext<object>(null)
#endif
                );
            }
            if (result is IqlLiteralExpression)
            {
                result = (result as IqlLiteralExpression).Value;
            }
            return result;
        }

        public async Task<IqlExpressonEvaluationResult> EvaluateIqlCustomAsync(
            IqlExpression expression,
            object context,
            IContextEvaluator contextEvaluator = null,
            IServiceProviderProvider serviceProviderProvider = null,
            IIqlDataEvaluator dataEvaluator = null,
            ITypeResolver typeResolver = null,
            Type contextType = null,
            bool populatePath = false,
            bool? trackResults = null,
            Func<object, string, Task<object>> propertyValueResolverAsync = null)
        {
            expression = expression.Clone();
            if (typeResolver == null)
            {
                typeResolver = new TypeResolver();
            }
            var success = true;
            var paths = new List<IqlPropertyPathEvaluationResult>();
            contextType = contextType ?? context.GetType();
            if (contextEvaluator != null)
            {
                var flattened = expression.Flatten();
                foreach (var item in flattened)
                {
                    if (item.Expression.Kind == IqlExpressionKind.Variable)
                    {
                        var result = contextEvaluator.ResolveVariable(((IqlVariableExpression)item.Expression)
                            .VariableName);
                        if (result.Success && result.CanBeLiteral)
                        {
                            expression = expression.ReplaceExpression(item.Expression,
                                new IqlLiteralExpression(result.Value,
                                    result.Value == null ? IqlType.Unknown : result.Value.GetType().ToIqlType()));
                        }
                    }
                }
            }
            var processResult = await ProcessIqlExpressionAsync(
                expression.Clone(),
                contextType,
                typeResolver,
                serviceProviderProvider,
                contextEvaluator);
            success = processResult.Success;
            var flattenedExpressions = processResult.propertyExpressions.ToArray();

            var keys = processResult.lookup.Keys.ToArray();
            var expands = new Dictionary<object, ExpandGroupDefinition>();
            for (var i = 0; i < flattenedExpressions.Length && i < keys.Length; i++)
            {
                var propertyPath = keys[i];
                if (propertyPath.HasRelationshipPathToHere)
                {
                    var path = propertyPath.RelationshipPathToHere;
                    var root = propertyPath.RelationshipPathToHereRoot;
                    object rootEntity = null;
                    if (root != null)
                    {
                        rootEntity = (await EvaluateCustomAsync(
                            root,
                            context,
                            dataEvaluator,
                            populatePath,
                            null,
                            null,
                            null,
                            propertyValueResolverAsync)).Value;
                    }
                    else
                    {
                        rootEntity = context;
                    }

                    if (rootEntity != null)
                    {
                        if (!expands.ContainsKey(rootEntity))
                        {
                            var entityConfigurationTypeProvider =
                                (root?.Child == null ? (object)typeResolver : (object)root.Child.EntityConfiguration)
                                as EntityConfigurationTypeProvider;
                            if (entityConfigurationTypeProvider == null)
                            {
                                continue;
                            }

                            expands.Add(rootEntity,
                                new ExpandGroupDefinition(entityConfigurationTypeProvider.EntityConfiguration,
                                    rootEntity));
                        }

                        expands[rootEntity].ExpandPaths.Add(path);
                    }
                }
                else if (propertyPath.HasRootEntity && EnforceLatest)
                {
                    var root = propertyPath.RootEntity;
                    object rootEntity = (await EvaluateCustomAsync(
                        root,
                        context,
                        dataEvaluator,
                        populatePath,
                        null,
                        null,
                        null,
                        propertyValueResolverAsync)).Value;
                    if (rootEntity != null && !expands.ContainsKey(rootEntity))
                    {
                        var entityConfigurationTypeProvider =
                            (root?.Child == null ? (object)typeResolver : (object)root.Child.EntityConfiguration) as
                            EntityConfigurationTypeProvider;
                        if (entityConfigurationTypeProvider == null)
                        {
                            continue;
                        }

                        expands.Add(rootEntity,
                            new ExpandGroupDefinition(entityConfigurationTypeProvider.EntityConfiguration, rootEntity));
                    }
                }
            }

            Dictionary<object, object> replacements = null;
            foreach (var expandGroup in expands)
            {
                var entity = expandGroup.Key;
                if (dataEvaluator.EntityStatus(entity, expandGroup.Value.EntityConfiguration) ==
                    IqlEntityStatus.Existing)
                {
                    var expandPaths = expandGroup.Value.ExpandPaths.Distinct().ToArray();
                    // Ensure the relationships are populated
                    object newEntity = null;
                    var cachedEntityResult = CacheMode == EvaluationCacheMode.None ? null : GetCachedEntity(expandGroup.Value.EntityConfiguration, entity);
                    if (cachedEntityResult?.Exists == true)
                    {
                        newEntity = cachedEntityResult.Entity;
                    }
                    else
                    {
                        var compositeKey = expandGroup.Value.EntityConfiguration.GetCompositeKey(entity);
                        trackResults = trackResults == null ? dataEvaluator.IsTracked(entity) : trackResults.Value;
                        newEntity = await dataEvaluator.GetEntityByKeyAsync(
                            expandGroup.Value.EntityConfiguration,
                            compositeKey,
                            expandPaths,
                            trackResults.Value);
                        switch (CacheMode)
                        {
                            case EvaluationCacheMode.All:
                                SetCachedEntity(expandGroup.Value.EntityConfiguration, compositeKey, newEntity);
                                break;
                            case EvaluationCacheMode.FetchesOnly:
                                var all = expandGroup.Value.EntityConfiguration.Builder.FlattenObjectGraph(newEntity, expandGroup.Value.EntityConfiguration.Type);
                                foreach (var itemGroup in all)
                                {
                                    for (var i = 0; i < itemGroup.Value.Count; i++)
                                    {
                                        var item = itemGroup.Value[i];
                                        var itemConfiguration = expandGroup.Value.EntityConfiguration.Builder.GetEntityByType(
                                            itemGroup.Key);
                                        if (dataEvaluator.EntityStatus(item, itemConfiguration) == IqlEntityStatus.NotTracked)
                                        {
                                            var itemKey = itemConfiguration.GetCompositeKey(item);
                                            SetCachedEntity(itemConfiguration, itemKey, item);
                                        }
                                    }
                                }
                                break;
                        }
                    }
                    if (newEntity != entity && newEntity != null &&
                        (EnforceLatest || (!EnforceLatest && entity == null && newEntity != null)))
                    {
                        replacements = replacements ?? new Dictionary<object, object>();
                        replacements.Add(entity, newEntity);
                    }
                }
            }

            for (var i = 0; i < flattenedExpressions.Length; i++)
            {
                var propertyPath = keys[i];
                var root = flattenedExpressions[i].Expression.RootExpression();
                var thisContext = context;
                string rootName = null;
                if (root != null)
                {
                    if (root.Kind == IqlExpressionKind.Literal)
                    {
                        thisContext = ((IqlLiteralExpression)root).Value;
                    }
                    else if (root.Kind == IqlExpressionKind.Variable)
                    {
                        var variableExpression = (IqlVariableExpression)root;
                        rootName = variableExpression.VariableName;
                        if(variableExpression.Value != null)
                        {
                            thisContext = variableExpression.Value;
                        }
                    }
                }
                var evaluationResult = await EvaluateCustomAsync(
                    propertyPath,
                    thisContext,
                    dataEvaluator,
                    populatePath,
                    replacements,
                    trackResults,
                    rootName,
                    propertyValueResolverAsync);
                paths.Add(evaluationResult);
                var flattenedExpression =
                    processResult.propertyExpressions.First(_ => _.Expression == flattenedExpressions[i].Expression);
                var value = evaluationResult.Value ?? ResolveNull(propertyPath, flattenedExpression);
                var iqlEvaluationResult = new IqlObjectEvaluationResult(evaluationResult.Success, value);
                processResult.lookup[keys[i]] = iqlEvaluationResult.Result;
                if (!iqlEvaluationResult.Success)
                {
                    success = false;
                }
            }

            var finalResult = await FinaliseAsync(
                context,
                contextType,
                serviceProviderProvider,
                typeResolver,
                processResult.lookup,
                processResult.expression,
                processResult.propertyExpressions);
            return new IqlExpressonEvaluationResult(success, finalResult, paths);
        }

        private static async Task<ProcessExpressionResult> ProcessIqlExpressionAsync(
            IqlExpression iql,
            Type entityType,
            ITypeResolver typeResolver,
            IServiceProviderProvider serviceProviderProvider,
            IContextEvaluator contextEvaluator)
        {
            serviceProviderProvider = serviceProviderProvider ?? DataContext.FindBuilderForEntityType(entityType);

            var propertyExpressions = iql.TopLevelPropertyExpressions();
            var finalPropertyPaths = new List<IqlFlattenedExpression>();
            var lookup = new Dictionary<IqlPropertyPath, object>();
            var success = true;
            var resolvedType = typeResolver?.FindTypeByType(entityType);
            if (resolvedType != null)
            {
                var processResult = await iql.ProcessAsync(resolvedType, typeResolver, serviceProviderProvider, contextEvaluator, true);
                iql = processResult.Result;
                success = processResult.Success;
            }
            for (var i = 0; i < propertyExpressions.Length; i++)
            {
                var propertyExpression = propertyExpressions[i];
                var path = IqlPropertyPath.FromPropertyExpression(
                    typeResolver,
                    resolvedType,
                    propertyExpression.Expression as IqlPropertyExpression,
                    true,
                    true);
                if (path != null)
                {
                    lookup.Add(path, null);
                    finalPropertyPaths.Add(propertyExpression);
                }
            }
            return new ProcessExpressionResult(
                success,
                finalPropertyPaths.ToArray(),
                lookup,
                iql);
        }
    }
}