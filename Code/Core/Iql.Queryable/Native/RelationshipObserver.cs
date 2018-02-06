using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Iql.Extensions;
using Iql.Queryable.Data;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Events;
using Iql.Queryable.Extensions;

namespace Iql.Queryable.Native
{
    public class RelationshipObserver
    {
        private readonly Dictionary<object, bool> _observed = new Dictionary<object, bool>();

        private readonly Dictionary<Type, Dictionary<string, IProperty>> _properties
            = new Dictionary<Type, Dictionary<string, IProperty>>();
        private readonly Dictionary<Type, List<RelationshipMatch>> _relationships
            = new Dictionary<Type, List<RelationshipMatch>>();

        static RelationshipObserver()
        {
            ObserveAllTypedMethod = typeof(RelationshipObserver)
                .GetMethod(nameof(ObserveAllTyped));
            PairAllTypedMethod = typeof(RelationshipObserver)
                .GetMethod(nameof(PairAllTyped));
        }

        public RelationshipObserver(IDataContext dataContext)
        {
            DataContext = dataContext;
            EntityConfigurationContext = dataContext.EntityConfigurationContext;
        }

        public IDataContext DataContext { get; }

        public EntityConfigurationBuilder EntityConfigurationContext { get; set; }

        public static MethodInfo PairAllTypedMethod { get; set; }
        public static MethodInfo ObserveAllTypedMethod { get; set; }

        public void ObserveList(IList list, Type entityType)
        {
            ObserveAllTypedMethod.InvokeGeneric(
                this,
                new object[] { list },
                entityType);
        }

        public void ObserveAll(Dictionary<Type, IList> dictionary)
        {
            foreach (var item in dictionary)
            {
                ObserveAllTypedMethod.InvokeGeneric(
                    this,
                    new object[] { item.Value },
                    item.Key);
            }
            foreach (var item in dictionary)
            {
                PairAllTypedMethod.InvokeGeneric(
                    this,
                    new object[] { item.Value },
                    item.Key);
            }
        }

        public void ObserveAllTyped<T>(List<T> list)
            where T : class
        {
            var entityConfiguration = EntityConfigurationContext.GetEntity<T>();
            Dictionary<string, IProperty> lookup;
            if (!_properties.ContainsKey(typeof(T)))
            {
                lookup = new Dictionary<string, IProperty>();
                _properties.Add(typeof(T), lookup);
                var relationships = new List<RelationshipMatch>();
                _relationships.Add(typeof(T), relationships);
                var matches = entityConfiguration.AllRelationships();
                for (var i = 0; i < matches.Count; i++)
                {
                    var relationship = matches[i];
                    relationships.Add(relationship);
                    switch (relationship.Relationship.Kind)
                    {
                        case RelationshipKind.OneToOne:
                            if (!_oneToOneSourceRelationshipKeyMaps.ContainsKey(relationship.Relationship))
                            {
                                _oneToOneSourceRelationshipKeyMaps.Add(relationship.Relationship, new Dictionary<string, object>());
                                _oneToOneRelationshipReferenceMaps.Add(relationship.Relationship, new Dictionary<object, object>());
                            }
                            break;
                        case RelationshipKind.OneToMany:
                            if (!_oneToManySourceRelationshipKeyMaps.ContainsKey(relationship.Relationship))
                            {
                                _oneToManySourceRelationshipKeyMaps.Add(relationship.Relationship,
                                    new Dictionary<string, Dictionary<object, object>>());
                                _oneToManyRelationshipReferenceMaps.Add(relationship.Relationship,
                                    new Dictionary<object, Dictionary<object, object>>());
                            }
                            break;
                    }
                    lookup.Add(relationship.ThisEnd.Property.Name, relationship.ThisEnd.Property);
                    var properties = relationship.ThisEnd.Constraints();
                    for (var j = 0; j < properties.Length; j++)
                    {
                        var property = properties[j];
                        if (!lookup.ContainsKey(property.Name))
                        {
                            lookup.Add(property.Name, property);
                        }
                    }
                }
            }
            else
            {
                lookup = _properties[typeof(T)];
            }

            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                if (!_observed.ContainsKey(item))
                {
                    var entity = item as IEntity;
                    if (entity != null)
                    {
                        _observed.Add(item, true);
                        if (entity.PropertyChanged == null)
                        {
                            entity.PropertyChanged = new EventEmitter<IPropertyChangeEvent>();
                        }
                        entity.PropertyChanged.Subscribe(e => { PropertyChangeEvent(e, entityConfiguration, lookup); });
                        MapRelationships<T>(entity);
                    }
                }
            }
        }

        public void PairAllTyped<T>(List<T> items)
        {
            var relationships = _relationships[typeof(T)];
            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                for (var j = 0; j < relationships.Count; j++)
                {
                    var relationship = relationships[j];
                    PairRelationship(item, relationship.ThisEnd);
                }
            }
        }

        private void PairRelationship(object entity, IRelationshipDetail relationship)
        {
            if (relationship.RelationshipSide == RelationshipSide.Source)
            {
                var key = relationship.GetCompositeKey(entity);
                if (!key.HasDefaultValue())
                {
                    var keyString = key.AsKeyString(false);
                    switch (relationship.Relationship.Kind)
                    {
                        case RelationshipKind.OneToMany:
                            if (_oneToTargetRelationshipKeyMaps.ContainsKey(relationship.Relationship.QualifiedConstraintKey))
                            {
                                var oneToManyKeyMaps = _oneToTargetRelationshipKeyMaps[relationship.Relationship.QualifiedConstraintKey];
                                if (oneToManyKeyMaps.ContainsKey(keyString))
                                {
                                    PairOneToMany(relationship, entity, oneToManyKeyMaps[keyString]);
                                }
                            }
                            break;
                    }
                }
            }
            else
            {
                var key = relationship.GetCompositeKey(entity);
                if (!key.HasDefaultValue())
                {
                    var keyString = key.AsKeyString(false);
                    switch (relationship.Relationship.Kind)
                    {
                        case RelationshipKind.OneToMany:
                            if (_oneToManySourceRelationshipKeyMaps.ContainsKey(relationship.Relationship))
                            {
                                var oneToManyKeyMaps = _oneToManySourceRelationshipKeyMaps[relationship.Relationship];
                                if (oneToManyKeyMaps.ContainsKey(keyString))
                                {
                                    var matched = oneToManyKeyMaps[keyString];
                                    foreach (var sourceEntity in matched)
                                    {
                                        PairOneToMany(relationship, sourceEntity.Value, entity);
                                    }
                                }
                            }
                            break;
                    }
                }
            }
        }

        private static void PairOneToMany(IRelationshipDetail relationship, 
            object sourceEntity, 
            object target)
        {
            sourceEntity.SetPropertyValue(relationship.Relationship
                .Source.Property, target);
            var list = target.GetPropertyValueAs<IList>(relationship.Relationship.Target.Property);
            if (!list.Contains(sourceEntity))
            {
                list.Add(sourceEntity);
            }
        }

        private void MapRelationships<T>(IEntity entity)
        {
            var relationships = _relationships[typeof(T)];
            for (var i = 0; i < relationships.Count; i++)
            {
                MapRelationship<T>(entity, relationships[i].ThisEnd);
            }
        }

        private readonly Dictionary<string, Dictionary<string, object>> _oneToTargetRelationshipKeyMaps
            = new Dictionary<string, Dictionary<string, object>>();
        private readonly Dictionary<IRelationship, Dictionary<string, object>> _oneToOneSourceRelationshipKeyMaps
            = new Dictionary<IRelationship, Dictionary<string, object>>();
        private readonly Dictionary<IRelationship, Dictionary<object, object>> _oneToOneRelationshipReferenceMaps
            = new Dictionary<IRelationship, Dictionary<object, object>>();
        private readonly Dictionary<IRelationship, Dictionary<string, Dictionary<object, object>>> _oneToManySourceRelationshipKeyMaps
            = new Dictionary<IRelationship, Dictionary<string, Dictionary<object, object>>>();
        private readonly Dictionary<IRelationship, Dictionary<object, Dictionary<object, object>>> _oneToManyRelationshipReferenceMaps
            = new Dictionary<IRelationship, Dictionary<object, Dictionary<object, object>>>();

        private void MapRelationship<T>(object entity, IRelationshipDetail relationship)
        {
            if (relationship.RelationshipSide == RelationshipSide.Source)
            {
                var key = relationship.GetCompositeKey(entity);
                if (!key.HasDefaultValue())
                {
                    var keyString = key.AsKeyString(false);
                    switch (relationship.Relationship.Kind)
                    {
                        case RelationshipKind.OneToOne:
                            var oneToOneKeyMaps = _oneToOneSourceRelationshipKeyMaps[relationship.Relationship];
                            oneToOneKeyMaps.Add(keyString, entity);
                            break;
                        case RelationshipKind.OneToMany:
                            var oneToManyKeyMaps = _oneToManySourceRelationshipKeyMaps[relationship.Relationship];
                            Dictionary<object, object> collection;
                            if (!oneToManyKeyMaps.ContainsKey(keyString))
                            {
                                collection = new Dictionary<object, object>();
                                oneToManyKeyMaps.Add(keyString, collection);
                            }
                            else
                            {
                                collection = oneToManyKeyMaps[keyString];
                            }

                            if (!collection.ContainsKey(entity))
                            {
                                collection.Add(entity, entity);
                            }
                            break;
                    }
                }
            }
            else
            {
                var key = relationship.Relationship.Target.GetCompositeKey(entity);
                var keyString = key.AsKeyString(false);
                Dictionary<string, object> dictionary;
                if (!_oneToTargetRelationshipKeyMaps.ContainsKey(relationship.Relationship.QualifiedConstraintKey))
                {
                    dictionary = new Dictionary<string, object>();
                    _oneToTargetRelationshipKeyMaps.Add(relationship.Relationship.QualifiedConstraintKey,
                        dictionary);
                }
                else
                {
                    dictionary = _oneToTargetRelationshipKeyMaps[relationship.Relationship.QualifiedConstraintKey];
                }

                if (dictionary.ContainsKey(keyString))
                {
                    var existing = dictionary[keyString];
                    if (existing != entity)
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    dictionary.Add(keyString, entity);
                }
            }
        }

        private void PropertyChangeEvent(
            IPropertyChangeEvent propertyChangeEvent,
            IEntityConfiguration entityConfiguration,
            Dictionary<string, IProperty> lookup)
        {
            if (lookup.ContainsKey(propertyChangeEvent.PropertyName))
            {
                var property = lookup[propertyChangeEvent.PropertyName];
                switch (property.Kind)
                {
                    case PropertyKind.RelationshipKey:
                        int a = 0;
                        break;
                    case PropertyKind.Relationship:
                        break;
                }
            }
        }
    }
}