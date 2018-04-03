namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlStringSubStringExpressionReducer : IqlReducerBase<IqlStringSubStringExpression>
    {
        public override IIqlLiteralExpression Evaluate(IqlStringSubStringExpression expression, IqlReducer reducer)
        {
            var substring = reducer.EvaluateAs<string>(expression.Parent).Substring(
                reducer.EvaluateAs<int>(expression.Value),
                reducer.EvaluateAs<int>(expression.Take)
            );
            return new IqlLiteralExpression(substring, IqlType.String);
        }
    }
}