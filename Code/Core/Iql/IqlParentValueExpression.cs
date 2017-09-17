namespace Iql
{
    public abstract class IqlParentValueExpression : IqlReferenceExpression
    {
        protected IqlParentValueExpression(IqlReferenceExpression parent,
            IqlExpression value,
            IqlExpressionType type,
            IqlType returnType)
            : base(type, returnType, parent)
        {
            Value = value;
        }

        public IqlExpression Value { get; set; }
    }
}