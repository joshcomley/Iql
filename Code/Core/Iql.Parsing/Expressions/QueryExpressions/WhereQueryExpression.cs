using System.Linq.Expressions;
using Iql.Parsing.Extensions;

namespace Iql.Parsing.Expressions.QueryExpressions
{
    public class WhereQueryExpression : ExpressionQueryExpression
    {
        public WhereQueryExpression(
            LambdaExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        ) : base(expression, QueryExpressionKind.Where
#if TypeScript
            , evaluateContext
#endif
            )
        {
        }
    }
}