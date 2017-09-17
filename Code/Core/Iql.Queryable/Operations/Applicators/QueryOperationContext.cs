using Iql.Queryable.Data;

namespace Iql.Queryable.Operations.Applicators
{
    public class QueryOperationContext<
            TEntity,
            TQueryOperation,
            TQueryResult>
        : IQueryOperationContext<TQueryOperation, TEntity, TQueryResult>
        where TEntity : class
        where TQueryOperation : IQueryOperation
        where TQueryResult : IQueryResultBase
    {
        public QueryOperationContext(
            IDataContext dataContext,
            TQueryOperation operation,
            TQueryResult data,
            IQueryable<TEntity> queryable)
        {
            DataContext = dataContext;
            Operation = operation;
            Data = data;
            Queryable = queryable;
        }

        IQueryOperation IQueryOperationContextBase.Operation => Operation;
        IQueryableBase IQueryOperationContextBase.Queryable => Queryable;
        IQueryResultBase IQueryOperationContextBase.Data => Data;
        public IQueryable<TEntity> Queryable { get; set; }

        public TQueryResult Data { get; set; }

        public IDataContext DataContext { get; }
        public TQueryOperation Operation { get; set; }
    }
}