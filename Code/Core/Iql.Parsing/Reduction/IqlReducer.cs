using Iql.Parsing.Reduction.Reducers;

namespace Iql.Parsing.Reduction
{
    public class IqlReducer
    {
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
            _registry = registry ?? new IqlReducerRegistry();
        }

#if TypeScript
        public EvaluateContext EvaluateContext { get; }
#endif

        public T EvaluateAs<T>(IqlExpression expression)
        {
            return (T) Evaluate(expression)?.Value;
        }

        public IqlLiteralExpression Evaluate(IqlExpression expression)
        {
            var reducer = _registry.Resolve(expression);
            return reducer?.Evaluate(expression, this);
        }

        public IqlExpression ReduceStaticContent(IqlExpression expression)
        {
            if (expression != null && !expression.ContainsRootEntity())
            {
                var value = Evaluate(expression);
                return value ?? expression;
            }
            var reducer = _registry.Resolve(expression);
            return reducer != null ? reducer.ReduceStaticContent(expression, this) : expression;
        }
    }
}