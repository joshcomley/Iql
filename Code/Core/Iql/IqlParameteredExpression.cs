using System.Collections.Generic;

namespace Iql
{
    public abstract class IqlParameteredExpression<TParameter> : IqlParameteredExpressionBase
        where TParameter : IqlExpression
    {
        private bool _parametersInitialized;
        private List<TParameter> _parameters;
        public List<TParameter> Parameters { get { if(!_parametersInitialized) { _parametersInitialized = true; _parameters = new List<TParameter>(); } return _parameters; } set { _parametersInitialized = true; _parameters = value; } }

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