using Iql.Parsing;

namespace Iql.Queryable.Expressions.QueryExpressions
{
    public class OrQueryExpression : BinaryQueryExpression
    {
        public OrQueryExpression(
            EvaluateContext evaluateContext,
            QueryExpression left,
            params QueryExpression[] right) : base(QueryExpressionType.Or, evaluateContext, left, right)
        {
        }
    }
}