namespace Iql
{
    public class IqlOrExpression : IqlBinaryExpression
    {
        public IqlOrExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.Or, left, right)
        {
        }

        public IqlOrExpression() : this(null, null)
        {
        }
    }
}