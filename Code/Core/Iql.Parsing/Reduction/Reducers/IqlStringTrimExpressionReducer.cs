namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlStringTrimExpressionReducer : IqlReducerBase<IqlStringTrimExpression>
    {
        public override IqlLiteralExpression Evaluate(IqlStringTrimExpression expression, IqlReducer reducer)
        {
            return new IqlLiteralExpression(reducer.EvaluateAs<string>(expression.Parent).Trim(), IqlType.String);
        }
    }
}