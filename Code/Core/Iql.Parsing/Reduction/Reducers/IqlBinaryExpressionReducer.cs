using Iql.Extensions;

namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlBinaryExpressionReducer : IqlReducerBase<IqlBinaryExpression>
    {
        public override IqlExpression ReduceStaticContent(IqlBinaryExpression expression, IqlReducer reducer)
        {
            expression.Left = reducer.ReduceStaticContent(expression.Left);
            expression.Right = reducer.ReduceStaticContent(expression.Right);
            if (expression.Left.Type == IqlExpressionType.Literal && expression.Right.Type == IqlExpressionType.Literal)
            {
                var value = reducer.Evaluate(expression);
                var type = value?.GetType();
                return new IqlLiteralExpression(value, type.ToIqlType());
            }
            return expression;
        }
    }
}