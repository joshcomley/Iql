using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iql.Entities.Enums;
using Iql.Entities.Extensions;
using Iql.Entities.Relationships;
using Iql.Entities.Services;
using Iql.Entities.SpecialTypes;

namespace Iql.Entities
{
    public class EntityConfigurationBuilder : IEntityConfigurationBuilder
    {
        private readonly Dictionary<Type, IEntityConfiguration> _entities =
            new Dictionary<Type, IEntityConfiguration>();
        private readonly Dictionary<string, IEntityConfiguration> _entitiesByTypeName =
            new Dictionary<string, IEntityConfiguration>();

        private static readonly List<EntityConfigurationBuilder> EntityConfigurationBuilders =
            new List<EntityConfigurationBuilder>();

        public EntityConfigurationBuilder()
        {
            EntityConfigurationBuilders.Add(this);
        }

        public static Type GetEntityTypeFromName(string entityTypeName)
        {
            return FindConfigurationForEntityTypeName(entityTypeName)?.Type;
        }

        public static IEntityConfiguration FindConfigurationForEntityTypeName(string entityTypeName)
        {
            foreach (var builder in EntityConfigurationBuilders)
            {
                foreach (var config in builder._entities)
                {
                    if (config.Key.Name == entityTypeName)
                    {
                        return config.Value;
                    }
                }
            }

            return null;
        }

        public static IEntityConfiguration FindConfigurationForEntity(object entity)
        {
            return entity == null 
                ? null 
                : FindConfigurationForEntityType(entity.GetType());
        }

        public static IEntityConfiguration FindConfigurationForEntityType(Type entityType)
        {
            foreach (var builder in EntityConfigurationBuilders)
            {
                foreach (var config in builder._entities)
                {
                    if (config.Key == entityType)
                    {
                        return config.Value;
                    }
                }
            }

            return null;
        }

        public static EntityConfiguration<T> FindConfigurationForEntityTypeTyped<T>()
            where T : class
        {
            return (EntityConfiguration<T>) FindConfigurationForEntityType(typeof(T));
        }

        public static EntityConfigurationBuilder FindConfigurationBuilderForEntityType(Type entityType)
        {
            var config = FindConfigurationForEntityType(entityType);
            return config?.Builder;
        }

        public IEntityConfiguration GetEntityByTypeName(string typeName)
        {
            if (_entitiesByTypeName.ContainsKey(typeName))
            {
                return _entitiesByTypeName[typeName];
            }
            foreach (var entityConfiguration in _entities)
            {
                var type = entityConfiguration.Value.Type;
                while (type != null && type != typeof(object))
                {
                    if (entityConfiguration.Key.Name == typeName)
                    {
                        return entityConfiguration.Value;
                    }
                    type = type.BaseType;
                }
            }
            return null;
        }

        public bool IsEntityType(Type type)
        {
            return _entities.ContainsKey(type);
        }

        private readonly Dictionary<string, IEnumConfiguration> _enumTypes = new Dictionary<string, IEnumConfiguration>();
        public IEnumerable<IEnumConfiguration> AllEnumTypes()
        {
            return _enumTypes.Values.ToArray();
        }

        public IEnumerable<IRelationship> AllRelationships()
        {
            var relationships = new List<IRelationship>();
            var entityTypes = AllEntityTypes().ToArray();
            for (var i = 0; i < entityTypes.Length; i++)
            {
                var entityType = entityTypes[i];
                relationships.AddRange(entityType.Relationships);
            }
            return relationships.Distinct().ToArray();
        }

        public void ForEntityTypes(Func<IEntityConfiguration, bool> filter, Action<IEntityConfiguration> action)
        {
            var all = AllEntityTypes().ToList();
            foreach (var config in all)
            {
                if (filter(config))
                {
                    action(config);
                }
            }
        }

        public EntityConfiguration<T> EntityType<T>() where T : class
        {
            var entityType = typeof(T);
            EntityConfiguration<T> entityConfiguration;
            if (_entities.ContainsKey(entityType))
            {
                entityConfiguration = _entities[entityType] as EntityConfiguration<T>;
            }
            else
            {
                entityConfiguration = new EntityConfiguration<T>(this);
                CacheEntityConfigurationByType(entityType, entityConfiguration);
            }
            return entityConfiguration;
        }

        private void CacheEntityConfigurationByType(Type entityType, IEntityConfiguration entityConfiguration)
        {
            if (!_entities.ContainsKey(entityType))
            {
                _entities.Add(entityType, entityConfiguration);
            }
            if (!_entitiesByTypeName.ContainsKey(entityType.Name))
            {
                _entitiesByTypeName.Add(entityType.Name, entityConfiguration);
            }
        }

        public IEnumConfiguration EnumType<T>()
        {
            var name = typeof(T).Name;
            if (!_enumTypes.ContainsKey(name))
            {
                _enumTypes.Add(name, new EnumConfiguration(name));
            }
            return _enumTypes[name];
        }

        public IEnumerable<IEntityConfiguration> AllEntityTypes()
        {
            return _entities.Values;
        }

        public IEntityConfiguration GetEntityByType(Type type)
        {
            if (!_entities.ContainsKey(type))
            {
                foreach (var entityConfiguration in _entities)
                {
                    if (entityConfiguration.Key.IsAssignableFrom(type))
                    {
                        return entityConfiguration.Value;
                    }
                }
            }
            return _entities[type];
        }

        public IqlServiceProvider ServiceProvider { get; } = new IqlServiceProvider();

        public bool ValidateInferredWithClientSide { get; set; }

        public SpecialTypeDefinition GetSpecialTypeMap(string name)
        {
            return GetEntityByTypeName(name).SpecialTypeDefinition;
        }

        public SpecialTypeDefinition UsersDefinition { get; set; }
        public SpecialTypeDefinition UserSettingsDefinition { get; set; }
        public SpecialTypeDefinition CustomReportsDefinition { get; set; }

        public bool IsEntityType<T>()
        {
            return IsEntityTypeByType(typeof(T));
        }

        public bool IsEntityTypeByType(Type type)
        {
            return GetEntityByType(type) != null;
        }

        /// <summary>
        /// Flattens an object graph, producing a list of distinctive entities contained within
        /// </summary>
        /// <param name="entity">The entity to flatten</param>
        /// <param name="entityType">The type of the entity to flatten</param>
        /// <returns></returns>
        public Dictionary<Type, IList> FlattenObjectGraph(object entity, Type entityType)
        {
            return GraphFlattener.FlattenObjectGraphInternal(this, entity, entityType);
        }

        public Dictionary<Type, IList> FlattenObjectGraphs(Type entityType, IEnumerable entities)
        {
            return GraphFlattener.FlattenObjectGraphsInternal(this, entityType, entities);
        }

        //public Dictionary<Type, IList> FlattenDependencyGraph(object entity, Type entityType)
        //{
        //    return this.FlattenDependencyGraphInternal(entity, entityType);
        //}

        //public Dictionary<Type, IList> FlattenDependencyGraphs(Type entityType, IEnumerable entities)
        //{
        //    return this.FlattenDependencyGraphsInternal(entityType, entities);
        //}
    }
}