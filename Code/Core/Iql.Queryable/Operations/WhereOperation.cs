using Iql.Parsing.Expressions.QueryExpressions;

namespace Iql.Queryable.Operations
{
    public class WhereOperation : ExpressionQueryOperation<IqlExpression, QueryExpression>
    {
        public WhereOperation(QueryExpression queryExpression = null) : base(queryExpression)
        {
#if TypeScript
            EvaluateContext = QueryExpression?.EvaluateContext;
#endif
        }
    }
}