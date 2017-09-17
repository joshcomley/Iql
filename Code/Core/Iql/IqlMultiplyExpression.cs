namespace Iql
{
    public class IqlMultiplyExpression : IqlBinaryExpression
    {
        public IqlMultiplyExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionType.Multiply, left, right)
        {
        }

        public IqlMultiplyExpression() : this(null, null)
        {
        }
    }
}