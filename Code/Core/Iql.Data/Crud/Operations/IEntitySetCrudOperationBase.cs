using System;

namespace Iql.Data.Crud.Operations
{
    //public interfacce IEntityS
    public interface IEntitySetCrudOperationBase : ICrudOperation
    {
        Type EntityType { get; set; }
    }
}