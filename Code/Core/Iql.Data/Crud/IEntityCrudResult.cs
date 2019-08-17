using System;
using System.Collections.Generic;
using Iql.Data.Context;
using Iql.Data.Crud.Operations;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Entities.Validation.Validation;

namespace Iql.Data.Crud
{
    public interface IEntityCrudResult : ICrudResult
    {
        CompositeKey KeyBeforeSave { get; }
        IEntityStateBase EntityState { get; }
        IDataContext DataContext { get; }
        Type EntityType { get; }
        IqlOperationKind Kind { get; }
        object LocalEntity { get; }
        Dictionary<object, IEntityValidationResult> EntityValidationResults { get; set; }
        IEntityValidationResult RootEntityValidationResult { get; set; }
    }
}