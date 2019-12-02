using System.Collections.Generic;

namespace Iql
{
    public abstract class IqlParameteredExpression<TParameter> : IqlParameteredExpressionBase
        where TParameter : IqlExpression
    {
        private List<TParameter> _parameters = null;
        public List<TParameter> Parameters { get => _parameters = _parameters ?? new List<TParameter>(); set => _parameters = value; }

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