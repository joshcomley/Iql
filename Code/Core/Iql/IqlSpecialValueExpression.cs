namespace Iql
{
    public abstract class IqlSpecialValueExpression : IqlExpression
    {
        protected IqlSpecialValueExpression(
            IqlExpressionKind kind) : base(kind, IqlType.Unknown, null)
        {
        }
    }
}