using Iql.Extensions;

namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlBinaryExpressionReducer : IqlReducerBase<IqlBinaryExpression>
    {
        //public override IqlLiteralExpression Evaluate(IqlBinaryExpression expression, IqlReducer reducer)
        //{
        //    return null;
        //}

        public override IqlExpression ReduceStaticContent(IqlBinaryExpression expression, IqlReducer reducer)
        {
            expression.Left = reducer.ReduceStaticContent(expression.Left);
            expression.Right = reducer.ReduceStaticContent(expression.Right);
            if (expression.Type == IqlExpressionType.And &&
                expression.Left.Type == IqlExpressionType.Property && expression.Right.Type == IqlExpressionType.Literal
                                                                   && expression.Right.ReturnType == IqlType.Integer)
            {
                return new IqlHasExpression(expression.Left, expression.Right);
            }
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