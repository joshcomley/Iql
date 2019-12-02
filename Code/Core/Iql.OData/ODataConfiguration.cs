using System;
using System.Collections.Generic;
using Iql.Data.Http;
using Iql.Entities;

namespace Iql.OData
{
    public class ODataConfiguration
    {
        public IEntityConfigurationBuilder Builder { get; }
        private bool _entitySetsDelayedInitialized;
        private Dictionary<Type, string> _entitySetsDelayed;
        private Dictionary<Type, string> _entitySets { get { if(!_entitySetsDelayedInitialized) { _entitySetsDelayedInitialized = true; _entitySetsDelayed = new Dictionary<Type, string>(); } return _entitySetsDelayed; } set { _entitySetsDelayedInitialized = true; _entitySetsDelayed = value; } }
        public IHttpProvider HttpProvider { get; set; }
        public Func<string> ApiUriBase { get; set; }

        public ODataConfiguration(IEntityConfigurationBuilder builder)
        {
            Builder = builder;
        }

        public void RegisterEntitySet<T>(string name)
        {
            _entitySets.Add(typeof(T), name);
        }

        public string GetEntitySetName<T>()
        {
            return GetEntitySetNameByType(typeof(T));
        }

        public string GetEntitySetNameByType(Type type)
        {
            return _entitySets.ContainsKey(type) ? _entitySets[type] : Builder.GetEntityByType(type)?.SetName;
        }

        public Type GetEntityTypeFromSetName(string entitySetName)
        {
            foreach (var entityType in _entitySets.Keys)
            {
                if (_entitySets[entityType] == entitySetName)
                {
                    return entityType;
                }
            }
            return null;
        }
    }
}