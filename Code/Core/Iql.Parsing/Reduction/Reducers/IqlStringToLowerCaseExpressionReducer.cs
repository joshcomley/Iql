namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlStringToLowerCaseExpressionReducer : IqlReducerBase<IqlStringToLowerCaseExpression>
    {
        public override IqlLiteralExpression Evaluate(IqlStringToLowerCaseExpression expression, IqlReducer reducer)
        {
            return new IqlLiteralExpression(reducer.EvaluateAs<string>(expression.Parent).ToLower(), IqlType.String);
        }
    }
}