using System.Collections.Generic;
using Iql.Data.Crud.Operations;
using Iql.Entities.Validation.Validation;

namespace Iql.Data.Crud
{
    public class EntityCrudResult<T, TOperation> : CrudResult<T, TOperation>, IEntityCrudResult
        where TOperation : IEntitySetCrudOperationBase
    {
        public T LocalEntity { get; }
        public Dictionary<object, IEntityValidationResult> EntityValidationResults { get; set; } = new Dictionary<object, IEntityValidationResult>();
        public IqlOperationKind Kind => Operation.Kind;
        object IEntityCrudResult.LocalEntity => LocalEntity;
        public EntityValidationResult<T> RootEntityValidationResult { get; set; }

        IEntityValidationResult IEntityCrudResult.RootEntityValidationResult
        {
            get => RootEntityValidationResult;
            set => RootEntityValidationResult = (EntityValidationResult<T>) value;
        }

        public EntityCrudResult(T localEntity, bool success, TOperation operation) : base(success, operation)
        {
            LocalEntity = localEntity;
        }
    }
}