namespace Iql
{
    public abstract class IqlNonLiteralExpression : IqlReferenceExpression
    {
        protected IqlNonLiteralExpression(IqlExpressionKind kind,
            IqlType returnType)
            : base(kind, returnType)
        {
        }

        public override bool ContainsRootEntity()
        {
            return Parent.ContainsRootEntity();
        }
    }
}