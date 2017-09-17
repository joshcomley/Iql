namespace Iql
{
    public class IqlAssignExpression : IqlBinaryExpression
    {
        public IqlAssignExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionType.Assign, left, right)
        {
        }

        public IqlAssignExpression() : this(null, null)
        {
        }
    }
}