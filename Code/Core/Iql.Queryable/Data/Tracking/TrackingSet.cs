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
using Iql.Queryable.Events;
using Iql.Queryable.Extensions;

namespace Iql.Queryable.Data.Tracking
{
    public class TrackingSet<T> : TrackingSetBase, ITrackingSet
        where T : class
    {
        public IDataContext DataContext { get; }
        public TrackingSetCollection TrackingSetCollection { get; }
        public EntityConfiguration<T> EntityConfiguration { get; }
        public SimplePropertyMerger SimplePropertyMerger { get; }

        private Dictionary<Guid, IEntityStateBase> EntitiesByPersistenceKey {get; set; }
        private Dictionary<object, IEntityStateBase> EntitiesByObject { get; set; }
        private Dictionary<string, IEntityStateBase> EntitiesByKey { get; set; }

        public TrackingSet(IDataContext dataContext, TrackingSetCollection trackingSetCollection)
        {
            DataContext = dataContext;
            TrackingSetCollection = trackingSetCollection;
            EntityConfiguration = DataContext.EntityConfigurationContext.GetEntity<T>();
            SimplePropertyMerger = new SimplePropertyMerger(EntityConfiguration);
            EntitiesByPersistenceKey = new Dictionary<Guid, IEntityStateBase>();
            EntitiesByObject = new Dictionary<object, IEntityStateBase>();
            EntitiesByKey = new Dictionary<string, IEntityStateBase>();
            PersistenceKey = EntityConfiguration.Properties.SingleOrDefault(p => p.Name == "PersistenceKey");
        }

        protected IProperty PersistenceKey { get; set; }

        public IEntityStateBase GetEntityState(object entity)
        {
            if (EntitiesByObject.ContainsKey(entity))
            {
                return EntitiesByObject[entity];
            }

            Guid persistenceKey = Guid.Empty;
            if (PersistenceKey != null)
            {
                persistenceKey = entity.GetPropertyValueAs<Guid>(PersistenceKey);
                if (!Equals(persistenceKey, Guid.Empty) && EntitiesByPersistenceKey.ContainsKey(persistenceKey))
                {
                    return EntitiesByObject[entity];
                }
            }

            var keyString = EntityConfiguration.GetCompositeKey(entity).AsKeyString();
            if (EntitiesByKey.ContainsKey(keyString))
            {
                return EntitiesByKey[keyString];
            }

            var entityState = new EntityState<T>((T)entity, typeof(T), DataContext, EntityConfiguration);
            EntitiesByObject.Add(entity, entityState);
            if (PersistenceKey != null)
            {
                persistenceKey = EnsurePersistenceKey(entity).Value;
                EntitiesByPersistenceKey.Add(persistenceKey, entityState);
            }

            Watch((T) entity);
            return entityState;
        }

        public void Delete(object entity)
        {
            var entityState = GetEntityState(entity);
            if (entityState.MarkedForDeletion || entityState.MarkedForCascadeDeletion || entityState.IsNew)
            {
                if (!entityState.MarkedForCascadeDeletion)
                {
                    entityState.MarkedForDeletion = true;
                }
            }
            else
            {
                entityState.MarkedForDeletion = true;
            }
            Unwatch((T)entity);
        }

        public void TrackEntities(IList data, bool isNew = true)
        {
            var flattened = DataContext.EntityConfigurationContext.FlattenObjectGraphs(
                typeof(T), data);
            foreach (var flattenedEntity in flattened)
            {
                var trackingSet = TrackingSetCollection.TrackingSetByType(flattenedEntity.EntityType)
                    as TrackingSetBase;
                trackingSet.TrackEntityInternal(flattenedEntity.Entity, null, isNew);
            }
        }

        public IEntityStateBase TrackEntity(object entity, object mergeWith = null, bool isNew = true)
        {
            var flattened = DataContext.EntityConfigurationContext.FlattenObjectGraph(
                entity, typeof(T));
            IEntityStateBase entityState = null;
            foreach (var flattenedEntity in flattened)
            {
                var trackingSet = TrackingSetCollection.TrackingSetByType(flattenedEntity.EntityType)
                    as TrackingSetBase;
                var state = trackingSet.TrackEntityInternal(flattenedEntity.Entity, mergeWith, isNew);
                if (flattenedEntity.Entity == entity)
                {
                    entityState = state;
                }
            }

            return entityState;
        }

        private readonly Dictionary<IRelatedList, int> _collectionChangeSubscriptions =
            new Dictionary<IRelatedList, int>();
        private readonly Dictionary<T, int> _entityStateChangedSubscriptions =
            new Dictionary<T, int>();
        private readonly Dictionary<T, int> _propertyChangingSubscriptions =
            new Dictionary<T, int>();
        private readonly Dictionary<T, int> _propertyChangedSubscriptions =
            new Dictionary<T, int>();
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
                    var relatedList = entity.GetPropertyValue(relationship.ThisEnd.Property)
                        as IRelatedList;
                    if (relatedList != null && !_collectionChangeSubscriptions.ContainsKey(relatedList))
                    {
                        _collectionChangeSubscriptions.Add(relatedList, relatedList.Changed.Subscribe(RelatedListChanged));
                    }
                }
            }
        }

        private void EntityPropertyChanged(IPropertyChangeEvent obj)
        {
            
        }

        private void EntityPropertyChanging(IPropertyChangeEvent obj)
        {
            
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
                    var relatedList = entity.GetPropertyValue(relationship.ThisEnd.Property)
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
            //if (!markedForDeletionChangeEvent.NewValue)
            //{
            //    DataContext.DataStore.RemoveQueuedOperationsOfTypeForEntity(
            //        markedForDeletionChangeEvent.EntityState.Entity,
            //        QueuedOperationType.Delete);
            //}
        }

        private void RelatedListChanged(IRelatedListChangedEvent ev)
        {
//            var relationship = DataContext.EntityConfigurationContext.GetEntityByType(ev.OwnerType)
//                .FindRelationship(ev.List.Property);
//            var method = this.GetType().GetMethod(nameof(ProcessOneToManyCollectionChange),
//                    BindingFlags.NonPublic | BindingFlags.Instance)
//                .MakeGenericMethod(relationship.OtherEnd.Type);
//            method.Invoke(this, new object[]
//            {
//                ev,
//                relationship,
//#if TypeScript
//                relationship.OtherEnd.Type
//#endif
//            });
        }

        internal override void ChangeEntity(object entity, Action action, ChangeEntityMode silently)
        {
            action();
        }

        internal override async Task ChangeEntityAsync(object entity, Func<Task> action, ChangeEntityMode silently, bool allowAsync = true)
        {
            await action();
        }

        internal override IEntityStateBase TrackEntityInternal(object entity, object mergeWith = null, bool isNew = true)
        {
            if (mergeWith != null)
            {
                SilentlyMerge(entity, mergeWith);
            }

            var entityState = GetEntityState(entity);
            Watch((T)entityState.Entity);
            entityState.IsNew = isNew;
            if (entityState.Entity != entity)
            {
                SilentlyMerge(entityState.Entity, entity);
            }

            return entityState;
            //trackingSet.ChangeEntity(localEntity, () =>
            //{
            //foreach (var keyProperty in EntityConfiguration.Key.Properties)
            //{
            //    localEntity.SetPropertyValue(keyProperty, remoteEntity.GetPropertyValue(keyProperty));
            //}
            //}, ChangeEntityMode.NoKeyChecks);
        }

        private void SilentlyMerge(object entity, object with)
        {
            ChangeEntity(entity, () =>
            {
                SimplePropertyMerger.Merge(
                    entity,
                    with
                );
            }, ChangeEntityMode.Silent);
        }

        private Guid? EnsurePersistenceKey(object entity)
        {
            if (PersistenceKey != null)
            {
                var value = entity.GetPropertyValueAs<Guid>(PersistenceKey);
                if (Equals(value, Guid.Empty))
                {
                    var guid = Guid.NewGuid();
                    entity.SetPropertyValue(PersistenceKey, guid);
                    return guid;
                }

                return value;
            }

            return null;
            //var flattened = DataContext.EntityConfigurationContext.FlattenObjectGraph(localEntity, entityType);
            //foreach (var entity in flattened)
            //{
            //    var isEntityNew = DataContext.IsEntityNew(entity.Entity, entity.EntityType);
            //    if (isEntityNew == true)
            //    {
            //        var tracking = GetTracking();
            //        var trackedEntity = tracking.FindEntity(entity.Entity);
            //        if (trackedEntity == null)
            //        {
            //            tracking.TrackGraph(entity.Entity, entityType);
            //        }
            //    }
            //}
        }

        public IEnumerable<IEntityCrudOperationBase> GetInserts()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEntityCrudOperationBase> GetDeletions()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IUpdateEntityOperation> GetUpdates()
        {
            throw new NotImplementedException();
        }
    }
}