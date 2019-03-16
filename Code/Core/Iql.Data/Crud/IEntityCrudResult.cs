using System;
using System.Collections.Generic;
using Iql.Data.Crud.Operations;
using Iql.Entities.Validation.Validation;

namespace Iql.Data.Crud
{
    public interface IEntityCrudResult : ICrudResult
    {
        Type EntityType { get; }
        IqlOperationKind Kind { get; }
        object LocalEntity { get; }
        Dictionary<object, IEntityValidationResult> EntityValidationResults { get; set; }
        IEntityValidationResult RootEntityValidationResult { get; set; }
    }
}