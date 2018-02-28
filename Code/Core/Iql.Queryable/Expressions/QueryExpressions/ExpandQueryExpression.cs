using System;
using System.Linq.Expressions;
#if TypeScript
using Iql.Parsing;
#endif

namespace Iql.Queryable.Expressions.QueryExpressions
{
    public class ExpandQueryExpression
        : ExpressionQueryExpression, IExpandQueryExpression
    {
        public ExpandQueryExpression(
            LambdaExpression expression,
            Func<IQueryableBase, IQueryableBase> queryable = null
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

        public Func<IQueryableBase, IQueryableBase> Queryable { get; set; }

        public Func<IQueryableBase, IQueryableBase> GetQueryable()
        {
            if (Queryable == null)
            {
                return q => q;
            }
            return q => Queryable(q);
        }
    }
}