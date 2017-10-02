using System;
using System.Linq.Expressions;
using Iql.Parsing;

namespace Iql.Queryable.Expressions.QueryExpressions
{
    public class ExpandQueryExpression<T, TTarget, TTargetElement> 
        : ExpressionQueryExpression<T, TTarget>, IExpandQueryExpression
        where TTarget : class
    {
        public ExpandQueryExpression(
            Expression<Func<T, TTarget>> expression,
            Func<IQueryable<TTargetElement>, IQueryable<TTargetElement>> queryable = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
            : base(expression, QueryExpressionType.NonBinary
#if TypeScript
                  , evaluateContext
#endif
                  )
        {
            Queryable = queryable;
        }

        public Func<IQueryable<TTargetElement>, IQueryable<TTargetElement>> Queryable { get; set; }

        public Func<IQueryableBase, IQueryableBase> GetQueryable()
        {
            if (Queryable == null)
            {
                return q => q;
            }
            return q => Queryable((IQueryable<TTargetElement>) q);
        }
    }
}