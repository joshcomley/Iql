namespace Iql
{
    public class IqlOrExpression : IqlBinaryExpression
    {
        public IqlOrExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionType.Or, left, right)
        {
        }

        public IqlOrExpression() : this(null, null)
        {
        }
    }
}