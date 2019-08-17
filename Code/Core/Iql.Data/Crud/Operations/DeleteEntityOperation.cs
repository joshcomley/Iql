using Iql.Data.Context;
using Iql.Data.Tracking.State;
using Iql.Entities;

namespace Iql.Data.Crud.Operations
{
    public class DeleteEntityOperation<T> : EntityCrudOperation<T>, IDeleteEntityOperation
        where T : class
    {
        public CompositeKey Key { get; set; }
        public DeleteEntityOperation(CompositeKey key, IEntityState<T> entityState, IDataContext dataContext)
            : base(IqlOperationKind.Delete, entityState, dataContext)
        {
            Key = key;
        }

        IEntityStateBase IDeleteEntityOperation.EntityState => EntityState;
    }
}