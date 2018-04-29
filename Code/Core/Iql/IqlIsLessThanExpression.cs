namespace Iql
{
    public class IqlIsLessThanExpression : IqlBinaryExpression
    {
        public IqlIsLessThanExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.IsLessThan, left, right)
        {
        }

        public IqlIsLessThanExpression() : this(null, null)
        {
        }
    }
}