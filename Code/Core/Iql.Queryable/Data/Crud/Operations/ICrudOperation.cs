using Iql.Queryable.Data.Context;

namespace Iql.Queryable.Data.Crud.Operations
{
    public interface ICrudOperation
    {
        OperationType Type { get; }
        IDataContext DataContext { get; }
    }
}