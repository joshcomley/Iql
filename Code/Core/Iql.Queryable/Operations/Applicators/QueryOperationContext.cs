using Iql.Queryable.Data;

namespace Iql.Queryable.Operations.Applicators
{
    public class QueryOperationContext<
            TEntity,
            TQueryOperation,
            TQueryResult,
            TQueryAdapter>
        : IQueryOperationContext<TQueryOperation, TEntity, TQueryResult, TQueryAdapter>
        where TEntity : class
        where TQueryOperation : IQueryOperation
        where TQueryResult : IQueryResultBase
        where TQueryAdapter : IQueryableAdapterBase
    {
        public QueryOperationContext(
            IDataContext dataContext,
            TQueryOperation operation,
            TQueryResult data,
            IQueryable<TEntity> queryable,
            TQueryAdapter adapter,
            //IQueryOperationContext<TQueryOperation, TEntity, TQueryResult, TQueryAdapter> parentContext
            IQueryOperationContextBase parentContext
            )
        {
            DataContext = dataContext;
            Operation = operation;
            Data = data;
            Queryable = queryable;
            Adapter = adapter;
            ParentContext = parentContext;
        }

        IQueryOperationContextBase IQueryOperationContextBase.ParentContext => ParentContext;
        IQueryableAdapterBase IQueryOperationContextBase.Adapter => Adapter;
        IQueryOperation IQueryOperationContextBase.Operation => Operation;
        IQueryableBase IQueryOperationContextBase.Queryable => Queryable;
        IQueryResultBase IQueryOperationContextBase.Data => Data;

        public TQueryAdapter Adapter { get; set; }
        public IQueryOperationContextBase ParentContext { get; }
        public IQueryable<TEntity> Queryable { get; set; }
        public TQueryResult Data { get; set; }
        public IDataContext DataContext { get; }
        public TQueryOperation Operation { get; set; }
    }
}