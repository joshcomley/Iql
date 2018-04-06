using System;
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

        Task<T> FirstAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<GetSingleResult<T>> FirstWithResponseAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<GetSingleResult<T>> FirstQueryAsync(WhereQueryExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<GetSingleResult<T>> FirstOrDefaultWithResponseAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<GetSingleResult<T>> FirstOrDefaultQueryAsync(WhereQueryExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<T> LastAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<GetSingleResult<T>> LastWithResponseAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<GetSingleResult<T>> LastQueryAsync(WhereQueryExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<GetSingleResult<T>> LastOrDefaultWithResponseAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<GetSingleResult<T>> LastOrDefaultQueryAsync(WhereQueryExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<T> SingleAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<GetSingleResult<T>> SingleWithResponseAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<GetSingleResult<T>> SingleQueryAsync(WhereQueryExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<GetSingleResult<T>> SingleOrDefaultWithResponseAsync(Expression<Func<T, bool>> expression = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<GetSingleResult<T>> SingleOrDefaultQueryAsync(WhereQueryExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        Task<SaveChangesResult> SaveChangesAsync(T entity);
        Task<DbList<T>> ToListAsync();
        Task<GetDataResult<T>> ToListWithResponseAsync();
        //UpdateEntityResult<T> Update(T entity);
        Task<T> WithKeyAsync(TKey key);
        Task<GetSingleResult<T>> WithKeyWithResponseAsync(TKey key);
    }
}