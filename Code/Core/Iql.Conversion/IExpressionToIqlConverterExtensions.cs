using System.Linq.Expressions;

namespace Iql.Conversion
{
    public static class IExpressionToIqlConverterExtensions
    {
        public static string GetPropertyName(this IExpressionToIqlConverter converter, LambdaExpression property)
        {
            var iql = converter.ConvertLambdaExpressionToIql<object>(property).Expression;
            if (iql is IqlLambdaExpression)
            {
                iql = (iql as IqlLambdaExpression).Body;
            }
            var propertyExpression = iql as IqlPropertyExpression;
            return propertyExpression?.PropertyName;
        }
    }
}