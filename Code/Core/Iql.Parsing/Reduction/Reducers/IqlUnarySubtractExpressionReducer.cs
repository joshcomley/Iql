namespace Iql.Parsing.Reduction.Reducers
{
    public class IqlUnarySubtractExpressionReducer : IqlReducerBase<IqlUnarySubtractExpression>
    {
        public override IIqlLiteralExpression Evaluate(IqlUnarySubtractExpression expression, IqlReducer reducer)
        {
            if (expression.Value is IqlExpression)
            {
                var literal = reducer.Evaluate(expression.Value as IqlExpression);
                if (Equals(null, literal.Value))
                {
                    return literal;
                }
                if (literal.Value is int)
                {
                    literal.Value = -(int)literal.Value;
                }
#if !TypeScript
                else if (literal.Value is long)
                {
                    literal.Value = -(long)literal.Value;
                }
                else if (literal.Value is short)
                {
                    literal.Value = -(short)literal.Value;
                }
                else if (literal.Value is double)
                {
                    literal.Value = -(double)literal.Value;
                }
                else if (literal.Value is float)
                {
                    literal.Value = -(float)literal.Value;
                }
                else if (literal.Value is decimal)
                {
                    literal.Value = -(decimal)literal.Value;
                }
#endif
                return literal;
            }
            return base.Evaluate(expression, reducer);
        }
    }
}