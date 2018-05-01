using System;
using System.Collections.Generic;

namespace Iql.Parsing
{
    public class RegistryStore<TInstance, TResolveTo>
        where TResolveTo : class
    {
        private readonly Dictionary<Type, Func<TResolveTo>> _registry =
            new Dictionary<Type, Func<TResolveTo>>();

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