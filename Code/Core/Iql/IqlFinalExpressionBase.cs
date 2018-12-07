namespace Iql
{
    public abstract class IqlFinalExpressionBase : IqlExpression
    {
        protected IqlFinalExpressionBase(IqlExpressionKind kind, IqlType? returnType, IqlExpression parent = null) : base(kind, returnType, parent)
        {
        }

        public abstract object ResolveValue();
    }
}