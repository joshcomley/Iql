namespace Iql
{
    public class IqlMultiplyEqualsExpression : IqlBinaryExpression
    {
        public IqlMultiplyEqualsExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionType.MultiplyEquals, left, right)
        {
        }

        public IqlMultiplyEqualsExpression() : this(null, null)
        {
        }
    }
}