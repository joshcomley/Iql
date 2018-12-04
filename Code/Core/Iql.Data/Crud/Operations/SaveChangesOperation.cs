using Iql.Data.Context;

namespace Iql.Data.Crud.Operations
{
    public class SaveChangesOperation : CrudOperation
    {
        public object[] Entities { get; }

        public SaveChangesOperation(
            IDataContext dataContext,
            object[] entities = null) : base(OperationType.SaveChanges, dataContext)
        {
            Entities = entities;
        }
    }
}