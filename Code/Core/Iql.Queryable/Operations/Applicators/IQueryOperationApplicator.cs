namespace Iql.Queryable.Operations.Applicators
{
    public interface IQueryOperationApplicator<TOperation, TData>
        : IQueryOperationApplicatorBase
        where TOperation : IQueryOperation
        where TData : IQueryResultBase
    {
        void Apply<TEntity>(IQueryOperationContext<TOperation, TEntity, TData> context) where TEntity : class;
    }
}