using System;
using System.Linq;
using System.Linq.Expressions;

namespace Iql.DotNet.DotNetExpressionToIql
{
    public class DotNetExpressionToIqlExpressionConverter
    {
        public static IqlExpression Parse<T, TResult>(Expression<Func<T, TResult>> exp)
        {
            return new DotNetExpressionToIqlExpressionParser<T>()
                .ToIqlExpression(exp);
        }
        public static IqlExpression Parse(LambdaExpression expression)
        {
            var parameter = expression.Parameters.First();
            var type = typeof(DotNetExpressionToIqlExpressionParser<>).MakeGenericType(parameter.Type);
            var parser = Activator.CreateInstance(type);
            var method = parser.GetType().GetMethod(nameof(DotNetExpressionToIqlExpressionParser<object>.ToIqlExpression));
            return (IqlExpression)method.Invoke(parser, new object[] {expression});
        }
    }
}