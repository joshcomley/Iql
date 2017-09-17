namespace Iql
{
    public class IqlUnarySubtractExpression : IqlUnaryExpression
    {
        public IqlUnarySubtractExpression(object value) : base(value, IqlExpressionType.UnarySubtract)
        {
        }

        public IqlUnarySubtractExpression() : this(null)
        {
        }
    }
}