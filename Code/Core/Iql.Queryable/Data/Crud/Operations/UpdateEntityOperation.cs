using Iql.Queryable.Data.Crud.State;

namespace Iql.Queryable.Data.Crud.Operations
{
    public class UpdateEntityOperation<T> : EntityCrudOperation<T>, IUpdateEntityOperation
    {
        public IEntityStateBase EntityState { get; }

        public UpdateEntityOperation(T entity, IDataContext dataContext, IEntityStateBase entityState = null)
            : base(OperationType.Update, entity, dataContext)
        {
            EntityState = entityState?? dataContext.GetEntityState(entity
#if TypeScript
            , typeof(T)
#endif
                );
        }
    }
}