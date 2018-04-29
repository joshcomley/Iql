namespace Iql
{
    public class IqlIsNotEqualToExpression : IqlBinaryExpression
    {
        public IqlIsNotEqualToExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.IsNotEqualTo, left, right)
        {
        }

        public IqlIsNotEqualToExpression() : this(null, null)
        {
        }
    }
}