using Iql.Data.Context;

namespace Iql.Data.Crud.Operations
{
    public class CrudOperation : ICrudOperation
    {
        public CrudOperation(OperationType type, IDataContext dataContext)
        {
            Type = type;
            DataContext = dataContext;
        }

        public OperationType Type { get; }
        public IDataContext DataContext { get; }
    }
}