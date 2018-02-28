using System;
using System.Linq.Expressions;
using Iql.Parsing;
using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.Queryable.Expressions
{
    public class ExpressionQueryExpression : ExpressionQueryExpressionBase
    {
        public ExpressionQueryExpression(
            LambdaExpression expression,
            QueryExpressionType type
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            ) : base(type
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