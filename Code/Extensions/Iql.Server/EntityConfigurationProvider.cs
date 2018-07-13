using System;
using System.Collections.Generic;
using Iql.Entities;

namespace Iql.Server
{
    internal class EntityConfigurationProvider : IEntityConfigurationProvider
    {
        private Dictionary<string, IEntityConfigurationBuilder> ConfigurationBuilders { get; set; }
            = new Dictionary<string, IEntityConfigurationBuilder>();
        internal void Set(string key, IEntityConfigurationBuilder builder)
        {
            if (ConfigurationBuilders.ContainsKey(key))
            {
                ConfigurationBuilders[key] = builder;
            }
            else
            {
                ConfigurationBuilders.Add(key, builder);
            }
        }
        public IEntityConfigurationBuilder Get(string key)
        {
            if (ConfigurationBuilders.ContainsKey(key))
            {
                return ConfigurationBuilders[key];
            }

            return null;
        }

        public IEntityConfigurationBuilder Get(Type serviceType)
        {
            return Get(serviceType.FullName);
        }

        public IEntityConfigurationBuilder Get<T>()
        {
            return Get(typeof(T));
        }
    }
}