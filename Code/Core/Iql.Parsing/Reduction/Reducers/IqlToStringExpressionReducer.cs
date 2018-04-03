namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlToStringExpressionReducer : IqlReducerBase<IqlToStringExpression>
    {
        public override IIqlLiteralExpression Evaluate(IqlToStringExpression expression, IqlReducer reducer)
        {
            var value = reducer.Evaluate(expression.Parent).ToString();
            return new IqlLiteralExpression(value, IqlType.String);
        }
    }
}