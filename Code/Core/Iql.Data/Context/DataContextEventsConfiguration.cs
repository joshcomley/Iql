using System;
using System.Collections.Generic;
using Iql.Data.Crud.Operations;

namespace Iql.Data.Context
{
    public class DataContextEventsConfiguration
    {
        private List<Action<IEntitySetCrudOperationBase>> _getBeginListeners = null;
        public List<Action<IEntitySetCrudOperationBase>> GetBeginListeners { get => _getBeginListeners = _getBeginListeners ?? new List<Action<IEntitySetCrudOperationBase>>(); set => _getBeginListeners = value; }
    }
}