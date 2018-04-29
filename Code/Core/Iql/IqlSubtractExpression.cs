namespace Iql
{
    public class IqlSubtractExpression : IqlBinaryExpression
    {
        public IqlSubtractExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.Subtract, left, right)
        {
        }

        public IqlSubtractExpression() : this(null, null)
        {
        }
    }
}