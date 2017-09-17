using Iql.Queryable.Data;

namespace Iql.Queryable.Operations.Applicators
{
    public interface IQueryOperationContextBase
    {
        IQueryResultBase Data { get; }
        IQueryOperation Operation { get; }
        IQueryableBase Queryable { get; }
        IDataContext DataContext { get; }
    }
}