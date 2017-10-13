using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Parsing;
using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.Queryable
{
    public interface IDbSetOperations<T, in TKey> where T : class
    {
        AddEntityResult<T> Add(T entity);
        DeleteEntityResult<T> Delete(T entity);

        Task<GetSingleResult<T>> First(Expression<Func<T, bool>> expression = null,
            EvaluateContext evaluateContext = null);

        Task<GetSingleResult<T>> FirstQuery(WhereQueryExpression<T> expression, EvaluateContext evaluateContext = null);

        Task<GetSingleResult<T>> FirstOrDefault(Expression<Func<T, bool>> expression = null,
            EvaluateContext evaluateContext = null);

        Task<GetSingleResult<T>> FirstOrDefaultQuery(WhereQueryExpression<T> expression,
            EvaluateContext evaluateContext = null);

        Task<GetSingleResult<T>> Single(Expression<Func<T, bool>> expression = null,
            EvaluateContext evaluateContext = null);

        Task<GetSingleResult<T>> SingleQuery(WhereQueryExpression<T> expression,
            EvaluateContext evaluateContext = null);

        Task<GetSingleResult<T>> SingleOrDefault(Expression<Func<T, bool>> expression = null,
            EvaluateContext evaluateContext = null);

        Task<GetSingleResult<T>> SingleOrDefaultQuery(WhereQueryExpression<T> expression,
            EvaluateContext evaluateContext = null);

        Task<SaveChangesResult> SaveChanges(T entity);
        Task<DbList<T>> ToList();
        Task<GetDataResult<T>> ToListWithResponse();
        UpdateEntityResult<T> Update(T entity);
        Task<T> WithKey(TKey key);
        Task<GetSingleResult<T>> WithKeyWithResponse(TKey key);
    }
}