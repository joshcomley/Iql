using System.Collections.Generic;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Validation;

namespace Iql.Queryable.Data.Crud
{
    public class EntityCrudResult<T, TOperation> : CrudResult<T, TOperation>, IEntityCrudResult
        where TOperation : IEntitySetCrudOperationBase
    {
        public T LocalEntity { get; }
        public OperationType Type => Operation.Type;
        object IEntityCrudResult.LocalEntity => LocalEntity;
        public EntityValidationResult EntityValidationResult { get; set; }

        public EntityCrudResult(T localEntity, bool success, TOperation operation) : base(success, operation)
        {
            LocalEntity = localEntity;
        }
    }
}