using System.Linq.Expressions;
using Iql.Parsing;
using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.Queryable.Expressions
{
    public abstract class ExpressionQueryExpressionBase : QueryExpression
    {
        public ExpressionQueryExpressionBase(
            QueryExpressionType type,
            EvaluateContext evaluateContext = null)
            : base(type, evaluateContext)
        {
        }

        public abstract LambdaExpression GetExpression();
    }
}