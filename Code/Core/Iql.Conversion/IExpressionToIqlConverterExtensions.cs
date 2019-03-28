using System.Linq.Expressions;
using Iql.Parsing.Types;

namespace Iql.Conversion
{
    public static class IExpressionToIqlConverterExtensions
    {
        public static string GetPropertyName(this IExpressionToIqlConverter converter, LambdaExpression property, ITypeResolver typeResolver)
        {
            var iql = converter.ConvertLambdaExpressionToIql<object>(property, typeResolver).Expression;
            if (iql is IqlLambdaExpression)
            {
                iql = (iql as IqlLambdaExpression).Body;
            }
            var propertyExpression = iql as IqlPropertyExpression;
            return propertyExpression?.PropertyName;
        }
    }
}