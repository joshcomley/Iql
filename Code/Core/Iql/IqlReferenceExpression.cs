namespace Iql
{
    public abstract class IqlReferenceExpression : IqlExpression
    {
        protected IqlReferenceExpression(IqlExpressionType type, IqlType returnType) : base(type, returnType)
        {
        }
    }
}