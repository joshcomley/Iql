using Iql.Queryable.Data.Context;

namespace Iql.Queryable.Data.Crud.Operations
{
    public class SaveChangesOperation : CrudOperation
    {
        public SaveChangesOperation(IDataContext dataContext) : base(OperationType.SaveChanges, dataContext)
        {
        }
    }
}