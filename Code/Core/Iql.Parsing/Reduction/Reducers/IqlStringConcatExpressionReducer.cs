namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlStringConcatExpressionReducer : IqlParentValueReducerBase<IqlStringConcatExpression>
    {
        public override IqlLiteralExpression Evaluate(IqlStringConcatExpression expression, IqlReducer reducer)
        {
            return new IqlLiteralExpression(
                reducer.EvaluateAs<string>(expression.Parent) + reducer.EvaluateAs<string>(expression.Value),
                IqlType.String);
        }
    }
}