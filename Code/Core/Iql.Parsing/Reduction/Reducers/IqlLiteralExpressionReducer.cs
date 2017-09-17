namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlLiteralExpressionReducer : IqlReducerBase<IqlLiteralExpression>
    {
        public override IqlLiteralExpression Evaluate(IqlLiteralExpression expression, IqlReducer reducer)
        {
            return expression;
        }
    }
}