using System;
using System.Collections.Generic;

namespace Iql.Parsing
{
    public class RegistryStore<TInstance, TResolveTo>
        where TResolveTo : class
    {
        private readonly Dictionary<string, Func<TResolveTo>> _registry =
            new Dictionary<string, Func<TResolveTo>>();

        public void Register(Type type, Func<TResolveTo> reducerType)
        {
            _registry[type.Name] = reducerType;
        }

        public TResolveTo Resolve(TInstance expression)
        {
            var type = expression.GetType();
            while (true)
            {
                if (type == null)
                {
                    break;
                }
                if (_registry.ContainsKey(type.Name))
                {
                    return _registry[type.Name]();
                }
                type = type.BaseType;
            }
            return null;
        }
    }
}