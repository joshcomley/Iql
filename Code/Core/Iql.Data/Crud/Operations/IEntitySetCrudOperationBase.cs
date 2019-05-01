using System;
using Iql.Data.Tracking.State;
using Iql.Entities;

namespace Iql.Data.Crud.Operations
{
    //public interfacce IEntityS
    public interface IEntitySetCrudOperationBase : ICrudOperation
    {
        Type EntityType { get; set; }
        CompositeKey KeyBeforeSave { get; set; }
        IEntityStateBase EntityState { get; set; }
    }
}