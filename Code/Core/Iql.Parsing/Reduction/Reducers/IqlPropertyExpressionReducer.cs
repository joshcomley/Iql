namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlPropertyExpressionReducer : IqlReducerBase<IqlPropertyExpression>
    {
        public override IqlLiteralExpression Evaluate(IqlPropertyExpression expression, IqlReducer reducer)
        {
            // We should never have a property without a parent
            var parent = reducer.Evaluate(expression.Parent);
            var value = parent.Value.GetType().GetProperty(expression.PropertyName)
                .GetValue(parent.Value);
            return new IqlLiteralExpression(value, expression.ReturnType);
        }
    }
}