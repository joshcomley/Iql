namespace Iql
{
    public abstract class IqlParameteredLambdaExpression : IqlParameteredExpression<IqlRootReferenceExpression>
    {
        protected IqlParameteredLambdaExpression(IqlExpressionKind kind, IqlType? returnType, IqlExpression parent = null) : base(kind, returnType, parent)
        {
        }
    }
}