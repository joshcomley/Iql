using System.Collections.Generic;

namespace Iql.Parsing.Reduction
{
    public class IqlTraverser
    {
        private readonly IqlReducerRegistryBase _registry;
        private List<IqlExpression> _expressions = null;
        internal List<IqlExpression> Expressions { get => _expressions = _expressions ?? new List<IqlExpression>(); set => _expressions = value; }
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