namespace Iql
{
    public class IqlIsEqualToExpression : IqlBinaryExpression
    {
        public IqlIsEqualToExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.IsEqualTo, left, right)
        {
        }

        public IqlIsEqualToExpression() : this(null, null)
        {
        }
    }
}