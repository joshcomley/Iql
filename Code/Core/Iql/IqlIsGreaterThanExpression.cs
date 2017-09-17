namespace Iql
{
    public class IqlIsGreaterThanExpression : IqlBinaryExpression
    {
        public IqlIsGreaterThanExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionType.IsGreaterThan, left, right)
        {
        }

        public IqlIsGreaterThanExpression() : this(null, null)
        {
        }
    }
}