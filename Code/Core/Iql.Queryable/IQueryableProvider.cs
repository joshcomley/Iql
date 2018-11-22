using System;
using System.Linq.Expressions;
using Iql.Entities;
using Iql.Parsing.Expressions.QueryExpressions;
#if TypeScript
using Iql.Parsing;
#endif

namespace Iql.Queryable
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

        TQueryable ApplyRelationshipFilters<TProperty>(IProperty relatedProperty, TProperty entity);

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