namespace Iql
{
    public class IqlStringEndsWithExpression : IqlParentValueExpression
    {
        public IqlStringEndsWithExpression(IqlReferenceExpression parent, IqlReferenceExpression value)
            : base(parent, value, IqlExpressionType.StringEndsWith, IqlType.Boolean)
        {
        }

        public IqlStringEndsWithExpression() : this(null, null)
        {
        }
    }
}