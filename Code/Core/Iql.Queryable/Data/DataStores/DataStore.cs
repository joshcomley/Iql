using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Iql.Extensions;
using Iql.Queryable.Data.Context;
using Iql.Queryable.Data.Crud;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Crud.Operations.Queued;
using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.Lists;
using Iql.Queryable.Data.Paging;
using Iql.Queryable.Data.Queryable;
using Iql.Queryable.Data.Relationships;
using Iql.Queryable.Data.Tracking;
using Iql.Queryable.Data.Tracking.State;
using Iql.Queryable.Data.Validation;
using Iql.Queryable.Events;
using Iql.Queryable.Extensions;
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

        public IQueuedOperation[] GetChanges()
        {
            return Tracking.GetChanges().ToArray();
        }

        public IQueuedOperation[] GetUpdates()
        {
            return Tracking.GetChanges().Where(op => op.Type == QueuedOperationType.Update).ToArray();
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
            where T : class
        {
            var rootTrackingSet = Tracking.TrackingSet<T>();
            if (rootTrackingSet.IsTracked(entity))
            {
                var state = rootTrackingSet.GetEntityState(entity);
                state.MarkedForDeletion = false;
                return state;
            }
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
                    var queryable = queryableGetter() as Queryable.IQueryable<TEntity>;
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

            // Clone the queryable so any changes made in the application code
            // don't trickle down to our result
            response.Queryable = (Queryable.IQueryable<TEntity>)operation.Queryable.Copy();

            var dbList = TrackGetDataResult(response);

            var getDataResult =
                new GetDataResult<TEntity>(dbList, operation, response.IsSuccessful())
                {
                    TotalCount = response.TotalCount
                };

            ApplyPaging(dbList, response);

            return getDataResult;
        }

        public IDbList TrackGetDataResultByType(
            Type entityType,
            IFlattenedGetDataResult response)
        {
            return (IDbList)GetType()
                .GetMethod(nameof(TrackGetDataResult))
                .InvokeGeneric(this,
                    new object[] { response },
                    entityType
                );
        }

        public DbList<TEntity> TrackGetDataResult<TEntity>(FlattenedGetDataResult<TEntity> response) where TEntity : class
        {
            var dbList = new DbList<TEntity>();
            dbList.SourceQueryable = (DbQueryable<TEntity>)response.Queryable;
            // Flatten before we merge because the merge will update the result data set with
            // tracked data
            if (response.IsSuccessful())
            {
                var trackResults = dbList.SourceQueryable != null &&
                                   DataContext.TrackEntities;
                if (dbList.SourceQueryable != null && dbList.SourceQueryable.TrackEntities.HasValue)
                {
                    trackResults = dbList.SourceQueryable.TrackEntities.Value;
                }
                ForAllDataStores(tracker =>
                {
                    if (tracker == DataTracker)
                    {
                        if (!trackResults)
                        {
                            tracker = new DataTracker(this, false);
                        }
                        response.Root = tracker.TrackResults(response.Data, response.Root);
                    }
                    else
                    {
                        foreach (var entityType in response.Data)
                        {
                            foreach (var entity in entityType.Value)
                            {
                                var trackingSet = tracker.Tracking.TrackingSetByType(entityType.Key);
                                if (trackingSet.IsTracked(entity))
                                {
                                    var state = trackingSet.GetEntityState(entity);
                                    trackingSet.TrackEntity(state.Entity, entity, isNew: false, onlyMergeWithExisting: true);
                                    state.Reset();
                                }
                            }
                        }
                    }
                });
                dbList.AddRange(response.Root);
            }

            return dbList;
        }

        private static void ApplyPaging<TEntity>(DbList<TEntity> dbList, FlattenedGetDataResult<TEntity> response) where TEntity : class
        {
            dbList.SourceQueryable = (DbQueryable<TEntity>)response.Queryable;
            if (response.TotalCount.HasValue && dbList.Count != 0)
            {
                var skipOperations = response.Queryable.Operations.Where(o => o is SkipOperation);
                var skippedSoFar = skipOperations.Sum(o => (o as SkipOperation).Skip);
                int pageSize;
                var totalCount = response.TotalCount.Value;
                var page = 0;
                if (skippedSoFar == 0)
                {
                    pageSize = dbList.Count;
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
        }

        public virtual async Task<FlattenedGetDataResult<TEntity>> PerformGet<TEntity>(
            QueuedGetDataOperation<TEntity> operation)
            where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual async Task<SaveChangesResult> SaveChangesAsync(
            SaveChangesOperation operation)
        {
            // Sets could be added to whilst detecting changes
            // so get a copy now
            //var observable = this.Observable<SaveChangesResult>();
            var saveChangesResult = new SaveChangesResult(true);
            var queue = GetChanges();
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
                if (!queuedOperation.Result.Success)
                {
                    saveChangesResult.Success = false;
                }
            }

            return saveChangesResult;
        }

        protected int FindEntityIndex<TEntity>(
            Type entityType,
            TEntity clone,
            IList<TEntity> data) where TEntity : class
        {
            return Entity.FindIndexOfEntityInSetByEntity(
                DataContext,
                clone,
                data
            );
        }

        protected int FindEntityIndexByKey<TEntity>(
            Type entityType,
            CompositeKey key,
            IList<TEntity> data) where TEntity : class
        {
            return Entity.FindIndexOfEntityByKey(
                data,
                key
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
                    var entityConfig = DataContext.EntityConfigurationContext.EntityType<TEntity>();
                    var validationResult = entityConfig.ValidateEntity(addEntityOperation.Operation.Entity);
                    if (validationResult.HasValidationFailures())
                    {
                        addEntityOperation.Result.Success = false;
                        addEntityOperation.Result.EntityValidationResults = new Dictionary<object, IEntityValidationResult>();
                        addEntityOperation.Result.EntityValidationResults.Add(addEntityOperation.Operation.Entity, validationResult);
                    }
                    else if (CheckPendingDependencies(addEntityOperation.Operation, addEntityOperation.Result) &&
                            await CheckNotAlreadyExistsAsync(addEntityOperation))
                    {
                        var localEntity = addEntityOperation.Operation.Entity;
                        result = await PerformAdd(addEntityOperation);

                        var remoteEntity = addEntityOperation.Result.RemoteEntity;
                        if (remoteEntity != null && result.Success)
                        {
#if TypeScript
                            remoteEntity =
                                (TEntity) DataContext.EnsureTypedEntityByType(remoteEntity, typeof(TEntity), false);
#endif
                            var trackingSet = Tracking.TrackingSet<TEntity>();
                            trackingSet.TrackEntity(localEntity, remoteEntity, false);
                            trackingSet.GetEntityState(localEntity).Reset();
                            await DataContext.RefreshEntity(localEntity
#if TypeScript
                        , typeof(TEntity)
#endif
                            );
                        }
                        //GetTracking().TrackingSetByType(typeof(TEntity)).TrackEntity(addEntityOperation.Operation.Entity);
                    }
                    break;
                case OperationType.Update:
                    var updateEntityOperation = (QueuedUpdateEntityOperation<TEntity>)operation;
                    var isEntityNew = DataContext.IsEntityNew(updateEntityOperation.Operation.Entity, typeof(TEntity));
                    if (isEntityNew == true)
                    {
                        operation.Result.Success = false;
                        var failure = new EntityValidationResult<TEntity>(updateEntityOperation.Operation.Entity);
                        failure.AddFailure("", "This entity has not yet been saved so it cannot be updated.");
                        updateEntityOperation.Result.RootEntityValidationResult = failure;
                    }
                    else if (isEntityNew != null && CheckPendingDependencies(updateEntityOperation.Operation, updateEntityOperation.Result))
                    {
                        result = await PerformUpdate(updateEntityOperation);
                        var operationEntity = updateEntityOperation
                            .Operation
                            .Entity;
                        if (result.Success)
                        {
                            //var flattenObjectGraph = DataContext.EntityConfigurationContext.FlattenObjectGraph(
                            //    operationEntity, typeof(TEntity));
                            var rootDictionary = new Dictionary<Type, IList>();
                            rootDictionary.Ensure(
                                typeof(TEntity),
                                () => new List<TEntity> { updateEntityOperation.Operation.Entity });
                            ForAnEntityAcrossAllDataStores<TEntity>(updateEntityOperation.Operation.EntityState.CurrentKey, (tracker, state) =>
                            {
                                if (state.Entity != operationEntity)
                                {
                                    tracker.TrackResults<TEntity>(rootDictionary, null, true);
                                }
                                tracker.Tracking.TrackingSet<TEntity>().ResetEntity(operationEntity);
                            });
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
                    var entity = deleteEntityOperation.Operation.Entity;
                    bool? entityNew = null;
                    if (entity != null)
                    {
                        entityNew = DataContext.IsEntityNew(entity, typeof(TEntity));
                    }
                    if (entityNew == true)
                    {
                        operation.Result.Success = false;
                        var failure = new EntityValidationResult<TEntity>(
                            deleteEntityOperation.Operation.Entity);
                        failure.AddFailure("", "This entity has not yet been saved so it cannot be updated.");
                        deleteEntityOperation.Result.RootEntityValidationResult = failure;
                    }
                    else if (entityNew != null || deleteEntityOperation.Key != null)
                    {
                        result = await PerformDelete(deleteEntityOperation);
                        if (result.Success)
                        {
                            ForAnEntityAcrossAllDataStores<TEntity>(deleteEntityOperation.Operation.Key, (dataTracker, key) =>
                            {
                                var state = dataTracker.DataStore.Tracking.TrackingSet<TEntity>()
                                    .GetEntityStateByKey(key);
                                dataTracker.RemoveEntityByKey<TEntity>(deleteEntityOperation.Operation.Key);
                                var iEntity = state?.Entity as IEntity;
                                iEntity?.ExistsChanged?.Emit(() => new ExistsChangeEvent(state, false));
                            });
                        }
                    }

                    break;
            }

            var entityCrudResult = operation.Result as IEntityCrudResult;
            if (entityCrudResult != null)
            {
                saveChangesResult.Results.Add(entityCrudResult);
            }
        }

        private async Task<bool> CheckNotAlreadyExistsAsync<TEntity>(
            QueuedAddEntityOperation<TEntity> operation) where TEntity : class
        {
            var entityWithKeyAlreadyExists = await EntityWithKeyAlreadyExists(operation.Operation.Entity);
            if (entityWithKeyAlreadyExists)
            {
                operation.Result.EntityValidationResults = operation.Result.EntityValidationResults ??
                                                           new Dictionary<object, IEntityValidationResult>();
                operation.Result.EntityValidationResults.Add("",
                    new EntityValidationResult<TEntity>(operation.Operation.Entity)
                        .AddFailure("EntityWithKeyAlreadyExists", "An entity with this key already exists"));
                return false;
            }

            return true;
        }

        private async Task<bool> EntityWithKeyAlreadyExists<TEntity>(TEntity entity) where TEntity : class
        {
            var entityWithKeyAlreadyExists =
                Tracking.EntityWithSameKeyIsBeingTracked(entity, typeof(TEntity));
            if (!entityWithKeyAlreadyExists)
            {
                var compositeKey = DataContext.EntityConfigurationContext.EntityType<TEntity>()
                    .GetCompositeKey(entity);
                if (!compositeKey.HasDefaultValue())
                {
                    var remoteEntity = await DataContext.GetDbQueryable<TEntity>()
                        .SetTracking(false)
                        .GetWithCompositeKeyAsync(compositeKey);
                    if (remoteEntity != null && remoteEntity != entity)
                    {
                        entityWithKeyAlreadyExists = true;
                    }
                }
            }

            return entityWithKeyAlreadyExists;
        }

        private bool CheckPendingDependencies<TEntity, TOperation>(TOperation operation, EntityCrudResult<TEntity, TOperation> result)
            where TEntity : class
            where TOperation : EntityCrudOperation<TEntity>
        {
            if (Tracking.GetPendingDependencyCount(operation.Entity,
                    operation.EntityType) > 0)
            {
                result.Success = false;
                result.EntityValidationResults = new Dictionary<object, IEntityValidationResult>();
                result.EntityValidationResults.Add(operation.Entity,
                    new EntityValidationResult<TEntity>(operation.Entity)
                        .AddFailure("PendingDependency",
                            "Unable to save this entity because it has remaining, unsaved dependencies"));
                return false;
            }

            return true;
        }

        private void ForAllDataStores(Action<DataTracker> action)
        {
            var dataTrackersDealtWith = new Dictionary<DataTracker, DataTracker>();
            var allDataTrackers = DataTracker.AllDataTrackers();
            foreach (var dataTracker in allDataTrackers)
            {
                if (!dataTrackersDealtWith.ContainsKey(dataTracker))
                {
                    dataTrackersDealtWith.Add(dataTracker, dataTracker);
                }

                if (dataTracker.DataContext.GetType() == DataTracker.DataContext.GetType() &&
                    dataTracker.DataContext.SynchronicityKey == DataTracker.DataContext.SynchronicityKey)
                {
                    action(dataTracker);
                }
            }
        }

        private void ForAnEntityAcrossAllDataStores<TEntity>(CompositeKey key, Action<DataTracker, CompositeKey> action) where TEntity : class
        {
            // This needs to also accept a CompositeKey
            //var sourceEntity = entity as IEntity;
            //var alreadyEmitted = new Dictionary<string, string>();
            //var dataTrackersDealtWith = new Dictionary<DataTracker, DataTracker>();
            ForAllDataStores(dataTracker =>
            {
                //var keyString = key.AsKeyString();
                //if (!alreadyEmitted.ContainsKey(keyString))
                //{
                //    alreadyEmitted.Add(keyString, keyString);
                //}
                action(dataTracker, key);
            });
            //if (sourceEntity != null && !dataTrackersDealtWith.ContainsKey(DataTracker) &&
            //    !alreadyEmitted.ContainsKey(sourceEntity))
            //{
            //    var entityState = Tracking.TrackingSet<TEntity>().GetEntityState(entity);
            //    action(DataTracker, (EntityState<TEntity>)entityState);
            //}
        }
    }
}