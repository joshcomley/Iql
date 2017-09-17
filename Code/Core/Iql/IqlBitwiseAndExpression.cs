namespace Iql
{
    public class IqlBitwiseAndExpression : IqlBinaryExpression
    {
        public IqlBitwiseAndExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionType.BitwiseAnd, left, right)
        {
        }

        public IqlBitwiseAndExpression() : this(null, null)
        {
        }
    }
}