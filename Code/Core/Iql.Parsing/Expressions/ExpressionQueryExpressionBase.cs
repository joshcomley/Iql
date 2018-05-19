using System.Linq.Expressions;
using Iql.Parsing.Expressions.QueryExpressions;

namespace Iql.Parsing.Expressions
{
    public abstract class ExpressionQueryExpressionBase : QueryExpression
    {
        protected ExpressionQueryExpressionBase(
            QueryExpressionKind kind
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
            : base(kind
#if TypeScript
                  , evaluateContext
#endif
                  )
        {
        }

        public abstract LambdaExpression GetExpression();
    }
}