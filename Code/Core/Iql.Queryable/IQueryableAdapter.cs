using System;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.Queryable
{
    public interface IQueryableAdapter<TQueryResult> : IQueryableAdapterBase
        where TQueryResult : IQueryResultBase
    {
        //    generateQuery(): TQueryResult;

        TQueryResult ToQueryResult<TEntity>(IQueryable<TEntity> queryable, TQueryResult data) where TEntity : class;
        new TQueryResult NewQueryData<TEntity>(IQueryable<TEntity> queryable) where TEntity : class;


        void RegisterApplicator<TOperation>(Func<IQueryOperationApplicator<TOperation, TQueryResult>> resolve)
            where TOperation : IQueryOperation;
    }
}