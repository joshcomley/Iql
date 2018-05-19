using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Iql.Data.DataStores.InMemory
{
    public class InMemoryDataStoreConfiguration
    {
        private readonly Dictionary<string, LambdaExpression> _sources = new Dictionary<string, LambdaExpression>();

        public void RegisterSource<T>(Expression<Func<IList<T>>> getter)
        {
            _sources[typeof(T).Name] = getter;
        }

        public List<T> GetSource<T>()
        {
            return GetSourceByType(typeof(T)) as List<T>;
        }

        public IList GetSourceByType(Type type)
        {
            return GetSourceByTypeName(type.Name);
        }

        public IList GetSourceByTypeName(string name)
        {
            if (!_sources.ContainsKey(name))
            {
                throw new Exception($"Unable to find data source with name \"{name}\"");
            }
            return _sources[name].Compile().DynamicInvoke() as IList;
        }
    }
}