using System;
using System.Linq.Expressions;
#if TypeScript
using Iql.Parsing;
#endif
using Iql.Queryable.Expressions.QueryExpressions;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.Queryable
{
    public interface IQueryableProvider<T, out TQueryable> : IQueryable<T>
        where T : class
        where TQueryable : IQueryableProvider<T, TQueryable>
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

        TQueryable OrderByQuery(PropertyQueryExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            );

        TQueryable OrderByDescending<TProperty>(Expression<Func<T, TProperty>> expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            );

        TQueryable OrderByDescendingQuery(PropertyQueryExpression expression
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

        TQueryable Skip(int skip);

        TQueryable Take(int take);

        TQueryable Reverse();

        TQueryable Then(IQueryOperation operation);
    }
}