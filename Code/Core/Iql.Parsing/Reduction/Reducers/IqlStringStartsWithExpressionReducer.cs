namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlStringStartsWithExpressionReducer : IqlParentValueReducerBase<IqlStringStartsWithExpression>
    {
        public override IIqlLiteralExpression Evaluate(IqlStringStartsWithExpression expression, IqlReducer reducer)
        {
            var value = reducer.EvaluateAs<string>(expression.Parent).StartsWith(
                reducer.EvaluateAs<string>(expression.Value)
            );
            return new IqlLiteralExpression(value, IqlType.Boolean);
        }
    }
}