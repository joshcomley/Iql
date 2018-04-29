namespace Iql
{
    public class IqlStringConcatExpression : IqlParentValueExpression
    {
        public IqlStringConcatExpression(IqlReferenceExpression parent, IqlReferenceExpression value)
            : base(parent, value, IqlExpressionKind.StringConcat, IqlType.String)
        {
        }

        public IqlStringConcatExpression() : this(null, null)
        {
        }
    }
}