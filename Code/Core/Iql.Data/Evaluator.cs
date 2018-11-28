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
            return EvaluateLambdaInternalAsync(expression, entity, entityType, null, false).Result;
        }

        public static Task<object> EvaluateExpressionAsync<T>(
            Expression<Func<T, object>> expression,
            T entity,
            IDataContext dataContext)
            where T : class
        {
            return EvaluateLambdaAsync(expression, entity, dataContext);
        }

        public static Task<object> EvaluateLambdaAsync(
            LambdaExpression expression,
            object entity,
            IDataContext dataContext,
            Type entityType = null)
        {
            return EvaluateLambdaInternalAsync(expression, entity, entityType, dataContext, true);
        }

        private static async Task<object> EvaluateLambdaInternalAsync(
            LambdaExpression expression,
            object entity,
            Type entityType,
            IDataContext dataContext,
            bool isAsync)
        {
            entityType = entityType ?? entity.GetType();
            var iql = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(expression, entityType).Expression;
            var propertyExpressions = iql.TopLevelPropertyExpressions();
            var lookup = new Dictionary<IqlExpression, object>();
            for (var i = 0; i < propertyExpressions.Length; i++)
            {
                var propertyExpression = propertyExpressions[i];
                var path = IqlPropertyPath.FromPropertyExpression(
                    (dataContext?.EntityConfigurationContext ?? DataContext.FindBuilderForEntityType(entityType)).GetEntityByType(entityType),
                    propertyExpression);
                object value = null;
                if (!isAsync)
                {
                    value = path.Evaluate(entity);
                }
                else
                {
                    value = await path.EvaluateAsync(entity, dataContext);
                }
                lookup.Add(propertyExpression, value);
            }

            var processedIql = iql.ReplaceWith((context, iqlExpression) =>
            {
                if (propertyExpressions.Contains(iqlExpression))
                {
                    return new IqlLiteralExpression(lookup[iqlExpression]);
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