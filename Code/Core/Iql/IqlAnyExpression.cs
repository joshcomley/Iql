namespace Iql
{
    public class IqlAnyExpression : IqlAnyAllExpression
    {
        public IqlAnyExpression(
            string rootVariableName,
            IqlReferenceExpression parent,
            IqlExpression expression) : base(rootVariableName, IqlExpressionKind.Any, parent, expression)
        {
        }

        public IqlAnyExpression() : this(null, null, null)
        {
        }
    }
}