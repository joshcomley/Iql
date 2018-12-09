namespace Iql
{
    public abstract class IqlSpecialValueExpression : IqlReferenceExpression
    {
        protected IqlSpecialValueExpression(
            IqlExpressionKind kind) : base(kind, IqlType.Unknown, null)
        {
        }
    }
}