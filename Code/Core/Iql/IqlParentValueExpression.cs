namespace Iql
{
    public abstract class IqlParentValueExpression : IqlExpression
    {
        protected IqlParentValueExpression(IqlExpression parent,
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