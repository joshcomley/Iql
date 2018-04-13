namespace Iql
{
    public abstract class IqlAnyAllExpression : IqlParentValueExpression
    {
        protected IqlAnyAllExpression(
            IqlExpressionType type,
            IqlReferenceExpression parent,
            IqlExpression expression) : base(parent, expression, type, IqlType.Boolean)
        {
        }
    }
}