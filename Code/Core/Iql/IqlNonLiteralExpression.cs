namespace Iql
{
    public abstract class IqlNonLiteralExpression : IqlReferenceExpression
    {
        protected IqlNonLiteralExpression(IqlExpressionType type,
            IqlType returnType)
            : base(type, returnType)
        {
        }

        public override bool ContainsRootEntity()
        {
            return Parent.ContainsRootEntity();
        }
    }
}