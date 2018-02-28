using Iql.Queryable.Data.Context;
using Iql.Queryable.Data.Queryable;
using Iql.Queryable.Operations;

namespace Iql.Queryable.QueryApplicator.Applicators
{
    public interface IQueryOperationContextBase
    {
        IQueryOperationContextBase ParentContext { get; }
        IQueryableAdapterBase Adapter { get; }
        IQueryResultBase Data { get; }
        IQueryOperation Operation { get; }
        IQueryableBase Queryable { get; }
        IDataContext DataContext { get; }
    }
}