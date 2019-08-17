using System;

namespace Iql.Data.Crud.Operations
{
    public interface IEntitySetCrudOperationBase : ICrudOperation
    {
        Type EntityType { get; set; }
    }
}