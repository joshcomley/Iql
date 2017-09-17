namespace Iql
{
    public class IqlIsGreaterThanOrEqualToExpression : IqlBinaryExpression
    {
        public IqlIsGreaterThanOrEqualToExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionType.IsGreaterThanOrEqualTo, left, right)
        {
        }

        public IqlIsGreaterThanOrEqualToExpression() : this(null, null)
        {
        }
    }
}