namespace Iql
{
    public class IqlSubtractEqualsExpression : IqlBinaryExpression
    {
        public IqlSubtractEqualsExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionType.SubtractEquals, left, right)
        {
        }

        public IqlSubtractEqualsExpression() : this(null, null)
        {
        }
    }
}