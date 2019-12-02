using System;
using System.Collections.Generic;

namespace Iql.Parsing
{
    public class RegistryStore<TInstance, TResolveTo>
        where TResolveTo : class
    {
        private bool _registryDelayedInitialized;
        private Dictionary<Type, Func<TResolveTo>> _registryDelayed;
        private Dictionary<Type, Func<TResolveTo>> _registry { get { if(!_registryDelayedInitialized) { _registryDelayedInitialized = true; _registryDelayed =             new Dictionary<Type, Func<TResolveTo>>(); } return _registryDelayed; } set { _registryDelayedInitialized = true; _registryDelayed = value; } }

        public void Register(Type type, Func<TResolveTo> reducerType)
        {
            _registry[type] = reducerType;
        }

        public TResolveTo Resolve(Type type)
        {
            while (true)
            {
                if (type == null)
                {
                    break;
                }

                if (_registry.ContainsKey(type))
                {
                    return _registry[type]();
                }

                type = type.BaseType;
            }

            return null;
        }
    }
}