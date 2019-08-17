using Iql.Data.Context;
using Iql.Data.Tracking.State;

namespace Iql.Data.Crud.Operations
{
    public class AddEntityOperation<T> : EntityCrudOperation<T>, IAddEntityOperation
        where T : class
    {
        public AddEntityOperation(IEntityState<T> entityState, IDataContext dataContext)
            : base(IqlOperationKind.Add, entityState, dataContext)
        {
        }
    }
}