namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlStringEndsWithExpressionReducer : IqlParentValueReducerBase<IqlStringEndsWithExpression>
    {
        public override IIqlLiteralExpression Evaluate(IqlStringEndsWithExpression expression, IqlReducer reducer)
        {
            var value = reducer.EvaluateAs<string>(expression.Parent).EndsWith(
                reducer.EvaluateAs<string>(expression.Value)
            );
            return new IqlLiteralExpression(value, IqlType.Boolean);
        }
    }
}