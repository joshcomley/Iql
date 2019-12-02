using System.Collections.Generic;
using Iql.Data.Context;
using Iql.Data.Crud.Operations;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Entities.Validation.Validation;

namespace Iql.Data.Crud
{
    public class EntityCrudResult<T, TOperation> : CrudResult<T, TOperation>, IEntityCrudResult
        where TOperation : IEntityCrudOperationBase
        where T : class
    {
        public T LocalEntity { get; }
        public CompositeKey KeyBeforeSave { get; }
        public IEntityState<T> EntityState => (IEntityState<T>)Operation.EntityState;
        IEntityStateBase IEntityCrudResult.EntityState => EntityState;
        private Dictionary<object, IEntityValidationResult> _entityValidationResults = null;
        public Dictionary<object, IEntityValidationResult> EntityValidationResults { get => _entityValidationResults = _entityValidationResults ?? new Dictionary<object, IEntityValidationResult>(); set => _entityValidationResults = value; }
        public IDataContext DataContext { get; }
        public IqlOperationKind Kind => Operation.Kind;
        object IEntityCrudResult.LocalEntity => LocalEntity;
        public EntityValidationResult<T> RootEntityValidationResult { get; set; }

        IEntityValidationResult IEntityCrudResult.RootEntityValidationResult
        {
            get => RootEntityValidationResult;
            set => RootEntityValidationResult = (EntityValidationResult<T>)value;
        }

        public EntityCrudResult(T localEntity, bool success, TOperation operation) : base(success, operation)
        {
            LocalEntity = localEntity;
            DataContext = operation.DataContext;
            KeyBeforeSave = operation?.KeyBeforeSave;
        }
    }
}