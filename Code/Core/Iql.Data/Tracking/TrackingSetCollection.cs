using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Iql.Conversion;
using Iql.Data.Context;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.DataStores;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Extensions;
using Newtonsoft.Json;

namespace Iql.Data.Tracking
{
    public class TrackingSetCollection : IJsonSerializable
    {
        public DataTracker DataTracker { get; }
        public bool TrackEntities { get; }
        public string Id { get; }

        public TrackingSetCollection(DataTracker dataTracker, bool trackEntities = true)
        {
            DataTracker = dataTracker;
            TrackEntities = trackEntities;
            SetsMap = new Dictionary<string, ITrackingSet>();
            Sets = new List<ITrackingSet>();
            Id = Guid.NewGuid().ToString();
        }

        internal bool ProcessingRelationshipChange { get; set; }
        public Dictionary<string, ITrackingSet> SetsMap { get; set; }
        public List<ITrackingSet> Sets { get; set; }

        public int GetPendingDependencyCount(object entity, Type entityType = null)
        {
            var count = 0;
            var flattened =
                DataTracker.EntityConfigurationBuilder.FlattenDependencyGraph(entity,
                    entityType);
            foreach (var item in flattened)
            {
                var tracking = TrackingSetByType(item.Key);
                foreach (var dependency in item.Value)
                {
                    if (dependency == entity)
                    {
                        continue;
                    }
                    if (tracking.FindMatchingEntityState(dependency).IsNew)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public List<IEntityCrudOperationBase> GetInserts(object[] entities = null)
        {
            var inserts = new List<IEntityCrudOperationBase>();
            foreach (var set in Sets)
            {
                inserts.AddRange(set.GetInserts(entities));
            }

            inserts = inserts
                .OrderBy(operation => GetPendingDependencyCount(operation.Entity, operation.EntityType))
                .ToList();
            return inserts;
        }

        public List<IUpdateEntityOperation> GetUpdates(object[] entities = null, IProperty[] properties = null)
        {
            var updates = new List<IUpdateEntityOperation>();
            foreach (var set in Sets)
            {
                updates.AddRange(set.GetUpdates(entities, properties));
            }
            return updates;
        }

        public List<IEntityCrudOperationBase> GetDeletions(object[] entities = null)
        {
            var deletions = new List<IEntityCrudOperationBase>();
            foreach (var set in Sets)
            {
                deletions.AddRange(set.GetDeletions(entities));
            }
            return deletions;
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
                var set = new TrackingSet<T>(DataTracker);
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
            return TrackingSetByType(entityType).IsMatchingEntityTracked(key);
        }

        public bool IsTracked(object entity, Type entityType = null)
        {
            return GetTrackingSetForEntity(entity, entityType) != null;
        }

        public ITrackingSet GetTrackingSetForEntity(object entity, Type entityType = null)
        {
            if (entity == null)
            {
                return null;
            }

            var trackingSetByType = TrackingSetByType(entityType ?? entity.GetType());
            var isTracked = trackingSetByType.IsTracked(entity);
            if (isTracked)
            {
                return trackingSetByType;
            }
            foreach (var trackingSet in Sets)
            {
                if (trackingSet != trackingSetByType && trackingSet.IsTracked(entity))
                {
                    return trackingSet;
                }
            }
            return null;
        }

        public bool IsMarkedForDeletion(object entity, Type entityType)
        {
            var set = TrackingSetByType(entityType);
            return set.FindMatchingEntityState(entity).MarkedForDeletion;
        }

        public bool IsMarkedForCascadeDeletion(object entity, Type entityType)
        {
            var set = TrackingSetByType(entityType);
            return set.FindMatchingEntityState(entity).MarkedForCascadeDeletion;
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

        private IEnumerable<IQueuedOperation> GetQueuedOperations(object[] entities = null, IProperty[] properties = null)
        {
            var changes = new List<IQueuedOperation>();
            GetDeletions(entities).ForEach(deletion =>
            {
                var queuedOperation =
                    Activator.CreateInstance(
                        typeof(QueuedDeleteEntityOperation<>).MakeGenericType(deletion.EntityType), deletion, null);
                changes.Add((IQueuedOperation)queuedOperation);
            });
            GetUpdates(entities, properties).ForEach(update =>
            {
                var queuedOperation =
                    Activator.CreateInstance(
                        typeof(QueuedUpdateEntityOperation<>).MakeGenericType(update.EntityType), update, null);
                changes.Add((IQueuedOperation)queuedOperation);
            });
            GetInserts(entities).ForEach(insert =>
            {
                var queuedOperation =
                    Activator.CreateInstance(
                        typeof(QueuedAddEntityOperation<>).MakeGenericType(insert.EntityType), insert, null);
                changes.Add((IQueuedOperation)queuedOperation);
            });
            return changes;
        }

        public IEnumerable<IQueuedOperation> GetChanges(object[] entities = null, IProperty[] properties = null)
        {
            var queue = new List<IQueuedOperation>();
            var queuedOperations = GetQueuedOperations(entities, properties).ToArray();
            for (var i = 0; i < queuedOperations.Length; i++)
            {
                var operation = queuedOperations[i];
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

        public string SerializeToJson()
        {
            return JsonConvert.SerializeObject(PrepareForJson());
        }

        public object PrepareForJson()
        {
            return new
            {
                TrackEntities,
                Id,
                Sets = Sets.Select(_ => _.PrepareForJson())
            };
        }

        public void Reset(Dictionary<Type, IList> entities)
        {
            foreach (var entry in entities)
            {
                var setType = entry.Key;
                var set = Sets.FirstOrDefault(_ => _.EntityConfiguration.Type == setType);
                if (set != null)
                {
                    foreach (var entity in entry.Value)
                    {
                        set.FindMatchingEntityState(entity)?.Reset();
                    }
                }
            }
        }

        public void AbandonChanges()
        {
            for (var i = 0; i < Sets.Count; i++)
            {
                var set = Sets[i];
                set.AbandonChanges();
            }
        }

        public void Clear()
        {
            for (var i = 0; i < Sets.Count; i++)
            {
                var set = Sets[i];
                set.Clear();
            }
        }
    }
}