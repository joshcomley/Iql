namespace Iql
{
    public abstract class IqlReferenceExpression : IqlExpression
    {
        protected IqlReferenceExpression(IqlExpressionType type, IqlType returnType, IqlReferenceExpression parent = null) : base(type, returnType, parent)
        {
        }
    }
}