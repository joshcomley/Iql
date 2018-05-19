using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.DataStores;
using Iql.Entities;
using Iql.Extensions;

namespace Iql.Data.Tracking
{
    public class TrackingSetCollection
    {
        public IDataStore DataStore { get; }
        public bool TrackEntities { get; }
        public string Id { get; }

        public TrackingSetCollection(IDataStore dataStore, bool trackEntities = true)
        {
            DataStore = dataStore;
            TrackEntities = trackEntities;
            SetsMap = new Dictionary<string, ITrackingSet>();
            Sets = new List<ITrackingSet>();
            Id = Guid.NewGuid().ToString();
        }

        internal bool ProcessingRelationshipChange { get; set; }
        public Dictionary<string, ITrackingSet> SetsMap { get; set; }
        public List<ITrackingSet> Sets { get; set; }

        public List<IEntityCrudOperationBase> GetInserts()
        {
            var inserts = new List<IEntityCrudOperationBase>();
            foreach (var set in Sets)
            {
                inserts.AddRange(set.GetInserts());
            }

            inserts = inserts
                .OrderBy(operation => GetPendingDependencyCount(operation.Entity, operation.EntityType))
                .ToList();
            return inserts;
        }

        public int GetPendingDependencyCount(object entity, Type entityType = null)
        {
            var count = 0;
            var flattened =
                DataStore.DataContext.EntityConfigurationContext.FlattenDependencyGraph(entity,
                    entityType);
            foreach (var item in flattened)
            {
                var tracking = DataStore.Tracking.TrackingSetByType(item.Key);
                foreach (var dependency in item.Value)
                {
                    if (dependency == entity)
                    {
                        continue;
                    }
                    if (tracking.GetEntityState(dependency).IsNew)
                    {
                        count++;
                    }
                }
            }

            return count;
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
        }

        public ITrackingSet TrackingSetByType(Type type)
        {
            if (!SetsMap.ContainsKey(type.Name))
            {
                var set = (ITrackingSet)typeof(TrackingSetCollection).GetRuntimeMethods()
                    .First(m => m.Name == nameof(TrackingSet))
                    .InvokeGeneric(this, new object[] { }, type);
                return set;
            }
            return SetsMap[type.Name];
        }

        public TrackingSet<T> TrackingSet<T>() where T : class
        {
            var type = typeof(T);
            if (!SetsMap.ContainsKey(type.Name))
            {
                var set = new TrackingSet<T>(DataStore, this);
                SetsMap[type.Name] = set;
                Sets.Add(set);
            }
            return (TrackingSet<T>)SetsMap[type.Name];
        }

        public bool EntityWithSameKeyIsBeingTracked(object entity, Type entityType)
        {
            return TrackingSetByType(entityType).DifferentEntityWithSameKeyIsTracked(entity);
        }

        public bool KeyIsTracked(CompositeKey key, Type entityType)
        {
            return TrackingSetByType(entityType).IsTracked(key);
        }

        public bool IsTracked(object entity, Type entityType)
        {
            return TrackingSetByType(entityType).IsTracked(entity);
        }

        public bool IsMarkedForDeletion(object entity, Type entityType)
        {
            var set = TrackingSetByType(entityType);
            return set.GetEntityState(entity).MarkedForDeletion;
        }

        public bool IsMarkedForCascadeDeletion(object entity, Type entityType)
        {
            var set = TrackingSetByType(entityType);
            return set.GetEntityState(entity).MarkedForCascadeDeletion;
        }

        //public void TrackGraph(object entity, Type entityType)
        //{
        //    var flattenObjectGraph = DataContext.EntityConfigurationContext.FlattenObjectGraph(entity, entityType);
        //    foreach (var obj in flattenObjectGraph)
        //    {
        //        var set = TrackingSetByType(obj.EntityType);
        //        set.TrackEntity(obj.Entity);
        //    }
        //}

        private IEnumerable<IQueuedOperation> GetQueuedOperations()
        {
            var changes = new List<IQueuedOperation>();
            GetUpdates().ForEach(update =>
            {
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

        public IEnumerable<IQueuedOperation> GetChanges()
        {
            var queue = new List<IQueuedOperation>();
            foreach (var operation in GetQueuedOperations())
            {
                var filteredOperation = GetType()
                    .GetMethod(nameof(Filter))
                    .InvokeGeneric(this, new object[]
                    {
                        operation
                    }, operation.Operation.EntityType)
                    as IQueuedOperation;
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