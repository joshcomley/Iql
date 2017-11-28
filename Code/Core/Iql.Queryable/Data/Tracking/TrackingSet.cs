using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Extensions;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.Tracking
{
    public class TrackingSet<T> : ITrackingSet where T : class, IEntity
    {
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

        List<IUpdateEntityOperation> ITrackingSet.GetChangesInternal(List<IQueuedOperation> queue, bool reset = false)
        {
            return GetChangesInternal(queue, reset).Cast<IUpdateEntityOperation>().ToList();
        }

        public void Reset()
        {
            Clone = Set.CloneAs(DataContext, typeof(T));
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

        void ITrackingSet.MergeEntity(object entity)
        {
            MergeEntity((T)entity);
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

        public void Merge(List<T> data)
        {
            for (var i = 0; i < data.Count; i++)
            {
                var element = data[i];
                if (element == null)
                {
                    continue;
                }
                MergeEntity(element);
                data[i] = FindTrackedEntity(data[i]).Entity;
            }
        }

        public void MergeEntity(T element)
        {
            TrackInternal(element, true);
        }

        public void Track(T entity)
        {
            TrackInternal(entity, false);
        }

        private void TrackInternal(T entity, bool allowMerge)
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
                    if (!DataContext.IsEntityNew(entity, typeof(T))
                        && !DataContext.IsEntityNew(trackedEntity, typeof(T))
                        && DataContext.IsIdMatch(entity, trackedEntity, typeof(T)))
                    {
                        throw new EntityAlreadyTrackedException("Already tracking an entity with the same key.");
                    }
                }
            }
            if (!allowMerge && found)
            {
                return;
            }
            var clone = entity.CloneAs(DataContext, typeof(T));
            if (!found)
            {
                Set.Add(entity);
                Clone.Add(clone);
                _trackedEntityClones.Add(entity, clone);
            }
            else
            {
                if (existingEntity.Entity != entity)
                {
                    ObjectMerger.Merge(DataContext, TrackingSetCollection, entity);
                }
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

        public virtual TrackedEntity<T> FindTrackedEntityByKey(CompositeKey key)
        {
            foreach (var entity in Set)
            {
                if (DataContext.EntityHasKey(entity, typeof(T), key))
                {
                    return FindTrackedEntity(entity);
                }
            }
            return null;
        }

        public virtual TrackedEntity<T> FindTrackedEntity(T localEntity)
        {
            var entityConfiguration = DataContext.EntityConfigurationContext.GetEntity<T>();
            var key = entityConfiguration.Key;
            T matchedEntity = null;
            var relationships = new List<ITrackedRelationship>();
            var isNewEntity = DataContext.IsEntityNew(localEntity, typeof(T));
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
                    var persistenceKeyString = persistenceKey.ToString();
                    if (persistenceKeyString != Guid.Empty.ToString() && trackedEntityPersistenceKey != null && trackedEntityPersistenceKey.ToString() == persistenceKeyString)
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

        public List<UpdateEntityOperation<T>> GetChangesInternal(List<IQueuedOperation> queue, bool reset = false)
        {
            var updates = new List<UpdateEntityOperation<T>>();
            Set.ForEach(entity =>
            {
                //let ctor = entity["__ctor"]();
                var clone = FindClone(entity);
                if (clone != null)
                {
                    var entityState = GetEntityState(queue, entity, clone, typeof(T));
                    if (entityState != null)
                    {
                        updates.Add(new UpdateEntityOperation<T>(entity, DataContext, entityState));
                    }
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
                propertyChange = new PropertyChange(property, oldValue, newValue);
            }
            return propertyChange;
        }

        private EntityState GetEntityState(List<IQueuedOperation> queue, T entity, object clone, Type entityType)
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
                return new EntityState(entity, entityType, changedProperties);
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
                                TrackingSetCollection.Track(child, relationship.OtherEnd.Type);
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