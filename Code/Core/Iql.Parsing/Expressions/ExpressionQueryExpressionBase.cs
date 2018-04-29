using System.Linq.Expressions;
using Iql.Parsing;
using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.Queryable.Expressions
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