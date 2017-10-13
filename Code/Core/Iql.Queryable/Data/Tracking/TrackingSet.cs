using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Extensions;

namespace Iql.Queryable.Data.Tracking
{
    public class TrackingSet<T> : ITrackingSet where T : class
    {
        public TrackingSet(IDataContext dataContext, TrackingSetCollection trackingSetCollection)
        {
            DataContext = dataContext;
            TrackingSetCollection = trackingSetCollection;
            Set = new List<T>();
            Clone = new List<T>();
        }

        public List<T> Set { get; set; }
        public List<T> Clone { get; set; }
        private IDataContext DataContext { get; }
        public TrackingSetCollection TrackingSetCollection { get; }

        List<IEntityCrudOperationBase> ITrackingSet.GetChanges()
        {
            return GetChanges().Cast<IEntityCrudOperationBase>().ToList();
        }

        public void Reset()
        {
            Clone = Set.Clone();
        }

        public IEnumerable<object> TrackedEntites()
        {
            return Set.ToList();
        }

        public Type EntityType => typeof(T);

        void ITrackingSet.Track(object entity)
        {
            Track((T)entity);
        }

        void ITrackingSet.Merge(IList data)
        {
            Merge((List<T>)data);
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

        public void Track(T entity)
        {
            var clone = entity.Clone();
            TrackWithClone(entity, clone);
        }

        public void TrackWithClone(T entity, T clone)
        {
            var existingEntity = FindTrackedEntity(entity);
            if (existingEntity == null)
            {
                Set.Add(entity);
                Clone.Add(clone);
                _trackedEntityClones.Add(entity, clone);
            }
            else
            {
                ObjectMerger.Merge(DataContext, TrackingSetCollection, entity);
                _trackedEntityClones[existingEntity.Entity] = clone;
            }
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

        public virtual TrackedEntity<T> FindTrackedEntity(T localEntity)
        {
            var entityConfiguration = DataContext.EntityConfigurationContext.GetEntity<T>();
            var key = entityConfiguration.Key;
            T matchedEntity = null;
            var relationships = new List<ITrackedRelationship>();
            var isNewEntity = DataContext.IsEntityNew(localEntity);
            var persistenceKeyProperty = DataContext.EntityConfigurationContext.GetEntityByType(typeof(T)).Properties
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
                    if (trackedEntityPersistenceKey != null && trackedEntityPersistenceKey.ToString() == persistenceKey.ToString())
                    {
                        matchedEntity = trackedEntity;
                    }
                }
                if (isNewEntity)
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
                foreach (var relationship in entityConfiguration.Relationships)
                {
                    var isSource = relationship.Source.Configuration == entityConfiguration;
                    var sourceRelationship = isSource ? relationship.Source : relationship.Target;
                    var targetRelationship = isSource ? relationship.Target : relationship.Source;
                    var sourcePropertyName = sourceRelationship.Property.PropertyName;
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
                                var relationshipList = (IList) ownerRelationshipValue;
                                foreach (var remoteItem in relationshipList)
                                {
                                    matchedEntity = MatchedEntity(localEntity, sourceRelationship, relationship, remoteItem, targetRelationship, owner, relationships, matchedEntity);
                                    if (matchedEntity != null)
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                matchedEntity = MatchedEntity(localEntity, sourceRelationship, relationship, ownerRelationshipValue, targetRelationship, owner, relationships, matchedEntity);
                                if (matchedEntity != null)
                                {
                                    break;
                                }
                            }
                        }
                        if (matchedEntity != null)
                        {
                            break;
                        }
                    }
                }
            }
            if (matchedEntity == null)
            {
                return null;
            }
            return new TrackedEntity<T>(matchedEntity, relationships);
        }

        private T MatchedEntity(T localEntity, IRelationshipDetail sourceRelationship, IRelationship relationship,
            object remoteItem, IRelationshipDetail targetRelationship, object owner, List<ITrackedRelationship> relationships, T matchedEntity)
        {
            object match = null;
            var isMatch = true;
            if (remoteItem == localEntity)
            {
                return localEntity;
            }
            foreach (var keyProperty in sourceRelationship.Configuration.Key.Properties)
            {
                if (!Equals(remoteItem.GetPropertyValue(keyProperty.PropertyName),
                    localEntity.GetPropertyValue(keyProperty.PropertyName)))
                {
                    isMatch = false;
                    break;
                }
            }
            if (isMatch)
            {
                match = remoteItem;
            }
            if (match != null)
            {
                var trakcedRelationship = (ITrackedRelationship) GetType()
                    .GetMethod(nameof(NewTrackedRelationship))
                    .MakeGenericMethod(targetRelationship.Type, sourceRelationship.Type)
                    .Invoke(this, new object[] {owner, remoteItem, relationship});
                relationships.Add(trakcedRelationship);
                matchedEntity = (T) match;
            }
            return matchedEntity;
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

        void ITrackingSet.TrackWithClone(object entity, object clone)
        {
            TrackWithClone((T)entity, (T)clone);
        }

        public void Merge(List<T> data)
        {
            for (var i = 0; i < data.Count; i++)
            {
                var element = data[i];
                if (element == null)
                {
                    continue;
                }
                Track(element);
                data[i] = FindTrackedEntity(data[i]).Entity;
            }
        }

        public List<UpdateEntityOperation<T>> GetChanges()
        {
            var updates = new List<UpdateEntityOperation<T>>();
            Set.ForEach(entity =>
            {
                //let ctor = entity["__ctor"]();
                var clone = FindClone(entity);
                if (clone != null)
                {
                    var changedProperties = GetChangedProperties(entity, clone);
                    if (changedProperties.Any())
                    {
                        updates.Add(new UpdateEntityOperation<T>(entity, DataContext, changedProperties.ToArray()));
                    }
                }
            });
            return updates;
        }

        private List<PropertyChange> GetChangedProperties(object entity, object clone)
        {
            var changedProperties = new List<PropertyChange>();
            var entityDefinition = DataContext.EntityConfigurationContext.GetEntityByType(entity.GetType());
            var properties = entityDefinition.Properties;
            for (var i = 0; i < properties.Count; i++)
            {
                var property = properties[i];
                var propertyChange = new PropertyChange(property);
                var entityValue = entity.GetPropertyValue(property.Name);
                var cloneValue = clone.GetPropertyValue(property.Name);
                var entityHasChanged = false;
                if (entityValue == null && cloneValue == null)
                {
                    continue;
                }
                if (new[] { entityValue, cloneValue }.Count(e => e == null) == 1)
                {
                    entityHasChanged = true;
                }
                else if (entityValue is IEnumerable && !(entityValue is string))
                {
                    var entityValueEnumerable = (entityValue as IEnumerable).Cast<object>().ToArray();
                    var cloneValueEnumerable = (cloneValue as IEnumerable).Cast<object>().ToArray();
                    for (var valueIndex = 0; valueIndex < entityValueEnumerable.Length; valueIndex++)
                    {
                        var entityValueAtIndex = entityValueEnumerable[valueIndex];
                        var cloneValueAtIndex = TrackingSetCollection.FindClone(entityValueAtIndex);
                        if (cloneValueAtIndex == null)
                        {
                            propertyChange.EnumerableChangedProperties.Add(valueIndex, new List<PropertyChange>());
                            entityHasChanged = true;
                            continue;
                        }
                        if (entityValueAtIndex is string || !entityValueAtIndex.GetType().IsClass)
                        {
                            if (Equals(entityValueAtIndex, cloneValueAtIndex))
                            {
                                continue;
                            }
                        }
                        else
                        {
                            var nullCount = new[] { entityValueAtIndex, cloneValueAtIndex }
                                .Count(x => x == null);
                            if (nullCount == 1)
                            {
                                entityHasChanged = true;
                                break;
                            }
                            var changedChildProperties = GetChangedProperties(entityValueAtIndex, cloneValueAtIndex);
                            if (!changedChildProperties.Any())
                            {
                                continue;
                            }
                            propertyChange.EnumerableChangedProperties.Add(valueIndex, changedChildProperties);
                        }
                        entityHasChanged = true;
                    }
                    if (entityValueEnumerable.Length != cloneValueEnumerable.Length)
                    {
                        entityHasChanged = true;
                    }
                }
                else
                {
                    if (entityValue.GetType().IsClass && !(entityValue is string))
                    {
                        var propertyChanges = GetChangedProperties(entityValue, cloneValue);
                        if (propertyChanges.Any())
                        {
                            propertyChange.ChildChangedProperties.AddRange(propertyChanges);
                            entityHasChanged = true;
                        }
                    }
                    else if (!Equals(entityValue, cloneValue))
                    {
                        entityHasChanged = true;
                    }
                }
                if (!entityHasChanged)
                {
                    continue;
                }
                changedProperties.Add(propertyChange);
            }
            return changedProperties;
        }
    }
}