﻿using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Data.Tracking.State;
using Iql.Queryable.Expressions.QueryExpressions;

#if TypeScript
using Iql.Parsing;
#endif

namespace Iql.Queryable.Data.Lists
{
    public interface IDbSetOperations<T, in TKey> where T : class
    {
        EntityState<T> Add(T entity);
        EntityState<T> Delete(T entity);

        Task<T> First(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<GetSingleResult<T>> FirstWithResponse(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<GetSingleResult<T>> FirstQuery(WhereQueryExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<T> FirstOrDefault(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<GetSingleResult<T>> FirstOrDefaultWithResponse(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<GetSingleResult<T>> FirstOrDefaultQuery(WhereQueryExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<T> Last(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<GetSingleResult<T>> LastWithResponse(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<GetSingleResult<T>> LastQuery(WhereQueryExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<T> LastOrDefault(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<GetSingleResult<T>> LastOrDefaultWithResponse(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<GetSingleResult<T>> LastOrDefaultQuery(WhereQueryExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<T> Single(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<GetSingleResult<T>> SingleWithResponse(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<GetSingleResult<T>> SingleQuery(WhereQueryExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<T> SingleOrDefault(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<GetSingleResult<T>> SingleOrDefaultWithResponse(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<GetSingleResult<T>> SingleOrDefaultQuery(WhereQueryExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<SaveChangesResult> SaveChanges(T entity);
        Task<DbList<T>> ToList();
        Task<GetDataResult<T>> ToListWithResponse();
        //UpdateEntityResult<T> Update(T entity);
        Task<T> WithKey(TKey key);
        Task<GetSingleResult<T>> WithKeyWithResponse(TKey key);
    }
}