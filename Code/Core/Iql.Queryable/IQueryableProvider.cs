using System;
using System.Linq.Expressions;
using Iql.Parsing;
using Iql.Queryable.Expressions.QueryExpressions;
using Iql.Queryable.Operations;

namespace Iql.Queryable
{
    public interface IQueryableProvider<T, out TQueryable> : IQueryable<T>
        where T : class
        where TQueryable : IQueryable<T>
    {
        TQueryable Where(Expression<Func<T, bool>> expression, EvaluateContext evaluateContext = null);

        TQueryable WhereQuery(QueryExpression expression, EvaluateContext evaluateContext = null);
        //TQueryable WhereOperation(WhereOperation operation, EvaluateContext evaluateContext = null);

        //TQueryable OrderByOperation<TProperty>(OrderByOperation operation, EvaluateContext evaluateContext = null);

        TQueryable OrderBy<TProperty>(Expression<Func<T, TProperty>> expression,
            EvaluateContext evaluateContext = null);

        TQueryable OrderByQuery<TProperty>(PropertyQueryExpression<T, TProperty> expression,
            EvaluateContext evaluateContext = null);

        TQueryable OrderByDescending<TProperty>(Expression<Func<T, TProperty>> expression,
            EvaluateContext evaluateContext = null);

        TQueryable OrderByDescendingQuery<TProperty>(PropertyQueryExpression<T, TProperty> expression,
            EvaluateContext evaluateContext = null);

        TQueryable Expand<TTarget>(Expression<Func<T, TTarget>> property,
            Expression<Func<IQueryableProvider<TTarget, IQueryable<TTarget>>, bool>> filter = null)
            where TTarget : class;

        TQueryable ExpandQuery<TTarget>(ExpandQueryExpression<T, TTarget> expression,
            Expression<Func<IQueryableProvider<TTarget, IQueryable<TTarget>>, bool>> filter = null,
            EvaluateContext evaluateContext = null)
            where TTarget : class;
        //TQueryable ExpandOperation<TTarget>(ExpandOperation<T, TTarget> operatioin, EvaluateContext evaluateContext = null) where TTarget : class;

        TQueryable Reverse();

        TQueryable Then(IQueryOperation operation);
    }
}