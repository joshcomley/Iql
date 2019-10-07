using Iql.Conversion;
using System;
using System.Linq.Expressions;

namespace Iql.Entities
{
    public static class EntitiesExpressionExtensions
    {
        public static IqlPropertyExpression ToIqlPropertyExpression(this LambdaExpression expression, IEntityConfiguration entityConfiguration)
        {
            var iql = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(expression, entityConfiguration.Builder, entityConfiguration.Type).Expression;
            var propertyExpression = (iql as IqlLambdaExpression).Body as IqlPropertyExpression;
            return propertyExpression;
        }

        //public static string GetPropertyName<T>(this Expression<Func<T, object>> expression, IEntityConfiguration entityConfiguration)
        //{
        //    return EntitiesExpressionExtensions.ToIqlPropertyExpression(expression, entityConfiguration)?.PropertyName;
        //}
    }
}
