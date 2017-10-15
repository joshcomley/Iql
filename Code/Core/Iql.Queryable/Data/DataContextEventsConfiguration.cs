using System;
using System.Collections.Generic;
using Iql.Queryable.Data.Crud.Operations;

namespace Iql.Queryable.Data
{
    public class DataContextEventsConfiguration
    {
        public List<Action<IEntitySetCrudOperationBase>> GetBeginListeners { get; set; } = new List<Action<IEntitySetCrudOperationBase>>();
    }
}