using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public class EntityConfigurationBuilder
    {
        private readonly Dictionary<Type, IEntityConfiguration> _entities =
            new Dictionary<Type, IEntityConfiguration>();

        public EntityConfiguration<T> DefineEntity<T>() where T : class
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

        public List<FlattenedEntity> FlattenObjectGraphs(Type entityType, params object[] entities)
        {
            var flattened = new List<FlattenedEntity>();
            foreach (var entity in entities)
            {
                flattened.AddRange(FlattenObjectGraph(entity, entityType));
            }
            return flattened.Distinct().ToList();
        }

        /// <summary>
        /// Flattens an object graph, producing a list of distinctive entities contained within
        /// </summary>
        /// <param name="entity">The entity to flatten</param>
        /// <param name="entityType">The type of the entity to flatten</param>
        /// <returns></returns>
        public List<FlattenedEntity> FlattenObjectGraph(object entity, Type entityType)
        {
            return FlattenObjectGraphInternal(entity, entityType, new List<FlattenedEntity>());
        }

        private List<FlattenedEntity> FlattenObjectGraphInternal(object objectGraphRoot, Type entityType, List<FlattenedEntity> objects)
        {
            if (objects.Any(o => o.Entity == objectGraphRoot))
            {
                // Prevent infinite recursion
                return objects;
            }
            var flattenedEntity = new FlattenedEntity(objectGraphRoot, entityType);
            objects.Add(flattenedEntity);
            var graphEntityConfiguration =
                GetEntityByType(entityType);
            foreach (var relationship in graphEntityConfiguration.Relationships)
            {
                var isSource = relationship.Source.Configuration == graphEntityConfiguration;
                var propertyName = isSource
                    ? relationship.Source.Property.PropertyName
                    : relationship.Target.Property.PropertyName;
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
                            FlattenObjectGraphInternal(item, childType, objects);
                        }
                    }
                    else
                    {
                        FlattenObjectGraphInternal(relationshipValue, childType, objects);
                    }
                }
            }
            return objects;
        }
    }
}