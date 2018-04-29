namespace Iql
{
    public class IqlStringStartsWithExpression : IqlParentValueExpression
    {
        public IqlStringStartsWithExpression(IqlReferenceExpression parent, IqlReferenceExpression value)
            : base(parent, value, IqlExpressionKind.StringStartsWith, IqlType.Boolean)
        {
        }

        public IqlStringStartsWithExpression() : this(null, null)
        {
        }
    }
}