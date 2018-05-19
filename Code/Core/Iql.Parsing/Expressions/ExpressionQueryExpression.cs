using System.Linq.Expressions;
using Iql.Parsing.Expressions.QueryExpressions;

namespace Iql.Parsing.Expressions
{
    public class ExpressionQueryExpression : ExpressionQueryExpressionBase
    {
        public ExpressionQueryExpression(
            LambdaExpression expression,
            QueryExpressionKind kind
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            ) : base(kind
#if TypeScript
                  , evaluateContext
#endif
                )
        {
            Expression = expression;
        }

        public LambdaExpression Expression { get; }

        public override LambdaExpression GetExpression()
        {
            return Expression;
        }
    }
}