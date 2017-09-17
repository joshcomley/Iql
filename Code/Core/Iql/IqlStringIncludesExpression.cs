namespace Iql
{
    public class IqlStringIncludesExpression : IqlParentValueExpression
    {
        public IqlStringIncludesExpression(IqlReferenceExpression parent, IqlReferenceExpression value)
            : base(parent, value, IqlExpressionType.StringIncludes, IqlType.Boolean)
        {
        }

        public IqlStringIncludesExpression() : this(null, null)
        {
        }
    }
}