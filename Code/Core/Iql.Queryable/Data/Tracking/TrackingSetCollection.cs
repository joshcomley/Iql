using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Crud.Operations.Queued;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Exceptions;

namespace Iql.Queryable.Data.Tracking
{
    public class TrackingSetCollection
    {
        public TrackingSetCollection(IDataContext dataContext)
        {
            DataContext = dataContext;
            SetsMap = new Dictionary<string, ITrackingSet>();
            Sets = new List<ITrackingSet>();
        }

        internal bool ProcessingRelationshipChange { get; set; }
        public Dictionary<string, ITrackingSet> SetsMap { get; set; }
        public List<ITrackingSet> Sets { get; set; }
        private IDataContext DataContext { get; }

        public List<IEntityCrudOperationBase> GetInserts()
        {
            var inserts = new List<IEntityCrudOperationBase>();
            foreach (var set in Sets)
            {
                inserts.AddRange(set.GetInserts());
            }
            return inserts;
        }

        public List<IEntityCrudOperationBase> GetDeletions()
        {
            var deletions = new List<IEntityCrudOperationBase>();
            foreach (var set in Sets)
            {
                deletions.AddRange(set.GetDeletions());
            }
            return deletions;
        }

        public List<IUpdateEntityOperation> GetUpdates()
        {
            var updates = new List<IUpdateEntityOperation>();
            foreach (var set in Sets)
            {
                updates.AddRange(set.GetUpdates());
            }
            return updates;
            //ClearParents();
            //var changes = new Dictionary<Type, List<IUpdateEntityOperation>>();
            //var setsChecked = new List<ITrackingSet>();

            //while (true)
            //{
            //    var sets = Sets.ToList();
            //    foreach (var set in sets)
            //    {
            //        if (!setsChecked.Contains(set))
            //        {
            //            if (!changes.ContainsKey(set.EntityType))
            //            {
            //                changes.Add(set.EntityType, new List<IUpdateEntityOperation>());
            //            }
            //            var updateEntityOperations = set.GetChangesInternal();                        
            //            changes[set.EntityType].AddRange(updateEntityOperations);
            //            setsChecked.Add(set);
            //        }
            //    }
            //    if (Sets.Count == sets.Count)
            //    {
            //        break;
            //    }
            //}
            //ClearParents();
            //foreach (var set in Sets.ToList())
            //{
            //    set.EnsureIntegrity();
            //    foreach (var entity in set.TrackedEntites())
            //    {
            //        var relationships = set.FindTrackedEntity(entity).TrackedRelationships;
            //        foreach (var relationship in relationships)
            //        {
            //            var compositeKey = relationship.OwnerDetail.GetCompositeKey(relationship.Owner, true);
            //            // Sanitize the relationship
            //            if (!DataContext.EntityPropertiesMatch(
            //                relationship.Entity,
            //                compositeKey))
            //            {
            //                var entityKey = relationship.EntityDetail.GetCompositeKey(relationship.Entity);
            //                //var properties = new List<string>();
            //                //foreach (var constraint in relationship.OwnerDetail.Constraints())
            //                //{
            //                //    properties.Add(constraint.PropertyName);
            //                //}
            //                //properties.Add(relationship.OwnerDetail.Property.PropertyName);
            //                if (changes.ContainsKey(relationship.OwnerDetail.Type))
            //                {
            //                    if (changes[relationship.OwnerDetail.Type].Any(c =>
            //                        c.EntityState.Entity == relationship.Entity &&
            //                        c.EntityState.ChangedProperties.Any(cp =>
            //                            cp.Property.Name == relationship.OwnerDetail.Property.PropertyName)))
            //                    {
            //                        // This is a deliberate change
            //                        // Throw some error
            //                    }
            //                    else if (entityKey.Keys.All(k => !Equals(k.Value, Activator.CreateInstance(k.ValueType))))
            //                    {
            //                        var value = relationship.Owner.GetPropertyValue(relationship.OwnerDetail.Property
            //                            .PropertyName);
            //                        ClearParent(value, relationship.OwnerDetail.Property
            //                            .PropertyName);
            //                        if (relationship.OwnerDetail.IsCollection)
            //                        {
            //                            var collection = value as IList;
            //                            collection.Remove(relationship.Entity);
            //                        }
            //                        else
            //                        {
            //                            relationship.Owner.SetPropertyValue(relationship.OwnerDetail.Property
            //                                .PropertyName, null);
            //                        }
            //                    }
            //                }
            //            }
            //            //var ownerProperty =
            //            //    relationship.Owner.GetPropertyValue(relationship.OwnerDetail.Property.PropertyName);
            //            //if (relationship.OwnerDetail.IsCollection)
            //            //{
            //            //    var collection = ownerProperty as IList;

            //            //}
            //        }
            //        /* - Run through each relationship
            //         * - Check integrity
            //         * - If OK
            //         *   - Do nothing
            //         * - Else
            //         *   - Check if this is a deliberate change from the user (i.e. we have a record of it in the changes above)
            //         *     - If it is deliberate
            //         *       - Throw integrity check error
            //         *     - If not
            //         *       - Correct integrity
            //         */
            //    }
            //}
            //var allChanges = new List<IUpdateEntityOperation>();
            //// At this point we must sanitise the changes
            //foreach (var type in changes)
            //{
            //    allChanges.AddRange(changes[type.Key]);
            //    foreach (var change in type.Value)
            //    {
            //        var relationshipChanges =
            //            change.EntityState.ChangedProperties.Where(p => p.Property.Kind == PropertyKind.Relationship)
            //                .ToList();
            //        var relationshipKeyChanges =
            //            change.EntityState.ChangedProperties.Where(p => p.Property.Kind == PropertyKind.RelationshipKey)
            //                .ToList();
            //        int a = 0;
            //    }
            //}
            //return allChanges;
        }

        public TrackingSet<T> GetSet<T>() where T : class
        {
            var type = typeof(T);
            if (!SetsMap.ContainsKey(type.Name))
            {
                var set = new TrackingSet<T>(DataContext, this);
                SetsMap[type.Name] = set;
                Sets.Add(set);
            }
            return SetsMap[type.Name] as TrackingSet<T>;
        }

        public bool IsTracked(object entity, Type entityType)
        {
            var set = TrackingSet(entityType);
            var trackedEntity = set.FindTrackedEntity(entity);
            return trackedEntity != null && trackedEntity.Entity == entity;
        }

        public bool IsMarkedForDeletion(object entity, Type entityType)
        {
            var set = TrackingSet(entityType);
            return set.GetEntityState(entity).MarkedForDeletion;
        }

        public bool IsMarkedForCascadeDeletion(object entity, Type entityType)
        {
            var set = TrackingSet(entityType);
            return set.GetEntityState(entity).MarkedForCascadeDeletion;
        }

        public void TrackGraph(object entity, Type entityType)
        {
            var flattenObjectGraph = DataContext.EntityConfigurationContext.FlattenObjectGraph(entity, entityType);
            foreach (var obj in flattenObjectGraph)
            {
                var set = TrackingSet(obj.EntityType);
                set.Track(obj.Entity, false);
            }
        }

        public void TrackEntity(object entity, Type entityType, bool isNew)
        {
            var set = TrackingSet(entityType);
            set.Track(entity, isNew);
        }

        public void Merge(object entity, Type entityType, bool isNew)
        {
            var flattenObjectGraph = DataContext.EntityConfigurationContext.FlattenObjectGraph(entity, entityType);
            foreach (var obj in flattenObjectGraph)
            {
                var set = TrackingSet(obj.EntityType);
                set.MergeEntity(obj.Entity, isNew);
            }
        }

        public ITrackingSet TrackingSet(Type entityType)
        {
            var set = (ITrackingSet)typeof(TrackingSetCollection).GetRuntimeMethods()
                .First(m => m.Name == nameof(GetSet))
                .MakeGenericMethod(entityType)
                .Invoke(this, new object[]
                {
#if TypeScript
                    entityType
#endif
                });
            return set;
        }

        public object FindClone(object entity)
        {
            if (entity == null)
            {
                return null;
            }
            var type = entity.GetType();
            foreach (var set in Sets)
            {
                if (set.EntityType == type)
                {
                    return set.FindClone(entity);
                }
            }
            return null;
        }

        public ITrackedEntity FindEntity(object entity)
        {
            if (entity == null)
            {
                return null;
            }
            var type = entity.GetType();
            foreach (var set in Sets)
            {
                if (set.EntityType == type)
                {
                    return set.FindTrackedEntity(entity);
                }
            }
            return null;
        }

        internal void ClearParents()
        {
            _parents = new Dictionary<object, Dictionary<string, object>>();
        }

        /// <summary>
        /// First we look up an entity.
        /// Then we look up the relationship property that we're concerned with.
        /// </summary>
        private Dictionary<object, Dictionary<string, object>> _parents = new Dictionary<object, Dictionary<string, object>>();
        /// <summary>
        /// Part of the integrity check.
        /// If an entity is assigned to multiple parents when the relationship only allows one
        /// then we throw an exception.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="parent"></param>
        /// <param name="property"></param>
        internal void RecordParent(object entity, object parent, string property)
        {
            if (!_parents.ContainsKey(entity))
            {
                _parents.Add(entity, new Dictionary<string, object>());
            }
            if (_parents[entity].ContainsKey(property) && _parents[entity][property] != parent)
            {
                throw new DuplicateParentException(entity);
            }
            _parents[entity].Add(property, parent);
        }
        internal void ClearParent(object entity, string property)
        {
            //if (!_parents.ContainsKey(entity))
            //{
            //    _parents.Add(entity, new Dictionary<string, object>());
            //}
            //if (_parents[entity].ContainsKey(property))
            //{
            //    _parents[entity].Remove(property);
            //}
        }

        private IEnumerable<IQueuedOperation> GetQueuedUpdates()
        {
            var changes = new List<IQueuedOperation>();
            GetUpdates().ForEach(update =>
            {
                //// If we are adding an entity in the same save changes operation
                //// then we don't need to do any scheduled updates on it because
                //// they will be negated by the add operation
                //if (GetQueue().Any(q => q.Operation.Type == OperationType.Add
                //                   && (q.Operation as IEntityCrudOperationBase).Entity == update.Entity))
                //{
                //    return;
                //}
                //var alreadyBeingUpdatedByAnotherOperation = false;
                //foreach (var queuedOperation in GetQueue())
                //{
                //    if (queuedOperation.Type == QueuedOperationType.Update)
                //    {
                //        var entityOperation = queuedOperation.Operation as IEntityCrudOperationBase;
                //        var entitiesInObjectGraph = DataContext.EntityConfigurationContext.FlattenObjectGraph(
                //            entityOperation.Entity,
                //            queuedOperation.Operation.EntityType);
                //        foreach (var flattenedEntity in entitiesInObjectGraph)
                //        {
                //            if (flattenedEntity.Entity == update.Entity)
                //            {
                //                alreadyBeingUpdatedByAnotherOperation = true;
                //                break;
                //            }
                //        }
                //        if (alreadyBeingUpdatedByAnotherOperation)
                //        {
                //            break;
                //        }
                //    }
                //}
                //if (!alreadyBeingUpdatedByAnotherOperation)
                //{
                //    var updateOperation =
                //        Activator.CreateInstance(
                //            typeof(QueuedUpdateEntityOperation<>).MakeGenericType(update.EntityType), update, null);
                //    changes.Add((IQueuedOperation)updateOperation);
                //}
                //this.Queue.Add(new QueuedUpdateEntityOperation<object>(update, new UpdateEntityResult<object>(true, update)));
                //Apply(update);
                var queuedOperation =
                    Activator.CreateInstance(
                        typeof(QueuedUpdateEntityOperation<>).MakeGenericType(update.EntityType), update, null);
                changes.Add((IQueuedOperation)queuedOperation);
            });
            GetInserts().ForEach(insert =>
            {
                var queuedOperation =
                    Activator.CreateInstance(
                        typeof(QueuedAddEntityOperation<>).MakeGenericType(insert.EntityType), insert, null);
                changes.Add((IQueuedOperation)queuedOperation);
            });
            GetDeletions().ForEach(deletion =>
            {
                var queuedOperation =
                    Activator.CreateInstance(
                        typeof(QueuedDeleteEntityOperation<>).MakeGenericType(deletion.EntityType), deletion, null);
                changes.Add((IQueuedOperation)queuedOperation);
            });
            return changes;
        }

        public IEnumerable<IQueuedOperation> GetQueue()
        {
            var queue = new List<IQueuedOperation>();
            foreach (var operation in GetQueuedUpdates())
            {
                var filteredOperation = GetType()
                    .GetMethod(nameof(Filter))
                    .MakeGenericMethod(operation.Operation.EntityType)
                    .Invoke(this, new object[]
                    {
                        operation
#if TypeScript // The type info
                        , operation.Operation.EntityType
#endif
                    }) as IQueuedOperation;
                if (filteredOperation != null)
                {
                    queue.Add(filteredOperation);
                }
            }
            return queue;
        }

        public virtual IQueuedOperation Filter<TEntity>(
            IQueuedOperation operation) where TEntity : class
        {

            switch (operation.Operation.Type)
            {
                case OperationType.Add:
                    break;
                case OperationType.Delete:
                    break;
                case OperationType.Update:
                    break;
            }
            return operation;
        }
    }
}