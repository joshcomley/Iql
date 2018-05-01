using System.Collections.Generic;

namespace Iql.Parsing.Reduction
{
    public class IqlTraverser
    {
        private readonly IqlReducerRegistryBase _registry;
        internal List<IqlExpression> Expressions { get; set; } = new List<IqlExpression>();
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