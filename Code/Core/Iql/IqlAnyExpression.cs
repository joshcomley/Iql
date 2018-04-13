namespace Iql
{
    public class IqlAnyExpression : IqlAnyAllExpression
    {
        public IqlAnyExpression(
            IqlReferenceExpression parent,
            IqlExpression expression) : base(IqlExpressionType.Any, parent, expression)
        {
        }

        public IqlAnyExpression() : this(null, null)
        {
        }
    }
}