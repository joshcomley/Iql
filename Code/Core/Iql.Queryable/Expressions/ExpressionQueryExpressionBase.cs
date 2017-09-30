using System.Linq.Expressions;
using Iql.Parsing;
using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.Queryable.Expressions
{
    public abstract class ExpressionQueryExpressionBase : QueryExpression
    {
        protected ExpressionQueryExpressionBase(
            QueryExpressionType type
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
            : base(type
#if TypeScript
                  evaluateContext
#endif
                  )
        {
        }

        public abstract LambdaExpression GetExpression();
    }
}