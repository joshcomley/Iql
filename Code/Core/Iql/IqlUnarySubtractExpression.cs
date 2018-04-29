namespace Iql
{
    public class IqlUnarySubtractExpression : IqlUnaryExpression
    {
        public IqlUnarySubtractExpression(object value) : base(value, IqlExpressionKind.UnarySubtract)
        {
        }

        public IqlUnarySubtractExpression() : this(null)
        {
        }
    }
}