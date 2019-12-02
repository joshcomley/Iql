using System.Collections.Generic;

namespace Iql.Parsing.Reduction
{
    public class IqlTraverser
    {
        private readonly IqlReducerRegistryBase _registry;
        private bool _expressionsInitialized;
        private List<IqlExpression> _expressions;
        internal List<IqlExpression> Expressions { get { if(!_expressionsInitialized) { _expressionsInitialized = true; _expressions = new List<IqlExpression>(); } return _expressions; } set { _expressionsInitialized = true; _expressions = value; } }
        internal IqlTraverser(IqlReducerRegistryBase registry)
        {
            _registry = registry;
        }
        public void Traverse(IqlExpression expression)
        {
            if (expression == null)
            {
                return;
            }
            Expressions.Add(expression);
            var reducer = _registry.Resolve(IqlExpression.ResolveExpressionType(expression));
            reducer?.Traverse(expression, this);
        }
    }
}