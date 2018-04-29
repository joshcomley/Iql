namespace Iql
{
    public abstract class IqlReferenceExpression : IqlExpression
    {
        protected IqlReferenceExpression(IqlExpressionKind kind, IqlType returnType, IqlReferenceExpression parent = null) : base(kind, returnType, parent)
        {
        }
    }
}