namespace Iql
{
    public class IqlAddEqualsExpression : IqlBinaryExpression
    {
        public IqlAddEqualsExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionType.AddEquals, left, right)
        {
        }

        public IqlAddEqualsExpression() : this(null, null)
        {
        }
    }
}