using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Crud.Operations.Queued;
using Iql.Queryable.Data.Crud.State;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Exceptions;
using Iql.Queryable.Extensions;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.Tracking
{
    public class TrackingSet<T> : ITrackingSet where T : class
    {
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
                    Set.Remove(trackedEntity.Entity);
                    _trackedEntityClones.Remove(trackedEntity.Entity);
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
                        data[index] = FindTrackedEntity(data[index]).Entity;
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
            //var flattened = DataContext.EntityConfigurationContext.FlattenObjectGraph(element, typeof(T));
            //foreach (var entity in flattened)
            //{
            //    if (entity.EntityType == typeof(T))
            //    {
            //        TrackInternal((T)entity.Entity, true);
            //    }
            //    else
            //    {
            //        TrackingSetCollection.TrackingSet(entity.EntityType)
            //            .MergeEntity(entity.Entity);
            //    }
            //}
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
                return existingEntity.Entity;
            }
            var clone = entity.CloneAs(DataContext, typeof(T), RelationshipCloneMode.DoNotClone);
            if (!found)
            {
                Set.Add(entity);
                var entityState = GetEntityState(entity);
                if (isNew)
                {
                    entityState.IsNew = isNew;
                }
                //entityState.IsNew = DataContext.IsEntityNew(entity, typeof(T));
                Watch(entity);
                //                foreach (var relationship in EntityConfiguration.AllRelationships())
                //                {
                //                    if (relationship.ThisEnd.IsCollection)
                //                    {
                //                        var relatedList = entity.GetPropertyValue(relationship.ThisEnd.Property.PropertyName)
                //                            as IRelatedList;
                //                        relatedList.Changed.Subscribe(ev =>
                //                        {
                //                            var method = this.GetType().GetMethod(nameof(ProcessOneToManyCollectionChange),
                //                                BindingFlags.NonPublic | BindingFlags.Instance)
                //                                .MakeGenericMethod(relationship.OtherEnd.Type);
                //                            method.Invoke(this, new[]
                //                            {
                //                                ev,
                //                                relationship,
                //#if TypeScript
                //                                relationship.OtherEnd.Type
                //#endif
                //                            });
                //                            // Do something
                //                        });
                //                    }
                //                }
                Clone.Add(clone);
                _trackedEntityClones.Add(entity, clone);
                AssignToMatchingRelationships(entity);
                return entity;
            }

            if (existingEntity.Entity != entity)
            {
                SimplePropertyMerger.Merge(DataContext, TrackingSetCollection, entity, typeof(T));
                //SilentlyChangeEntity(existingEntity.Entity, () =>
                //{
                //});
            }
            _trackedEntityClones[existingEntity.Entity] = clone;
            //var entityConfiguration = DataContext.EntityConfigurationContext.GetEntityByType(entity.GetType());
            //var relationships = entityConfiguration.Relationships;
            //foreach (var relationship in relationships)
            //{
            //    var end = relationship.Source.Configuration == entityConfiguration
            //        ? relationship.Source
            //        : relationship.Target;
            //    var entityRelationshipValue = entity.GetPropertyValue(end.Property.PropertyName);
            //    if (entityRelationshipValue == null)
            //    {
            //        continue;
            //    }
            //    if (entityRelationshipValue is IEnumerable && !(entityRelationshipValue is string))
            //    {
            //        var enumerable = (IEnumerable)entityRelationshipValue;
            //        var cloneEnumerable = ((IEnumerable)clone.GetPropertyValue(end.Property.PropertyName))
            //            .Cast<object>().ToArray();
            //        int i = 0;
            //        foreach (var item in enumerable)
            //        {
            //            TrackingSetCollection.TrackWithClone(item, cloneEnumerable[i]);
            //            i++;
            //        }
            //    }
            //    else
            //    {
            //        TrackingSetCollection.TrackWithClone(entityRelationshipValue,
            //            clone.GetPropertyValue(end.Property.PropertyName));
            //    }
            //}
            return existingEntity.Entity;
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
                            entity.SetPropertyValue(relationship.ThisEnd.Property.PropertyName, matchedEntity.Entity);
                        }
                    }
                }
            }
            //foreach (var config in DataContext.EntityConfigurationContext.AllConfigurations())
            //{
            //    foreach (var relationship in config.Relationships)
            //    {
            //        IRelationshipDetail otherEnd = null;
            //        IRelationshipDetail thisEnd = null;
            //        if (relationship.Source.Type == typeof(T) && !relationship.Target.IsCollection)
            //        {
            //            var trackingSet = TrackingSetCollection.TrackingSet(relationship.Target.Type);
            //            foreach (var trackedEntity in trackingSet.TrackedEntites())
            //            {
            //                if (DataContext.EntityPropertiesMatch(
            //                    trackedEntity,
            //                    relationship.Source.GetCompositeKey(entity, true)))
            //                {
            //                    trackedEntity.SetPropertyValue(relationship.Target.Property.PropertyName,
            //                        entity);
            //                }
            //            }
            //        }
            //    }
            //}
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

        internal void Watch(T entity)
        {
            if (!_entityStateChangedSubscriptions.ContainsKey(entity))
            {
                GetEntityState(entity).MarkedForDeletionChanged.Subscribe(MarkedForDeletionChanged);
            }
            if (!_propertyChangingSubscriptions.ContainsKey(entity))
            {
                var propertyChangingSubscriptionId = (entity as IEntity)?.PropertyChanging?.Subscribe(pc =>
                {
                    if (TrackingSetCollection.ProcessingRelationshipChange)
                    {
                        return;
                    }
                    if (_silentEntities.Contains(entity) || _preChangeCheckEntities.Contains(entity))
                    {
                        return;
                    }
                    var property = EntityConfiguration.FindProperty(pc.PropertyName);
                    if (pc.NewValue == null && !property.Nullable && !Equals(pc.OldValue, pc.NewValue)
#if TypeScript
                    && property.ConvertedFromType != "Guid"
#endif
                )
                    {
                        throw new NullNotAllowedException(typeof(T), property.Name);
                    }
                    //switch (property.Kind)
                    //{
                    //    case PropertyKind.Relationship:
                    //    case PropertyKind.RelationshipKey:
                    //        if (!Equals(pc.OldValue, pc.NewValue))
                    //        {
                    //            switch (property.Relationship.ThisEnd.Relationship.Type)
                    //            {
                    //                case RelationshipType.ManyToMany:
                    //                case RelationshipType.OneToMany:
                    //                    var entityState = GetEntityState(entity);
                    //                    var oldState = entityState.GetPropertyState(pc.PropertyName);
                    //                    var oldValue = oldState == null ? pc.OldValue : oldState.OldValue;
                    //                    var relatedTrackingSet = TrackingSetCollection.TrackingSet(property.Relationship.OtherEnd.Type);
                    //                    if (property.Relationship.OtherEnd.IsCollection)
                    //                    {
                    //                        var relatedEntity =
                    //                            entity.GetPropertyValue(property.Relationship.ThisEnd.Property.PropertyName);
                    //                        CompositeKey compositeKeyForNewRelationship;
                    //                        CompositeKey compositeKeyForOldRelationship;
                    //                        if (property.Kind == PropertyKind.RelationshipKey)
                    //                        {
                    //                            compositeKeyForNewRelationship =
                    //                                property.Relationship.ThisEnd.GetCompositeKey(entity);
                    //                            compositeKeyForNewRelationship.Keys.Single(k => k.Name == pc.PropertyName).Value = pc.NewValue;
                    //                            compositeKeyForNewRelationship =
                    //                                property.Relationship.ThisEnd.GetCompositeKey(
                    //                                    compositeKeyForNewRelationship, true);

                    //                            compositeKeyForOldRelationship =
                    //                                property.Relationship.ThisEnd.GetCompositeKey(entity);
                    //                            compositeKeyForOldRelationship.Keys.Single(k => k.Name == pc.PropertyName).Value = pc.OldValue;
                    //                            compositeKeyForOldRelationship =
                    //                                property.Relationship.ThisEnd.GetCompositeKey(
                    //                                    compositeKeyForOldRelationship, true);
                    //                        }
                    //                        else
                    //                        {
                    //                            compositeKeyForNewRelationship =
                    //                                pc.NewValue == null ? null : property.Relationship.OtherEnd.GetCompositeKey(pc.NewValue);
                    //                            compositeKeyForOldRelationship =
                    //                                oldValue == null ? null : property.Relationship.OtherEnd.GetCompositeKey(oldValue);
                    //                        }
                    //                        //relatedList.AssignRelationshipByKey(compositeKey);
                    //                        var trackedEntity = relatedTrackingSet.FindTrackedEntityByKey(compositeKeyForNewRelationship);
                    //                        if (trackedEntity != null)
                    //                        {
                    //                            var relatedList = trackedEntity.Entity.GetPropertyValue(property.Relationship.OtherEnd.Property
                    //                                .PropertyName) as IRelatedList;
                    //                            relatedList.AssignRelationship(entity);
                    //                        }
                    //                    }
                    //                    break;
                    //            }
                    //        }
                    //        break;
                    //}
                });
                if (propertyChangingSubscriptionId.HasValue)
                {
                    _propertyChangingSubscriptions.Add(entity, propertyChangingSubscriptionId.Value);
                }
            }
            if (!_propertyChangedSubscriptions.ContainsKey(entity))
            {
                var propertyChangedSubscriptionId = (entity as IEntity)?.PropertyChanged?.Subscribe(pc =>
                {
                    if (pc.PropertyName == "SiteInspectionId" && Equals(entity.GetPropertyValue("Id"), 1))
                    {
                        int a = 0;
                    }
                    var entityState = GetEntityState(entity);
                    var oldState = entityState.GetPropertyState(pc.PropertyName);
                    var property = EntityConfiguration.FindProperty(pc.PropertyName);
                    var oldValue = oldState == null ? pc.OldValue : oldState.OldValue;
                    if (_silentEntities.Contains(entity))
                    {
                        entityState.SetPropertyState(pc.PropertyName, pc.NewValue, pc.NewValue);
                    }
                    else
                    {
                        entityState.SetPropertyState(pc.PropertyName, oldValue, pc.NewValue);
                    }
                    if (!Equals(pc.OldValue, pc.NewValue))
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
                            var isRemove = Equals(pc.NewValue, null);
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
                });
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

        private void MarkedForDeletionChanged(MarkedForDeletionChangeEvent markedForDeletionChangeEvent)
        {
            if (markedForDeletionChangeEvent.NewValue)
            {
                //DataContext.DataStore.Delete(new DeleteEntityOperation<T>(
                //    (T) markedForDeletionChangeEvent.EntityState.Entity,
                //    DataContext));
                //DataContext.AsDbSetByType(markedForDeletionChangeEvent.EntityState.EntityType);
            }
            else
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
            if (trackedEntity != null && _trackedEntityClones.ContainsKey(trackedEntity.Entity))
            {
                return _trackedEntityClones[trackedEntity.Entity];
            }
            return null;
        }

        private T FindEntityByKeyInternal(CompositeKey key)
        {
            foreach (var entity in Set)
            {
                if (DataContext.EntityHasKey(entity, typeof(T), key))
                {
                    return entity;
                }
            }
            return null;
        }

        public virtual TrackedEntity<T> FindTrackedEntityByKey(CompositeKey key)
        {
            var entity = FindEntityByKeyInternal(key);
            if (entity != null)
            {
                return FindTrackedEntity(entity);
            }
            return null;
        }

        public virtual TrackedEntity<T> FindEntityByKey(CompositeKey key)
        {
            var entity = FindEntityByKeyInternal(key);
            if (entity != null)
            {
                return FindEntity(entity);
            }
            return null;
        }

        public virtual TrackedEntity<T> FindTrackedEntity(T localEntity)
        {
            return FindEntityInternal(localEntity, false);
        }

        public virtual TrackedEntity<T> FindEntity(T localEntity)
        {
            return FindEntityInternal(localEntity, true);
        }

        public virtual TrackedEntity<T> FindEntityInternal(T localEntity, bool searchRelationships)
        {
            var key = EntityConfiguration.Key;
            T matchedEntity = null;
            var relationships = new List<ITrackedRelationship>();
            var isNewEntity = DataContext.IsEntityNew(localEntity, typeof(T));
            var persistenceKeyProperty = EntityConfiguration.Properties
                .FirstOrDefault(p => p.Name == "PersistenceKey");
            object persistenceKey = null;
            if (persistenceKeyProperty != null)
            {
                persistenceKey = localEntity.GetPropertyValue(persistenceKeyProperty.Name);
            }
            foreach (var trackedEntity in Set)
            {
                if (localEntity == trackedEntity)
                {
                    matchedEntity = trackedEntity;
                }
                // Check PersistenceKey
                if (persistenceKey != null)
                {
                    var trackedEntityPersistenceKey = trackedEntity.GetPropertyValue(persistenceKeyProperty.Name);
                    var persistenceKeyString = persistenceKey.ToString();
                    if (persistenceKeyString != Guid.Empty.ToString() && trackedEntityPersistenceKey != null && trackedEntityPersistenceKey.ToString() == persistenceKeyString)
                    {
                        matchedEntity = trackedEntity;
                    }
                }
                if (matchedEntity == null && isNewEntity != null && isNewEntity.Value)
                {
                    continue;
                }
                if (matchedEntity == null)
                {
                    var keyMatches = true;
                    foreach (var keyProperty in key.Properties)
                    {
                        if (!Equals(
                            localEntity.GetPropertyValue(keyProperty.PropertyName),
                            trackedEntity.GetPropertyValue(keyProperty.PropertyName)))
                        {
                            keyMatches = false;
                            break;
                        }
                    }
                    if (keyMatches)
                    {
                        matchedEntity = trackedEntity;
                    }
                }
                if (searchRelationships)
                {
                    foreach (var relationship in EntityConfiguration.Relationships)
                    {
                        var isSource = relationship.Source.Configuration == EntityConfiguration;
                        var sourceRelationship = isSource ? relationship.Source : relationship.Target;
                        var targetRelationship = isSource ? relationship.Target : relationship.Source;
                        //var sourcePropertyName = sourceRelationship.Property.PropertyName;
                        var targetPropertyName = targetRelationship.Property.PropertyName;
                        foreach (var owner in TrackingSetCollection.TrackingSet(targetRelationship.Type).TrackedEntites())
                        {
                            var ownerRelationshipValue = owner.GetPropertyValue(targetPropertyName);
                            if (ownerRelationshipValue != null)
                            {
                                // Single entity, no current value locally so just assign
                                var isArray = ownerRelationshipValue is IEnumerable && !(ownerRelationshipValue is string);
                                if (isArray)
                                {
                                    var relationshipList = (IList)ownerRelationshipValue;
                                    foreach (var remoteItem in relationshipList)
                                    {
                                        var newMatchedEntity = MatchedEntity(localEntity, sourceRelationship, relationship, remoteItem, targetRelationship, owner, relationships, matchedEntity);
                                        if (newMatchedEntity != null)
                                        {
                                            matchedEntity = newMatchedEntity;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    var newMatchedEntity = MatchedEntity(localEntity, sourceRelationship, relationship, ownerRelationshipValue, targetRelationship, owner, relationships, matchedEntity);
                                    if (newMatchedEntity != null)
                                    {
                                        break;
                                    }
                                }
                            }
                            //                        if (matchedEntity != null)
                            //                       {
                            //                           break;
                            //                       }
                        }
                    }
                }
            }
            if (matchedEntity == null)
            {
                return null;
            }
            var findTrackedEntity = new TrackedEntity<T>(matchedEntity, relationships);
            return findTrackedEntity;
        }

        IEnumerable<ITrackedRelationship> ITrackingSet.FindRelationships(object entity, CompositeKey key)
        {
            return FindRelationships((T)entity, key);
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

        public IEnumerable<ITrackedRelationship> FindRelationships(T entity, CompositeKey key)
        {
            var relationships = new List<ITrackedRelationship>();
            foreach (var set in TrackingSetCollection.Sets)
            {
                var entityConfiguration = DataContext.EntityConfigurationContext.GetEntityByType(set.EntityType);
                foreach (var relationship in entityConfiguration.AllRelationships())
                {
                    if (relationship.ThisEnd.Type == typeof(T) && !relationship.ThisEnd.IsCollection)
                    {
                        foreach (var setEntity in set.TrackedEntites())
                        {
                            var relationshipPropertyValue =
                                setEntity.GetPropertyValue(relationship.ThisEnd.Property.PropertyName);
                            if (entity != null)
                            {
                                if (Equals(entity, relationshipPropertyValue))
                                {
                                    // Found a match
                                    int a = 0;
                                }
                            }
                            else
                            {
                                var setEntityRelationshipKey = relationship.ThisEnd.GetCompositeKey(setEntity, true);
                                if (setEntityRelationshipKey.Matches(key))
                                {
                                    // Found a match
                                    int a = 0;
                                }
                            }
                        }
                    }
                }
            }
            return relationships;
        }

        private T MatchedEntity(T localEntity, IRelationshipDetail sourceRelationship, IRelationship relationship,
            object remoteItem, IRelationshipDetail targetRelationship, object owner, List<ITrackedRelationship> relationships, T matchedEntity)
        {
            object match = null;
            var isMatch = true;
            if (DataContext.IsEntityNew(localEntity, typeof(T)) == false)
            {
                if (remoteItem != localEntity)
                {
                    foreach (var keyProperty in sourceRelationship.Configuration.Key.Properties)
                    {
                        if (!Equals(remoteItem.GetPropertyValue(keyProperty.PropertyName),
                            localEntity.GetPropertyValue(keyProperty.PropertyName)))
                        {
                            isMatch = false;
                            break;
                        }
                    }
                }
            }
            else
            {
                isMatch = Equals(localEntity, remoteItem);
            }
            if (isMatch)
            {
                match = remoteItem;
            }
            if (match != null)
            {
                if (!relationships.Any(r => r.Owner == owner && r.Entity == remoteItem && r.Relationship == relationship))
                {
                    var trackedRelationship = (ITrackedRelationship)GetType()
                        .GetMethod(nameof(NewTrackedRelationship))
                        .MakeGenericMethod(targetRelationship.Type, sourceRelationship.Type)
                        .Invoke(this, new object[] { owner, remoteItem, relationship });
                    relationships.Add(trackedRelationship);
                }
                return (T)match;
            }
            return null;
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

        ITrackedEntity ITrackingSet.FindTrackedEntity(object entity)
        {
            return FindTrackedEntity((T)entity);
        }

        ITrackedEntity ITrackingSet.FindTrackedEntityByKey(CompositeKey key)
        {
            return FindTrackedEntityByKey(key);
        }

        ITrackedEntity ITrackingSet.FindEntity(object entity)
        {
            return FindEntity((T)entity);
        }

        ITrackedEntity ITrackingSet.FindEntityByKey(CompositeKey key)
        {
            return FindEntityByKey(key);
        }

        public List<UpdateEntityOperation<T>> GetChangesInternal(bool reset = false)
        {
            var updates = new List<UpdateEntityOperation<T>>();
            Set.ForEach(entity =>
            {
                //let ctor = entity["__ctor"]();
                //var clone = FindClone(entity);
                //if (clone != null)
                //{
                //    var entityState = GetEntityStateOld(queue, entity, clone, typeof(T));
                //    if (entityState != null)
                //    {
                //        updates.Add(new UpdateEntityOperation<T>(entity, DataContext, entityState));
                //    }
                //}
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

        private PropertyChange GetPropertyChange(IProperty property, object entity, object clone)
        {
            var oldValue = clone.GetPropertyValue(property.Name);
            var newValue = entity.GetPropertyValue(property.Name);
            var hasChanged = false;
            var nullCount = new[] { oldValue, newValue }.Count(x => x == null);
            PropertyChange propertyChange = null;
            if (nullCount == 1)
            {
                hasChanged = true;
            }
            else if (nullCount == 0)
            {
                hasChanged = !Equals(oldValue, newValue);
            }
            if (hasChanged)
            {
                propertyChange = new PropertyChange(property, oldValue, newValue, null);
            }
            return propertyChange;
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
                EntityStates.Add(entity, entityState);
                return entityState;
            }
            return EntityStates[entity];
        }

        private IEntityStateBase GetEntityStateOld(List<IQueuedOperation> queue, T entity, object clone, Type entityType)
        {
            var changedProperties = new List<PropertyChange>();
            var entityConfiguration = DataContext.EntityConfigurationContext.GetEntityByType(entityType);
            foreach (var property in entityConfiguration.Properties)
            {
                switch (property.Kind)
                {
                    case PropertyKind.Key:
                    case PropertyKind.Primitive:
                        var change = GetPropertyChange(property, entity, clone);
                        if (change != null)
                        {
                            changedProperties.Add(change);
                        }
                        break;
                    case PropertyKind.Relationship:
                        if (!property.IsCollection)
                        {
                            var constraints = property.Relationship.ThisEnd.GetCompositeKey(entity);
                            var constraintChanges = new List<PropertyChange>();
                            foreach (var constraint in constraints.Keys)
                            {
                                var constraintProperty = entityConfiguration.FindProperty(constraint.Name);
                                var constraintChange = GetPropertyChange(constraintProperty, entity, clone);
                                if (constraintChange != null)
                                {
                                    constraintChanges.Add(constraintChange);
                                }
                            }
                            var relationshipChange = GetPropertyChange(property, entity, clone);
                            var relationshipChanged = false;

                            if (relationshipChange != null)
                            {
                                relationshipChanged = !DataContext.IsIdMatch(relationshipChange.OldValue,
                                    relationshipChange.NewValue, relationshipChange.Property.Type);
                            }
                            if (relationshipChanged &&
                                constraintChanges.Any() &&
                                relationshipChange.NewValue != null)
                            {
                                // Ensure these match, as we've changed them both
                                var constraintsInverse = property.Relationship.ThisEnd.GetCompositeKey(entity, true);
                                if (!DataContext.EntityPropertiesMatch(relationshipChange.NewValue, constraintsInverse))
                                {
                                    throw new InconsistentRelationshipAssignmentException();
                                }
                                // If we get to here, both key value and relationship value have changed
                                // and they are consistent
                            }
                            else if (relationshipChanged || constraintChanges.Any())
                            {
                                var constraintsInverse = property.Relationship.ThisEnd.GetCompositeKey(entity, true);
                                if (!relationshipChanged)
                                {
                                    var childEntity = TrackingSetCollection
                                        .TrackingSet(property.Relationship.OtherEnd.Type)
                                        .FindTrackedEntityByKey(constraintsInverse);
                                    if (childEntity != null)
                                    {
                                        entity.SetPropertyValue(property.Name, childEntity.Entity);
                                        AssignRelationship(entity, property.Relationship.OtherEnd, childEntity.Entity,
                                            property);
                                    }
                                }
                                else if (!constraintChanges.Any())
                                {
                                    var childEntity = TrackingSetCollection
                                        .TrackingSet(property.Relationship.OtherEnd.Type)
                                        .FindTrackedEntityByKey(constraintsInverse);
                                    var constraintsDirect = property.Relationship.ThisEnd.GetCompositeKey(entity);
                                    foreach (var key in constraintsDirect.Keys)
                                    {
                                        entity.SetPropertyValue(key.Name, key.Value);
                                        if (childEntity != null)
                                        {
                                            AssignRelationship(entity, property.Relationship.OtherEnd, childEntity,
                                                property);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            var relationshipChange = GetPropertyChange(property, entity, clone);
                            if (relationshipChange != null)
                            {
                                var oldCollection = relationshipChange.OldValue as IList;
                                var newCollection = relationshipChange.NewValue as IList;
                                var oldValues = new List<CompositeKey>();
                                var newValues = new List<CompositeKey>();
                                if (oldCollection != null)
                                {
                                    foreach (var item in oldCollection)
                                    {
                                        oldValues.Add(property.Relationship.OtherEnd.GetCompositeKey(item, true));
                                    }
                                }
                                if (newCollection != null)
                                {
                                    foreach (var item in newCollection)
                                    {
                                        newValues.Add(property.Relationship.OtherEnd.GetCompositeKey(item, true));
                                    }
                                    foreach (var value in newValues)
                                    {
                                        DataContext.AsDbSetByType(property.Relationship.OtherEnd.Type).AddEntity(value.Entity);
                                        if (!DataContext.EntityPropertiesMatch(entity, value))
                                        {
                                            if (oldValues.Any(v => DataContext.EntityPropertiesMatch(v.Entity, value)))
                                            {
                                                TrackingSetCollection.ClearParent(entity, property.Name);
                                                newCollection.Remove(value.Entity);
                                            }
                                            else
                                            {
                                                AssignRelationship(entity, property.Relationship.OtherEnd, value.Entity,
                                                    property);
                                                //var composite = property.Relationship.ThisEnd.GetCompositeKey(entity, true);
                                                //foreach (var key in composite.Keys)
                                                //{
                                                //    value.Entity.SetPropertyValue(key.Name, key.Value);
                                                //}
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        // If the old has a value, and the new doesn't, don't do anything
                        // If the old has a value, and the new has a new non-null value, check it matches
                        // the key constraint
                        // - If it doesn't match
                        // If the old does not have a value, and the new does, check it matches the
                        // key constraint. If the key constraint has been changed also and is different, throw
                        // an error, otherwise reassign the key constraint
                        break;
                }
            }

            foreach (var change in changedProperties)
            {
                // If we changed the key, check if we've changed the relationship property
                // If we haven't, then the key change should propogate to the relationship property
                // If we have, then the key of the realtionship property should match this
                switch (change.Property.Kind)
                {
                    case PropertyKind.RelationshipKey:
                        var relationshipPropertyName = change.Property.Relationship.ThisEnd.Property.PropertyName;
                        var relationshipChange = changedProperties.SingleOrDefault(p =>
                            p.Property.Name == relationshipPropertyName);
                        var compositeKey = new CompositeKey();
                        foreach (var constraint in change.Property.Relationship.Relationship.Constraints)
                        {
                            compositeKey.Keys.Add(new KeyValue(
                                constraint.TargetKeyProperty.PropertyName,
                                entity.GetPropertyValue(constraint.SourceKeyProperty.PropertyName),
                                entityConfiguration.FindProperty(constraint.SourceKeyProperty.PropertyName).Type));
                        }
                        var relatedTrackedEntity = TrackingSetCollection.TrackingSet(change.Property.Relationship.OtherEnd.Type)
                            .FindTrackedEntityByKey(compositeKey);
                        if (relationshipChange == null)
                        {
                            entity.SetPropertyValue(relationshipPropertyName, relatedTrackedEntity?.Entity);
                        }
                        else if (relationshipChange.NewValue != null)
                        {
                            if (!DataContext.EntityHasKey(relationshipChange.NewValue,
                                relationshipChange.Property.Relationship.OtherEnd.Type, compositeKey))
                            {
                                throw new InconsistentRelationshipAssignmentException();
                            }
                        }
                        if (relatedTrackedEntity != null)
                        {
                            //var entityRelationships = FindTrackedEntity((T)entity).TrackedRelationships;
                            var partnerPropertyName = change.Property.Relationship.OtherEnd.Property.PropertyName;
                            var partnerType = change.Property.Relationship.ThisEnd.Type;
                            var partnerCollection = relatedTrackedEntity.Entity.GetPropertyValue(partnerPropertyName) as IList;
                            if (partnerCollection != null && !partnerCollection.Contains(entity))
                            {
                                partnerCollection.Add(entity);
                                //TrackingSetCollection.RecordParent(entity, relatedTrackedEntity.Entity, partnerPropertyName);
                            }
                        }
                        break;
                }
            }

            if (changedProperties.Any())
            {
                var state = new EntityState<T>(entity, entityType, DataContext, EntityConfiguration);
                state.ChangedProperties.AddRange(changedProperties);
                return state;
            }
            return null;
        }

        private void AssignRelationship(T entity, IRelationshipDetail relationshipDetail,
            object childEntity, IProperty property)
        {
            if (relationshipDetail.IsCollection)
            {
                var collection =
                    childEntity.GetPropertyValue(property.Relationship.OtherEnd
                        .Property.PropertyName) as IList;
                if (collection != null && !collection.Contains(entity))
                {
                    collection.Add(entity);
                }
            }
            else
            {
                var composite = property.Relationship.ThisEnd.GetCompositeKey(entity, true);
                foreach (var key in composite.Keys)
                {
                    childEntity.SetPropertyValue(key.Name, key.Value);
                }
                childEntity.SetPropertyValue(
                    property.Relationship.OtherEnd.Property.PropertyName, entity);
                TrackingSetCollection.ClearParent(childEntity, property.Relationship.ThisEnd.Property.PropertyName);
                //TrackingSetCollection.RecordParent(childEntity, entity, property.Relationship.ThisEnd.Property.PropertyName);
            }
            // Go through all the relationships that contain this entity
            // and update them if need be
            var trackedEntity = FindTrackedEntity(entity);
            if (trackedEntity != null)
            {
                foreach (var relationship in trackedEntity.TrackedRelationships)
                {
                    if (!RelationshipIsValid(relationship))
                    {
                        if (relationship.OwnerDetail.IsCollection)
                        {
                            var collection =
                                relationship.Owner.GetPropertyValue(property.Relationship.OtherEnd
                                    .Property.PropertyName) as IList;
                            if (collection != null && collection.Contains(entity))
                            {
                                TrackingSetCollection.ClearParent(entity, property.Name);
                                collection.Remove(entity);
                            }
                        }
                        else
                        {
                            int a = 0;
                        }
                    }
                }
            }
        }

        private bool RelationshipIsValid(ITrackedRelationship relationship)
        {
            var entityKey = relationship.EntityDetail.GetCompositeKey(relationship.Entity, true);
            if (!DataContext.EntityPropertiesMatch(relationship.Owner, entityKey))
            {
                return false;
            }
            return true;
        }

        private List<T> _preChangeCheckEntities = new List<T>();
        private List<T> _silentEntities = new List<T>();

        public void SilentlyChangeEntity(T entity, Action action)
        {
            ChangeEntity(entity, action, ChangeEntityMode.Silent);
        }

        public void ChangeEntity(T entity, Action action, ChangeEntityMode mode)
        {
            // Instead of unwatching, we can check if an entity is being
            // silently changed, in which case we reset the property state
            // for any updates
            switch (mode)
            {
                case ChangeEntityMode.Normal:
                    action();
                    break;
                case ChangeEntityMode.Silent:
                    var hasSilent = _silentEntities.Contains(entity);
                    if (!hasSilent)
                    {
                        _silentEntities.Add(entity);
                    }
                    action();
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
                    action();
                    if (!hasPreCheck)
                    {
                        _preChangeCheckEntities.Remove(entity);
                    }
                    break;
            }
            //Unwatch(entity);
            //foreach (var property in EntityConfiguration.Properties)
            //{
            //    var entityState = GetEntityState(entity);
            //}
            //Watch(entity);
        }

        void ITrackingSet.SilentlyChangeEntity(object entity, Action action)
        {
            SilentlyChangeEntity((T)entity, action);
        }

        void ITrackingSet.ChangeEntity(object entity, Action action, ChangeEntityMode mode)
        {
            ChangeEntity((T)entity, action, mode);
        }

        public bool IsTracked(object entity)
        {
            var trackedEntity = FindTrackedEntity((T)entity);
            return trackedEntity != null && trackedEntity.Entity == entity;
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

        public void EnsureIntegrity()
        {
            foreach (var entity in TrackedEntites())
            {
                EnsureEntityIntegrity(entity);
            }
        }

        private void EnsureEntityIntegrity(object entity)
        {
            /* Iterate through the relationships
             * Log the parent for each child, ensuring local integrity check
             * For one-endian relationships:
             * - If the "parent" ID has changed, mark that property as changed, and if the attached parent does not match the parent ID, delete association
             * - If the "parent" ID has not changed, but does not match the current parent, change the parent ID and mark that property as changed
             * - If the entity is new, track the entity, and if the parent ID does not match the attached parent, throw exception
             * 
             * For multi-endian relaitonships:
             * - 
             * - Log parent ID as 
             * - For one to one relationships check the ID and the attached object.
             *   - If the ID has changed, assume deliberate and ignore checking the related object property
             *   - If the attached object has changed, update the ID to either the attached object's ID or, if it is new, to null/default
             * Iterate through the remaining properties and perform simple value equality check
             * 
             */
            var relationships = EntityConfiguration.AllRelationships();
            foreach (var relationship in relationships)
            {
                var value = entity.GetPropertyValue(relationship.ThisEnd.Property.PropertyName);
                if (value.IsArray())
                {
                    var values = value as IEnumerable;
                    if (values != null)
                    {
                        foreach (var child in values)
                        {
                            if (!TrackingSetCollection.IsTracked(child, relationship.OtherEnd.Type))
                            {
                                TrackingSetCollection.TrackGraph(child, relationship.OtherEnd.Type);
                            }
                            // Check if the child's relationship keys
                            var key = relationship.OtherEnd.GetCompositeKey(child);
                            TrackingSetCollection.RecordParent(child, entity, relationship.ThisEnd.Property.PropertyName);
                        }
                    }
                }
                SanitizeRelationship(relationship.Relationship, relationship.ThisEnd, entity, value);
            }
        }

        private void SanitizeRelationship(IRelationship relationship, IRelationshipDetail detail, object parent, object child)
        {
        }

        //private List<PropertyChange> GetChangedPropertiesOld(object entity, object clone)
        //{
        //    var changedProperties = new List<PropertyChange>();
        //    var entityDefinition = DataContext.EntityConfigurationContext.GetEntityByType(entity.GetType());
        //    var properties = entityDefinition.Properties;
        //    for (var i = 0; i < properties.Count; i++)
        //    {
        //        var property = properties[i];
        //        var propertyChange = new PropertyChange(property);
        //        var entityValue = entity.GetPropertyValue(property.Name);
        //        var cloneValue = clone.GetPropertyValue(property.Name);
        //        var entityHasChanged = false;
        //        if (entityValue == null && cloneValue == null)
        //        {
        //            continue;
        //        }
        //        if (new[] { entityValue, cloneValue }.Count(e => e == null) == 1)
        //        {
        //            entityHasChanged = true;
        //        }
        //        else if (entityValue is IEnumerable && !(entityValue is string))
        //        {
        //            var entityValueEnumerable = (entityValue as IEnumerable).Cast<object>().ToArray();
        //            var cloneValueEnumerable = (cloneValue as IEnumerable).Cast<object>().ToArray();
        //            for (var valueIndex = 0; valueIndex < entityValueEnumerable.Length; valueIndex++)
        //            {
        //                var entityValueAtIndex = entityValueEnumerable[valueIndex];
        //                var cloneValueAtIndex = TrackingSetCollection.FindClone(entityValueAtIndex);
        //                if (cloneValueAtIndex == null)
        //                {
        //                    propertyChange.EnumerableChangedProperties.Add(valueIndex, new List<PropertyChange>());
        //                    entityHasChanged = true;
        //                    continue;
        //                }
        //                if (entityValueAtIndex is string || !entityValueAtIndex.GetType().IsClass)
        //                {
        //                    if (Equals(entityValueAtIndex, cloneValueAtIndex))
        //                    {
        //                        continue;
        //                    }
        //                }
        //                else
        //                {
        //                    var nullCount = new[] { entityValueAtIndex, cloneValueAtIndex }
        //                        .Count(x => x == null);
        //                    if (nullCount == 1)
        //                    {
        //                        entityHasChanged = true;
        //                        break;
        //                    }
        //                    var changedChildProperties = GetChangedProperties(entityValueAtIndex, cloneValueAtIndex, property.Type);
        //                    if (!changedChildProperties.Any())
        //                    {
        //                        continue;
        //                    }
        //                    propertyChange.EnumerableChangedProperties.Add(valueIndex, changedChildProperties);
        //                }
        //                entityHasChanged = true;
        //            }
        //            if (entityValueEnumerable.Length != cloneValueEnumerable.Length)
        //            {
        //                entityHasChanged = true;
        //            }
        //        }
        //        else
        //        {
        //            if (entityValue.GetType().IsClass && !(entityValue is string))
        //            {
        //                var propertyChanges = GetChangedProperties(entityValue, cloneValue, property.Type);
        //                if (propertyChanges.Any())
        //                {
        //                    propertyChange.ChildChangedProperties.AddRange(propertyChanges);
        //                    entityHasChanged = true;
        //                }
        //            }
        //            else if (!Equals(entityValue, cloneValue))
        //            {
        //                entityHasChanged = true;
        //            }
        //        }
        //        if (!entityHasChanged)
        //        {
        //            continue;
        //        }
        //        changedProperties.Add(propertyChange);
        //    }
        //    return changedProperties;
        //}
    }
}