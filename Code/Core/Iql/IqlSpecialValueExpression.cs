namespace Iql
{
    public abstract class IqlSpecialValueExpression : IqlReferenceExpression
    {
        public bool CanFail { get; set; }
        protected IqlSpecialValueExpression(
            IqlExpressionKind kind) : base(kind, IqlType.Unknown, null)
        {
        }
    }
}