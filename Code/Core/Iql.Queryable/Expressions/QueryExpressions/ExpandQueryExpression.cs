using System;
using System.Linq.Expressions;
using Iql.Parsing;

namespace Iql.Queryable.Expressions.QueryExpressions
{
    public class ExpandQueryExpression<T, TTarget> : ExpressionQueryExpression<T, TTarget> where TTarget : class
    {
        public ExpandQueryExpression(
            Expression<Func<T, TTarget>> expression,
            Func<IQueryable<TTarget>> queryable = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
            : base(expression, QueryExpressionType.NonBinary
#if TypeScript
                  evaluateContext
#endif
                  )
        {
            Queryable = queryable;
        }

        public Func<IQueryable<TTarget>> Queryable { get; set; }
    }
}