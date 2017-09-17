namespace Iql
{
    public class IqlBitwiseNotExpression : IqlBinaryExpression
    {
        public IqlBitwiseNotExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionType.BitwiseNot, left, right)
        {
        }

        public IqlBitwiseNotExpression() : this(null, null)
        {
        }
    }
}