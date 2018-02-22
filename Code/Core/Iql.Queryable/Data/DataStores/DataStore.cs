using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Iql.Extensions;
using Iql.Queryable.Data.Crud;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Crud.Operations.Queued;
using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Data.Crud.State;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.Tracking;
using Iql.Queryable.Data.Validation;
using Iql.Queryable.Extensions;
using Iql.Queryable.Native;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.DataStores
{
    public class DataStore : IDataStore
    {
        private DataTracker _dataTracker;
        public static MethodInfo ToListTypedMethod { get; set; }

        public DataTracker DataTracker
        {
            get { return _dataTracker = _dataTracker ?? new DataTracker(this, true); }
        }

        public virtual TrackingSetCollection Tracking => DataTracker.Tracking;
        public virtual IRelationshipObserver RelationshipObserver => DataTracker.RelationshipObserver;

        public virtual IDataContext DataContext { get; set; }

        public IEnumerable<IQueuedOperation> GetQueue()
        {
            return Tracking.GetQueue();
        }

        public IEnumerable<IQueuedOperation> GetChanges()
        {
            return Tracking.GetQueue().Where(op => op.Type == QueuedOperationType.Update);
        }

        static DataStore()
        {
            AddInternalMethod = typeof(DataStore)
                .GetMethod(nameof(AddInternal),
                    BindingFlags.Instance | BindingFlags.NonPublic);
            DeleteInternalMethod = typeof(DataStore)
                .GetMethod(nameof(DeleteInternal),
                    BindingFlags.Instance | BindingFlags.NonPublic);
        }

        public static MethodInfo AddInternalMethod { get; set; }
        public static MethodInfo DeleteInternalMethod { get; set; }

#if !TypeScript
        public IEntityStateBase Add(object entity)
        {
            return (IEntityStateBase)AddInternalMethod.InvokeGeneric(this, new[] { entity }, entity.GetType());
        }
#endif

        public virtual EntityState<TEntity> Add<TEntity>(TEntity entity)
            where TEntity : class
        {
            var entityType = typeof(TEntity);
            if (entityType == typeof(object))
            {
                entityType = entity.GetType();
            }
            return (EntityState<TEntity>)AddInternalMethod.InvokeGeneric(this, new[] { entity }, entityType);
        }

        private IEntityStateBase AddInternal<T>(T entity)
        {
            var entityType = typeof(T);
            var flattened = DataContext.EntityConfigurationContext.FlattenObjectGraph(entity, entityType);
            IEntityStateBase entityState = null;
            foreach (var group in flattened)
            {
                foreach (var item in group.Value)
                {
                    var thisTrackingSet = Tracking.TrackingSetByType(group.Key);
                    var state = thisTrackingSet.TrackEntity(item);
                    state.UnmarkForDeletion();
                    if (item == (object)entity)
                    {
                        entityState = state;
                        if (entityState.Entity != (object)entity)
                        {
                            throw new Exception("An item with the same key is already being tracked.");
                        }
                    }
                }
            }
            RelationshipObserver.ObserveAll(flattened);
            return (EntityState<T>)entityState;
        }


#if !TypeScript
        public IEntityStateBase Delete(object entity)
        {
            return (IEntityStateBase)DeleteInternalMethod.InvokeGeneric(this, new[] { entity }, entity.GetType());
        }
#endif

        public virtual EntityState<TEntity> Delete<TEntity>(TEntity entity)
            where TEntity : class
        {
            var entityType = typeof(TEntity);
            if (entityType == typeof(object))
            {
                entityType = entity.GetType();
            }
            return (EntityState<TEntity>)DeleteInternalMethod.InvokeGeneric(this, new[] { entity }, entityType);
        }

        private EntityState<T> DeleteInternal<T>(T entity)
            where T : class
        {
            var trackingSet = Tracking.TrackingSet<T>();
            trackingSet.MarkForDelete(entity);
            RelationshipObserver.DeleteRelationships(entity, typeof(T));
            var entityState = (EntityState<T>)trackingSet.GetEntityState(entity);
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

            var response = new FlattenedGetDataResult<TEntity>(null, operation, true);
            // perform get and set up tracking on the objects
            await PerformGet(new QueuedGetDataOperation<TEntity>(
                operation,
                response));
            var success = response.Success && response.Data != null;
            // Clone the queryable so any changes made in the application code
            // don't trickle down to our result
            response.Queryable = (IQueryable<TEntity>)operation.Queryable.Copy();
            var dbList = new DbList<TEntity>();
            dbList.SourceQueryable = (DbQueryable<TEntity>)response.Queryable;
            if (response.TotalCount.HasValue && response.Data.Count != 0)
            {
                var skipOperations = response.Queryable.Operations.Where(o => o is SkipOperation);
                var skippedSoFar = skipOperations.Sum(o => (o as SkipOperation).Skip);
                int pageSize;
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

                dbList.PagingInfo = new PagingInfo(skippedSoFar, totalCount, pageSize, page, pageCount);
            }

            // Flatten before we merge because the merge will update the result data set with
            // tracked data
            GetDataResult<TEntity> getDataResult;
            if (success)
            {
                var tracker = dbList.SourceQueryable.TrackEntities 
                    ? DataTracker 
                    : new DataTracker(this, false);

                tracker.TrackResults<TEntity>(response);

                //
                //if (!operation.IsSingleResult)
                //{
                //    if (data.ContainsKey(typeof(TEntity)))
                //    {
                //        dbList.AddRange((List<TEntity>)data[typeof(TEntity)]);
                //    }
                //}
                //else
                //{
                //}
                dbList.AddRange(response.Root);
                getDataResult = new GetDataResult<TEntity>(dbList, operation, response.Success);
                getDataResult.TotalCount = response.TotalCount;
            }
            else
            {
                getDataResult = new GetDataResult<TEntity>(new DbList<TEntity>(), operation, false);
            }

            return getDataResult;
        }

        public virtual async Task<FlattenedGetDataResult<TEntity>> PerformGet<TEntity>(
            QueuedGetDataOperation<TEntity> operation)
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
                    .InvokeGeneric(this, new object[]
                        {
                            queuedOperation, saveChangesResult
                        },
                        queuedOperation.Operation.EntityType) as Task;
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
                    result = await PerformAdd(addEntityOperation);
                    var remoteEntity = addEntityOperation.Result.RemoteEntity;
                    if (remoteEntity != null)
                    {
                        var trackingSet = Tracking.TrackingSet<TEntity>();
                        trackingSet.TrackEntity(localEntity, addEntityOperation.Result.RemoteEntity, false);
                        trackingSet.GetEntityState(localEntity).Reset();
                        trackingSet.GetEntityState(localEntity).IsNew = false;
                    }

                    await DataContext.RefreshEntity(localEntity
#if TypeScript
                        , typeof(TEntity)
#endif
                    );
                    //GetTracking().TrackingSetByType(typeof(TEntity)).TrackEntity(addEntityOperation.Operation.Entity);
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
                    else if (isEntityNew != null)
                    {
                        result = await PerformUpdate(updateEntityOperation);
                        var operationEntity = updateEntityOperation
                            .Operation
                            .Entity;
                        if (result.Success)
                        {
                            Tracking.TrackingSet<TEntity>().ResetEntity(operationEntity);
                        }
                        await DataContext.RefreshEntity(operationEntity
#if TypeScript
                        , typeof(TEntity)
#endif
                        );
                        //GetTracking().TrackingSet<TEntity>().TrackEntity(operationEntity);
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
                    else if (entityNew != null)
                    {
                        result = await PerformDelete(deleteEntityOperation);
                        if (result.Success)
                        {
                            DataTracker.RemoveEntity(deleteEntityOperation.Operation.Entity);
                        }
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

        private static void ParseEntityResult(IDictionary<object, EntityValidationResult> resultsDictionary,
            object entity,
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
                    ParseEntityResult(resultsDictionary, collectionEntity,
                        validationResult.Value.EntityValidationResult);
                }
            }

            foreach (var relationshipResult in entityValidationResult.RelationshipValidationResults)
            {
                var relationshipEntity = entity.GetPropertyValueByName(relationshipResult.PropertyName);
                ParseEntityResult(resultsDictionary, relationshipEntity, relationshipResult.EntityValidationResult);
            }
        }
    }
}