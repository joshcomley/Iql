namespace Iql
{
    public abstract class IqlAnyAllExpression : IqlParentValueExpression
    {
        public string RootVariableName { get; set; }
        protected IqlAnyAllExpression(
            string rootVariableName,
            IqlExpressionKind kind,
            IqlReferenceExpression parent,
            IqlExpression expression) : base(parent, expression, kind, IqlType.Boolean)
        {
            RootVariableName = rootVariableName;
        }
    }
}