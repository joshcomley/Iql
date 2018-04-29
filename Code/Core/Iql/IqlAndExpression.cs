namespace Iql
{
    public class IqlAndExpression : IqlBinaryExpression
    {
        public IqlAndExpression(
            IqlExpression left,
            IqlExpression right) :
            base(IqlExpressionKind.And, left, right)
        {
        }

        public IqlAndExpression() : this(null, null)
        {
        }
    }
}