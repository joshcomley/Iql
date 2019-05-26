namespace Iql
{
    public abstract class IqlAnyAllExpression : IqlParentValueLambdaExpression
    {
        protected IqlAnyAllExpression(
            string rootVariableName,
            IqlExpressionKind kind,
            IqlReferenceExpression parent,
            IqlLambdaExpression expression) : base(parent, expression, kind, IqlType.Boolean)
        {
            RootVariableName = rootVariableName;
        }
    }
}