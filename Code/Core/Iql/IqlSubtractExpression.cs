namespace Iql
{
    public class IqlSubtractExpression : IqlBinaryExpression
    {
        public IqlSubtractExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionType.Subtract, left, right)
        {
        }

        public IqlSubtractExpression() : this(null, null)
        {
        }
    }
}