using Iql.Data.Context;
using Iql.Data.Crud.Operations.Results;
using Iql.Entities;

namespace Iql.Data.Crud.Operations
{
    public class SaveChangesOperation : CrudOperation, IGetChangesOperation
    {
        private IOperationEvents<SaveChangesOperation, SaveChangesResult> _events;

        public SaveChangesOperation(
            IDataContext dataContext,
            object[] entities = null,
            IProperty[] properties = null) : base(IqlOperationKind.SaveChanges, dataContext)
        {
            Entities = entities;
            Properties = properties;
        }

        public IOperationEvents<SaveChangesOperation, SaveChangesResult> Events =>
            _events = _events ?? new OperationEvents<SaveChangesOperation, SaveChangesResult>();

        public SaveChangesResult Result { get; private set; }

        public object[] Entities { get; }
        public IProperty[] Properties { get; }

        public SaveChangesOperation SetResult(SaveChangesResult result)
        {
            Result = result;
            return this;
        }
    }
}