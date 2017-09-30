using System;
using System.Collections.Generic;
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
        TQueryable Where(Expression<Func<T, bool>> expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            );

        TQueryable WhereQuery(QueryExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            );
        //TQueryable WhereOperation(WhereOperation operation, EvaluateContext evaluateContext = null);

        //TQueryable OrderByOperation<TProperty>(OrderByOperation operation, EvaluateContext evaluateContext = null);

        TQueryable OrderBy<TProperty>(Expression<Func<T, TProperty>> expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            );

        TQueryable OrderByQuery<TProperty>(PropertyQueryExpression<T, TProperty> expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            );

        TQueryable OrderByDescending<TProperty>(Expression<Func<T, TProperty>> expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            );

        TQueryable OrderByDescendingQuery<TProperty>(PropertyQueryExpression<T, TProperty> expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            );

        //TQueryable Expand<TTarget>(Expression<Func<T, TTarget>> property)
        //    where TTarget : class;

        //TQueryable ExpandQuery<TTarget>(
        //    ExpandQueryExpression<T, TTarget> expression,
        //    EvaluateContext evaluateContext = null)
        //    where TTarget : class;

        //TQueryable ExpandSingleWithQuery<TTarget>(
        //    Expression<Func<T, TTarget>> property,
        //    Expression<Func<IQueryableProvider<TTarget, IQueryableProvider<TTarget, IQueryable<TTarget>>>, IQueryableProvider<TTarget, IQueryableProvider<TTarget, IQueryable<TTarget>>>>> filter = null)
        //    where TTarget : class;

        //TQueryable ExpandQuerySingleWithQuery<TTarget>(
        //    ExpandQueryExpression<T, TTarget> expression,
        //    Expression<Func<IQueryableProvider<TTarget, IQueryableProvider<TTarget, IQueryable<TTarget>>>, IQueryableProvider<TTarget, IQueryableProvider<TTarget, IQueryable<TTarget>>>>> filter = null,
        //    EvaluateContext evaluateContext = null)
        //    where TTarget : class;

        //TQueryable ExpandCollectionWithQuery<TTarget>(
        //    Expression<Func<T, IEnumerable<TTarget>>> property,
        //    Expression<Func<IQueryableProvider<TTarget, IQueryableProvider<TTarget, IQueryable<TTarget>>>, IQueryableProvider<TTarget, IQueryableProvider<TTarget, IQueryable<TTarget>>>>> filter = null)
        //    where TTarget : class;

        //TQueryable ExpandQueryCollectionWithQuery<TTarget>(
        //    ExpandQueryExpression<T, IEnumerable<TTarget>> expression,
        //    Expression<Func<IQueryableProvider<TTarget, IQueryableProvider<TTarget, IQueryable<TTarget>>>, IQueryableProvider<TTarget, IQueryableProvider<TTarget, IQueryable<TTarget>>>>> filter = null,
        //    EvaluateContext evaluateContext = null)
        //    where TTarget : class;
        //TQueryable ExpandOperation<TTarget>(ExpandOperation<T, TTarget> operatioin, EvaluateContext evaluateContext = null) where TTarget : class;

        TQueryable Reverse();

        TQueryable Then(IQueryOperation operation);
    }
}