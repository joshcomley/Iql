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
        public IEvaluationSession Session => this;

        private readonly Dictionary<string, object> _resolvedEntities = new Dictionary<string, object>();
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
            Dictionary<object, object> replacements = null)
        {
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
            var result = entity;
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
                result = part.PropertyName == null ? result : result.GetPropertyValueByName(part.PropertyName);
                if (part.Property != null && part.Property.Kind.HasFlag(PropertyKind.Relationship))
                {
                    var key = part.Property.EntityProperty().Relationship.ThisEnd.GetCompositeKey(parent, true);
                    result = await dataEvaluator.GetEntityByKeyAsync(
                        part.Property.EntityProperty().Relationship.OtherEnd.EntityConfiguration,
                        key,
                        new string[] { });
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
            Type entityType = null
            )
        {
            var iql = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(expression, typeResolver, entityType).Expression;
            var processResult = await ProcessIqlExpressionAsync(iql, entity, entityType, typeResolver, serviceProviderProvider);
            foreach (var item in processResult.lookup.Keys.ToArray())
            {
                processResult.lookup[item] = item.Evaluate(entity) ?? ResolveNull(item, processResult.propertyExpressions.First(_ => _.Expression == item.Expression));
            }
            var value = Finalise(entity, typeResolver, processResult.lookup, iql, processResult.propertyExpressions);
            return new IqlObjectEvaluationResult(processResult.Success, value);
        }

        public Task<IqlObjectEvaluationResult> EvaluateExpressionWithDbAsync<T>(
            Expression<Func<T, object>> expression,
            T entity,
            IDataContext dataContext)
            where T : class
        {
            return EvaluateLambdaWithDbAsync(expression, entity, dataContext);
        }

        public async Task<IqlObjectEvaluationResult> EvaluateLambdaWithDbAsync(
            LambdaExpression expression,
            object entity,
            IDataContext dataContext,
            Type entityType = null)
        {
            entityType = entityType ?? entity.GetType();
            var iql = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(expression, dataContext.EntityConfigurationContext, entityType).Expression;
            return await EvaluateIqlAsync(
                iql,
                entity,
                dataContext,
                entityType);
        }

        public Task<IqlExpressonEvaluationResult> EvaluateIqlAsync(
            IqlExpression expression,
            object entity,
            IDataContext dataContext,
            Type contextType = null,
            ITypeResolver typeResolver = null)
        {
            return EvaluateIqlCustomAsync(
                expression,
                (IServiceProviderProvider)dataContext ?? DataContext.FindBuilderForEntityType(contextType),
                entity,
                dataContext,
                typeResolver ?? dataContext.EntityConfigurationContext,
                contextType);
        }

        public async Task<IqlExpressonEvaluationResult> EvaluateIqlPathAsync(
            IqlExpression expression,
            object context,
            IDataContext dataContext,
            Type contextType,
            ITypeResolver typeResolver = null,
            bool populatePath = false
            )
        {
            var value = await EvaluateIqlCustomAsync(
                expression,
                dataContext?.EntityConfigurationContext ?? DataContext.FindBuilderForEntityType(contextType),
                context,
                dataContext,
                typeResolver ?? dataContext.EntityConfigurationContext,
                contextType,
                populatePath);
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
        //    var iql = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(expression, entityType).Expression;
        //    return ProcessIqlExpression(iql, entityType, dataContext, out propertyExpressions, out lookup);
        //}

        private static object Finalise(object entity,
            ITypeResolver typeResolver,
            Dictionary<IqlPropertyPath, object> lookup,
            IqlExpression iql,
            IqlFlattenedExpression[] propertyExpressions)
        {
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
            var processedLambda = IqlConverter.Instance.ConvertIqlToLambdaExpression(processedIql, typeResolver);
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
            IServiceProviderProvider serviceProviderProvider,
            object context,
            IIqlDataEvaluator dataEvaluator,
            ITypeResolver typeResolver,
            Type contextType = null,
            bool populatePath = false,
            bool enforceLatest = false
        )
        {
            var success = true;
            var paths = new List<IqlPropertyPathEvaluationResult>();
            contextType = contextType ?? context.GetType();
            var clone = expression.Clone();
            var processResult = await ProcessIqlExpressionAsync(
                clone,
                context,
                contextType,
                typeResolver,
                serviceProviderProvider);
            success = processResult.Success;
            var flattenedExpressions = processResult.propertyExpressions.ToArray();

            var keys = processResult.lookup.Keys.ToArray();
            var expands = new Dictionary<object, ExpandGroupDefinition>();
            for (var i = 0; i < flattenedExpressions.Length; i++)
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
                            populatePath)).Value;
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
                else if (propertyPath.HasRootEntity && enforceLatest)
                {
                    var root = propertyPath.RootEntity;
                    object rootEntity = (await EvaluateCustomAsync(
                        root,
                        context,
                        dataEvaluator,
                        populatePath)).Value;
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
                    var compositeKey = expandGroup.Value.EntityConfiguration.GetCompositeKey(entity);
                    var compositeKeyLookup = $"{expandGroup.Value.EntityConfiguration.Name}::{compositeKey.AsKeyString(true)}";
                    object newEntity = null;
                    if (_resolvedEntities.ContainsKey(compositeKeyLookup))
                    {
                        newEntity = _resolvedEntities[compositeKeyLookup];
                    }
                    else
                    {
                        newEntity = await dataEvaluator.GetEntityByKeyAsync(
                            expandGroup.Value.EntityConfiguration,
                            compositeKey,
                            expandPaths);
                    }
                    if (newEntity != entity && newEntity != null)
                    {
                        replacements = replacements ?? new Dictionary<object, object>();
                        replacements.Add(entity, newEntity);
                    }
                }
            }

            for (var i = 0; i < flattenedExpressions.Length; i++)
            {
                var propertyPath = keys[i];
                var evaluationResult = await EvaluateCustomAsync(
                    propertyPath,
                    context,
                    dataEvaluator,
                    populatePath,
                    replacements);
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

            var finalResult = Finalise(context, typeResolver, processResult.lookup, processResult.expression,
                processResult.propertyExpressions);
            return new IqlExpressonEvaluationResult(success, finalResult, paths);
        }

        private static async Task<ProcessExpressionResult> ProcessIqlExpressionAsync(
            IqlExpression iql,
            object parameter,
            Type entityType,
            ITypeResolver typeResolver,
            IServiceProviderProvider serviceProviderProvider)
        {
            serviceProviderProvider = serviceProviderProvider ?? DataContext.FindBuilderForEntityType(entityType);

            var propertyExpressions = iql.TopLevelPropertyExpressions();
            var finalPropertyPaths = new List<IqlFlattenedExpression>();
            var lookup = new Dictionary<IqlPropertyPath, object>();
            var success = true;
            var resolvedType = typeResolver.FindTypeByType(entityType);
            if (resolvedType != null)
            {
                var processResult = await iql.ProcessAsync(resolvedType, typeResolver, serviceProviderProvider, true);
                iql = processResult.Result;
                for (var i = 0; i < propertyExpressions.Length; i++)
                {
                    var propertyExpression = propertyExpressions[i];
                    var path = IqlPropertyPath.FromPropertyExpression(
                        typeResolver,
                        resolvedType,
                        propertyExpression.Expression as IqlPropertyExpression);
                    if (path != null)
                    {
                        lookup.Add(path, null);
                        finalPropertyPaths.Add(propertyExpression);
                    }
                }

                success = processResult.Success;
            }
            else
            {
                finalPropertyPaths = propertyExpressions.ToList();
            }
            return new ProcessExpressionResult(
                success,
                finalPropertyPaths.ToArray(),
                lookup,
                iql);
        }
    }
}