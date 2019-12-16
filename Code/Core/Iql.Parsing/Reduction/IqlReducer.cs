using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Parsing.Reduction.Reducers;

namespace Iql.Parsing.Reduction
{
    public class IqlReducer
    {
        private static IqlReducerRegistryBase _registryCore;
        private readonly IqlReducerRegistryBase _registry;

        public IqlReducer(
#if TypeScript || CustomEvaluate || true
            EvaluateContext evaluateContext = null, 
#endif
            IqlReducerRegistryBase registry = null)
        {
#if TypeScript || CustomEvaluate || true
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

#if TypeScript || CustomEvaluate || true
        public EvaluateContext EvaluateContext { get; }
#endif

        public T EvaluateAs<T>(IqlExpression expression)
        {
            return (T)Evaluate(expression)?.Value;
        }
        private bool _ancestorsInitialized;
        private List<IqlExpression> _ancestors;

        public List<IqlExpression> Ancestors { get { if(!_ancestorsInitialized) { _ancestorsInitialized = true; _ancestors = new List<IqlExpression>(); } return _ancestors; } set { _ancestorsInitialized = true; _ancestors = value; } }

        public IIqlLiteralExpression Evaluate(IqlExpression expression)
        {
            var reducer = _registry.Resolve(IqlExpression.ResolveExpressionType(expression));
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

            // TODO: Only update this when an IqlLambdaExpression is added or removed from the ancestors
            var ancestralParameters = new List<string>();
            var lambdaAncestors = Ancestors.Where(a => a is IqlLambdaExpression);
            foreach (var lambda in lambdaAncestors)
            {
                foreach (var parameter in (lambda as IqlLambdaExpression).Parameters)
                {
                    if (!ancestralParameters.Contains(parameter.VariableName))
                    {
                        ancestralParameters.Add(parameter.VariableName);
                    }
                }
            }

            if (!expression.IsOrHas(i => i is IqlRootReferenceExpression || i is IqlVariableExpression && ancestralParameters.Contains((i as IqlVariableExpression).VariableName)))
            {
                // We need this initial cast to object in TypeScript because for some reason
                // TypeScript won't let us cast an interface to another object
                var value = (object)Evaluate(expression);
                return (IqlExpression)value ?? expression;
            }
            Ancestors.Add(expression);
            var reducer = _registry.Resolve(IqlExpression.ResolveExpressionType(expression));
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