using Iql.Data.Context;

namespace Iql.Data.Crud.Operations
{
    public interface ICrudOperation
    {
        OperationType Type { get; }
        IDataContext DataContext { get; }
    }
}