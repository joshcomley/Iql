using System.Collections.Generic;
using Iql.Data.Configuration.Validation.Validation;
using Iql.Data.Crud.Operations;

namespace Iql.Data.Crud
{
    public interface IEntityCrudResult : ICrudResult
    {
        OperationType Type { get; }
        object LocalEntity { get; }
        Dictionary<object, IEntityValidationResult> EntityValidationResults { get; set; }
        IEntityValidationResult RootEntityValidationResult { get; set; }
    }
}