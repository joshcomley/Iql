using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Crud.State;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Events;
using Iql.Queryable.Exceptions;
using Iql.Queryable.Extensions;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.Tracking
{
    public class TrackingSet<T> : ITrackingSet where T : class
    {
        private bool _hasPersistenceKeySet;
        private bool _hasPersistenceKey;
        private List<T> _noKeyCheckEntities = new List<T>();
        private List<T> _preChangeCheckEntities = new List<T>();
        private List<T> _silentEntities = new List<T>();
        private Dictionary<T, T> _entitiesByEntityReference = new Dictionary<T, T>();
        private Dictionary<string, T> _entitiesByKey = new Dictionary<string, T>();
        private Dictionary<Guid, T> _entitiesByPersistenceKey = new Dictionary<Guid, T>();
        private Dictionary<T, IEntityStateBase> EntityStates { get; } = new Dictionary<T, IEntityStateBase>();
        public TrackingSet(IDataContext dataContext, TrackingSetCollection trackingSetCollection)
        {
            DataContext = dataContext;
            TrackingSetCollection = trackingSetCollection;
            Set = new List<T>();
            Clone = new List<T>();
            EntityConfiguration = DataContext.EntityConfigurationContext.GetEntityByType(typeof(T));
        }

        public List<T> Set { get; set; }
        public List<T> Clone { get; set; }
        public IEntityConfiguration EntityConfiguration { get; }
        private IDataContext DataContext { get; }
        public TrackingSetCollection TrackingSetCollection { get; }

        List<IUpdateEntityOperation> ITrackingSet.GetChangesInternal(bool reset = false)
        {
            return GetChangesInternal(reset).Cast<IUpdateEntityOperation>().ToList();
        }

        public void Reset()
        {
            Clone = Set.CloneAs(DataContext, typeof(T), RelationshipCloneMode.DoNotClone);
        }

        public IEnumerable<object> TrackedEntites()
        {
            return Set.ToList();
        }

        public Type EntityType => typeof(T);

        object ITrackingSet.Track(object entity, bool isNew)
        {
            return Track((T)entity, isNew);
        }

        void ITrackingSet.Untrack(object entity)
        {
            Untrack((T)entity);
        }

        void ITrackingSet.Merge(IList data, bool isNew)
        {
            Merge((List<T>)data, isNew);
        }

        object ITrackingSet.MergeEntity(object entity, bool isNew)
        {
            return MergeEntity((T)entity, isNew);
        }
        private readonly Dictionary<T, T> _trackedEntityClones = new Dictionary<T, T>();

        public void Untrack(T entity)
        {
            var existingClone = FindClone(entity);
            if (existingClone != null)
            {
                Clone.Remove(existingClone);
                var trackedEntity = FindTrackedEntity(entity);
                if (trackedEntity != null)
                {
                    var entityState = GetEntityState(entity);
                    Set.Remove(trackedEntity);
                    _entitiesByEntityReference.Remove(entity);
                    if (entityState.PersistenceKey.HasValue)
                    {
                        _entitiesByPersistenceKey.Remove(entityState.PersistenceKey.Value);
                    }
                    if (!entityState.Key.HasDefaultValue())
                    {
                        _entitiesByKey.Remove(entityState.Key.AsKeyString());
                    }
                    _trackedEntityClones.Remove(trackedEntity);
                }
            }
        }

        public void Merge(List<T> data, bool isNew)
        {
            var flattened = DataContext.EntityConfigurationContext.FlattenObjectGraphs(typeof(T),
                data.ToArray());
            foreach (var entity in flattened)
            {
                if (entity.EntityType == typeof(T))
                {
                    MergeEntity((T)entity.Entity, isNew);
                    var index = data.IndexOf((T)entity.Entity);
                    if (index != -1)
                    {
                        data[index] = FindTrackedEntity(data[index]);
                    }
                }
                else
                {
                    TrackingSetCollection.TrackingSet(entity.EntityType)
                        .MergeEntity(entity.Entity, isNew);
                }
            }
        }

        public T MergeEntity(T element, bool isNew)
        {
            return TrackInternal(element, true, isNew);
        }

        public T Track(T entity, bool isNew)
        {
            return TrackInternal(entity, false, isNew);
        }

        private Dictionary<IRelatedList, int> _collectionChangeSubscriptions =
            new Dictionary<IRelatedList, int>();
        private Dictionary<T, int> _entityStateChangedSubscriptions =
            new Dictionary<T, int>();
        private Dictionary<T, int> _propertyChangingSubscriptions =
            new Dictionary<T, int>();
        private Dictionary<T, int> _propertyChangedSubscriptions =
            new Dictionary<T, int>();

        private T TrackInternal(T entity, bool allowMerge, bool isNew)
        {
            var existingEntity = FindTrackedEntity(entity);
            if (isNew && EntityConfiguration.Key.IsGeneratedRemotely && DataContext.EntityHasKey(entity, typeof(T)))
            {
                throw new AttemptingToAssignKeyToEntityWhoseKeysAreGeneratedRemotelException();
            }
            var found = existingEntity != null;
            if (!allowMerge && found)
            {
                found = false;
                foreach (var trackedEntity in Set)
                {
                    if (trackedEntity == entity)
                    {
                        found = true;
                        break;
                    }
                    var isTrackedEntityNew = DataContext.IsEntityNew(trackedEntity, typeof(T));
                    if (isTrackedEntityNew != null && isTrackedEntityNew.Value == false
                        && DataContext.IsIdMatch(entity, trackedEntity, typeof(T)))
                    {
                        throw new EntityAlreadyTrackedException("Already tracking an entity with the same key.");
                    }
                }
            }
            if (!allowMerge && found)
            {
                return existingEntity;
            }
            var clone = entity.CloneAs(DataContext, typeof(T), RelationshipCloneMode.DoNotClone);
            if (!found)
            {
                Set.Add(entity);
                var entityState = GetEntityState(entity);
                if (!entityState.Key.HasDefaultValue())
                {
                    _entitiesByKey.Add(entityState.Key.AsKeyString(), entity);
                }
                if (entityState.PersistenceKey != null)
                {
                    _entitiesByPersistenceKey.Add(entityState.PersistenceKey.Value, entity);
                }
                _entitiesByEntityReference.Add(entity, entity);
                if (isNew)
                {
                    entityState.IsNew = isNew;
                }
                Watch(entity);
                Clone.Add(clone);
                _trackedEntityClones.Add(entity, clone);
                AssignToMatchingRelationships(entity);
                return entity;
            }

            if (existingEntity != entity)
            {
                SimplePropertyMerger.Merge(DataContext, TrackingSetCollection, entity, typeof(T));
            }
            _trackedEntityClones[existingEntity] = clone;
            return existingEntity;
        }

        private void AssignToMatchingRelationships(T entity)
        {
            foreach (var relationship in EntityConfiguration.AllRelationships())
            {
                // Find entities with reference keys that match this entity
                var isEntityNew = DataContext.IsEntityNew(entity, typeof(T));
                if ((isEntityNew != null && isEntityNew == false) || DataContext.EntityHasKey(entity, typeof(T)))
                {
                    if (!relationship.OtherEnd.IsCollection)
                    {
                        var trackingSet = TrackingSetCollection.TrackingSet(relationship.OtherEnd.Type);
                        foreach (var trackedEntity in trackingSet.TrackedEntites())
                        {
                            var compositeKey = relationship.ThisEnd.GetCompositeKey(entity, true);
                            if (!compositeKey.HasDefaultValue())
                            {
                                if (DataContext.EntityPropertiesMatch(
                                    trackedEntity,
                                    compositeKey))
                                {
                                    trackedEntity.SetPropertyValue(relationship.OtherEnd.Property.PropertyName,
                                        entity);
                                }
                            }
                        }
                    }
                }
                if (!relationship.ThisEnd.IsCollection)
                {
                    var trackingSet = TrackingSetCollection.TrackingSet(relationship.OtherEnd.Type);
                    var compositeKey = relationship.ThisEnd.GetCompositeKey(entity, true);
                    if (!compositeKey.HasDefaultValue())
                    {
                        var matchedEntity = trackingSet.FindTrackedEntityByKey(compositeKey);
                        if (matchedEntity != null)
                        {
                            entity.SetPropertyValue(relationship.ThisEnd.Property.PropertyName, matchedEntity);
                        }
                    }
                }
            }
        }

        internal void Watch(T entity)
        {
            if (!_entityStateChangedSubscriptions.ContainsKey(entity))
            {
                GetEntityState(entity).MarkedForDeletionChanged.Subscribe(MarkedForDeletionChanged);
            }
            if (!_propertyChangingSubscriptions.ContainsKey(entity))
            {
                var propertyChangingSubscriptionId = (entity as IEntity)?.PropertyChanging?.Subscribe(EntityPropertyChanging);
                if (propertyChangingSubscriptionId.HasValue)
                {
                    _propertyChangingSubscriptions.Add(entity, propertyChangingSubscriptionId.Value);
                }
            }
            if (!_propertyChangedSubscriptions.ContainsKey(entity))
            {
                var propertyChangedSubscriptionId = (entity as IEntity)?.PropertyChanged?.Subscribe(EntityPropertyChanged);
                if (propertyChangedSubscriptionId.HasValue)
                {
                    _propertyChangedSubscriptions.Add(entity, propertyChangedSubscriptionId.Value);
                }
            }
            foreach (var relationship in EntityConfiguration.AllRelationships())
            {
                if (relationship.ThisEnd.IsCollection)
                {
                    var relatedList = entity.GetPropertyValue(relationship.ThisEnd.Property.PropertyName)
                        as IRelatedList;
                    if (relatedList != null && !_collectionChangeSubscriptions.ContainsKey(relatedList))
                    {
                        _collectionChangeSubscriptions.Add(relatedList, relatedList.Changed.Subscribe(RelatedListChanged));
                    }
                }
            }
        }

        private void EntityPropertyChanging(IPropertyChangeEvent pc)
        {
            var entity = (T)pc.Entity;
            if (TrackingSetCollection.ProcessingRelationshipChange)
            {
                return;
            }
            if (_silentEntities.Contains(entity))
            {
                return;
            }
            var property = EntityConfiguration.FindProperty(pc.PropertyName);
            if (!_noKeyCheckEntities.Contains(entity))
            {
                if (property.Kind == PropertyKind.Key && EntityConfiguration.Key.IsGeneratedRemotely
                    && !pc.NewValue.IsDefaultValue())
                {
                    throw new AttemptingToAssignKeyToEntityWhoseKeysAreGeneratedRemotelException();
                }
            }
            if (!_preChangeCheckEntities.Contains(entity))
            {
                if (pc.NewValue == null && !property.Nullable && !Equals(pc.OldValue, pc.NewValue)
#if TypeScript
                    && property.ConvertedFromType != "Guid"
#endif
                )
                {
                    throw new NullNotAllowedException(typeof(T), property.Name);
                }
            }
        }

        private void EntityPropertyChanged(IPropertyChangeEvent pc)
        {
            var entity = (T)pc.Entity;
            // TODO: Watch persistence key and update dictionary
            // TODO: Watch key and update dictionary
            // Both of the above can ignore silent mode
            // TODO: Add FindTrackedRelationships to RelationshipManagerBase
            var entityState = GetEntityState(entity);
            var oldState = entityState.GetPropertyState(pc.PropertyName);
            var property = EntityConfiguration.FindProperty(pc.PropertyName);
            var oldValue = oldState == null ? pc.OldValue : oldState.OldValue;
            var hasChanged = !Equals(pc.OldValue, pc.NewValue);
            if (hasChanged)
            {
                if (pc.PropertyName == "PersistenceKey")
                {
                    if (!Equals(null, pc.OldValue) &&
                        !Equals(pc.OldValue, Guid.Empty) &&
                        _entitiesByPersistenceKey.ContainsKey((Guid)pc.OldValue))
                    {
                        _entitiesByPersistenceKey.Remove((Guid)pc.OldValue);
                    }
                    if (!Equals(null, pc.NewValue) &&
                        !Equals(pc.NewValue, Guid.Empty))
                    {
                        _entitiesByPersistenceKey.Add((Guid)pc.NewValue, entity);
                    }
                }
                else
                {
                    if (property.Kind == PropertyKind.Key)
                    {
                        var newKey = BuildEntityKey(entity);
                        var oldKey = BuildEntityKey(entity);
                        oldKey.Keys.Single(k => k.Name == property.Name).Value = pc.OldValue;
                        var oldKeyString = oldKey.AsKeyString();
                        if (_entitiesByKey.ContainsKey(oldKeyString))
                        {
                            _entitiesByKey.Remove(oldKeyString);
                        }
                        if (!newKey.HasDefaultValue())
                        {
                            var newKeyString = newKey.AsKeyString();
                            _entitiesByKey.Add(newKeyString, entity);
                        }
                    }
                }
            }
            if (_silentEntities.Contains(entity))
            {
                entityState.SetPropertyState(pc.PropertyName, pc.NewValue, pc.NewValue);
            }
            else
            {
                entityState.SetPropertyState(pc.PropertyName, oldValue, pc.NewValue);
                if (hasChanged)
                {
                    if (property.Kind == PropertyKind.Key)
                    {
                        foreach (var relationship in EntityConfiguration.AllRelationships())
                        {
                            switch (relationship.Relationship.Type)
                            {
                                case RelationshipType.OneToOne:
                                case RelationshipType.OneToMany:
                                    if (relationship.ThisIsTarget)
                                    {
                                        foreach (var relatedEntity in TrackingSetCollection
                                            .TrackingSet(relationship.OtherEnd.Type).TrackedEntites())
                                        {
                                            var oldCompositeKey = relationship.ThisEnd.GetCompositeKey(entity, true);
                                            var newCompositeKey = relationship.ThisEnd.GetCompositeKey(entity, true);
                                            foreach (var key in oldCompositeKey.Keys)
                                            {
                                                if (key.Name == pc.PropertyName)
                                                {
                                                    key.Value = pc.OldValue;
                                                }
                                            }
                                            if (Equals(entity,
                                                    relatedEntity.GetPropertyValue(relationship.OtherEnd.Property
                                                        .PropertyName)) ||
                                                DataContext.EntityPropertiesMatch(relatedEntity, oldCompositeKey))
                                            {
                                                relatedEntity.SetPropertyValue(
                                                    relationship.OtherEnd.Property.PropertyName,
                                                    entity);
                                                relatedEntity.SetPropertyValues(newCompositeKey);
                                            }
                                        }
                                    }
                                    break;
                                case RelationshipType.ManyToMany:
                                    throw new NotImplementedException();
                            }
                        }
                    }
                    else if (property.Kind == PropertyKind.RelationshipKey || property.Kind == PropertyKind.Relationship)
                    {
                        //var isRemove = Equals(pc.NewValue, null);
                        var relationshipManager =
                            RelationshipManagerBase.GetRelationshipManager(property.Relationship.Relationship, DataContext);
                        switch (property.Relationship.Relationship.Type)
                        {
                            case RelationshipType.OneToOne:
                                if (property.Relationship.ThisIsTarget)
                                {
                                    switch (property.Kind)
                                    {
                                        case PropertyKind.RelationshipKey:
                                            relationshipManager.ProcessOneToOneKeyChange(entity);
                                            break;
                                        case PropertyKind.Relationship:
                                            relationshipManager.ProcessOneToOneReferenceChange(entity);
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (property.Kind)
                                    {
                                        case PropertyKind.RelationshipKey:
                                            throw new Exception(
                                                "Relationship key on target of one-to-one relationship shouldn't exist.");
                                        case PropertyKind.Relationship:
                                            relationshipManager.ProcessOneToOneInverseReferenceChange(entity);
                                            break;
                                    }
                                }
                                break;
                            case RelationshipType.OneToMany:
                                if (property.Relationship.ThisEnd.IsCollection)
                                {
                                    // We have reassigned a whole new collection;
                                    // unsubscribe from the pervious collection and 
                                    // subscribe to the new one
                                    var oldList = pc.OldValue as IRelatedList;
                                    if (_collectionChangeSubscriptions.ContainsKey(oldList))
                                    {
                                        oldList.Changed.Unsubscribe(_collectionChangeSubscriptions[oldList]);
                                        _collectionChangeSubscriptions.Remove(oldList);
                                    }
                                    var newList = pc.NewValue as IRelatedList;
                                    _collectionChangeSubscriptions.Add(newList, newList.Changed.Subscribe(RelatedListChanged));
                                }
                                else
                                {
                                    switch (property.Kind)
                                    {
                                        case PropertyKind.RelationshipKey:
                                            relationshipManager.ProcessOneToManyKeyChange(entity);
                                            break;
                                        case PropertyKind.Relationship:
                                            relationshipManager.ProcessOneToManyReferenceChange(entity);
                                            break;
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }

        internal void Unwatch(T entity)
        {
            if (_propertyChangingSubscriptions.ContainsKey(entity))
            {
                (entity as IEntity)?.PropertyChanging?.Unsubscribe(
                    _propertyChangingSubscriptions[entity]);
                _propertyChangingSubscriptions.Remove(entity);
            }
            if (_propertyChangedSubscriptions.ContainsKey(entity))
            {
                (entity as IEntity)?.PropertyChanged?.Unsubscribe(
                    _propertyChangedSubscriptions[entity]);
                _propertyChangedSubscriptions.Remove(entity);
            }
            foreach (var relationship in EntityConfiguration.AllRelationships())
            {
                if (relationship.ThisEnd.IsCollection)
                {
                    var relatedList = entity.GetPropertyValue(relationship.ThisEnd.Property.PropertyName)
                        as IRelatedList;
                    if (relatedList != null && _collectionChangeSubscriptions.ContainsKey(relatedList))
                    {
                        relatedList.Changed.Unsubscribe(_collectionChangeSubscriptions[relatedList]);
                        _collectionChangeSubscriptions.Remove(relatedList);
                    }
                }
            }
        }

        private void MarkedForDeletionChanged(MarkedForDeletionChangeEvent markedForDeletionChangeEvent)
        {
            if (!markedForDeletionChangeEvent.NewValue)
            {
                DataContext.DataStore.RemoveQueuedOperationsOfTypeForEntity(
                    markedForDeletionChangeEvent.EntityState.Entity,
                    QueuedOperationType.Delete);
            }
        }

        private void RelatedListChanged(IRelatedListChangedEvent ev)
        {
            var relationship = DataContext.EntityConfigurationContext.GetEntityByType(ev.OwnerType)
                .FindRelationship(ev.List.Property);
            var method = this.GetType().GetMethod(nameof(ProcessOneToManyCollectionChange),
                    BindingFlags.NonPublic | BindingFlags.Instance)
                .MakeGenericMethod(relationship.OtherEnd.Type);
            method.Invoke(this, new object[]
            {
                ev,
                relationship,
#if TypeScript
                relationship.OtherEnd.Type
#endif
            });
        }

        /// <summary>
        /// Customer.Orders.Add([new])
        /// </summary>
        /// <typeparam name="TRelationship"></typeparam>
        private void ProcessOneToManyCollectionChange<TRelationship>(
            RelatedListChangeEvent<T, TRelationship> change,
            RelationshipMatch relationship)
            where TRelationship : class
        {
            var relationshipManager = RelationshipManagerBase.GetRelationshipManager(relationship.Relationship, DataContext);
            switch (change.Kind)
            {
                case RelatedListChangeKind.Assign:
                    relationshipManager.ProcessOneToManyCollectionAdd(change.Owner, change.Item, change.ItemKey);
                    break;
                case RelatedListChangeKind.Remove:
                    relationshipManager.ProcessOneToManyCollectionRemove(change.Owner, change.Item, change.ItemKey);
                    break;
            }
        }

        public T FindClone(T entity)
        {
            var trackedEntity = FindTrackedEntity(entity);
            if (trackedEntity != null && _trackedEntityClones.ContainsKey(trackedEntity))
            {
                return _trackedEntityClones[trackedEntity];
            }
            return null;
        }

        public IEnumerable<AddEntityOperation<T>> GetInserts()
        {
            var inserts = new List<AddEntityOperation<T>>();
            foreach (var entity in Set)
            {
                var entityState = GetEntityState(entity);
                if (entityState.IsNew && !entityState.MarkedForAnyDeletion)
                {
                    inserts.Add(new AddEntityOperation<T>(entity, DataContext));
                }
            }
            return inserts;
        }

        IEnumerable<IEntityCrudOperationBase> ITrackingSet.GetInserts()
        {
            return GetInserts();
        }

        public IEnumerable<DeleteEntityOperation<T>> GetDeletions()
        {
            var deletions = new List<DeleteEntityOperation<T>>();
            foreach (var entity in Set)
            {
                var entityState = GetEntityState(entity);
                if (entityState.MarkedForAnyDeletion && !entityState.IsNew)
                {
                    deletions.Add(new DeleteEntityOperation<T>(entity, DataContext));
                }
            }
            return deletions;
        }

        IEnumerable<IEntityCrudOperationBase> ITrackingSet.GetDeletions()
        {
            return GetDeletions();
        }

        public IEnumerable<UpdateEntityOperation<T>> GetUpdates()
        {
            var updates = new List<UpdateEntityOperation<T>>();
            foreach (var entity in Set)
            {
                var entityState = GetEntityState(entity);
                if (!entityState.IsNew && !entityState.MarkedForAnyDeletion &&
                    entityState.ChangedProperties.Any())
                {
                    updates.Add(new UpdateEntityOperation<T>(entity, DataContext));
                }
            }
            return updates;
        }

        IEnumerable<IUpdateEntityOperation> ITrackingSet.GetUpdates()
        {
            return GetUpdates();
        }

        public ITrackedRelationship NewTrackedRelationship<TOwner, TEntity>(TOwner owner, TEntity entity,
            IRelationship relationship)
        {
            return new TrackedRelationship<TOwner, TEntity>(owner, entity, relationship);
        }

        object ITrackingSet.FindClone(object entity)
        {
            return FindClone((T)entity);
        }

        public List<UpdateEntityOperation<T>> GetChangesInternal(bool reset = false)
        {
            var updates = new List<UpdateEntityOperation<T>>();
            Set.ForEach(entity =>
            {
                var entityState = GetEntityState(entity);
                if (entityState.ChangedProperties.Any())
                {
                    updates.Add(new UpdateEntityOperation<T>(entity, DataContext, entityState));
                }
            });
            if (reset)
            {
                Reset();
            }
            return updates;
        }

        public IEntityStateBase GetEntityState(T entity)
        {
            if (!Set.Contains(entity))
            {
                return null;
            }
            if (!EntityStates.ContainsKey(entity))
            {
                var entityState = new EntityState<T>(entity, typeof(T), DataContext, EntityConfiguration);
                entityState.Key = BuildEntityKey(entity);
                entityState.PersistenceKey = ResolvePersistenceKey(entity);
                EntityStates.Add(entity, entityState);
                return entityState;
            }
            return EntityStates[entity];
        }

        private Guid? ResolvePersistenceKey(T entity)
        {
            IProperty persistenceKeyProperty = null;
            if (!_hasPersistenceKeySet)
            {
                _hasPersistenceKeySet = true;
                persistenceKeyProperty = EntityConfiguration.Properties.SingleOrDefault(
                    p => p.Name == "PersistenceKey");
                _hasPersistenceKey = persistenceKeyProperty != null;
            }
            if (_hasPersistenceKey)
            {
                if (persistenceKeyProperty == null)
                {
                    persistenceKeyProperty = EntityConfiguration.Properties.SingleOrDefault(
                        p => p.Name == "PersistenceKey");
                }
                var persistenceKey = (Guid)entity.GetPropertyValue(persistenceKeyProperty.Name);
                if (persistenceKey != Guid.Empty)
                {
                    return persistenceKey;
                }
            }
            return null;
        }

        private CompositeKey BuildEntityKey(T entity)
        {
            var compositeKey = new CompositeKey();
            foreach (var keyProperty in EntityConfiguration.Key.Properties)
            {
                compositeKey.Keys.Add(new KeyValue(
                    keyProperty.PropertyName,
                    entity.GetPropertyValue(keyProperty.PropertyName),
                    null));
            }
            return compositeKey;
        }

        public void SilentlyChangeEntity(T entity, Action action)
        {
            ChangeEntity(entity, action, ChangeEntityMode.Silent);
        }

        public async Task ChangeEntityAsync(T entity, Func<Task> action, ChangeEntityMode mode)
        {
            // Instead of unwatching, we can check if an entity is being
            // silently changed, in which case we reset the property state
            // for any updates
            switch (mode)
            {
                case ChangeEntityMode.Normal:
                    await action();
                    break;
                case ChangeEntityMode.Silent:
                    var hasSilent = _silentEntities.Contains(entity);
                    if (!hasSilent)
                    {
                        _silentEntities.Add(entity);
                    }
                    await action();
                    if (!hasSilent)
                    {
                        _silentEntities.Remove(entity);
                    }
                    break;
                case ChangeEntityMode.NoNullChecks:
                    var hasPreCheck = _preChangeCheckEntities.Contains(entity);
                    if (!hasPreCheck)
                    {
                        _preChangeCheckEntities.Add(entity);
                    }
                    await action();
                    if (!hasPreCheck)
                    {
                        _preChangeCheckEntities.Remove(entity);
                    }
                    break;
                case ChangeEntityMode.NoKeyChecks:
                    var hasNoKeyCheck = _noKeyCheckEntities.Contains(entity);
                    if (!hasNoKeyCheck)
                    {
                        _noKeyCheckEntities.Add(entity);
                    }
                    await action();
                    if (!hasNoKeyCheck)
                    {
                        _noKeyCheckEntities.Remove(entity);
                    }
                    break;
            }
        }

        public void ChangeEntity(T entity, Action action, ChangeEntityMode mode)
        {
#pragma warning disable 4014
            ChangeEntityAsync(entity, async () =>
#pragma warning restore 4014
            {
                action();
            }, mode);
        }

        void ITrackingSet.SilentlyChangeEntity(object entity, Action action)
        {
            SilentlyChangeEntity((T)entity, action);
        }

        void ITrackingSet.ChangeEntity(object entity, Action action, ChangeEntityMode mode)
        {
            ChangeEntity((T)entity, action, mode);
        }
        async Task ITrackingSet.ChangeEntityAsync(object entity, Func<Task> action, ChangeEntityMode mode)
        {
            await ChangeEntityAsync((T)entity, action, mode);
        }

        public bool IsTracked(object entity)
        {
            var trackedEntity = FindTrackedEntity((T)entity);
            return trackedEntity != null && trackedEntity == entity;
        }

        void ITrackingSet.Watch(object entity)
        {
            Watch((T)entity);
        }

        void ITrackingSet.Unwatch(object entity)
        {
            Unwatch((T)entity);
        }

        IEntityStateBase ITrackingSet.GetEntityState(object entity)
        {
            return GetEntityState((T)entity);
        }

        public T FindTrackedEntity(T entity)
        {
            if (_entitiesByEntityReference.ContainsKey(entity))
            {
                return entity;
            }
            var key = BuildEntityKey(entity);
            if (!key.HasDefaultValue())
            {
                var asKeyString = key.AsKeyString();
                if (_entitiesByKey.ContainsKey(asKeyString))
                {
                    return _entitiesByKey[asKeyString];
                }
            }
            var persistenceKey = ResolvePersistenceKey(entity);
            if (persistenceKey != null && _entitiesByPersistenceKey.ContainsKey(persistenceKey.Value))
            {
                return _entitiesByPersistenceKey[persistenceKey.Value];
            }
            return null;
        }

        public T FindTrackedEntityByKey(CompositeKey key)
        {
            if (key.HasDefaultValue())
            {
                return null;
            }
            var keyString = key.AsKeyString();
            if (_entitiesByKey.ContainsKey(keyString))
            {
                return _entitiesByKey[keyString];
            }
            return null;
        }

        object ITrackingSet.FindTrackedEntity(object entity)
        {
            return FindTrackedEntity((T)entity);
        }

        object ITrackingSet.FindTrackedEntityByKey(CompositeKey key)
        {
            return FindTrackedEntityByKey(key);
        }
    }
}