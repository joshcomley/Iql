using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iql.Entities.Enums;
using Iql.Entities.Extensions;
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
                _entities[entityType] = entityConfiguration;
                _entitiesByTypeName[entityType.Name] = entityConfiguration;
            }
            return entityConfiguration;
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
                return null;
            }
            return _entities[type];
        }

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

        public Dictionary<Type, IList> FlattenObjectGraphs(Type entityType, IEnumerable entities)
        {
            return FlattenObjectGraphsInternal(entityType, entities, false);
        }

        public Dictionary<Type, IList> FlattenDependencyGraphs(Type entityType, IEnumerable entities)
        {
            return FlattenObjectGraphsInternal(entityType, entities, true);
        }

        private Dictionary<Type, IList> FlattenObjectGraphsInternal(Type entityType, IEnumerable entities, bool dependenciesOnly)
        {
            var dictionary = new Dictionary<Type, Dictionary<string, object>>();
            var recursionLookup = new Dictionary<object, object>();
            var result = new Dictionary<Type, IList>();
            foreach (var entity in entities)
            {
                FlattenObjectGraphRecursive(
                    entity,
                    entityType,
                    dictionary,
                    result,
                    recursionLookup,
                    dependenciesOnly
                );
            }
            return result;
        }

        /// <summary>
        /// Flattens an object graph, producing a list of distinctive entities contained within
        /// </summary>
        /// <param name="entity">The entity to flatten</param>
        /// <param name="entityType">The type of the entity to flatten</param>
        /// <returns></returns>
        public Dictionary<Type, IList> FlattenObjectGraph(object entity, Type entityType)
        {
            return FlattenObjectGraphInternal(entity, entityType, false);
        }

        public Dictionary<Type, IList> FlattenDependencyGraph(object entity, Type entityType)
        {
            return FlattenObjectGraphInternal(entity, entityType, true);
        }

        private Dictionary<Type, IList> FlattenObjectGraphInternal(object entity, Type entityType, bool dependenciesOnly)
        {
            var result = new Dictionary<Type, IList>();
            FlattenObjectGraphRecursive(
                entity,
                entityType,
                new Dictionary<Type, Dictionary<string, object>>(),
                result,
                new Dictionary<object, object>(),
                dependenciesOnly
            );
            return result;
        }

        private void FlattenObjectGraphRecursive(
            object objectGraphRoot,
            Type entityType,
            Dictionary<Type, Dictionary<string, object>> dictionary,
            Dictionary<Type, IList> result,
            Dictionary<object, object> recursionLookup,
            bool dependenciesOnly
            )
        {
            if (!dictionary.ContainsKey(entityType))
            {
                dictionary.Add(entityType, new Dictionary<string, object>());
                result.Add(entityType, ListOfType(entityType));
            }

            var typeGroup = dictionary[entityType];
            if (recursionLookup.ContainsKey(objectGraphRoot))
            {
                // Prevent infinite recursion
                return;
            }
            recursionLookup.Add(objectGraphRoot, objectGraphRoot);
            var graphEntityConfiguration = GetEntityByType(entityType);
            var compositeKey = graphEntityConfiguration.GetCompositeKey(objectGraphRoot);
            var keyString = compositeKey.AsKeyString();
            if (compositeKey.HasDefaultValue())
            {
                keyString += Guid.NewGuid().ToString();
            }

            if (typeGroup.ContainsKey(keyString))
            {
                return;
            }

            result[entityType].Add(objectGraphRoot);
            typeGroup.Add(
                keyString,
                objectGraphRoot);

            foreach (var relationship in graphEntityConfiguration.AllRelationships())
            {
                if (relationship.ThisIsTarget && dependenciesOnly)
                {
                    continue;
                }
                var property = relationship.ThisEnd.Property;
                var relationshipValue = objectGraphRoot.GetPropertyValue(property);
                var childType = relationship.OtherEnd.Type;
                if (relationshipValue != null)
                {
                    var isArray = relationshipValue is IEnumerable && !(relationshipValue is string);
                    if (isArray)
                    {
                            var list = (IList)relationshipValue;
                            foreach (var item in list)
                            {
                                FlattenObjectGraphRecursive(item, childType, dictionary, result, recursionLookup, dependenciesOnly);
                            }
                    }
                    else
                    {
                        FlattenObjectGraphRecursive(relationshipValue, childType, dictionary, result, recursionLookup, dependenciesOnly);
                    }
                }
            }
        }

        private static IList ListOfType(Type entityType)
        {
            return (IList) Activator.CreateInstance(typeof(List<>).MakeGenericType(entityType));
        }
    }
}