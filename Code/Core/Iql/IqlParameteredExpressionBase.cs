using System.Collections.Generic;

namespace Iql
{
    public abstract class IqlParameteredExpressionBase : IqlExpression
    {
        public abstract IEnumerable<IqlExpression> GetParameters();

        protected IqlParameteredExpressionBase(IqlExpressionKind kind, IqlType? returnType, IqlExpression parent = null) :
            base(kind, returnType, parent)
        {
        }
    }
}