namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlLiteralExpressionReducer : IqlReducerBase<IqlLiteralExpression>
    {
        public override IIqlLiteralExpression Evaluate(IqlLiteralExpression expression, IqlReducer reducer)
        {
            return expression;
        }
    }
}