using System;

namespace Iql.Queryable.Data.Crud.Operations
{
    //public interfacce IEntityS
    public interface IEntitySetCrudOperationBase : ICrudOperation
    {
        Type EntityType { get; set; }
    }
}