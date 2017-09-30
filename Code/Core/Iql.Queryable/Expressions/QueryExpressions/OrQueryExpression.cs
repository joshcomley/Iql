using Iql.Parsing;

namespace Iql.Queryable.Expressions.QueryExpressions
{
    public class OrQueryExpression : BinaryQueryExpression
    {
        public OrQueryExpression(
#if TypeScript
            EvaluateContext evaluateContext,
#endif
            QueryExpression left,
            params QueryExpression[] right) : base(
                QueryExpressionType.Or,
#if TypeScript
                evaluateContext, 
#endif
                left, right)
        {
        }
    }
}