namespace Iql
{
    public abstract class IqlAnyAllExpression : IqlParentValueExpression
    {
        public string RootVariableName { get; set; }
        protected IqlAnyAllExpression(
            string rootVariableName,
            IqlExpressionType type,
            IqlReferenceExpression parent,
            IqlExpression expression) : base(parent, expression, type, IqlType.Boolean)
        {
            RootVariableName = rootVariableName;
        }
    }
}