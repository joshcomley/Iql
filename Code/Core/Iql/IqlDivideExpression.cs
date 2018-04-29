namespace Iql
{
    public class IqlDivideExpression : IqlBinaryExpression
    {
        public IqlDivideExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.Divide, left, right)
        {
        }

        public IqlDivideExpression() : this(null, null)
        {
        }
    }
}