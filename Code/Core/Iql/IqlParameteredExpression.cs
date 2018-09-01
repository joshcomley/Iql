using System.Collections.Generic;

namespace Iql
{
    public abstract class IqlParameteredExpression<TParameter> : IqlParameteredExpressionBase
        where TParameter : IqlExpression
    {
        public List<TParameter> Parameters { get; set; } = new List<TParameter>();

        public override IEnumerable<IqlExpression> GetParameters()
        {
            return Parameters;
        }

        protected IqlParameteredExpression(IqlExpressionKind kind, IqlType? returnType, IqlExpression parent = null) :
            base(kind, returnType, parent)
        {
        }
    }
}