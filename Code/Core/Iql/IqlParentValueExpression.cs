namespace Iql
{
    public abstract class IqlParentValueExpression : IqlReferenceExpression
    {
        protected IqlParentValueExpression(IqlReferenceExpression parent,
            IqlExpression value,
            IqlExpressionKind kind,
            IqlType returnType)
            : base(kind, returnType, parent)
        {
            Value = value;
        }

        public IqlExpression Value { get; set; }
    }
}