using System;
using System.Linq.Expressions;
using Iql.Parsing;

namespace Iql.Queryable.Expressions.QueryExpressions
{
    public class ExpandQueryExpression<T, TTarget> : ExpressionQueryExpression<T, TTarget> where TTarget : class
    {
        public ExpandQueryExpression(
            Expression<Func<T, TTarget>> expression,
            Func<IQueryable<TTarget>> queryable = null,
            EvaluateContext evaluateContext = null)
            : base(expression, QueryExpressionType.NonBinary, evaluateContext)
        {
            Queryable = queryable;
        }

        public Func<IQueryable<TTarget>> Queryable { get; set; }
    }
}