namespace Iql
{
    public abstract class IqlFinalExpressionBase : IqlExpression
    {
        protected IqlFinalExpressionBase(IqlExpressionType type, IqlType? returnType, IqlExpression parent = null) : base(type, returnType, parent)
        {
        }
    }
}