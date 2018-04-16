using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Iql.Extensions;
using Iql.Queryable.Data.Context;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Data.Lists;
using Iql.Queryable.Data.Tracking;
using Iql.Queryable.Data.Tracking.State;
using Iql.Queryable.Events;
using Iql.Queryable.Extensions;

namespace Iql.Queryable.Data.Relationships
{
    public class RelationshipObserver : IRelationshipObserver
    {
        private readonly PropertyChangeIgnorer _propertyChangeIgnorer =
            new PropertyChangeIgnorer();
        private readonly Dictionary<object, object> _moving = new Dictionary<object, object>();
        private readonly Dictionary<IRelationship, Dictionary<string, object>> _oneToTargetRelationshipKeyMaps
            = new Dictionary<IRelationship, Dictionary<string, object>>();
        private readonly Dictionary<IRelationship, Dictionary<string, Dictionary<object, object>>> _oneToSourceRelationshipKeyMaps
            = new Dictionary<IRelationship, Dictionary<string, Dictionary<object, object>>>();
        private readonly Dictionary<IRelationship, Dictionary<object, string>> _ids =
            new Dictionary<IRelationship, Dictionary<object, string>>();
        //private readonly Dictionary<object, Dictionary<IRelationship, string>> _relationshipKeys
        //    = new Dictionary<object, Dictionary<IRelationship, string>>();

        static RelationshipObserver()
        {
            WatchListTypedMethod = typeof(RelationshipObserver)
                .GetMethod(nameof(WatchListTyped));
            PairListTypedMethod = typeof(RelationshipObserver)
                .GetMethod(nameof(PairListTyped));
            MapListTypedMethod = typeof(RelationshipObserver)
                .GetMethod(nameof(MapListTyped));
        }

        public static MethodInfo MapListTypedMethod { get; set; }
        public static MethodInfo PairListTypedMethod { get; set; }
        public static MethodInfo WatchListTypedMethod { get; set; }
        public IDataContext DataContext { get; }
        public TrackingSetCollection TrackingSetCollection { get; }
        public bool TrackEntities { get; }

        public RelationshipObserver(IDataContext dataContext,
            TrackingSetCollection trackingSetCollection,
            bool trackEntities)
        {
            DataContext = dataContext;
            TrackingSetCollection = trackingSetCollection;
            TrackEntities = trackEntities;
            EntityConfigurationContext = TrackingSetCollection.DataStore.DataContext.EntityConfigurationContext;
        }

        public EntityConfigurationBuilder EntityConfigurationContext { get; set; }

        public void RunIfNotIgnored(Action action, IProperty property, object entity)
        {
            _propertyChangeIgnorer.RunIfNotAlreadyIgnored(action,
                new[] {property},
                entity
            );
        }

        public void ObserveAll(Dictionary<Type, IList> dictionary)
        {
            var filtered = new Dictionary<Type, IList>();
            foreach (var grouping in dictionary)
            {
                var filteredList = new object[] { }.ToList(grouping.Key);
                foreach (var item in grouping.Value)
                {
                    if (!_observed.ContainsKey(item))
                    {
                        filteredList.Add(item);
                    }
                }
                filtered.Add(grouping.Key, filteredList);
            }

            foreach (var item in filtered)
            {
                WatchListTypedMethod.InvokeGeneric(
                    this,
                    new object[] { item.Value },
                    item.Key);
            }

            foreach (var item in filtered)
            {
                MapListTypedMethod.InvokeGeneric(
                    this,
                    new object[] { item.Value },
                    item.Key);
            }

            foreach (var item in filtered)
            {
                PairListTypedMethod.InvokeGeneric(
                    this,
                    new object[] { item.Value },
                    item.Key);
            }
        }

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

        public void Unobserve(object entity, Type entityType)
        {
            if (_observed.ContainsKey(entity))
            {
                _observed[entity].Unobserve();
                _observed.Remove(entity);
            }
        }

        public bool IsAssignedToAnyRelationship(object entity, Type entityType)
        {
            if (_moving.ContainsKey(entity))
            {
                return true;
            }
            var entityConfiguration = EntityConfigurationContext.GetEntityByType(entityType);
            var relationships = entityConfiguration.AllRelationships();
            for (var i = 0; i < relationships.Count; i++)
            {
                var relationship = relationships[i];
                if (relationship.ThisIsTarget &&
                    _oneToSourceRelationshipKeyMaps.ContainsKey(relationship.Relationship) &&
                    _oneToTargetRelationshipKeyMaps.ContainsKey(relationship.Relationship))
                {
                    var mapping = _oneToTargetRelationshipKeyMaps[relationship.Relationship];
                    var key = GetTargetKeyString(entity, relationship.Relationship);
                    if (mapping.ContainsKey(key))
                    {
                        var sourceMaps = _oneToSourceRelationshipKeyMaps[relationship.Relationship];
                        if (sourceMaps.ContainsKey(key) && sourceMaps[key].Count > 0)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
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
                    entity.SetPropertyValue(
                        relationship.Relationship.Source.Property,
                        null);
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

        private string GetRelationshipKeyString(
            object entity,
            IRelationship relationship,
            Action<CompositeKey> modifyCompositeKey = null,
            bool keyOnly = false
            )
        {
            string targetKey = null;
            if (!keyOnly)
            {
                var target = relationship.Source.Property.PropertyGetter(entity);
                if (target != null)
                {
                    var targetTracking = TrackingSetCollection.TrackingSetByType(relationship.Target.Type);
                    var state = targetTracking.GetEntityState(target);
                    targetKey = GetTargetKeyString(target, relationship);
                    if (state.IsNew)
                    {
                        return targetKey;
                    }
                }
            }

            var constraintKey = GetRelationshipKeyStringFromConstraints(entity, relationship, modifyCompositeKey);
            if (constraintKey == null && !keyOnly)
            {
                return targetKey;
            }
            return constraintKey;
        }

        private static string GetRelationshipKeyStringFromConstraints(object entity, IRelationship relationship,
            Action<CompositeKey> modifyCompositeKey)
        {
            var key = relationship.Source.GetCompositeKey(entity);
            if (modifyCompositeKey != null)
            {
                modifyCompositeKey(key);
            }

            var keyString = key.HasDefaultValue()
                ? null
                : key.AsKeyString(false);
            return keyString;
        }

        //private Dictionary<IRelationship, string> GetEntityRelationshipKeyMap(object entity, IRelationship relationship)
        //{
        //    var entityRelationshipKeyMap = _relationshipKeys.Ensure(entity, () => new Dictionary<IRelationship, string>());

        //    if (!entityRelationshipKeyMap.ContainsKey(relationship))
        //    {
        //        entityRelationshipKeyMap.Add(relationship, null);
        //    }

        //    return entityRelationshipKeyMap;
        //}

        private readonly Dictionary<object, EntityObserver> _observed = new Dictionary<object, EntityObserver>();
        public void WatchListTyped<T>(List<T> entities) where T : class
        {
            // Subscribe to the entity property change events
            var trackingSet = TrackingSetCollection.TrackingSet<T>();
            var entityConfiguration = trackingSet.EntityConfiguration;
            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                if (!_observed.ContainsKey(entity))
                {
                    var entityObserver = new EntityObserver(trackingSet.GetEntityState(entity));
                    _observed.Add(entity, entityObserver);
                    entityObserver.RegisterMarkForDeletionChanged(MarkedForDeletionChange);
                    entityObserver.RegisterPropertyChanged(e => { PropertyChangeEvent(e, entityConfiguration); });
                    entityObserver.RegisterRelatedListChanged(RelatedListChanged);
                }
            }
        }

        private void RelatedListChanged(IRelatedListChangeEvent relatedListChangeEvent)
        {
            var listProperty = EntityConfigurationByType(relatedListChangeEvent.OwnerType).FindProperty(
                relatedListChangeEvent.List.PropertyName);
            var sourceProperty = listProperty.Relationship.Relationship.Source.Property;
            if (!_propertyChangeIgnorer.AreAnyIgnored(new[] { listProperty }, relatedListChangeEvent.Owner))
            {
                switch (relatedListChangeEvent.Kind)
                {
                    case RelatedListChangeKind.Added:
                        relatedListChangeEvent.Item.SetPropertyValue(
                            sourceProperty,
                            relatedListChangeEvent.Owner);
                        break;
                    case RelatedListChangeKind.Removed:
                        relatedListChangeEvent.Item.SetPropertyValue(
                            sourceProperty,
                            null);
                        break;
                    case RelatedListChangeKind.AssignByKey:
                        var state = TrackingSetCollection.TrackingSetByType(
                                relatedListChangeEvent.ItemType)
                            .GetEntityStateByKey(relatedListChangeEvent.ItemKey);
                        if (state != null)
                        {
                            relatedListChangeEvent.List.Add(state.Entity);
                        }
                        break;
                }
            }
        }

        private void PropertyChangeEvent(
            IPropertyChangeEvent propertyChangeEvent,
            IEntityConfiguration entityConfiguration
            )
        {
            var property = entityConfiguration.FindProperty(propertyChangeEvent.PropertyName);
            _propertyChangeIgnorer.RunIfNotAlreadyIgnored(
                () =>
                {
                    if (property.Kind.HasFlag(PropertyKind.Key))
                    {
                        ProcessTargetKeyChange(propertyChangeEvent.Entity, entityConfiguration);
                    }
                    if (property.Kind.HasFlag(PropertyKind.RelationshipKey))
                    {
                        ProcessRelationshipKeyChange(
                            propertyChangeEvent.Entity,
                            property,
                            propertyChangeEvent.OldValue);
                    }
                    else if (property.Kind.HasFlag(PropertyKind.Relationship))
                    {
                        if (!property.TypeDefinition.IsCollection)
                        {
                            ProcessRelationshipReferenceChange(
                                propertyChangeEvent.Entity,
                                property,
                                propertyChangeEvent.OldValue,
                                propertyChangeEvent.NewValue);
                        }
                    }
                },
                new[] { property },
                propertyChangeEvent.Entity
            );
        }

        private void ProcessRelationshipReferenceChange(
            object entity,
            IProperty property,
            object oldValue,
            object newValue)
        {
            if (TrackEntities && newValue != null)
            {
                var targetType = property.Relationship.Relationship.Target.Type;
                var targetTrackingSet = TrackingSetCollection
                    .TrackingSetByType(targetType);
                if (!targetTrackingSet
                    .IsTracked(newValue))
                {
                    DataContext.DataStore.Add(newValue);
                    Observe(newValue, targetType);
                }
            }
            var relationship = property.Relationship.Relationship;
            var oldRelationshipKey = oldValue != null
                ? GetTargetKeyString(oldValue, relationship)
                : GetRelationshipKeyString(
                    entity,
                    relationship,
                    null,
                    true);
            var newRelationshipKey = GetTargetKeyString(newValue, relationship);
            ChangeRelationship(
                relationship,
                oldRelationshipKey,
                newRelationshipKey,
                entity);
        }

        private void ProcessRelationshipKeyChange(
            object entity,
            IProperty property,
            object oldValue)
        {
            var relationship = property.Relationship.Relationship;
            property.Relationship.ThisEnd.MarkDirty(entity);
            var oldRelationshipKey = GetRelationshipKeyString(
                entity,
                relationship,
                compositeKey =>
                {
                    compositeKey.Keys.Single(k => k.Name == property.Name)
                        .Value = oldValue;
                });
            property.Relationship.ThisEnd.MarkDirty(entity);
            var newRelationshipKey = GetRelationshipKeyString(
                entity,
                relationship);

            ChangeRelationship(
                relationship,
                oldRelationshipKey,
                newRelationshipKey,
                entity);
        }

        private void ProcessTargetKeyChange(object entity, IEntityConfiguration entityConfiguration)
        {
            foreach (var relationship in entityConfiguration.AllRelationships())
            {
                if (relationship.ThisIsTarget)
                {
                    var idsMap = _ids.Ensure(relationship.Relationship, () => new Dictionary<object, string>());
                    var newId = GetTargetKeyString(entity, relationship.Relationship);
                    string oldId = null;
                    if (idsMap.ContainsKey(entity))
                    {
                        oldId = idsMap[entity];
                        idsMap[entity] = newId;
                    }
                    else
                    {
                        idsMap.Add(entity, newId);
                    }

                    var targetMapping = _oneToTargetRelationshipKeyMaps
                        .Ensure(relationship.Relationship, () => new Dictionary<string, object>());
                    if (oldId != null)
                    {
                        targetMapping.Remove(oldId);
                    }
                    targetMapping.Add(newId, entity);

                    Dictionary<string, Dictionary<object, object>> sourceMapping;
                    if (!_oneToSourceRelationshipKeyMaps.ContainsKey(relationship.Relationship))
                    {
                        sourceMapping = new Dictionary<string, Dictionary<object, object>>();
                        _oneToSourceRelationshipKeyMaps.Add(relationship.Relationship, sourceMapping);
                    }
                    else
                    {
                        sourceMapping = _oneToSourceRelationshipKeyMaps[relationship.Relationship];
                    }
                    if (oldId != null)
                    {
                        if (sourceMapping.ContainsKey(oldId))
                        {
                            var mapping = sourceMapping[oldId];
                            var sources = mapping.Keys.ToList();
                            foreach (var source in sources)
                            {
                                ChangeRelationship(
                                    relationship.Relationship,
                                    oldId,
                                    newId,
                                    source);
                            }
                            sourceMapping.Remove(oldId);
                        }
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
                if (relationship.ThisIsTarget && !relationship.Relationship.Source.Property.TypeDefinition.Nullable)
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

        public void MapListTyped<T>(List<T> entities)
            where T : class
        {
            // Map all relationships
            var entityConfiguration = EntityConfiguration<T>();
            var relationshipMatches = entityConfiguration.AllRelationships();
            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                foreach (var relationship in relationshipMatches)
                {
                    if (relationship.ThisIsTarget)
                    {
                        var targetKeyString = GetTargetKeyString(entity, relationship.Relationship);
                        var targetRelationshipKeyMaps = _oneToTargetRelationshipKeyMaps
                            .Ensure(relationship.Relationship, () => new Dictionary<string, object>());
                        targetRelationshipKeyMaps.Ensure(targetKeyString, () => entity);
                    }
                    else
                    {
                        var relationshipKeyString = GetRelationshipKeyString(entity, relationship.Relationship);
                        var sourceRelationshipKeyMaps = _oneToSourceRelationshipKeyMaps
                            .Ensure(relationship.Relationship, () => new Dictionary<string, Dictionary<object, object>>());
                        if (relationshipKeyString != null)
                        {
                            var sourceMap = sourceRelationshipKeyMaps.Ensure(
                                relationshipKeyString,
                                () => new Dictionary<object, object>());
                            sourceMap.Ensure(entity, () => entity);
                        }
                    }
                }
            }
        }

        private IEntityConfiguration EntityConfigurationByType(Type type)
        {
            return TrackingSetCollection.TrackingSetByType(type).EntityConfiguration;
        }

        private EntityConfiguration<T> EntityConfiguration<T>()
            where T : class
        {
            return TrackingSetCollection.TrackingSet<T>().EntityConfiguration;
        }

        public void PairListTyped<T>(List<T> entities)
            where T : class
        {
            var entityConfiguration = EntityConfiguration<T>();
            var relationshipMatches = entityConfiguration.AllRelationships();
            // Pair up any relationships using the mappings
            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                foreach (var relationship in relationshipMatches)
                {
                    if (relationship.ThisIsTarget)
                    {
                        var targetKeyString = GetTargetKeyString(entity, relationship.Relationship);
                        var relationshipSourceMap = _oneToSourceRelationshipKeyMaps
                            .Ensure(
                                relationship.Relationship,
                                () => new Dictionary<string, Dictionary<object, object>>());
                        var targetSourceMap = relationshipSourceMap
                            .Ensure(targetKeyString, () => new Dictionary<object, object>());
                        var dealtWith = new Dictionary<object, object>();
                        foreach (var item in targetSourceMap)
                        {
                            ChangeRelationship(
                                relationship.Relationship,
                                null,
                                targetKeyString,
                                item.Value);
                            dealtWith.Add(item.Key, item.Key);
                        }

                        if (relationship.ThisEnd.IsCollection)
                        {
                            var list = entity.GetPropertyValueAs<IRelatedList>(relationship.ThisEnd.Property);
                            foreach (var item in list)
                            {
                                if (!dealtWith.ContainsKey(item))
                                {
                                    ChangeRelationship(
                                        relationship.Relationship,
                                        null,
                                        targetKeyString,
                                        item);
                                }
                            }
                        }
                    }
                    else
                    {
                        var sourceRelationshipKeyString = GetRelationshipKeyString(entity, relationship.Relationship);
                        if (sourceRelationshipKeyString != null)
                        {
                            var targetMap = _oneToTargetRelationshipKeyMaps
                                .Ensure(relationship.Relationship, () => new Dictionary<string, object>());
                            if (targetMap.ContainsKey(sourceRelationshipKeyString))
                            {
                                ChangeRelationship(
                                    relationship.Relationship,
                                    null,
                                    sourceRelationshipKeyString,
                                    entity);
                            }
                        }
                    }
                }
            }
        }

        private void ChangeRelationship(IRelationship relationship,
            string oldRelationshipKey,
            string newRelationshipKey,
            object source)
        {
            if (oldRelationshipKey == newRelationshipKey)
            {
                return;
            }
            if (oldRelationshipKey != null)
            {
                var relationshipSourceMaps = _oneToSourceRelationshipKeyMaps.Ensure(
                    relationship,
                    () => new Dictionary<string, Dictionary<object, object>>());
                var relationshipSourceMap = relationshipSourceMaps.Ensure(
                    oldRelationshipKey,
                    () => new Dictionary<object, object>());
                relationshipSourceMap.Remove(source);
            }
            if (newRelationshipKey != null)
            {
                var relationshipSourceMaps = _oneToSourceRelationshipKeyMaps.Ensure(
                    relationship,
                    () => new Dictionary<string, Dictionary<object, object>>());
                var relationshipSourceMap = relationshipSourceMaps.Ensure(
                    newRelationshipKey,
                    () => new Dictionary<object, object>());
                if (!relationshipSourceMap.ContainsKey(source))
                {
                    relationshipSourceMap.Add(source, source);
                }
            }

            var targetMapping = _oneToTargetRelationshipKeyMaps
                .Ensure(
                    relationship,
                    () => new Dictionary<string, object>());
            var oldTarget = oldRelationshipKey != null && targetMapping.ContainsKey(oldRelationshipKey)
                ? targetMapping[oldRelationshipKey]
                : null;
            var newTarget = newRelationshipKey != null && targetMapping.ContainsKey(newRelationshipKey)
                ? targetMapping[newRelationshipKey]
                : null;

            var removeFromMoving = false;
            if (newTarget != null || newRelationshipKey != null)
            {
                if (!_moving.ContainsKey(source))
                {
                    removeFromMoving = true;
                    _moving.Add(source, source);
                }
            }
            switch (relationship.Kind)
            {
                case RelationshipKind.OneToOne:
                    if (oldTarget != null)
                    {
                        _propertyChangeIgnorer.IgnoreAndRunEvenIfAlreadyIgnored(
                            () =>
                            {
                                oldTarget.SetPropertyValue(relationship.Target.Property, null);
                            },
                            new[] { relationship.Target.Property }, oldTarget);
                    }
                    if (newTarget != null)
                    {
                        _propertyChangeIgnorer.IgnoreAndRunEvenIfAlreadyIgnored(
                            () =>
                            {
                                newTarget.SetPropertyValue(relationship.Target.Property, source);
                            },
                            new[] { relationship.Target.Property }, newTarget);
                    }
                    _propertyChangeIgnorer.IgnoreAndRunEvenIfAlreadyIgnored(
                        () =>
                        {
                            source.SetPropertyValue(relationship.Source.Property, newTarget);
                        },
                        new[] { relationship.Source.Property }, source);
                    break;
                case RelationshipKind.OneToMany:
                    if (oldTarget != null)
                    {
                        var oldList = oldTarget.GetPropertyValueAs<IList>(relationship.Target.Property);
                        _propertyChangeIgnorer.IgnoreAndRunEvenIfAlreadyIgnored(
                            () =>
                            {
                                oldList.Remove(source);
                            },
                            new[] { relationship.Target.Property }, oldTarget);
                    }
                    if (newTarget != null)
                    {
                        var newList = newTarget.GetPropertyValueAs<IList>(relationship.Target.Property);
                        if (!newList.Contains(source))
                        {
                            _propertyChangeIgnorer.IgnoreAndRunEvenIfAlreadyIgnored(
                                () =>
                                {
                                    newList.Add(source);
                                },
                                new[] { relationship.Target.Property }, newTarget);
                        }
                    }
                    _propertyChangeIgnorer.IgnoreAndRunEvenIfAlreadyIgnored(
                        () =>
                        {
                            source.SetPropertyValue(relationship.Source.Property, newTarget);
                        },
                        new[] { relationship.Source.Property }, source);
                    break;
            }

            if (newTarget != null || newRelationshipKey == null)
            {
                foreach (var constraint in relationship.Constraints)
                {
                    //DataContext.DataStore.DataTracker.
                    _propertyChangeIgnorer.IgnoreAndRunEvenIfAlreadyIgnored(
                        () =>
                        {
                            object newValue = null;
                            if (newTarget != null)
                            {
                                newValue = newTarget.GetPropertyValue(constraint.TargetKeyProperty);
                            }
                            else if (!constraint.SourceKeyProperty.TypeDefinition.Nullable)
                            {
                                newValue = constraint.SourceKeyProperty.TypeDefinition.DefaultValue();
                            }

                            source.SetPropertyValue(
                                constraint.SourceKeyProperty,
                                newValue
                            );
                        },
                        new[] { constraint.SourceKeyProperty }, source);
                }

                relationship.Source.MarkDirty(source);
            }

            if (removeFromMoving)
            {
                _moving.Remove(source);
            }
        }
    }
}