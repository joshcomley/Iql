namespace Iql
{
    public class IqlIsLessThanExpression : IqlBinaryExpression
    {
        public IqlIsLessThanExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionType.IsLessThan, left, right)
        {
        }

        public IqlIsLessThanExpression() : this(null, null)
        {
        }
    }
}