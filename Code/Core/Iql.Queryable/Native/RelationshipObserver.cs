using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Iql.Extensions;
using Iql.Queryable.Data;
using Iql.Queryable.Data.Crud.State;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Data.Tracking;
using Iql.Queryable.Events;
using Iql.Queryable.Extensions;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Native
{
    public class RelationshipObserver : IRelationshipObserver
    {
        public IDataStore DataStore { get; }
        private readonly Dictionary<object, object> _changingEntities =
            new Dictionary<object, object>();

        private readonly Dictionary<IRelationship, Dictionary<object, string>> _ids =
            new Dictionary<IRelationship, Dictionary<object, string>>();

        private readonly Dictionary<object, EntityObserver> _observed = new Dictionary<object, EntityObserver>();

        //private readonly Dictionary<IRelationship, Dictionary<string, object>> _oneToOneSourceRelationshipKeyMaps
        //    = new Dictionary<IRelationship, Dictionary<string, object>>();
        private readonly Dictionary<object, Dictionary<IRelationship, string>>
            _oneToSourceRelationshipMaps
            = new Dictionary<object, Dictionary<IRelationship, string>>();
        private readonly Dictionary<IRelationship, Dictionary<string, Dictionary<object, object>>>
            _oneToSourceRelationshipKeyMaps
                = new Dictionary<IRelationship, Dictionary<string, Dictionary<object, object>>>();

        private readonly Dictionary<IRelationship, Dictionary<string, object>> _oneToTargetRelationshipKeyMaps
            = new Dictionary<IRelationship, Dictionary<string, object>>();

        private readonly Dictionary<object, bool> _paired = new Dictionary<object, bool>();

        private readonly Dictionary<Type, Dictionary<string, IProperty>> _properties
            = new Dictionary<Type, Dictionary<string, IProperty>>();

        private readonly Dictionary<Type, List<RelationshipMatch>> _relationships
            = new Dictionary<Type, List<RelationshipMatch>>();

        private readonly Dictionary<object, Dictionary<IRelationship, string>> _relationshipKeys
            = new Dictionary<object, Dictionary<IRelationship, string>>();

        static RelationshipObserver()
        {
            ObserveListTypedMethod = typeof(RelationshipObserver)
                .GetMethod(nameof(ObserveListTyped));
            PairAllTypedMethod = typeof(RelationshipObserver)
                .GetMethod(nameof(PairAllTyped));
        }

        public RelationshipObserver(IDataStore dataStore, bool trackEntities)
        {
            DataStore = dataStore;
            TrackEntities = trackEntities;
        }

        public IDataContext DataContext => DataStore.DataContext;
        public TrackingSetCollection TrackingSetCollection => DataStore.Tracking;

        public EntityConfigurationBuilder EntityConfigurationContext => DataContext.EntityConfigurationContext;

        public static MethodInfo PairAllTypedMethod { get; set; }
        public static MethodInfo ObserveListTypedMethod { get; set; }
        //public IDataStore DataStore { get; }
        public bool TrackEntities { get; }

        public void ObserveList(IList list, Type entityType)
        {
            var dictionary = new Dictionary<Type, IList>();
            dictionary.Add(entityType, list);
            ObserveAll(dictionary);
        }

        public void Observe(object entity, Type entityType)
        {
            var dictionary = new Dictionary<Type, IList>();
            dictionary.Add(entityType, new List<object>(new[] { entity }).ToList(entityType));
            ObserveAll(dictionary);
        }

        public void ObserveAll(Dictionary<Type, IList> dictionary)
        {
            foreach (var item in dictionary)
            {
                ObserveListTypedMethod.InvokeGeneric(
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

        public void ObserveListTyped<T>(List<T> list)
            where T : class
        {
            var entityConfiguration = EntityConfigurationContext.GetEntity<T>();
            Dictionary<string, IProperty> lookup;
            if (!_properties.ContainsKey(typeof(T)))
            {
                //foreach (var relationship in EntityConfiguration.AllRelationships())
                //{
                //    if (relationship.ThisEnd.IsCollection)
                //    {
                //        var relatedList = entity.GetPropertyValue(relationship.ThisEnd.Property)
                //            as IRelatedList;
                //        if (relatedList != null && !_collectionChangeSubscriptions.ContainsKey(relatedList))
                //        {
                //            _collectionChangeSubscriptions.Add(relatedList, relatedList.Changed.Subscribe(RelatedListChanged));
                //        }
                //    }
                //}
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
                        //if (!_oneToOneSourceRelationshipKeyMaps.ContainsKey(relationship.Relationship))
                        //{
                        //    _oneToOneSourceRelationshipKeyMaps.Add(relationship.Relationship, new Dictionary<string, object>());
                        //}
                        //break;
                        case RelationshipKind.OneToMany:
                            if (!_oneToSourceRelationshipKeyMaps.ContainsKey(relationship.Relationship))
                            {
                                _oneToSourceRelationshipKeyMaps.Add(relationship.Relationship,
                                    new Dictionary<string, Dictionary<object, object>>());
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
                        var observer = new EntityObserver(TrackingSetCollection.TrackingSet<T>().GetEntityState(item));
                        _observed.Add(item, observer);
                        observer.RegisterMarkForDeletionChanged(MarkedForDeletionChange);
                        observer.RegisterPropertyChanged(e => { PropertyChangeEvent(e, entityConfiguration, lookup); });
                        observer.RegisterRelatedListChanged(RelatedListChanged);
                        MapRelationships<T>(entity);
                    }
                }
            }
        }

        private void MarkedForDeletionChange(MarkedForDeletionChangeEvent e)
        {
            // Recurse all relationships marking for cascade delete if necessary
            UpdateDeletionStatus(e.NewValue, e.EntityState, null, null);
        }

        private void UpdateDeletionStatus(bool deleted, IEntityStateBase entityState, object parent, IRelationship parentRelationship)
        {
            if (parent != null)
            {
                if (deleted)
                {
                    entityState.MarkForCascadeDeletion(parent, parentRelationship);
                }
                else
                {
                    entityState.UnmarkForDeletion();
                }
            }
            var relationships = entityState.EntityConfiguration.AllRelationships();
            for (var i = 0; i < relationships.Count; i++)
            {
                var relationship = relationships[i];
                if (relationship.ThisIsTarget && !relationship.Relationship.Source.Property.Nullable)
                {
                    var sourceTrackingSet =
                        TrackingSetCollection.TrackingSetByType(relationship.Relationship.Source.Type);
                    var targetSourceValue = entityState.Entity.GetPropertyValue(
                        relationship.Relationship.Target.Property);
                    if (targetSourceValue != null)
                    {
                        switch (relationship.Relationship.Kind)
                        {
                            case RelationshipKind.OneToMany:
                                {
                                    var list = targetSourceValue as IList;
                                    if (list != null)
                                    {
                                        foreach (var source in list)
                                        {
                                            var state = sourceTrackingSet.GetEntityState(source);
                                            UpdateDeletionStatus(deleted, state, entityState.Entity, relationship.Relationship);
                                        }
                                    }
                                }
                                break;
                            case RelationshipKind.OneToOne:
                                {
                                    var state = sourceTrackingSet.GetEntityState(targetSourceValue);
                                    UpdateDeletionStatus(deleted, state, entityState.Entity, relationship.Relationship);
                                }
                                break;
                        }
                    }
                }
            }
        }

        public void Unobserve(object entity, Type entityType)
        {
            if (_observed.ContainsKey(entity))
            {
                var observer = _observed[entity];
                observer.Unobserve();
                _observed.Remove(entity);
            }
        }

        public void DeleteRelationships(object entity, Type entityType)
        {
            var relationships = EntityConfigurationContext.GetEntityByType(entityType)
                .AllRelationships();
            for (var i = 0; i < relationships.Count; i++)
            {
                var relationship = relationships[i];
                if (!relationship.ThisIsTarget)
                {
                    if (_oneToTargetRelationshipKeyMaps.ContainsKey(relationship.Relationship))
                    {
                        var mapping = _oneToTargetRelationshipKeyMaps[relationship.Relationship];
                        var key = GetRelationshipKeyString(entity, relationship.Relationship);
                        if (key != null && mapping.ContainsKey(key))
                        {
                            var target = mapping[key];
                            Unpair(relationship.Relationship, entity, target);
                        }
                    }
                }
                //if (_oneToSourceRelationshipMaps.ContainsKey(entity))
                //{
                //    var sourceMap = _oneToSourceRelationshipMaps[entity];
                //    var list = new List<KeyValuePair<IRelationship, string>>();
                //    foreach (var item in sourceMap)
                //    {
                //        list.Add(item);
                //    }

                //    for (var j = 0; j < list.Count; j++)
                //    {
                //        var item = list[j];
                //    }
                //}
            }

        }

        public bool IsAssignedToAnyRelationship(object entity, Type entityType)
        {
            var entityConfiguration = EntityConfigurationContext.GetEntityByType(entityType);
            var list = entityConfiguration.AllRelationships();
            for (var i = 0; i < list.Count; i++)
            {
                var relationship = list[i];
                if (relationship.ThisIsTarget)
                {
                    if (_oneToSourceRelationshipKeyMaps.ContainsKey(relationship.Relationship))
                    {
                        if (_oneToTargetRelationshipKeyMaps.ContainsKey(relationship.Relationship))
                        {
                            var mapping = _oneToTargetRelationshipKeyMaps[relationship.Relationship];
                            var key = GetTargetKeyString(entity, relationship.Relationship);
                            if (mapping.ContainsKey(key))
                            {
                                var sourceMaps = _oneToSourceRelationshipKeyMaps[relationship.Relationship];
                                if (sourceMaps.ContainsKey(key))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        private void RelatedListChanged(IRelatedListChangedEvent relatedListChangedEvent)
        {
            var newSource = relatedListChangedEvent.Item;
            object oldTarget = null;
            var propertyName = relatedListChangedEvent.List.PropertyName;
            var targetConfiguration =
                EntityConfigurationContext.GetEntityByType(relatedListChangedEvent.List.OwnerType);
            var relationship = targetConfiguration.FindProperty(propertyName).Relationship;
            var sourceProperty = relationship.Relationship.Source.Property;
            if (newSource == null)
            {
                var sourceTracking = DataStore.Tracking                    .TrackingSetByType(relationship.Relationship.Source.Configuration.Type);
                var sourceState = sourceTracking.GetEntityState(relatedListChangedEvent.ItemKey);
                newSource = sourceState.Entity;
            }

            oldTarget = sourceProperty.PropertyGetter(newSource);
            switch (relatedListChangedEvent.Kind)
            {
                case RelatedListChangeKind.Assign:
                    ProcessRelationshipReferenceChange(
                        oldTarget,
                        relatedListChangedEvent.Owner,
                        sourceProperty,
                        newSource,
                        relationship.Relationship.Source
                    );
                    break;
                case RelatedListChangeKind.Remove:
                    ProcessRelationshipReferenceChange(
                        oldTarget,
                        null,
                        sourceProperty,
                        newSource,
                        relationship.Relationship.Source
                    );
                    break;
            }
        }

        public void PairAllTyped<T>(List<T> items)
        {
            var relationships = _relationships[typeof(T)];
            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                if (_paired.ContainsKey(item))
                {
                    continue;
                }

                _paired.Add(item, true);
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
                // entity is source
                if (_oneToTargetRelationshipKeyMaps.ContainsKey(relationship.Relationship))
                {
                    var keyString = GetRelationshipKeyString(entity, relationship.Relationship);
                    if (keyString != null)
                    {
                        if (_oneToTargetRelationshipKeyMaps.ContainsKey(
                            relationship.Relationship))
                        {
                            var oneToTargetKeyMaps =
                                _oneToTargetRelationshipKeyMaps[relationship.Relationship];
                            if (oneToTargetKeyMaps.ContainsKey(keyString))
                            {
                                Pair(relationship.Relationship, entity, oneToTargetKeyMaps[keyString]);
                            }
                        }
                    }
                }
            }
            else
            {
                // entity is target
                var keyString = GetTargetKeyString(entity, relationship.Relationship);
                if (_oneToSourceRelationshipKeyMaps.ContainsKey(relationship.Relationship))
                {
                    var oneToManyKeyMaps = _oneToSourceRelationshipKeyMaps[relationship.Relationship];
                    if (oneToManyKeyMaps.ContainsKey(keyString))
                    {
                        var matched = oneToManyKeyMaps[keyString];
                        var list = new List<object>();
                        foreach (var sourceEntity in matched)
                        {
                            list.Add(sourceEntity.Value);
                        }

                        foreach (var item in list)
                        {
                            Pair(relationship.Relationship, item, entity);
                        }
                    }
                }
            }
        }

        private string GetRelationshipKeyString(object entity, IRelationship relationship)
        {
            var entityRelationshipKeyMap = GetEntityRelationshipKeyMap(entity, relationship);
            var targetReference = relationship.Source.Property.PropertyGetter(entity);
            if (targetReference != null)
            {
                var targetTracking = TrackingSetCollection.TrackingSetByType(relationship.Target.Type);
                var state = targetTracking.GetEntityState(targetReference);
                if (state.IsNew)
                {
                    var referenceKey = GetTargetKeyString(targetReference, relationship);
                    entityRelationshipKeyMap[relationship] = referenceKey;
                    return referenceKey;
                }
            }

            var key = relationship.Source.GetCompositeKey(entity);
            var keyString = key.HasDefaultValue()
                ? null
                : key.AsKeyString(false);
            entityRelationshipKeyMap[relationship] = keyString;
            return keyString;
        }

        private Dictionary<IRelationship, string> GetEntityRelationshipKeyMap(object entity, IRelationship relationship)
        {
            Dictionary<IRelationship, string> entityRelationshipKeyMap;
            if (!_relationshipKeys.ContainsKey(entity))
            {
                entityRelationshipKeyMap = new Dictionary<IRelationship, string>();
                _relationshipKeys.Add(entity, entityRelationshipKeyMap);
            }
            else
            {
                entityRelationshipKeyMap = _relationshipKeys[entity];
            }

            if (!entityRelationshipKeyMap.ContainsKey(relationship))
            {
                entityRelationshipKeyMap.Add(relationship, null);
            }

            return entityRelationshipKeyMap;
        }

        private void Unpair(IRelationship relationship,
            object source,
            object target)
        {
            IgnoreChanges(() =>
            {
                var targetMapping = GetTargetMapping(
                    relationship,
                    target);

                if (targetMapping.ContainsKey(source))
                {
                    targetMapping.Remove(source);
                }

                var sourceMap = GetSourceMap(source);
                if (sourceMap.ContainsKey(relationship))
                {
                    sourceMap.Remove(relationship);
                    if (sourceMap.Count == 0)
                    {
                        _oneToSourceRelationshipMaps.Remove(source);
                    }
                }

                var keys = relationship.Source.Constraints();
                for (var i = 0; i < keys.Length; i++)
                {
                    var key = keys[i];
                    source.SetPropertyValue(key, key.Nullable ? null : key.Type.DefaultValue());
                }

                source.SetPropertyValue(relationship.Source.Property,
                    null);
                switch (relationship.Kind)
                {
                    case RelationshipKind.OneToOne:
                        target.SetPropertyValue(relationship.Target.Property, null);
                        break;
                    case RelationshipKind.OneToMany:
                        var list = target.GetPropertyValueAs<IList>(
                            relationship.Target.Property);
                        list.Remove(source);
                        break;
                }
            }, source, target);
        }

        private void Pair(IRelationship relationship, object source, object target)
        {
            IgnoreChanges(() =>
            {
                var targetMapping = GetTargetMapping(relationship, target);

                if (!targetMapping.ContainsKey(source))
                {
                    targetMapping.Add(source, source);
                }

                source.SetPropertyValue(relationship
                    .Source.Property, target);

                var targetState = TrackingSetCollection.TrackingSetByType(
                    relationship.Target.Type).GetEntityState(target);
                if (!targetState.IsNew)
                {
                    var targetCompositeKey = relationship.Target.GetCompositeKey(target, true);
                    if (!targetCompositeKey.HasDefaultValue())
                    {
                        for (var i = 0; i < targetCompositeKey.Keys.Length; i++)
                        {
                            var key = targetCompositeKey.Keys[i];
                            source.SetPropertyValue(relationship.Source.Configuration.FindProperty(key.Name),
                                key.Value);
                        }
                    }
                }

                var sourceMap = GetSourceMap(source);

                if (!sourceMap.ContainsKey(relationship))
                {
                    sourceMap.Add(relationship, GetTargetKeyString(target, relationship));
                }

                switch (relationship.Kind)
                {
                    case RelationshipKind.OneToOne:
                        target.SetPropertyValue(relationship
                            .Target.Property, source);
                        break;
                    case RelationshipKind.OneToMany:
                        var list = target.GetPropertyValueAs<IList>(relationship.Target.Property);
                        if (!list.Contains(source))
                        {
                            list.Add(source);
                        }
                        break;
                }
            }, source, target);

        }

        private Dictionary<IRelationship, string> GetSourceMap(object source)
        {
            Dictionary<IRelationship, string> sourceMap;
            if (!_oneToSourceRelationshipMaps.ContainsKey(source))
            {
                sourceMap = new Dictionary<IRelationship, string>();
                _oneToSourceRelationshipMaps.Add(source, sourceMap);
            }
            else
            {
                sourceMap = _oneToSourceRelationshipMaps[source];
            }

            return sourceMap;
        }

        private Dictionary<object, object> GetTargetMapping(IRelationship relationship, object target)
        {
            Dictionary<string, Dictionary<object, object>> sourceMapping;
            if (!_oneToSourceRelationshipKeyMaps.ContainsKey(relationship))
            {
                sourceMapping = new Dictionary<string, Dictionary<object, object>>();
                _oneToSourceRelationshipKeyMaps.Add(relationship, new Dictionary<string, Dictionary<object, object>>());
            }
            else
            {
                sourceMapping = _oneToSourceRelationshipKeyMaps[relationship];
            }

            var targetKey = GetTargetKeyString(target, relationship);
            Dictionary<object, object> targetMapping;
            if (!sourceMapping.ContainsKey(targetKey))
            {
                targetMapping = new Dictionary<object, object>();
                sourceMapping.Add(targetKey, targetMapping);
            }
            else
            {
                targetMapping = sourceMapping[targetKey];
            }

            return targetMapping;
        }

        private void MapRelationships<T>(IEntity entity)
        {
            var relationships = _relationships[typeof(T)];
            for (var i = 0; i < relationships.Count; i++)
            {
                MapRelationship(entity, relationships[i].ThisEnd);
            }
        }

        private void MapRelationship(object entity, IRelationshipDetail relationship)
        {
            if (relationship.RelationshipSide == RelationshipSide.Source)
            {
                MapRelationshipSource(entity, relationship.Relationship);
            }
            else
            {
                MapRelationshipTarget(entity, relationship.Relationship);
            }
        }

        private void MapRelationshipSource(object entity, IRelationship relationship)
        {
            var relationshipSource = relationship.Source;
            var key = relationshipSource.GetCompositeKey(entity);
            if (!key.HasDefaultValue())
            {
                var keyString = key.AsKeyString(false);
                var oneToManyKeyMaps = _oneToSourceRelationshipKeyMaps[relationshipSource.Relationship];
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
            }
        }

        private void MapRelationshipTarget(object entity, IRelationship relationship)
        {
            // Target is always one-endian
            var relationshipTarget = relationship.Target;
            var keyString = GetTargetKeyString(entity, relationship);
            Dictionary<string, object> oneToTargetLookup;
            if (!_oneToTargetRelationshipKeyMaps.ContainsKey(relationshipTarget.Relationship))
            {
                oneToTargetLookup = new Dictionary<string, object>();
                _oneToTargetRelationshipKeyMaps.Add(relationshipTarget.Relationship,
                    oneToTargetLookup);
            }
            else
            {
                oneToTargetLookup =
                    _oneToTargetRelationshipKeyMaps[relationshipTarget.Relationship];
            }

            //var keyString = key.AsKeyString(false);
            if (oneToTargetLookup.ContainsKey(keyString))
            {
                var existing = oneToTargetLookup[keyString];
                if (existing != entity)
                {
                    throw new Exception();
                }
            }
            else
            {
                oneToTargetLookup.Add(keyString, entity);
            }

            if (relationship.Target.IsCollection)
            {
                var collection = relationship.Target.Property.PropertyGetter(entity) as IList;
                if (collection != null)
                {
                    foreach (var source in collection)
                    {
                        Pair(relationshipTarget.Relationship, source, entity);
                        //MapRelationshipSource(source, relationship);
                    }
                }
            }
            //switch (relationship.Relationship.Kind)
            //{
            //    case RelationshipKind.OneToOne:
            //        break;
            //    case RelationshipKind.OneToMany:
            //        break;
            //}
        }

        private bool IsIgnored(object entity)
        {
            return _changingEntities.ContainsKey(entity);
        }

        private void IgnoreChanges(Action action, params object[] entities)
        {
            var ignored = new List<object>();
            foreach (var entity in entities)
            {
                if (!IsIgnored(entity))
                {
                    ignored.Add(entity);
                    _changingEntities.Add(entity, entity);
                }
            }

            action();
            foreach (var entity in ignored)
            {
                _changingEntities.Remove(entity);
            }
        }

        private void PropertyChangeEvent(
            IPropertyChangeEvent propertyChangeEvent,
            IEntityConfiguration entityConfiguration,
            Dictionary<string, IProperty> lookup)
        {
            if (lookup.ContainsKey(propertyChangeEvent.PropertyName))
            {
                var entity = propertyChangeEvent.Entity;
                if (!_changingEntities.ContainsKey(entity))
                {
                    IgnoreChanges(() =>
                    {
                        var property = lookup[propertyChangeEvent.PropertyName];
                        switch (property.Kind)
                        {
                            case PropertyKind.RelationshipKey:
                                //var newCompositeKey = property.Relationship.ThisEnd
                                //    .GetCompositeKey(entity);
                                //var oldCompositeKey = property.Relationship.ThisEnd
                                //    .GetCompositeKey(entity);
                                //for (var i = 0; i < oldCompositeKey.Keys.Count; i++)
                                //{
                                //    var key = oldCompositeKey.Keys[i];
                                //    if (key.Name == propertyChangeEvent.PropertyName)
                                //    {
                                //        key.Value = propertyChangeEvent.OldValue;
                                //    }
                                //}
                                var entityRelationshipKeyMap =
                                    GetEntityRelationshipKeyMap(entity, property.Relationship.Relationship);
                                property.Relationship.ThisEnd.MarkDirty(entity);
                                ProcessRelationshipKeyChange(
                                    entityRelationshipKeyMap[property.Relationship.Relationship],
                                    GetRelationshipKeyString(entity, property.Relationship.Relationship),
                                    entity,
                                    property.Relationship.ThisEnd
                                );
                                break;
                            case PropertyKind.Relationship:
                                ProcessRelationshipReferenceChange(propertyChangeEvent.OldValue,
                                    propertyChangeEvent.NewValue, property, entity, property.Relationship.ThisEnd);
                                break;
                            case PropertyKind.Key:
                                ProcessKeyChange(propertyChangeEvent, entityConfiguration);
                                break;
                        }
                    }, entity);
                }
            }
        }

        private void ProcessKeyChange(
            IPropertyChangeEvent propertyChangeEvent,
            IEntityConfiguration entityConfiguration)
        {
            var list = entityConfiguration.AllRelationships();
            for (var i = 0; i < list.Count; i++)
            {
                var relationship = list[i];
                var state = TrackingSetCollection.TrackingSetByType(relationship.ThisEnd.Type)
                    .GetEntityState(propertyChangeEvent.Entity);
                if (_ids.ContainsKey(relationship.Relationship))
                {
                    var relationshipMap = _ids[relationship.Relationship];
                    if (relationshipMap.ContainsKey(state.Entity))
                    {
                        var oldKeyString = relationshipMap[state.Entity];
                        var newKeyString = GetTargetKeyString(state.Entity, relationship.Relationship);
                        if (_oneToTargetRelationshipKeyMaps.ContainsKey(
                            relationship.Relationship))
                        {
                            var targetMap =
                                _oneToTargetRelationshipKeyMaps[relationship.Relationship];
                            if (targetMap.ContainsKey(oldKeyString))
                            {
                                targetMap.Remove(oldKeyString);
                            }

                            if (targetMap.ContainsKey(newKeyString))
                            {
                                if (targetMap[newKeyString] != state.Entity)
                                {
                                    throw new Exception("Two entities with the same key have been added.");
                                }
                            }
                            else
                            {
                                targetMap.Add(newKeyString, state.Entity);
                            }

                            if (_oneToSourceRelationshipKeyMaps.ContainsKey(relationship.Relationship))
                            {
                                var sourceMaps = _oneToSourceRelationshipKeyMaps[relationship.Relationship];
                                if (sourceMaps.ContainsKey(oldKeyString))
                                {
                                    var sources = sourceMaps[oldKeyString];
                                    sourceMaps.Remove(oldKeyString);
                                    sourceMaps.Add(newKeyString, sources);
                                    foreach (var source in sources)
                                    {
                                        var sourceEntity = source.Value;
                                        relationship.OtherEnd.MarkDirty(sourceEntity);
                                        if (_relationshipKeys.ContainsKey(sourceEntity))
                                        {
                                            var sourceMap = _relationshipKeys[sourceEntity];
                                            if (sourceMap.ContainsKey(relationship.Relationship))
                                            {
                                                sourceMap[relationship.Relationship] = newKeyString;
                                            }
                                        }
                                        Pair(relationship.Relationship, sourceEntity, state.Entity);
                                    }
                                }
                            }

                            //foreach(var item in _oneToSourceRelationshipKeyMaps[])
                            // Update all source maps that point to the old target key string
                            // and set IDs to the new 
                        }

                        relationshipMap.Remove(state.Entity);
                    }
                }
            }
        }

        private string GetTargetKeyString(object entity, IRelationship relationship)
        {
            if (entity == null)
            {
                return null;
            }

            var state = TrackingSetCollection.TrackingSetByType(relationship.Target.Type).GetEntityState(entity);
            if (state.IsNew)
            {
                // Assign a temporary ID
                Dictionary<object, string> idLookup;
                if (!_ids.ContainsKey(relationship))
                {
                    idLookup = new Dictionary<object, string>();
                    _ids.Add(relationship, idLookup);
                }
                else
                {
                    idLookup = _ids[relationship];
                }

                if (!idLookup.ContainsKey(entity))
                {
                    idLookup.Add(entity, Guid.NewGuid().ToString());
                }

                return idLookup[entity];
            }

            return relationship.Target.GetCompositeKey(state.Entity).AsKeyString(false);
        }

        private void ProcessRelationshipReferenceChange(
            object oldTarget,
            object newTarget,
            IProperty property,
            object source,
            IRelationshipDetail relationship)
        {
            if (newTarget != null)
            {
                if (TrackEntities)
                {
                    var trackingSet = DataStore.Tracking.TrackingSetByType(relationship.Relationship.Target.Type);
                    trackingSet.TrackEntity(newTarget);
                }

                if (source != null)
                {
                    for (var i = 0; i < relationship.Relationship.Constraints.Count; i++)
                    {
                        var constraint = relationship.Relationship.Constraints[i];
                        source.SetPropertyValue(constraint.SourceKeyProperty,
                            newTarget.GetPropertyValue(constraint.TargetKeyProperty));
                    }
                }
            }

            ProcessRelationshipKeyChange(
                GetTargetKeyString(oldTarget, relationship.Relationship),
                GetTargetKeyString(newTarget, relationship.Relationship),
                source,
                relationship
            );
        }

        private void ProcessRelationshipKeyChange(string oldTargetKey, string newTargetKey, object entity,
            IRelationshipDetail relationship)
        {
            switch (relationship.RelationshipSide)
            {
                case RelationshipSide.Source:
                    if (_oneToSourceRelationshipKeyMaps.ContainsKey(relationship.Relationship))
                    {
                        var map = _oneToSourceRelationshipKeyMaps[relationship.Relationship];
                        if (!string.IsNullOrWhiteSpace(oldTargetKey))
                        {
                            if (map.ContainsKey(oldTargetKey))
                            {
                                var dictionary = map[oldTargetKey];
                                if (dictionary.ContainsKey(entity))
                                {
                                    dictionary.Remove(entity);
                                }
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(newTargetKey))
                        {
                            Dictionary<object, object> targetSourceEntityMap;
                            if (!map.ContainsKey(newTargetKey))
                            {
                                targetSourceEntityMap = new Dictionary<object, object>();
                                map.Add(newTargetKey, targetSourceEntityMap);
                            }
                            else
                            {
                                targetSourceEntityMap = map[newTargetKey];
                            }

                            if (!targetSourceEntityMap.ContainsKey(entity))
                            {
                                targetSourceEntityMap.Add(entity, entity);
                            }
                        }

                        object newTarget = null;
                        if (_oneToTargetRelationshipKeyMaps.ContainsKey(relationship
                            .Relationship))
                        {
                            var targetMap =
                                _oneToTargetRelationshipKeyMaps[
                                    relationship.Relationship];
                            if (!string.IsNullOrWhiteSpace(oldTargetKey))
                            {
                                if (targetMap.ContainsKey(oldTargetKey))
                                {
                                    Unpair(relationship.Relationship, entity, targetMap[oldTargetKey]);
                                }
                            }

                            if (!string.IsNullOrWhiteSpace(newTargetKey))
                            {
                                if (targetMap.ContainsKey(newTargetKey))
                                {
                                    switch (relationship.Relationship.Kind)
                                    {
                                        case RelationshipKind.OneToOne:
                                            Pair(relationship.Relationship, entity, targetMap[newTargetKey]);
                                            break;
                                        case RelationshipKind.OneToMany:
                                            Pair(relationship.Relationship, entity, targetMap[newTargetKey]);
                                            break;
                                    }
                                }
                            }
                        }
                    }

                    break;
                case RelationshipSide.Target:
                    throw new Exception("Assigning a key to a collection is invalid.");
            }
        }
    }
}