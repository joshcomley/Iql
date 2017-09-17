using Iql.Queryable.Data.Crud.Operations;

namespace Iql.Queryable.Data.Crud
{
    public class EntityCrudResult<T, TOperation> : CrudResult<T, TOperation>, IEntityCrudResult
        where TOperation : IEntitySetCrudOperationBase
    {
        public EntityCrudResult(bool success, TOperation operation) : base(success, operation)
        {
        }
    }
}