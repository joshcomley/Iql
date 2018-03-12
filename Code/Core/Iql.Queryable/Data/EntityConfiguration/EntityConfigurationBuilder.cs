using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iql.Queryable.Extensions;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public class EntityConfigurationBuilder
    {
        private readonly Dictionary<Type, IEntityConfiguration> _entities =
            new Dictionary<Type, IEntityConfiguration>();

        public bool IsEntityType(Type type)
        {
            return _entities.ContainsKey(type);
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
                entityConfiguration = new EntityConfiguration<T>(entityType, this);
                _entities[entityType] = entityConfiguration;
            }
            return entityConfiguration;
        }

        public IEnumerable<IEntityConfiguration> AllConfigurations()
        {
            return _entities.Values;
        }

        public EntityConfiguration<T> GetEntity<T>() where T : class
        {
            return GetEntityByType(typeof(T)) as EntityConfiguration<T>;
        }

        public IEntityConfiguration GetEntityByType(Type type)
        {
            if (!_entities.ContainsKey(type))
            {
                throw new Exception($"No entity of type \"{type.Name}\" has been configured for this context.");
            }
            return _entities[type];
        }

        public Dictionary<Type, IList> FlattenObjectGraphs(Type entityType, IEnumerable entities)
        {
            var dictionary = new Dictionary<Type, Dictionary<string, object>>();
            var recursionLookup = new Dictionary<object, object>();
            var result = new Dictionary<Type, IList>();
            var flattened = new List<object>();
            foreach (var entity in entities)
            {
                FlattenObjectGraphRecursive(
                    entity, 
                    entityType, 
                    dictionary,
                    result,
                    recursionLookup);
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
            var result = new Dictionary<Type, IList>();
            FlattenObjectGraphRecursive(
                entity,
                entityType,
                new Dictionary<Type, Dictionary<string, object>>(),
                result, 
                new Dictionary<object, object>()
            );
            return result;
        }

        private void FlattenObjectGraphRecursive(
            object objectGraphRoot,
            Type entityType,
            Dictionary<Type, Dictionary<string, object>> dictionary,
            Dictionary<Type, IList> result,
            Dictionary<object, object> recursionLookup
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

            foreach (var relationship in graphEntityConfiguration.Relationships)
            {
                var isSource = relationship.Source.Configuration == graphEntityConfiguration;
                var propertyName = isSource
                    ? relationship.Source.Property
                    : relationship.Target.Property;
                var relationshipValue = objectGraphRoot.GetPropertyValue(propertyName);
                var childType = isSource
                    ? relationship.Target.Type
                    : relationship.Source.Type;
                if (relationshipValue != null)
                {
                    var isArray = relationshipValue is IEnumerable && !(relationshipValue is string);
                    if (isArray)
                    {
                        var list = (IList)relationshipValue;
                        foreach (var item in list)
                        {
                            FlattenObjectGraphRecursive(item, childType, dictionary, result, recursionLookup);
                        }
                    }
                    else
                    {
                        FlattenObjectGraphRecursive(relationshipValue, childType, dictionary, result, recursionLookup);
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