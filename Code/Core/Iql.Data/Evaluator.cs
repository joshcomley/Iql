using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Data.Context;
using Iql.Data.DataStores.InMemory;
using Iql.Data.Evaluation;
using Iql.Data.Extensions;
using Iql.Data.IqlToIql;
using Iql.Data.Types;
using Iql.Entities;
using Iql.Entities.InferredValues;
using Iql.Entities.Permissions;
using Iql.Entities.Rules.Relationship;
using Iql.Entities.Services;
using Iql.Extensions;
using Iql.Parsing.Evaluation;
using Iql.Parsing.Types;

namespace Iql.Data
{
    class ProcessExpressionResult
    {
        public bool Success { get; }
        public IqlFlattenedExpression[] propertyExpressions { get; set; }
        public Dictionary<IqlPropertyPath, object> lookup { get; set; }
        public IqlExpression expression { get; set; }

        public ProcessExpressionResult(bool success, IqlFlattenedExpression[] propertyExpressions, Dictionary<IqlPropertyPath, object> lookup, IqlExpression expression)
        {
            Success = success;
            this.propertyExpressions = propertyExpressions;
            this.lookup = lookup;
            this.expression = expression;
        }
    }
    public class IqlExpressonEvaluatedResult
    {
        public IqlExpression Expression { get; }
        public IqlExpressonEvaluationResult RootResult { get; }
        public IqlPropertyPathEvaluationResult Result { get; }

        public IqlExpressonEvaluatedResult(
            IqlPropertyPathEvaluationResult result,
            IqlExpression expression,
            IqlExpressonEvaluationResult rootResult
            )
        {
            Result = result;
            Expression = expression;
            RootResult = rootResult;
        }
    }

    public class IqlExpressonEvaluationResult : IqlObjectEvaluationResult
    {
        public IqlPropertyPathEvaluationResult[] Paths { get; set; }

        public IqlExpressonEvaluationResult(
            bool success,
            object result,
            IEnumerable<IqlPropertyPathEvaluationResult> paths) : base(success, result)
        {
            Paths = paths.ToArray();
        }
    }

    public class IqlPropertyPathEvaluationResult
    {
        public bool Success { get; set; }
        public object Parent { get; }
        public IqlPropertyPath Source { get; }
        public IqlPropertyPathEvaluated[] Results { get; set; }
        public Func<IqlPropertyPathEvaluationResult, object> ResolveNull { get; set; }
        public object Value => Success ? Results.Last().Value : ResolveNull(this);

        public IqlPropertyPathEvaluationResult(
            bool success,
            object parent,
            IqlPropertyPath source,
            IqlPropertyPathEvaluated[] results,
            Func<IqlPropertyPathEvaluationResult, object> resolveNull = null
            )
        {
            Source = source;
            Results = results;
            ResolveNull = resolveNull ?? (_ => null);
            Success = success;
            Parent = parent;
        }
    }

    public class IqlPropertyPathEvaluated
    {
        public IqlPropertyPathEvaluationResult Result { get; }
        public IqlPropertyPath Path { get; set; }
        public object Parent { get; set; }
        public object Value { get; set; }
        public int PathLength { get; }
        public int Position { get; }
        public bool IsFinal => Position == PathLength - 1;
        public IqlPropertyPathEvaluated(
            IqlPropertyPathEvaluationResult result,
            IqlPropertyPath path,
            object parent,
            object value,
            int pathLength,
            int position)
        {
            Result = result;
            Path = path;
            Parent = parent;
            Value = value;
            PathLength = pathLength;
            Position = position;
        }
    }
    public static class ExpressionEvaluator
    {
        public static Task<IqlObjectEvaluationResult> EvaluateExpressionAsync<T>(
            Expression<Func<T, object>> expression,
            T entity,
            ITypeResolver typeResolver,
            IServiceProviderProvider serviceProviderProvider)
            where T : class
        {
            return EvaluateLambdaAsync(expression, entity, typeResolver, serviceProviderProvider, typeof(T));
        }

        public static async Task<IqlObjectEvaluationResult> EvaluateLambdaAsync(
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

        public static Task<IqlObjectEvaluationResult> EvaluateExpressionWithDbAsync<T>(
            Expression<Func<T, object>> expression,
            T entity,
            IDataContext dataContext)
            where T : class
        {
            return EvaluateLambdaWithDbAsync(expression, entity, dataContext);
        }

        public static async Task<IqlObjectEvaluationResult> EvaluateLambdaWithDbAsync(
            LambdaExpression expression,
            object entity,
            IDataContext dataContext,
            Type entityType = null)
        {
            entityType = entityType ?? entity.GetType();
            var iql = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(expression, dataContext.EntityConfigurationContext, entityType).Expression;
            return await EvaluateIqlAsync(iql, entity, dataContext, entityType);
        }

        public static async Task<IqlUserPermission> EvaluateEntityPermissionsRuleAsync<TEntity, TUser>(this IqlUserPermissionRule rule, TUser user, TEntity entity, IDataContext dataContext)
            where TEntity : class
            where TUser : class
        {
            var isEntityNew = dataContext.IsEntityNew(entity);
            var context = new IqlEntityUserPermissionContext<TEntity, TUser>(isEntityNew == null || isEntityNew == true, user, entity);
            var result = await rule.IqlExpression.EvaluateIqlAsync(context, dataContext ?? DataContext.FindDataContextForEntity(entity),
                typeof(IqlEntityUserPermissionContext<TEntity, TUser>));
            var permission = (IqlUserPermission)result.Result;
            return permission;
        }

        public static async Task<IqlUserPermission> GetUserPermissionAsync(
            this UserPermissionsManager permissionsManager,
            IIqlCustomEvaluator evaluationContext,
            object user,
            object entity = null,
            IServiceProviderProvider serviceProviderProvider = null,
            ITypeResolver typeResolver = null
        )
        {
            typeResolver = typeResolver ?? permissionsManager.EntityConfigurationBuilder;
            serviceProviderProvider = serviceProviderProvider ?? permissionsManager.EntityConfigurationBuilder;
            IqlUserPermission result = IqlUserPermission.Unset;
            for (var i = 0; i < permissionsManager.Container.PermissionRules.Count; i++)
            {
                var rule = permissionsManager.Container.PermissionRules[i];
                var evaluatedResult = await rule.EvaluateEntityPermissionsRuleCustomAsync(
                    user, 
                    entity, 
                    serviceProviderProvider, evaluationContext,
                    typeResolver,
                    evaluationContext.IsEntityNew(
                        permissionsManager.EntityConfigurationBuilder.GetEntityByType(entity.GetType()), 
                        entity));
                if (evaluatedResult == IqlUserPermission.None)
                {
                    return evaluatedResult;
                }

                if (result == IqlUserPermission.Unset && evaluatedResult != IqlUserPermission.Unset)
                {
                    result = evaluatedResult;
                }
                else if (result != IqlUserPermission.Unset && evaluatedResult != IqlUserPermission.Unset && evaluatedResult < result)
                {
                    result = evaluatedResult;
                }
            }
            // TODO: Evaluate the permissions
            return result;
        }


        public static async Task<IqlUserPermission> EvaluateEntityPermissionsRuleCustomAsync<TEntity, TUser>(
            this IqlUserPermissionRule rule,
            TUser user,
            TEntity entity,
            IServiceProviderProvider serviceProviderProvider,
            IIqlCustomEvaluator evaluator,
            ITypeResolver typeResolver,
            bool isEntityNew)
            where TEntity : class
            where TUser : class
        {
            var context = new IqlEntityUserPermissionContext<TEntity, TUser>(isEntityNew, user, entity);
            var result = await rule.IqlExpression.EvaluateIqlCustomAsync(
                serviceProviderProvider,
                context,
                evaluator,
                typeResolver,
                typeof(IqlEntityUserPermissionContext<TEntity, TUser>));
            var permission = (IqlUserPermission)result.Result;
            return permission;
        }

        public static async Task<IqlUserPermission> EvaluatePermissionsRuleAsync<TEntity, TUser>(this IqlUserPermissionRule rule, TUser user, TEntity entity, IDataContext dataContext)
            where TEntity : class
            where TUser : class
        {
            var isEntityNew = dataContext.IsEntityNew(entity);
            var context = new IqlEntityUserPermissionContext<TEntity, TUser>(isEntityNew == null || isEntityNew == true, user, entity);
            var result = await rule.IqlExpression.EvaluateIqlAsync(context, dataContext ?? DataContext.FindDataContextForEntity(entity),
                typeof(IqlEntityUserPermissionContext<TEntity, TUser>));
            var permission = (IqlUserPermission)result.Result;
            return permission;
        }

        public static async Task<IqlUserPermission> EvaluatePermissionsRuleCustomAsync<TEntity, TUser>(
            this IqlUserPermissionRule rule,
            TUser user,
            IServiceProviderProvider serviceProviderProvider,
            IIqlCustomEvaluator evaluator,
            ITypeResolver typeResolver)
            where TEntity : class
            where TUser : class
        {
            var context = new IqlUserPermissionContext<TUser>(user);
            var result = await rule.IqlExpression.EvaluateIqlCustomAsync(
                serviceProviderProvider,
                context,
                evaluator,
                typeResolver,
                typeof(IqlUserPermissionContext<TUser>));
            var permission = (IqlUserPermission)result.Result;
            return permission;
        }

        public static Task<IqlExpressonEvaluationResult> EvaluateIqlAsync(
            this IqlExpression expression,
            object entity,
            IDataContext dataContext,
            Type contextType = null,
            ITypeResolver typeResolver = null)
        {
            return expression.EvaluateIqlCustomAsync(
                (IServiceProviderProvider)dataContext ?? DataContext.FindBuilderForEntityType(contextType),
                entity,
                new DefaultEvaluator(dataContext),
                typeResolver ?? dataContext.EntityConfigurationContext,
                contextType);
        }

        public static async Task<IqlExpressonEvaluationResult> EvaluateIqlPathAsync(
            this IqlExpression expression,
            object context,
            IDataContext dataContext,
            Type contextType,
            ITypeResolver typeResolver = null,
            bool populatePath = false
            )
        {
            var evaluator = new DefaultEvaluator(dataContext);
            var value = await EvaluateIqlCustomAsync(
                expression,
                dataContext?.EntityConfigurationContext ?? DataContext.FindBuilderForEntityType(contextType),
                context,
                evaluator,
                typeResolver ?? dataContext.EntityConfigurationContext,
                contextType,
                populatePath);
            value.Result = value.Result is IqlPropertyPathEvaluationResult
                ? (value.Result as IqlPropertyPathEvaluationResult).Value
                : value.Result;
            return value;
        }
        public static async Task<IqlExpressonEvaluationResult> EvaluateIqlCustomAsync(
            this IqlExpression expression,
            IServiceProviderProvider serviceProviderProvider,
            object context,
            IIqlCustomEvaluator customEvaluator,
            ITypeResolver typeResolver,
            Type contextType = null,
            bool populatePath = false
            )
        {
            var success = true;
            var paths = new List<IqlPropertyPathEvaluationResult>();
            contextType = contextType ?? context.GetType();
            //if (parameter is IInferredValueContext context)
            //{
            //    entityType = context.EntityType;
            //}
            var clone = expression.Clone();
            var processResult = await ProcessIqlExpressionAsync(
                clone,
                context,
                contextType,
                typeResolver,
                serviceProviderProvider);
            success = processResult.Success;
            var iqlPropertyPaths = processResult.propertyExpressions.ToArray();
            var keys = processResult.lookup.Keys.ToArray();
            for (var i = 0; i < iqlPropertyPaths.Length; i++)
            {
                IqlPropertyPath item = null;
                var e = context;
                //var relationshipFilterContext = e as IRelationshipFilterContext;
                //var inferredValueContext = e as IInferredValueContext;
                //if (relationshipFilterContext != null)
                //{
                //    e = relationshipFilterContext.Owner;
                //}
                //else if (inferredValueContext != null)
                //{
                //    var path = iqlPropertyPaths[i].Expression.ToSimplePropertyPath();
                //    if (path.PathParts.Length > 0)
                //    {
                //        switch (path.PathParts[0])
                //        {
                //            case nameof(IInferredValueContext.PreviousEntityState):
                //                e = inferredValueContext.PreviousEntityState;
                //                break;
                //            case nameof(IInferredValueContext.CurrentEntityState):
                //                e = inferredValueContext.CurrentEntityState;
                //                break;
                //        }
                //    }
                //    contextType = inferredValueContext.EntityType;
                //    item = IqlPropertyPath.FromString(
                //        typeResolver,
                //        path.PathAfter(1),
                //        typeResolver.FindTypeByType(contextType), 
                //        null,
                //        path.PathParts[0]);
                //}
                item = item ?? keys[i];
                var evaluationResult = await item.EvaluateCustomAsync(
                    e,
                    customEvaluator,
                    populatePath);
                paths.Add(evaluationResult);
                var flattenedExpression = processResult.propertyExpressions.First(_ => _.Expression == iqlPropertyPaths[i].Expression);
                var value = evaluationResult.Value ?? ResolveNull(item, flattenedExpression);
                var iqlEvaluationResult = new IqlObjectEvaluationResult(evaluationResult.Success, value);
                processResult.lookup[keys[i]] = iqlEvaluationResult.Result;
                if (!iqlEvaluationResult.Success)
                {
                    success = false;
                }
            }

            var finalResult = Finalise(context, typeResolver, processResult.lookup, processResult.expression, processResult.propertyExpressions);
            return new IqlExpressonEvaluationResult(success, finalResult, paths);
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
            var result = compiledLambda.DynamicInvoke(entity
#if TypeScript
                , new InMemoryContext<object>(null)
#endif
                );
            if (result is IqlLiteralExpression)
            {
                result = (result as IqlLiteralExpression).Value;
            }
            return result;
        }
    }
}