namespace Iql.Parsing.Expressions.QueryExpressions
{
    public class OrQueryExpression : BinaryQueryExpression
    {
        public OrQueryExpression(
#if TypeScript
            EvaluateContext evaluateContext,
#endif
            QueryExpression left,
            params QueryExpression[] right) : base(
                QueryExpressionKind.Or,
#if TypeScript
                evaluateContext, 
#endif
                left, right)
        {
        }
    }
}