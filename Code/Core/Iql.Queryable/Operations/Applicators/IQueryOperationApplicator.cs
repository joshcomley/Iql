namespace Iql.Queryable.Operations.Applicators
{
    public interface IQueryOperationApplicator<TOperation, TData, TQueryAdapter>
        : IQueryOperationApplicatorBase
        where TOperation : IQueryOperation
        where TData : IQueryResultBase
        where TQueryAdapter : IQueryableAdapterBase
    {
        void Apply<TEntity>(IQueryOperationContext<TOperation, TEntity, TData, TQueryAdapter> context) where TEntity : class;
    }
}