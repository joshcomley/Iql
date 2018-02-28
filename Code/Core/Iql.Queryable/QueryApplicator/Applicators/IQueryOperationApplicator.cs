using Iql.Queryable.Data.Queryable;
using Iql.Queryable.Operations;

namespace Iql.Queryable.QueryApplicator.Applicators
{
    public interface IQueryOperationApplicator<in TOperation, in TData, in TQueryAdapter>
        : IQueryOperationApplicatorBase
        where TOperation : IQueryOperation
        where TData : IQueryResultBase
        where TQueryAdapter : IQueryableAdapterBase
    {
        void Apply<TEntity>(IQueryOperationContext<TOperation, TEntity, TData, TQueryAdapter> context) where TEntity : class;
    }
}