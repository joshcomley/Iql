namespace Iql
{
    public class IqlAllExpression : IqlAnyAllExpression
    {
        public IqlAllExpression(
            IqlReferenceExpression parent,
            IqlExpression expression) : base(IqlExpressionType.All, parent, expression)
        {
        }

        public IqlAllExpression() : this(null, null)
        {
        }
    }
}