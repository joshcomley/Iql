using System.Linq.Expressions;
using Iql.Parsing.Extensions;

namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlAddExpressionReducer : IqlReducerBase<IqlAddExpression>
    {
        public override IIqlLiteralExpression Evaluate(IqlAddExpression expression, IqlReducer reducer)
        {
            var left = reducer.Evaluate(expression.Left);
            var right = reducer.Evaluate(expression.Right);
            if (left?.Value is string || right?.Value is string)
            {
                return new IqlLiteralExpression(left?.Value?.ToString() + right, IqlType.String);
            }
            var value = Expression.Add(Expression.Constant(left?.Value), Expression.Constant(right?.Value)).GetValue();
            return new IqlLiteralExpression(value, expression.Left.ReturnType);
        }
    }
}