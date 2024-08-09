using Iql.Conversion;
using System;
using System.Linq.Expressions;

namespace Iql.Entities
{
    public static class EntitiesExpressionExtensions
    {
        public static IqlPropertyExpression ToIqlPropertyExpression(
            this LambdaExpression expression, 
            IEntityConfiguration entityConfiguration)
        {
            var iql = expression.ToIqlLambdaExpression(entityConfiguration);
            var propertyExpression = iql.Body as IqlPropertyExpression;
            return propertyExpression;
        }
        public static IqlLambdaExpression ToIqlLambdaExpression(
            this LambdaExpression expression, 
            IEntityConfiguration entityConfiguration)
        {
            var iql = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(expression, entityConfiguration.Builder, entityConfiguration.Type).Expression;
            return iql as IqlLambdaExpression;
        }

        //public static string GetPropertyName<T>(this Expression<Func<T, object>> expression, IEntityConfiguration entityConfiguration)
        //{
        //    return EntitiesExpressionExtensions.ToIqlPropertyExpression(expression, entityConfiguration)?.Name;
        //}
    }
}
