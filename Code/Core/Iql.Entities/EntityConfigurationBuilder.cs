using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iql.Data.Types;
using Iql.Entities.Enums;
using Iql.Entities.Extensions;
using Iql.Entities.Functions;
using Iql.Entities.PropertyGroups.Files;
using Iql.Entities.Relationships;
using Iql.Entities.Services;
using Iql.Entities.SpecialTypes;
using Iql.Events;
using Iql.Extensions;

namespace Iql.Entities
{
    public class EntityConfigurationBuilder : MetadataBase, IEntityConfigurationBuilder
    {
        private bool _guidManagerDelayedInitialized;
        private GuidManager _guidManagerDelayed;
        private GuidManager _guidManager { get { if(!_guidManagerDelayedInitialized) { _guidManagerDelayedInitialized = true; _guidManagerDelayed = new GuidManager(); } return _guidManagerDelayed; } set { _guidManagerDelayedInitialized = true; _guidManagerDelayed = value; } }
        public GuidManager GuidManager => _guidManager;

        private UserPermissionsManager? _permissionsManager;
        private bool _permissionRulesDelayedInitialized;
        private List<IqlUserPermissionRule> _permissionRulesDelayed;

        private List<IqlUserPermissionRule> _permissionRules { get { if(!_permissionRulesDelayedInitialized) { _permissionRulesDelayedInitialized = true; _permissionRulesDelayed =             new List<IqlUserPermissionRule>(); } return _permissionRulesDelayed; } set { _permissionRulesDelayedInitialized = true; _permissionRulesDelayed = value; } }
        public List<IqlUserPermissionRule> PermissionRules =>
            _permissionRules.EnsureHasBuilder(this);

        public UserPermissionsManager PermissionManager =>
            _permissionsManager = _permissionsManager ?? new UserPermissionsManager(
                this,
                this,
                EntityConfiguration
            );
        private bool _entitiesDelayedInitialized;
        private Dictionary<Type, IEntityConfiguration> _entitiesDelayed;

        private Dictionary<Type, IEntityConfiguration> _entities { get { if(!_entitiesDelayedInitialized) { _entitiesDelayedInitialized = true; _entitiesDelayed =             new Dictionary<Type, IEntityConfiguration>(); } return _entitiesDelayed; } set { _entitiesDelayedInitialized = true; _entitiesDelayed = value; } }
        private bool _entitiesByTypeNameDelayedInitialized;
        private Dictionary<string, IEntityConfiguration> _entitiesByTypeNameDelayed;
        private Dictionary<string, IEntityConfiguration> _entitiesByTypeName { get { if(!_entitiesByTypeNameDelayedInitialized) { _entitiesByTypeNameDelayedInitialized = true; _entitiesByTypeNameDelayed =             new Dictionary<string, IEntityConfiguration>(); } return _entitiesByTypeNameDelayed; } set { _entitiesByTypeNameDelayedInitialized = true; _entitiesByTypeNameDelayed = value; } }
        private static bool EntityConfigurationBuildersDelayedInitialized;
        private static List<EntityConfigurationBuilder> EntityConfigurationBuildersDelayed;

        private static List<EntityConfigurationBuilder> EntityConfigurationBuilders { get { if(!EntityConfigurationBuildersDelayedInitialized) { EntityConfigurationBuildersDelayedInitialized = true; EntityConfigurationBuildersDelayed =             new List<EntityConfigurationBuilder>(); } return EntityConfigurationBuildersDelayed; } set { EntityConfigurationBuildersDelayedInitialized = true; EntityConfigurationBuildersDelayed = value; } }

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
                    if (entityConfiguration.Value.HasNameOrAlias(typeName))
                    {
                        _entitiesByTypeName.Add(typeName, entityConfiguration.Value);
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
        private bool _enumTypesDelayedInitialized;
        private Dictionary<string, IEnumConfiguration> _enumTypesDelayed;

        private Dictionary<string, IEnumConfiguration> _enumTypes { get { if(!_enumTypesDelayedInitialized) { _enumTypesDelayedInitialized = true; _enumTypesDelayed = new Dictionary<string, IEnumConfiguration>(); } return _enumTypesDelayed; } set { _enumTypesDelayedInitialized = true; _enumTypesDelayed = value; } }
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

        public EntityConfiguration<T> EntityType<T>()
            where T : class
        {
            return DefineEntityType<T>();
        }
        
        public EntityConfiguration<T> DefineEntityType<T>(string name = null)
            where T : class
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
            if (entityConfiguration != null && !string.IsNullOrWhiteSpace(name))
            {
                entityConfiguration.AddAlias(name);
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
        private IqlServiceProvider _serviceProvider;

        public IqlServiceProvider ServiceProvider => _serviceProvider = _serviceProvider ?? new IqlServiceProvider();
        private bool _methodsInitialized;
        private List<IqlMethod> _methods;
        public List<IqlMethod> Methods { get { if(!_methodsInitialized) { _methodsInitialized = true; _methods = new List<IqlMethod>(); } return _methods; } set { _methodsInitialized = true; _methods = value; } }
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
                                                                        private EventEmitter<string> _resolvingType;

        //public Dictionary<Type, IList> FlattenDependencyGraph(object entity, Type entityType)
        //{
        //    return this.FlattenDependencyGraphInternal(entity, entityType);
        //}

        //public Dictionary<Type, IList> FlattenDependencyGraphs(Type entityType, IEnumerable entities)
        //{
        //    return this.FlattenDependencyGraphsInternal(entityType, entities);
        //}

        public EventEmitter<string> ResolvingType => _resolvingType = _resolvingType ?? new EventEmitter<string>();

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

        public override IUserPermission ParentPermissions => null;
    }
}