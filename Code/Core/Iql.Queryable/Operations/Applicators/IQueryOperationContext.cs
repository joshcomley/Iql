using Iql.Queryable.Data;

namespace Iql.Queryable.Operations.Applicators
{
    public interface IQueryOperationContext<out TOperation, out TEntity, out TData>
        : IQueryOperationContextBase
        where TOperation : IQueryOperation
        where TData : IQueryResultBase
        where TEntity : class
    {
        new TData Data { get; }
        new TOperation Operation { get; }
        new IQueryable<TEntity> Queryable { get; }
        new IDataContext DataContext { get; }
    }
}