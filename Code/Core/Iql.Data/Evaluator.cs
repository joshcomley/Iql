using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Data.Context;
using Iql.Data.Extensions;
using Iql.Data.IqlToIql;
using Iql.Entities;
using Iql.Entities.Rules.Relationship;

namespace Iql.Data
{
    class ProcessExpressionResult
    {
        public IqlFlattenedExpression[] propertyExpressions { get; set; }
        public Dictionary<IqlPropertyPath, object> lookup { get; set; }
        public IqlExpression expression { get; set; }

        public ProcessExpressionResult(IqlFlattenedExpression[] propertyExpressions, Dictionary<IqlPropertyPath, object> lookup, IqlExpression expression)
        {
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

    public class IqlExpressonEvaluationResult
    {
        public IqlPropertyPathEvaluationResult[] Results { get; set; }
        public object Value { get; set; }

        public IqlExpressonEvaluationResult()
        {
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
        public static Task<object> EvaluateExpressionAsync<T>(
            Expression<Func<T, object>> expression,
            T entity,
            IEntityConfigurationBuilder builder = null)
            where T : class
        {
            return EvaluateLambdaAsync(expression, entity, builder, typeof(T));
        }

        public static async Task<object> EvaluateLambdaAsync(
            LambdaExpression expression,
            object entity,
            IEntityConfigurationBuilder builder = null,
            Type entityType = null)
        {
            var iql = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(expression, entityType).Expression;
            var processResult = await ProcessIqlExpressionAsync(iql, entity, entityType, builder);
            foreach (var item in processResult.lookup.Keys.ToArray())
            {
                processResult.lookup[item] = item.Evaluate(entity) ?? ResolveNull(item, processResult.propertyExpressions.First(_ => _.Expression == item.Expression));
            }
            return Finalise(entity, processResult.lookup, iql, processResult.propertyExpressions);
        }

        public static Task<object> EvaluateExpressionAsync<T>(
            Expression<Func<T, object>> expression,
            T entity,
            IDataContext dataContext)
            where T : class
        {
            return EvaluateLambdaAsync(expression, entity, dataContext);
        }

        public static async Task<object> EvaluateLambdaAsync(
            LambdaExpression expression,
            object entity,
            IDataContext dataContext,
            Type entityType = null)
        {
            entityType = entityType ?? entity.GetType();
            var iql = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(expression, entityType).Expression;
            return await EvaluateIqlAsync(iql, entity, dataContext, entityType);
        }

        public static Task<object> EvaluateIqlAsync(
            this IqlExpression expression,
            object entity,
            IDataContext dataContext,
            Type entityType = null)
        {
            return EvaluateIqlCustomAsync(
                expression,
                dataContext?.EntityConfigurationContext ?? DataContext.FindBuilderForEntityType(entityType),
                entity,
                async (e, type, propertyPath, flattenedExpression, length, i) =>
                    (await propertyPath.EvaluateAsync(e, dataContext)).Value
                    ?? ResolveNull(propertyPath, flattenedExpression));
        }

        public static async Task<IqlExpressonEvaluationResult> EvaluateIqlPathAsync(
            this IqlExpression expression,
            object entity,
            IDataContext dataContext,
            Type entityType)
        {
            var evaluationResult = new IqlExpressonEvaluationResult();
            var paths = new List<IqlPropertyPathEvaluationResult>();
            var value = await EvaluateIqlCustomAsync(
                expression,
                dataContext?.EntityConfigurationContext ?? DataContext.FindBuilderForEntityType(entityType),
                entity,
                async (e, type, propertyPath, flattenedExpression, length, i) =>
                {
                    var result = await propertyPath.EvaluateAsync(e, dataContext);

                    //ResolveNull(propertyPath, flattenedExpression)
                    result.ResolveNull = _ => ResolveNull(propertyPath, flattenedExpression);
                    paths.Add(result);
                    return result;
                });
            evaluationResult.Results = paths.ToArray();
            evaluationResult.Value = value is IqlPropertyPathEvaluationResult
                ? (value as IqlPropertyPathEvaluationResult).Value
                : value;
            return evaluationResult;
        }

        private static object ResolveNull(IqlPropertyPath propertyPath, IqlFlattenedExpression flattenedExpression)
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

        public static async Task<object> EvaluateIqlCustomAsync(
            IqlExpression expression,
            IEntityConfigurationBuilder builder,
            object entity,
            Func<object, Type, IqlPropertyPath, IqlFlattenedExpression, int, int, Task<object>> evaluatorAsync,
            Type entityType = null)
        {
            entityType = entityType ?? entity.GetType();
            var processResult = await ProcessIqlExpressionAsync(
                expression.Clone(),
                entity,
                entityType,
                builder);
            var iqlPropertyPaths = processResult.lookup.Keys.ToArray();
            for (var i = 0; i < iqlPropertyPaths.Length; i++)
            {
                var item = iqlPropertyPaths[i];
                var e = entity;
                if (e is IRelationshipFilterContext)
                {
                    e = (e as IRelationshipFilterContext).Owner;
                }

                processResult.lookup[item] = await evaluatorAsync(
                    e,
                    entityType, 
                    item,
                    processResult.propertyExpressions.First(_ => _.Expression == item.Expression),
                    iqlPropertyPaths.Length,
                    i);
            }

            return Finalise(entity, processResult.lookup, processResult.expression, processResult.propertyExpressions);
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
            object entity,
            Type entityType,
            IEntityConfigurationBuilder builder)
        {
            builder = builder ?? DataContext.FindBuilderForEntityType(entityType);
            var propertyExpressions = iql.TopLevelPropertyExpressions();
            var lookup = new Dictionary<IqlPropertyPath, object>();
            if (entity is IRelationshipFilterContext)
            {
                entityType = (entity as IRelationshipFilterContext).Owner.GetType();
            }
            iql = await iql.ProcessAsync(builder.GetEntityByType(
                entityType));
            for (var i = 0; i < propertyExpressions.Length; i++)
            {
                var propertyExpression = propertyExpressions[i];
                var path = IqlPropertyPath.FromPropertyExpression(
                    builder.GetEntityByType(entityType),
                    propertyExpression.Expression as IqlPropertyExpression);
                lookup.Add(path, null);
            }
            return new ProcessExpressionResult(
                propertyExpressions,
                lookup,
                iql);
        }

        private static object Finalise(object entity, 
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
            var processedLambda = IqlConverter.Instance.ConvertIqlToLambdaExpression(processedIql);
            var compiledLambda = processedLambda.Compile();
            var result = compiledLambda.DynamicInvoke(new object[] { entity });
            return result;
        }
    }
}