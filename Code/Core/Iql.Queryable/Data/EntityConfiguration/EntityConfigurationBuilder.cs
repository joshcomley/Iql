using System;
using System.Collections.Generic;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public class EntityConfigurationBuilder
    {
        private readonly Dictionary<string, IEntityConfiguration> _entities =
            new Dictionary<string, IEntityConfiguration>();

        public EntityConfiguration<T> DefineEntity<T>() where T : class
        {
            var entityType = typeof(T);
            EntityConfiguration<T> entityConfiguration;
            var name = entityType.Name;
            if (_entities.ContainsKey(name))
            {
                entityConfiguration = _entities[name] as EntityConfiguration<T>;
            }
            else
            {
                entityConfiguration = new EntityConfiguration<T>(entityType, this);
                _entities[name] = entityConfiguration;
            }
            return entityConfiguration;
        }

        public EntityConfiguration<T> GetEntity<T>() where T : class
        {
            return GetEntityByName(typeof(T).Name) as EntityConfiguration<T>;
        }

        public IEntityConfiguration GetEntityByName(string typeName)
        {
            if (!_entities.ContainsKey(typeName))
            {
                throw new Exception($"No entity of type \"{typeName}\" has been configured for this context.");
            }
            return _entities[typeName];
        }

        public IEntityConfiguration GetEntityByType(Type type)
        {
            return GetEntityByName(type.Name);
        }
    }
}