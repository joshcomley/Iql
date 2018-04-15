using System;
using System.Collections.Generic;
using Iql.Parsing.Reduction.Reducers;

namespace Iql.Parsing.Reduction
{
    public class IqlReducer
    {
        private static IqlReducerRegistryBase _registryCore;
        private readonly IqlReducerRegistryBase _registry;

        public IqlReducer(
#if TypeScript
            EvaluateContext evaluateContext = null, 
#endif
            IqlReducerRegistryBase registry = null)
        {
#if TypeScript
            EvaluateContext = evaluateContext;
#endif
            if (registry == null)
            {
                if (_registryCore == null)
                {
                    _registryCore = new IqlReducerRegistry();
                }
                _registry = _registryCore;
            }
            else
            {
                _registry = registry;
            }
        }

#if TypeScript
        public EvaluateContext EvaluateContext { get; }
#endif

        public T EvaluateAs<T>(IqlExpression expression)
        {
            return (T)Evaluate(expression)?.Value;
        }

        public List<IqlExpression> Ancestors { get; set; } = new List<IqlExpression>();

        public IIqlLiteralExpression Evaluate(IqlExpression expression)
        {
            var reducer = _registry.Resolve(expression);
            Ancestors.Add(expression);
            var result = reducer?.Evaluate(expression, this);
            Ancestors.RemoveAt(Ancestors.Count - 1);
            return result;
        }

        public IqlExpression[] Traverse(IqlExpression expression)
        {
            var traverser = new IqlTraverser(_registry);
            traverser.Traverse(expression);
            return traverser.Expressions.ToArray();
        }

        public IqlExpression ReduceStaticContent(IqlExpression expression)
        {
            if (expression == null)
            {
                return null;
            }
            if (!expression.ContainsRootEntity())
            {
                // We need this initial cast to object in TypeScript because for some reason
                // TypeScript won't let us cast an interface to another object
                var value = (object)Evaluate(expression);
                return (IqlExpression)value ?? expression;
            }
            Ancestors.Add(expression);
            var reducer = _registry.Resolve(expression);
            var result = reducer != null ? reducer.ReduceStaticContent(expression, this) : expression;
            Ancestors.RemoveAt(Ancestors.Count - 1);
            return result;
        }

        public bool HasAncestorOfType<T>()
        {
            foreach (var ancestor in Ancestors)
            {
                if (ancestor is T)
                {
                    return true;
                }
            }
            return false;
        }
    }
}