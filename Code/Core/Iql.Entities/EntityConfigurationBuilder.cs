using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iql.Data.Types;
using Iql.Entities.Enums;
using Iql.Entities.Extensions;
using Iql.Entities.Functions;
using Iql.Entities.Relationships;
using Iql.Entities.Services;
using Iql.Entities.SpecialTypes;

namespace Iql.Entities
{
    public class EntityConfigurationBuilder : MetadataBase, IEntityConfigurationBuilder
    {
        private UserPermissionsManager _permissionsManager;

        private readonly List<IqlUserPermissionRule> _permissionRules =
            new List<IqlUserPermissionRule>();
        public List<IqlUserPermissionRule> PermissionRules =>
            _permissionRules.EnsureHasBuilder(this);
        public UserPermissionsManager PermissionManager =>
            _permissionsManager = _permissionsManager ?? new UserPermissionsManager(
                               this,
                               this);

        private readonly Dictionary<Type, IEntityConfiguration> _entities =
            new Dictionary<Type, IEntityConfiguration>();
        private readonly Dictionary<string, IEntityConfiguration> _entitiesByTypeName =
            new Dictionary<string, IEntityConfiguration>();

        private static readonly List<EntityConfigurationBuilder> EntityConfigurationBuilders =
            new List<EntityConfigurationBuilder>();

        public EntityConfigurationBuilder()
        {
            EntityConfigurationBuilders.Add(this);
            InternalTypeResolver = new EntityConfigurationTypeResolver(this);
        }

        public TypeResolver InternalTypeResolver { get; }

        public static IEntityConfiguration FindConfigurationForEntityTypeName(string entityTypeName)
        {
            for (var i = 0; i < EntityConfigurationBuilders.Count; i++)
            {
                var builder = EntityConfigurationBuilders[i];
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

        public IEntityConfiguration GetEntityByTypeName(string typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName))
            {
                return null;
            }
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
                //var existingType = EntityConfigurationBuilder.FindConfigurationForEntityType(typeof(T));
                //if (existingType != null && existingType.Builder != this)
                //{
                //    throw new Exception("Attempting to assign an entity type to multiple configurations.");
                //}
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

        public IEnumerable<IqlMethod> AllMethods()
        {
            return Methods;
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

                return null;
            }
            return _entities[type];
        }

        public IqlServiceProvider ServiceProvider { get; } = new IqlServiceProvider();
        public List<IqlMethod> Methods { get; set; } = new List<IqlMethod>();
        public IEnumerable<IEnumConfiguration> EnumTypes => AllEnumTypes();
        public IEnumerable<IEntityConfiguration> EntityTypes => AllEntityTypes();
        public IEnumerable<IRelationship> Relationships => AllRelationships();
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

        public IIqlTypeMetadata FindType<T>()
        {
            return InternalTypeResolver.FindType<T>();
        }

        public IIqlTypeMetadata FindTypeByType(Type type)
        {
            return InternalTypeResolver.FindTypeByType(type);
        }

        public IIqlTypeMetadata ResolveTypeFromTypeName(string typeName)
        {
            return InternalTypeResolver.ResolveTypeFromTypeName(typeName);
        }

        public IIqlTypeMetadata GetTypeMap(IIqlTypeMetadata type)
        {
            var entityByType = GetEntityByType(type.Type);
            if (entityByType.SpecialTypeDefinition != null &&
                entityByType.SpecialTypeDefinition.InternalType == type.Type)
            {
                return InternalTypeResolver.FindTypeByType(
                    entityByType.SpecialTypeDefinition.EntityConfiguration.Type);
            }

            return null;
        }
    }
}