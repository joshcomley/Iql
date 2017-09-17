namespace Iql
{
    public class IqlDivideExpression : IqlBinaryExpression
    {
        public IqlDivideExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionType.Divide, left, right)
        {
        }

        public IqlDivideExpression() : this(null, null)
        {
        }
    }
}