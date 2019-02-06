using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Iql.Entities;

namespace Iql.Data.DataStores.InMemory
{
    public class InMemoryDataStoreConfiguration
    {
        public IEntityConfigurationBuilder Builder { get; }
        private readonly Dictionary<string, LambdaExpression> _sources = new Dictionary<string, LambdaExpression>();

        public InMemoryDataStoreConfiguration(IEntityConfigurationBuilder builder)
        {
            Builder = builder;
        }

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
                var map = Builder.GetSpecialTypeMap(name);
                if (map != null)
                {
                    return GetSourceByType(map.EntityConfiguration.Type);
                }
                throw new Exception($"Unable to find data source with name \"{name}\"");
            }
            return _sources[name].Compile().DynamicInvoke() as IList;
        }

        public IList[] AllDataSources()
        {
            var list = new List<IList>();
            foreach (var source in _sources)
            {
                list.Add((IList)source.Value.Compile().DynamicInvoke());
            }
            return list.ToArray();
        }
    }
}