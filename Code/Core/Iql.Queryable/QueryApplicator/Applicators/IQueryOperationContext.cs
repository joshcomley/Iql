using Iql.Queryable.Data.Context;
using Iql.Queryable.Data.Queryable;
using Iql.Queryable.Operations;

namespace Iql.Queryable.QueryApplicator.Applicators
{
    public interface IQueryOperationContext<out TOperation, out TEntity, out TData, out TQueryableAdapter>
        : IQueryOperationContextBase
        where TOperation : IQueryOperation
        where TData : IQueryResultBase
        where TEntity : class
        where TQueryableAdapter : IQueryableAdapterBase
    {
        new TQueryableAdapter Adapter { get; }
        new TData Data { get; }
        new TOperation Operation { get; }
        new IQueryable<TEntity> Queryable { get; }
        new IDataContext DataContext { get; }
    }
}