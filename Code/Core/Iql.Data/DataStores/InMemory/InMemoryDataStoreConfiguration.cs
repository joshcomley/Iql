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
        private bool _sourcesDelayedInitialized;
        private Dictionary<string, LambdaExpression> _sourcesDelayed;
        private Dictionary<string, LambdaExpression> _sources { get { if(!_sourcesDelayedInitialized) { _sourcesDelayedInitialized = true; _sourcesDelayed = new Dictionary<string, LambdaExpression>(); } return _sourcesDelayed; } set { _sourcesDelayedInitialized = true; _sourcesDelayed = value; } }

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
                if (map != null && map.EntityConfiguration.Type.Name != name)
                {
                    return GetSourceByType(map.EntityConfiguration.Type);
                }
                throw new Exception($"Unable to find data source with type name \"{name}\"");
            }
            return _sources[name].Compile().DynamicInvoke() as IList;
        }

        public void Reset()
        {
            _sources.Clear();
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

        public Dictionary<IEntityConfiguration, IList> AllDataSourceMaps()
        {
            var list = new Dictionary<IEntityConfiguration, IList>();
            foreach (var source in _sources)
            {
                list.Add(Builder.GetEntityByTypeName(source.Key), (IList)source.Value.Compile().DynamicInvoke());
            }
            return list;
        }
    }
}