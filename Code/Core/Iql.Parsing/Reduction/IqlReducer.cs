using Iql.Parsing.Reduction.Reducers;

namespace Iql.Parsing.Reduction
{
    public class IqlReducer
    {
        private readonly IqlReducerRegistryBase _registry;

        public IqlReducer(EvaluateContext evaluateContext, IqlReducerRegistryBase registry = null)
        {
            EvaluateContext = evaluateContext;
            _registry = registry ?? new IqlReducerRegistry();
        }

        public EvaluateContext EvaluateContext { get; }

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