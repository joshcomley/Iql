namespace Iql
{
    public abstract class IqlParentValueExpression : IqlParentValueExpressionBase<IqlExpression>
    {
        protected IqlParentValueExpression(IqlReferenceExpression parent, IqlExpression value, IqlExpressionKind kind, IqlType returnType) : base(parent, value, kind, returnType)
        {
        }
    }

    public abstract class IqlParentValueExpressionBase<TExpression> : IqlReferenceExpression
        where TExpression : IqlExpression
    {
        protected IqlParentValueExpressionBase(IqlReferenceExpression parent,
            TExpression value,
            IqlExpressionKind kind,
            IqlType returnType)
            : base(kind, returnType, parent)
        {
            Value = value;
        }

        public TExpression Value { get; set; }
    }
}