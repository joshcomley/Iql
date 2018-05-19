using System.Collections.Generic;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Validation;

namespace Iql.Queryable.Data.Crud
{
    public interface IEntityCrudResult : ICrudResult
    {
        OperationType Type { get; }
        object LocalEntity { get; }
        Dictionary<object, IEntityValidationResult> EntityValidationResults { get; set; }
        IEntityValidationResult RootEntityValidationResult { get; set; }
    }
}