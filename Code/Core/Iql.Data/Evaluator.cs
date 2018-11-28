using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Data.Context;
using Iql.Data.Extensions;

namespace Iql.Entities
{
    public class ExpressionEvaluator
    {
        public static object EvaluateExpression<T>(
            Expression<Func<T, object>> expression,
            T entity)
            where T : class
        {
            return EvaluateLambda(expression, entity, typeof(T));
        }

        public static object EvaluateLambda(
            LambdaExpression expression,
            object entity,
            Type entityType = null)
        {
            var iql = ProcessIqlExpression(expression, entity, entityType, null, out var propertyExpressions, out var lookup);
            foreach (var item in lookup.Keys.ToArray())
            {
                lookup[item] = item.Evaluate(entity);
            }
            return ProcessLambdaInternal(entity, lookup, iql, propertyExpressions);
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
            var iql = ProcessIqlExpression(expression, entity, entityType, dataContext, out var propertyExpressions, out var lookup);
            foreach (var item in lookup.Keys.ToArray())
            {
                lookup[item] = await item.EvaluateAsync(entity, dataContext);
            }
            return ProcessLambdaInternal(entity, lookup, iql, propertyExpressions);
        }

        private static IqlExpression ProcessIqlExpression(LambdaExpression expression, object entity, Type entityType,
            IDataContext dataContext, out IqlPropertyExpression[] propertyExpressions, out Dictionary<IqlPropertyPath, object> lookup)
        {
            entityType = entityType ?? entity.GetType();
            var iql = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(expression, entityType).Expression;
            propertyExpressions = iql.TopLevelPropertyExpressions();
            lookup = new Dictionary<IqlPropertyPath, object>();
            for (var i = 0; i < propertyExpressions.Length; i++)
            {
                var propertyExpression = propertyExpressions[i];
                var path = IqlPropertyPath.FromPropertyExpression(
                    (dataContext?.EntityConfigurationContext ?? DataContext.FindBuilderForEntityType(entityType))
                    .GetEntityByType(entityType),
                    propertyExpression);
                lookup.Add(path, null);
            }

            return iql;
        }

        private static object ProcessLambdaInternal(object entity, Dictionary<IqlPropertyPath, object> lookup, IqlExpression iql,
            IqlPropertyExpression[] propertyExpressions)
        {
            var expressionResultLookup = new Dictionary<IqlExpression, object>();
            foreach (var item in lookup)
            {
                expressionResultLookup.Add(item.Key.Expression, item.Value);
            }

            var processedIql = iql.ReplaceWith((context, iqlExpression) =>
            {
                if (propertyExpressions.Contains(iqlExpression))
                {
                    return new IqlLiteralExpression(expressionResultLookup[iqlExpression]);
                }

                return iqlExpression;
            });
            var processedLambda = IqlConverter.Instance.ConvertIqlToLambdaExpression(processedIql);
            var compiledLambda = processedLambda.Compile();
            var result = compiledLambda.DynamicInvoke(new object[] {entity});
            return result;
        }
    }
}