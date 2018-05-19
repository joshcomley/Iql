using Iql.Data.Context;

namespace Iql.Data.Crud.Operations
{
    public class SaveChangesOperation : CrudOperation
    {
        public SaveChangesOperation(IDataContext dataContext) : base(OperationType.SaveChanges, dataContext)
        {
        }
    }
}