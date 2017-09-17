using Iql.Parsing;

namespace Iql.Queryable.Expressions.QueryExpressions
{
    public class AndQueryExpression : BinaryQueryExpression
    {
        public AndQueryExpression(
            EvaluateContext evaluateContext,
            QueryExpression left,
            params QueryExpression[] right)
            : base(QueryExpressionType.And, evaluateContext, left, right)
        {
        }
    }
}