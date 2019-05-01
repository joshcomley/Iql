using Iql.Data.Context;
using Iql.Entities;

namespace Iql.Data.Crud.Operations
{
    public class SaveChangesOperation : CrudOperation, IGetChangesOperation
    {
        public object[] Entities { get; }
        public IProperty[] Properties { get; }

        public SaveChangesOperation(
            IDataContext dataContext,
            object[] entities = null,
            IProperty[] properties = null) : base(IqlOperationKind.SaveChanges, dataContext)
        {
            Entities = entities;
            Properties = properties;
        }
    }
}