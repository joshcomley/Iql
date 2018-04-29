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

        TQueryable Skip(int skip);

        TQueryable Take(int take);

        TQueryable Reverse();

        TQueryable Then(IQueryOperation operation);
    }
}