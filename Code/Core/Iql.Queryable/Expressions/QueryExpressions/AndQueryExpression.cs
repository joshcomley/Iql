using Iql.Parsing;

namespace Iql.Queryable.Expressions.QueryExpressions
{
    public class AndQueryExpression : BinaryQueryExpression
    {
        public AndQueryExpression(
#if TypeScript
            EvaluateContext evaluateContext,
#endif
            QueryExpression left,
            params QueryExpression[] right)
            : base(QueryExpressionKind.And,
#if TypeScript
                evaluateContext, 
#endif
                  left, right)
        {
        }
    }
}