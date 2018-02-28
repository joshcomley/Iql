using System;
using System.Collections.Generic;
using Iql.Queryable.Data.Http;

namespace Iql.OData.Data
{
    public class ODataConfiguration
    {
        private readonly Dictionary<Type, string> _entitySets = new Dictionary<Type, string>();
        public IHttpProvider HttpProvider { get; set; }
        public string ApiUriBase { get; set; }

        public void RegisterEntitySet<T>(string name)
        {
            _entitySets.Add(typeof(T), name);
        }

        public string GetEntitySetName<T>()
        {
            return _entitySets[typeof(T)];
        }

        public string GetEntitySetNameByType(Type type)
        {
            return _entitySets[type];
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