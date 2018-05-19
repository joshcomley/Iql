using System;
using System.Collections.Generic;
using Iql.Data.Crud.Operations;

namespace Iql.Data.Context
{
    public class DataContextEventsConfiguration
    {
        public List<Action<IEntitySetCrudOperationBase>> GetBeginListeners { get; set; } = new List<Action<IEntitySetCrudOperationBase>>();
    }
}