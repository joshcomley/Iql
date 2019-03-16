using Iql.Data.Context;

namespace Iql.Data.Crud.Operations
{
    public interface ICrudOperation
    {
        IqlOperationKind Kind { get; }
        IDataContext DataContext { get; }
    }
}