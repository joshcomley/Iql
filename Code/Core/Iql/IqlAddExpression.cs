namespace Iql
{
    public class IqlAddExpression : IqlBinaryExpression
    {
        public IqlAddExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionType.Add, left, right)
        {
        }

        public IqlAddExpression() : this(null, null)
        {
        }
    }
}