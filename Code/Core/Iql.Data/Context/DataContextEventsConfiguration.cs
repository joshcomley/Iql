using System;
using System.Collections.Generic;
using Iql.Data.Crud.Operations;

namespace Iql.Data.Context
{
    public class DataContextEventsConfiguration
    {
        private bool _getBeginListenersInitialized;
        private List<Action<IEntitySetCrudOperationBase>> _getBeginListeners;
        public List<Action<IEntitySetCrudOperationBase>> GetBeginListeners { get { if(!_getBeginListenersInitialized) { _getBeginListenersInitialized = true; _getBeginListeners = new List<Action<IEntitySetCrudOperationBase>>(); } return _getBeginListeners; } set { _getBeginListenersInitialized = true; _getBeginListeners = value; } }
    }
}