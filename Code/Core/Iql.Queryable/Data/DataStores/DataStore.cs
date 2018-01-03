using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Queryable.Data.Crud;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Crud.Operations.Queued;
using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Data.Crud.State;
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


        public IEnumerable<IQueuedOperation> GetQueue()
        {
            return GetTracking().GetQueue();
        }

        public IEnumerable<IQueuedOperation> GetChanges()
        {
            return GetTracking().GetQueue().Where(op => op.Type == QueuedOperationType.Update);
        }

        public virtual EntityState<TEntity> Add<TEntity>(TEntity entity)
            where TEntity : class
        {
            var trackingSet = Tracking.GetSet<TEntity>();
            //var flattened = DataContext.EntityConfigurationContext.FlattenObjectGraph(operation.Entity, typeof(TEntity));
            
            var existingState = (EntityState<TEntity>)trackingSet.GetEntityState(entity);
            if (existingState != null)
            {
                if (existingState.MarkedForDeletion)
                {
                    existingState.MarkedForDeletion = false;
                }
            }
            var flattened = DataContext.EntityConfigurationContext.FlattenObjectGraph(entity, typeof(TEntity));
            var nonTracked = new List<FlattenedEntity>();
            var tracking = DataContext.DataStore.GetTracking();
            foreach (var flattenedEntity in flattened)
            {
                if (!tracking.IsTracked(flattenedEntity.Entity, flattenedEntity.EntityType))
                {
                    nonTracked.Add(flattenedEntity);
                    tracking.TrackEntity(flattenedEntity.Entity, flattenedEntity.EntityType, true);
                }
            }
            foreach (var nonTrackedEntity in nonTracked)
            {
                RelationshipManagerBase.TrackRelationships(nonTrackedEntity.Entity, nonTrackedEntity.EntityType, DataContext);
            }
            return existingState;
        }

        //public virtual EntityState<TEntity> Update<TEntity>(
        //    TEntity entity)
        //    where TEntity : class
        //{
        //    var trackingSet = Tracking.TrackingSet(typeof(TEntity));
        //    var entityState = trackingSet.GetEntityState(operation.Entity);
        //    if (entityState.MarkedForDeletion)
        //    {
        //        throw new Exception("You cannot update an entity that has been deleted.");
        //    }
        //    if (entityState.IsNew)
        //    {
        //        return null;
        //    }
        //    var result = new UpdateEntityResult<TEntity>(true, operation);
        //    Enqueue(
        //        new QueuedUpdateEntityOperation<TEntity>(
        //            operation,
        //            result));
        //    return result;
        //}

        public virtual EntityState<TEntity> Delete<TEntity>(TEntity entity)
            where TEntity : class
        {
            var trackingSet = Tracking.TrackingSet(typeof(TEntity));
            var entityState = (EntityState<TEntity>)trackingSet.GetEntityState(entity);


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
            //            new RelationshipManager(DataContext).DeleteRelationships(operation.Entity, operation.EntityType);
            //trackingSet.Untrack(operation.Entity);
            return entityState;
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

        public virtual TrackingSetCollection GetTracking()
        {
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
            // Flatten before we merge because the merge will update the result data set with
            // tracked data
            var flattened = DataContext.EntityConfigurationContext.FlattenObjectGraphs(typeof(TEntity), response.Data.ToArray());
            if (response.Data.SourceQueryable.TrackEntities)
            {
                foreach (var entity in flattened)
                {
                    var entityTrackingSet = GetTracking().TrackingSet(entity.EntityType);
                    //var state = DataContext.GetEntityState(entity.Entity);
                    var mergedEntity = entityTrackingSet.MergeEntity(entity.Entity, false);
                    if (mergedEntity != entity.Entity)
                    {
                        if (entity.EntityType == typeof(TEntity))
                        {
                            var index = response.Data.IndexOf((TEntity)entity.Entity);
                            if (index != -1)
                            {
                                response.Data[index] = (TEntity)mergedEntity;
                            }
                        }
                        var flattenedIndex = flattened.IndexOf(entity);
                        flattened[flattenedIndex].Entity = mergedEntity;
                    }
                }
            }
            foreach (var entity in flattened)
            {
                //var trackedEntity = GetTracking().TrackingSet(entity.EntityType).FindTrackedEntity(entity)
                await RelationshipManagerBase.TrackAndRefreshRelationships(entity.Entity, entity.EntityType, DataContext);
                var entityState = GetTracking().TrackingSet(entity.EntityType).GetEntityState(entity.Entity);
                entityState.IsNew = false;
                //entityState.IsNew = DataContext.IsEntityNew(entity.Entity, entity.EntityType);
            }
            for (var i = 0; i < response.Data.Count; i++)
            {
                response.Data[i] = trackingSet.FindTrackedEntity(response.Data[i]);
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
            //var observable = this.Observable<SaveChangesResult>();
            var saveChangesResult = new SaveChangesResult(false);
            var queue = GetQueue();
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
                        var trackingSet = GetTracking().TrackingSet(typeof(TEntity));
                        trackingSet.ChangeEntity(localEntity, () =>
                        {
                            foreach (var keyProperty in DataContext.EntityConfigurationContext.GetEntity<TEntity>().Key.Properties)
                            {
                                localEntity.SetPropertyValue(keyProperty, remoteEntity.GetPropertyValue(keyProperty));
                            }
                        }, ChangeEntityMode.NoKeyChecks);
                        trackingSet.GetEntityState(localEntity).IsNew = false;
                    }
                    await DataContext.RefreshEntity(localEntity, typeof(TEntity));
                    GetTracking().Merge(addEntityOperation.Operation.Entity, typeof(TEntity), false);
                    break;
                case OperationType.Update:
                    var updateEntityOperation = (QueuedUpdateEntityOperation<TEntity>)operation;
                    var isEntityNew = DataContext.IsEntityNew(updateEntityOperation.Operation.Entity, typeof(TEntity));
                    if (isEntityNew == true)
                    {
                        operation.Result.Success = false;
                        var failure = new EntityValidationResult(updateEntityOperation.Operation.EntityType);
                        failure.AddFailure("This entity has not yet been saved so it cannot be updated.");
                        updateEntityOperation.Result.RootEntityValidationResult = failure;
                    }
                    else if(isEntityNew != null)
                    {
                        EnsurePersistenceKeyInNewEntities(updateEntityOperation.Operation.Entity, typeof(TEntity));
                        result = await PerformUpdate(updateEntityOperation);
                        var operationEntity = updateEntityOperation.Operation
                            .Entity;
                        await DataContext.RefreshEntity(operationEntity, typeof(TEntity));
                        GetTracking().GetSet<TEntity>().Track(operationEntity, false);
                    }
                    break;
                case OperationType.Delete:
                    var deleteEntityOperation = (QueuedDeleteEntityOperation<TEntity>)operation;
                    var entityNew = DataContext.IsEntityNew(deleteEntityOperation.Operation.Entity, typeof(TEntity));
                    if (entityNew == true)
                    {
                        operation.Result.Success = false;
                        var failure = new EntityValidationResult(deleteEntityOperation.Operation.EntityType);
                        failure.AddFailure("This entity has not yet been saved so it cannot be updated.");
                        deleteEntityOperation.Result.RootEntityValidationResult = failure;
                    }
                    else if(entityNew != null)
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
                        ((IList)entity.GetPropertyValueByName(validationResult.Value.PropertyName))[index];
                    ParseEntityResult(resultsDictionary, collectionEntity, validationResult.Value.EntityValidationResult);
                }
            }
            foreach (var relationshipResult in entityValidationResult.RelationshipValidationResults)
            {
                var relationshipEntity = entity.GetPropertyValueByName(relationshipResult.PropertyName);
                ParseEntityResult(resultsDictionary, relationshipEntity, relationshipResult.EntityValidationResult);
            }
        }

        private void EnsurePersistenceKeyInNewEntities(object localEntity, Type entityType)
        {
            var flattened = DataContext.EntityConfigurationContext.FlattenObjectGraph(localEntity, entityType);
            foreach (var entity in flattened)
            {
                var isEntityNew = DataContext.IsEntityNew(entity.Entity, entity.EntityType);
                if (isEntityNew == true)
                {
                    var persistenceKey = DataContext.EntityConfigurationContext.GetEntityByType(entity.EntityType).Properties
                        .FirstOrDefault(p => p.Name == "PersistenceKey");
                    if (persistenceKey != null)
                    {
                        entity.Entity.SetPropertyValueByName("PersistenceKey", Guid.NewGuid());
                    }
                    var tracking = GetTracking();
                    var trackedEntity = tracking.FindEntity(entity.Entity);
                    if (trackedEntity == null)
                    {
                        tracking.TrackGraph(entity.Entity, entityType);
                    }
                }
            }
        }

        public void RemoveQueuedOperationsOfTypeForEntity(
            object changeItem,
            QueuedOperationType queuedOperationType)
        {
            //var addOperationsToRemove = DataContext.DataStore.GetQueue()
            //    .Where(q => q.Type == queuedOperationType)
            //    .Where(q => ((IEntityCrudOperationBase)q.Operation).Entity == changeItem)
            //    .ToList();
            //foreach (var item in addOperationsToRemove)
            //{
            //    Dequeue(item);
            //}
        }
    }
}