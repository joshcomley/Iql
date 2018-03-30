using Iql.Extensions;

namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlIsNotEqualToExpressionReducer : IqlReducerBase<IqlIsNotEqualToExpression>
    {
        //public override IqlLiteralExpression Evaluate(IqlBinaryExpression expression, IqlReducer reducer)
        //{
        //    return null;
        //}

        public override IqlExpression ReduceStaticContent(IqlIsNotEqualToExpression expression, IqlReducer reducer)
        {
            expression.Left = reducer.ReduceStaticContent(expression.Left);
            expression.Right = reducer.ReduceStaticContent(expression.Right);
            if (expression.Left.Type == IqlExpressionType.Has)
            {
                if (expression.Right.Type == IqlExpressionType.Literal &&
                    expression.Right is IqlLiteralExpression && 
                    Equals((expression.Right as IqlLiteralExpression).Value, 0))
                {
                    return expression.Left;
                }
            }
            return expression;
        }
    }
}