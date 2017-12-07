using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Queryable.Data.Crud;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Crud.Operations.Queued;
using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Data.Tracking;
using Iql.Queryable.Data.Validation;
using Iql.Queryable.Extensions;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.DataStores
{
    public class DataStore : IDataStore
    {
        public TrackingSetCollection Tracking { get; private set; }

        public IDataContext DataContext { get; set; }
        public List<IQueuedOperation> Queue { get; set; } = new List<IQueuedOperation>();

        public virtual AddEntityResult<TEntity> Add<TEntity>(AddEntityOperation<TEntity> operation)
            where TEntity : class
        {
            //var flattened = DataContext.EntityConfigurationContext.FlattenObjectGraph(operation.Entity, typeof(TEntity));
            var flattened = DataContext.EntityConfigurationContext.FlattenObjectGraph(operation.Entity, operation.EntityType);
            var nonTracked = new List<FlattenedEntity>();
            AddEntityResult<TEntity> result = null;
            foreach (var entity in flattened)
            {
                var alreadyTracked = DataContext.DataStore.GetTracking().IsTracked(entity.Entity, entity.EntityType);
                if (!alreadyTracked)
                {
                    nonTracked.Add(entity);
                    var trackingSetCollection = GetTracking();
                    trackingSetCollection.Track(entity.Entity, entity.EntityType);
                    var isRootEntity = entity.Entity == operation.Entity;
                    var entityOperation =
                        isRootEntity
                            ? operation
                            : Activator.CreateInstance(typeof(AddEntityOperation<>).MakeGenericType(entity.EntityType),
                                new object[] { entity.Entity, operation.DataContext });
                    var entityResult = Activator.CreateInstance(typeof(AddEntityResult<>).MakeGenericType(entity.EntityType),
                        new object[]
                        {
                            true,
                            entityOperation
                        });
                    var queuedOperation =
                        (IQueuedOperation)Activator.CreateInstance(typeof(QueuedAddEntityOperation<>).MakeGenericType(entity.EntityType),
                            new object[] { operation, entityResult });
                    Queue.Add(queuedOperation);
                    if (isRootEntity)
                    {
                        result = (AddEntityResult<TEntity>)entityResult;
                    }
                }
            }
            //foreach (var entity in nonTracked)
            //{
            //    Tracking.TrackingSet(entity.EntityType)
            //        .Unwatch(entity.Entity);
            //}
            foreach (var entity in nonTracked)
            {
                RelationshipManagerBase.TrackRelationships(entity.Entity, entity.EntityType, DataContext);
            }
            //foreach (var entity in nonTracked)
            //{
            //    Tracking.TrackingSet(entity.EntityType)
            //        .Watch(entity.Entity);
            //}
            return result;
        }

        public virtual UpdateEntityResult<TEntity> Update<TEntity>(
            UpdateEntityOperation<TEntity> operation)
            where TEntity : class
        {
            var result = new UpdateEntityResult<TEntity>(true, operation);
            Queue.Add(
                new QueuedUpdateEntityOperation<TEntity>(
                    operation,
                    result));
            return result;
        }

        public virtual DeleteEntityResult<TEntity> Delete<TEntity>(
            DeleteEntityOperation<TEntity> operation)
            where TEntity : class
        {
            //            new RelationshipManager(DataContext).DeleteRelationships(operation.Entity, operation.EntityType);
            var result = new DeleteEntityResult<TEntity>(true, operation);
            Queue.Add(
                new QueuedDeleteEntityOperation<TEntity>(
                    operation,
                    result));
            return result;
        }

        public virtual async Task<AddEntityResult<TEntity>> PerformAdd<TEntity>(
            QueuedAddEntityOperation<TEntity> operation) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual async Task<UpdateEntityResult<TEntity>> PerformUpdate<TEntity>(
            QueuedUpdateEntityOperation<TEntity> operation) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual async Task<DeleteEntityResult<TEntity>> PerformDelete<TEntity>(
            QueuedDeleteEntityOperation<TEntity> operation) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public bool DummyField { get; set; } = false;
        public int DummyCount { get; set; }
        public virtual TrackingSetCollection GetTracking()
        {
            DummyField = true;
            if (DummyCount > 7)
            {
                DummyCount = 1;
            }
            else if (DummyCount < 9)
            {
                DummyCount++;
            }
            return Tracking ?? (Tracking = new TrackingSetCollection(DataContext));
        }

        public virtual async Task<GetDataResult<TEntity>> Get<TEntity>(GetDataOperation<TEntity> operation)
            where TEntity : class
        {
            if (!operation.Queryable.HasDefaults)
            {
                var getConfiguration = DataContext.GetConfiguration<EntityDefaultQueryConfiguration>();
                if (getConfiguration == null)
                {
                    getConfiguration = new EntityDefaultQueryConfiguration();
                    DataContext.RegisterConfiguration(getConfiguration);
                }
                var queryableGetter = getConfiguration.GetQueryable<TEntity>();
                if (queryableGetter != null)
                {
                    var queryable = queryableGetter() as IQueryable<TEntity>;
                    queryable.Operations.AddRange(operation.Queryable.Operations);
                    operation.Queryable = queryable;
                }
                if (getConfiguration.AlwaysIncludeCount)
                {
                    var countOperationCount = operation.Queryable.Operations.Count(o => o is IncludeCountOperation);
                    if (countOperationCount == 0)
                    {
                        operation.Queryable.Operations.Add(new IncludeCountOperation());
                    }
                }
                operation.Queryable.HasDefaults = true;
            }
            var listenConfiguration = DataContext.GetConfiguration<DataContextEventsConfiguration>();
            if (listenConfiguration != null && listenConfiguration.GetBeginListeners != null)
            {
                foreach (var listener in listenConfiguration.GetBeginListeners)
                {
                    listener(operation);
                }
            }
            var result = new GetDataResult<TEntity>(null, operation, true);
            // perform get and set up tracking on the objects
            var trackingSet = GetTracking().GetSet<TEntity>();
            var response = await PerformGet(new QueuedGetDataOperation<TEntity>(
                operation,
                result));
            // Clone the queryable so any changes made in the application code
            // don't trickle down to our result
            response.Queryable = (IQueryable<TEntity>)operation.Queryable.Copy();
#if TypeScript
            response.Data = (DbList<TEntity>)DataContext.EnsureTypedListByType(response.Data, typeof(TEntity), null, null, true);
#endif
            response.Data.SourceQueryable = (DbQueryable<TEntity>)response.Queryable;
            if (response.TotalCount.HasValue)
            {
                var skipOperations = response.Queryable.Operations.Where(o => o is SkipOperation);
                var skippedSoFar = skipOperations.Sum(o => (o as SkipOperation).Skip);
                var pageSize = 0;
                var totalCount = response.TotalCount.Value;
                var page = 0;
                if (skippedSoFar == 0)
                {
                    pageSize = response.Data.Count;
                }
                else
                {
                    pageSize = (skipOperations.Last() as SkipOperation).Skip;
                    //if (skippedSoFar + response.Data.Count == totalCount)
                    //{
                    //    // We're on the last page
                    //}
                    //else
                    //{
                    //    pageSize = skippedSoFar / response.Data.Count;
                    //}
                }
                if (pageSize > 0)
                {
                    page = skippedSoFar / pageSize;
                }
                var pageCount = 0;
                var i = totalCount;
                while (i > 0)
                {
                    pageCount++;
                    i -= pageSize;
                }
                response.Data.PagingInfo = new PagingInfo(skippedSoFar, totalCount, pageSize, page, pageCount);
            }
            if (response.Data.SourceQueryable.TrackEntities)
            {
                trackingSet.Merge(response.Data);
            }
            var flattened = DataContext.EntityConfigurationContext.FlattenObjectGraphs(typeof(TEntity), response.Data.ToArray());
            foreach (var entity in flattened)
            {
                await RelationshipManagerBase.TrackAndRefreshRelationships(entity.Entity, entity.EntityType, DataContext);
            }
            return result;
        }

        public virtual async Task<GetDataResult<TEntity>> PerformGet<TEntity>(QueuedGetDataOperation<TEntity> operation)
            where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual async Task<SaveChangesResult> SaveChanges(
            SaveChangesOperation operation)
        {
            // Sets could be added to whilst detecting changes
            // so get a copy now
            Queue.AddRange(GetChanges(true));
            //var observable = this.Observable<SaveChangesResult>();
            var saveChangesResult = new SaveChangesResult(false);
            var queue = Queue;
            Queue = new List<IQueuedOperation>();
            foreach (var queuedOperation in queue)
            {
                var task = GetType()
                    .GetMethod(nameof(Perform))
                    .MakeGenericMethod(queuedOperation.Operation.EntityType)
                    .Invoke(this, new object[]
                    {
                        queuedOperation, saveChangesResult
#if TypeScript // The type info
                        ,queuedOperation.Operation.EntityType
#endif
                    }) as Task;
                await task;
            }
            return saveChangesResult;
        }

        public IEnumerable<IQueuedOperation> GetChanges(bool reset = false)
        {
            var changes = new List<IQueuedOperation>();
            GetTracking().GetChanges(this.Queue, reset).ForEach(update =>
            {
                // If we are adding an entity in the same save changes operation
                // then we don't need to do any scheduled updates on it because
                // they will be negated by the add operation
                if (Queue.Any(q => q.Operation.Type == OperationType.Add
                                   && (q.Operation as IEntityCrudOperationBase).Entity == update.Entity))
                {
                    return;
                }
                var alreadyBeingUpdatedByAnotherOperation = false;
                foreach (var queuedOperation in Queue)
                {
                    if (queuedOperation.Type == QueuedOperationType.Update)
                    {
                        var entityOperation = queuedOperation.Operation as IEntityCrudOperationBase;
                        var entitiesInObjectGraph = DataContext.EntityConfigurationContext.FlattenObjectGraph(
                            entityOperation.Entity,
                            queuedOperation.Operation.EntityType);
                        foreach (var flattenedEntity in entitiesInObjectGraph)
                        {
                            if (flattenedEntity.Entity == update.Entity)
                            {
                                alreadyBeingUpdatedByAnotherOperation = true;
                                break;
                            }
                        }
                        if (alreadyBeingUpdatedByAnotherOperation)
                        {
                            break;
                        }
                    }
                }
                if (!alreadyBeingUpdatedByAnotherOperation)
                {
                    var updateOperation =
                        Activator.CreateInstance(
                            typeof(QueuedUpdateEntityOperation<>).MakeGenericType(update.EntityType), update, null);
                    changes.Add((IQueuedOperation)updateOperation);
                }
                //this.Queue.Add(new QueuedUpdateEntityOperation<object>(update, new UpdateEntityResult<object>(true, update)));
                //Apply(update);
            });
            return changes;
        }

        protected int FindEntityIndex<TEntity>(
            Type entityType,
            TEntity clone,
            IList<TEntity> data) where TEntity : class
        {
            return Entity.FindIndexOfEntityInSetByKey(
                DataContext,
                clone,
                data
            );
        }

        public virtual async Task Perform<TEntity>(
            IQueuedOperation operation,
            SaveChangesResult saveChangesResult) where TEntity : class
        {
            //var ctor: { new(entityType: { new(): any }, success: boolean, entity: any): any };
            ICrudResult result = null;
            switch (operation.Operation.Type)
            {
                case OperationType.Add:
                    var addEntityOperation = (QueuedAddEntityOperation<TEntity>)operation;
                    var localEntity = addEntityOperation.Operation.Entity;
                    EnsurePersistenceKeyInNewEntities(localEntity, typeof(TEntity));
                    result = await PerformAdd(addEntityOperation);
                    var remoteEntity = addEntityOperation.Result.RemoteEntity;
                    if (remoteEntity != null)
                    {
                        foreach (var keyProperty in DataContext.EntityConfigurationContext.GetEntity<TEntity>().Key.Properties)
                        {
                            localEntity.SetPropertyValue(keyProperty.PropertyName, remoteEntity.GetPropertyValue(keyProperty.PropertyName));
                        }
                    }
                    await DataContext.RefreshEntity(localEntity, typeof(TEntity));
                    GetTracking().Merge(addEntityOperation.Operation.Entity, typeof(TEntity));
                    break;
                case OperationType.Update:
                    var updateEntityOperation = (QueuedUpdateEntityOperation<TEntity>)operation;
                    if (DataContext.IsEntityNew(updateEntityOperation.Operation.Entity, typeof(TEntity)))
                    {
                        operation.Result.Success = false;
                        var failure = new EntityValidationResult(updateEntityOperation.Operation.EntityType);
                        failure.AddFailure("This entity has not yet been saved so it cannot be updated.");
                        updateEntityOperation.Result.RootEntityValidationResult = failure;
                    }
                    else
                    {
                        EnsurePersistenceKeyInNewEntities(updateEntityOperation.Operation.Entity, typeof(TEntity));
                        result = await PerformUpdate(updateEntityOperation);
                        var operationEntity = updateEntityOperation.Operation
                            .Entity;
                        await DataContext.RefreshEntity(operationEntity, typeof(TEntity));
                        GetTracking().GetSet<TEntity>().Track(operationEntity);
                    }
                    break;
                case OperationType.Delete:
                    var deleteEntityOperation = (QueuedDeleteEntityOperation<TEntity>)operation;
                    if (DataContext.IsEntityNew(deleteEntityOperation.Operation.Entity, typeof(TEntity)))
                    {
                        operation.Result.Success = false;
                        var failure = new EntityValidationResult(deleteEntityOperation.Operation.EntityType);
                        failure.AddFailure("This entity has not yet been saved so it cannot be updated.");
                        deleteEntityOperation.Result.RootEntityValidationResult = failure;
                    }
                    else
                    {
                        result = await PerformDelete(deleteEntityOperation);
                        GetTracking().GetSet<TEntity>().Untrack(deleteEntityOperation.Operation.Entity);
                    }
                    break;
            }
            var entityCrudResult = operation.Result as IEntityCrudResult;
            if (entityCrudResult != null)
            {
                saveChangesResult.Results.Add(entityCrudResult);
                entityCrudResult.EntityValidationResults = entityCrudResult.EntityValidationResults ??
                                                       new Dictionary<object, EntityValidationResult>();
                ParseEntityResult(
                    entityCrudResult.EntityValidationResults,
                    entityCrudResult.LocalEntity,
                    entityCrudResult.RootEntityValidationResult);
            }
        }

        private static void ParseEntityResult(IDictionary<object, EntityValidationResult> resultsDictionary, object entity,
            EntityValidationResult entityValidationResult)
        {
            if (entityValidationResult == null || resultsDictionary.ContainsKey(entity))
            {
                return;
            }
            resultsDictionary.Add(entity, entityValidationResult);
            entityValidationResult.LocalEntity = entity;
            foreach (var collectionValidationResultSet in entityValidationResult
                .RelationshipCollectionValidationResults)
            {
                foreach (var validationResult in collectionValidationResultSet.RelationshipValidationResults)
                {
                    var index = validationResult.Key;
                    var collectionEntity =
                        ((IList)entity.GetPropertyValue(validationResult.Value.PropertyName))[index];
                    ParseEntityResult(resultsDictionary, collectionEntity, validationResult.Value.EntityValidationResult);
                }
            }
            foreach (var relationshipResult in entityValidationResult.RelationshipValidationResults)
            {
                var relationshipEntity = entity.GetPropertyValue(relationshipResult.PropertyName);
                ParseEntityResult(resultsDictionary, relationshipEntity, relationshipResult.EntityValidationResult);
            }
        }

        private void EnsurePersistenceKeyInNewEntities(object localEntity, Type entityType)
        {
            var flattened = DataContext.EntityConfigurationContext.FlattenObjectGraph(localEntity, entityType);
            foreach (var entity in flattened)
            {
                if (DataContext.IsEntityNew(entity.Entity, entity.EntityType))
                {
                    var persistenceKey = DataContext.EntityConfigurationContext.GetEntityByType(entity.EntityType).Properties
                        .FirstOrDefault(p => p.Name == "PersistenceKey");
                    if (persistenceKey != null)
                    {
                        entity.Entity.SetPropertyValue("PersistenceKey", Guid.NewGuid());
                    }
                    var tracking = GetTracking();
                    var trackedEntity = tracking.FindEntity(entity.Entity);
                    if (trackedEntity == null)
                    {
                        tracking.Track(entity.Entity, entityType);
                    }
                }
            }
        }
        public void RemoveQueuedOperationsForEntity(
            object changeItem,
            QueuedOperationType queuedOperationType)
        {
            var addOperationsToRemove = DataContext.DataStore.Queue
                .Where(q => q.Type == queuedOperationType)
                .Where(q => ((IEntityCrudOperationBase)q.Operation).Entity == changeItem)
                .ToList();
            foreach (var item in addOperationsToRemove)
            {
                DataContext.DataStore.Queue.Remove(item);
            }
        }
    }
}