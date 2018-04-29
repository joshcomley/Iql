namespace Iql
{
    public class IqlBitwiseNotExpression : IqlBinaryExpression
    {
        public IqlBitwiseNotExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.BitwiseNot, left, right)
        {
        }

        public IqlBitwiseNotExpression() : this(null, null)
        {
        }
    }
}