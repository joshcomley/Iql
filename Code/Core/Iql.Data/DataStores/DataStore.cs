using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Crud;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.Crud.Operations.Results;
using Iql.Data.DataStores.NestedSets;
using Iql.Data.Events;
using Iql.Data.Extensions;
using Iql.Data.Lists;
using Iql.Data.Operations;
using Iql.Data.Paging;
using Iql.Data.Queryable;
using Iql.Data.Relationships;
using Iql.Data.Tracking;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Entities.SpecialTypes;
using Iql.Entities.Validation.Validation;
using Iql.Extensions;
using Iql.Queryable.Extensions;
using Iql.Queryable.Operations;

namespace Iql.Data.DataStores
{
    public class DataStore : IDataStore
    {
        public DataStore(IOfflineDataStore offlineDataStore = null)
        {
            OfflineDataStore = offlineDataStore;
        }

        private DataTracker _dataTracker;
        private IDataContext _dataContext;
        public static MethodInfo MarkAsDeletedByKeyTypedMethod { get; set; }

        public IOfflineDataStore OfflineDataStore { get; set; }

        public virtual INestedSetsProviderBase NestedSetsProviderForType(Type type)
        {
            return null;
        }

        public virtual INestedSetsProvider<T> NestedSetsProviderFor<T>()
        {
            return (INestedSetsProvider<T>)NestedSetsProviderForType(typeof(T));
        }

        public DataTracker DataTracker
        {
            get { return _dataTracker = _dataTracker ?? new DataTracker(this, true); }
        }

        public virtual TrackingSetCollection Tracking => DataTracker.Tracking;
        public virtual IRelationshipObserver RelationshipObserver => DataTracker.RelationshipObserver;

        public virtual IDataContext DataContext
        {
            get => _dataContext;
            set
            {
                _dataContext = value;
                if (OfflineDataStore != null)
                {
                    OfflineDataStore.DataContext = value;
                }
            }
        }

        public IQueuedOperation[] GetChanges(object[] entities = null, IProperty[] properties = null)
        {
            return Tracking.GetChanges(entities, properties).ToArray();
        }

        public IQueuedOperation[] GetUpdates(object[] entities = null, IProperty[] properties = null)
        {
            return Tracking.GetChanges(entities, properties).Where(op => op.Type == QueuedOperationType.Update).ToArray();
        }

        static DataStore()
        {
            MarkAsDeletedByKeyTypedMethod = typeof(DataStore)
                .GetMethod(nameof(MarkAsDeletedByKey),
                    BindingFlags.Instance | BindingFlags.Public);
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
            if (rootTrackingSet.IsMatchingEntityTracked(entity))
            {
                var state = rootTrackingSet.FindMatchingEntityState(entity);
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
            var entityState = (EntityState<T>)trackingSet.FindMatchingEntityState(entity);
            return entityState;
        }

        public virtual async Task<AddEntityResult<TEntity>> PerformAddAsync<TEntity>(
            QueuedAddEntityOperation<TEntity> operation) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual async Task<UpdateEntityResult<TEntity>> PerformUpdateAsync<TEntity>(
            QueuedUpdateEntityOperation<TEntity> operation) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual async Task<DeleteEntityResult<TEntity>> PerformDeleteAsync<TEntity>(
            QueuedDeleteEntityOperation<TEntity> operation) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual async Task<GetDataResult<TEntity>> GetAsync<TEntity>(GetDataOperation<TEntity> operation)
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
                    var queryable = queryableGetter() as global::Iql.Queryable.IQueryable<TEntity>;
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
            await PerformGetAsync(new QueuedGetDataOperation<TEntity>(
                operation,
                response));

            if (response.RequestStatus == RequestStatus.Offline)
            {
                // Magic happens here...
                if (OfflineDataStore != null)
                {
                    return await OfflineDataStore.GetAsync(operation);
                }
            }
            else
            {
                OfflineDataStore?.SynchroniseData(response.Data);
                // Update "offline" repository with these results
            }

            // Clone the queryable so any changes made in the application code
            // don't trickle down to our result
            response.Queryable = (global::Iql.Queryable.IQueryable<TEntity>)operation.Queryable.Copy();

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
                                if (trackingSet.IsMatchingEntityTracked(entity))
                                {
                                    var state = trackingSet.FindMatchingEntityState(entity);
                                    trackingSet.TrackEntity(state.Entity, entity, isNew: false, onlyMergeWithExisting: true);
                                    state.Reset();
                                }
                            }
                        }
                    }
                });
                if (response.Root != null)
                {
                    dbList.AddRange(response.Root);
                }
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

        public virtual async Task<FlattenedGetDataResult<TEntity>> PerformGetAsync<TEntity>(
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
            var queue = GetChanges(operation.Entities, operation.Properties);
            foreach (var queuedOperation in queue)
            {
                var task = GetType()
                    .GetMethod(nameof(PerformAsync))
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

        public virtual async Task PerformAsync<TEntity>(
            IQueuedOperation operation,
            SaveChangesResult saveChangesResult) where TEntity : class
        {
            //var ctor: { new(entityType: { new(): any }, success: boolean, entity: any): any };
            ICrudResult result;
            switch (operation.Operation.Type)
            {
                case OperationType.Add:
                    var addEntityOperation = (QueuedAddEntityOperation<TEntity>)operation;
                    var addEntityValidationResult = await DataContext.ValidateEntityAsync(addEntityOperation.Operation.Entity);
                    if (addEntityValidationResult.HasValidationFailures())
                    {
                        addEntityOperation.Result.Success = false;
                        addEntityOperation.Result.EntityValidationResults = new Dictionary<object, IEntityValidationResult>();
                        addEntityOperation.Result.EntityValidationResults.Add(addEntityOperation.Operation.Entity, addEntityValidationResult);
                    }
                    else if (CheckPendingDependencies(addEntityOperation.Operation, addEntityOperation.Result) &&
                            await CheckNotAlreadyExistsAsync(addEntityOperation))
                    {
                        var localEntity = addEntityOperation.Operation.Entity;

                        var specialTypeMap = DataContext.EntityConfigurationContext.GetSpecialTypeMap(typeof(TEntity).Name);
                        if (specialTypeMap != null && specialTypeMap.EntityConfiguration.Type != typeof(TEntity))
                        {
                            var method = typeof(DataStore).GetMethod(nameof(PerformMappedAddAsync), BindingFlags.NonPublic | BindingFlags.Instance);
                            result = await (Task<AddEntityResult<TEntity>>)method.InvokeGeneric(
                                this,
                                new object[] { addEntityOperation, specialTypeMap },
                                typeof(TEntity), specialTypeMap.EntityConfiguration.Type);
                            if (result.RequestStatus == RequestStatus.Offline)
                            {
                                // Magic happens here...
                            }
                            addEntityOperation.Result.Success = result.Success;
                        }
                        else
                        {
                            result = await PerformAddAsync(addEntityOperation);
                            if (result.RequestStatus == RequestStatus.Offline)
                            {
                                // Magic happens here...
                            }
                        }

                        var remoteEntity = addEntityOperation.Result.RemoteEntity;
                        if (remoteEntity != null && result.Success)
                        {
#if TypeScript
                            remoteEntity =
                                (TEntity)DataContext.EnsureTypedEntityByType(remoteEntity, typeof(TEntity), false);
#endif
                            var trackingSet = Tracking.TrackingSet<TEntity>();
                            trackingSet.TrackEntity(localEntity, remoteEntity, false);
                            trackingSet.FindMatchingEntityState(localEntity).Reset();
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
                    var isEntityNew = DataContext.IsEntityNew(updateEntityOperation.Operation.Entity
#if TypeScript
                                            , typeof(TEntity)
#endif
                                        );
                    if (isEntityNew == true)
                    {
                        operation.Result.Success = false;
                        var failure = new EntityValidationResult<TEntity>(updateEntityOperation.Operation.Entity);
                        failure.AddFailure("", "This entity has not yet been saved so it cannot be updated.");
                        updateEntityOperation.Result.RootEntityValidationResult = failure;
                    }
                    else if (isEntityNew != null && CheckPendingDependencies(updateEntityOperation.Operation, updateEntityOperation.Result))
                    {
                        var updateEntityValidationResult = await DataContext.ValidateEntityAsync(updateEntityOperation.Operation.Entity);
                        if (updateEntityValidationResult.HasValidationFailures())
                        {
                            updateEntityOperation.Result.Success = false;
                            updateEntityOperation.Result.EntityValidationResults = new Dictionary<object, IEntityValidationResult>();
                            updateEntityOperation.Result.EntityValidationResults.Add(updateEntityOperation.Operation.Entity, updateEntityValidationResult);
                        }
                        else
                        {
                            var specialTypeMap = DataContext.EntityConfigurationContext.GetSpecialTypeMap(typeof(TEntity).Name);
                            if (specialTypeMap != null && specialTypeMap.EntityConfiguration.Type != typeof(TEntity))
                            {
                                var method = typeof(DataStore).GetMethod(nameof(PerformMappedUpdateAsync), BindingFlags.NonPublic | BindingFlags.Instance);
                                result = await (Task<UpdateEntityResult<TEntity>>)method.InvokeGeneric(
                                    this,
                                    new object[] { updateEntityOperation, specialTypeMap },
                                    typeof(TEntity), specialTypeMap.EntityConfiguration.Type);
                                if (result.RequestStatus == RequestStatus.Offline)
                                {
                                    // Magic happens here...
                                }
                            }
                            else
                            {
                                result = await PerformUpdateAsync(updateEntityOperation);
                                if (result.RequestStatus == RequestStatus.Offline)
                                {
                                    // Magic happens here...
                                }
                            }
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
                                // TODO: Should be able to refresh an entity yet maintain existing changes
                                await DataContext.RefreshEntity(operationEntity
#if TypeScript
                        , typeof(TEntity)
#endif
                                );
                            }
                            else
                            {
                                await MarkAsDeletedIfNecessary(operationEntity);
                            }
                        }
                        //GetTracking().TrackingSet<TEntity>().TrackEntity(operationEntity);
                    }

                    break;
                case OperationType.Delete:
                    var deleteEntityOperation = (QueuedDeleteEntityOperation<TEntity>)operation;
                    var entity = deleteEntityOperation.Operation.Entity;
                    bool? entityNew = null;
                    if (entity != null)
                    {
                        entityNew = DataContext.IsEntityNew(entity
#if TypeScript
                            , typeof(TEntity)
#endif
                            );
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
                        var specialTypeMap = DataContext.EntityConfigurationContext.GetSpecialTypeMap(typeof(TEntity).Name);
                        if (specialTypeMap != null && specialTypeMap.EntityConfiguration.Type != typeof(TEntity))
                        {
                            var method = typeof(DataStore).GetMethod(nameof(PerformMappedDeleteAsync), BindingFlags.NonPublic | BindingFlags.Instance);
                            result = await (Task<DeleteEntityResult<TEntity>>)method.InvokeGeneric(
                                this,
                                new object[] { deleteEntityOperation, specialTypeMap },
                                typeof(TEntity), specialTypeMap.EntityConfiguration.Type);
                            if (result.RequestStatus == RequestStatus.Offline)
                            {
                                // Magic happens here...
                            }
                            deleteEntityOperation.Result.Success = result.Success;
                        }
                        else
                        {
                            result = await PerformDeleteAsync(deleteEntityOperation);
                            if (result.RequestStatus == RequestStatus.Offline)
                            {
                                // Magic happens here...
                            }
                        }
                        if (result.Success)
                        {
                            MarkAsDeletedByKey<TEntity>(deleteEntityOperation.Operation.Key);
                        }
                        else
                        {
                            await MarkAsDeletedIfNecessary(deleteEntityOperation.Operation.Entity);
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

        public async Task MarkAsDeletedIfNecessary<TEntity>(TEntity entity) where TEntity : class
        {
            // TODO: We should return NotFound from our data store implementations
            // Todoot: 159
            var result = await DataContext.GetDbSetByEntityType(typeof(TEntity)).SetTracking(false).GetWithKeyAsync(DataContext.EntityConfigurationContext.GetEntityByType(typeof(TEntity)).GetCompositeKey(entity));
            if (result == null)
            {
                MarkAsDeleted(entity);
            }
        }

        public void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            var entityType = typeof(TEntity);
#if TypeScript
            entityType = entityType ?? entity.GetType();
#endif
            var key = DataContext.EntityConfigurationContext.GetEntityByType(typeof(TEntity)).GetCompositeKey(entity);
            MarkAsDeletedByKeyAndType(key, entityType);
        }

        public void MarkAsDeletedByKeyAndType(CompositeKey entityKey, Type entityType)
        {
            MarkAsDeletedByKeyTypedMethod.InvokeGeneric(this, new object[] { entityKey }, entityType);
        }

        public void MarkAsDeletedByKey<TEntity>(CompositeKey entityKey)
            where TEntity : class
        {
            ForAnEntityAcrossAllDataStores<TEntity>(entityKey, (dataTracker, key) =>
            {
                var trackingSet = dataTracker.DataStore.Tracking.TrackingSet<TEntity>();
                var state = trackingSet.GetEntityStateByKey(key);
                dataTracker.RemoveEntityByKey<TEntity>(entityKey);
                var iEntity = (IEntity)state?.Entity;
                iEntity?.ExistsChanged?.Emit(() => new ExistsChangeEvent(state, false));
            });
        }

        private async Task<AddEntityResult<TEntity>> PerformMappedAddAsync<TEntity, TMap>(
            QueuedAddEntityOperation<TEntity> add,
            SpecialTypeDefinition definition)
            where TMap : class
        where TEntity : class
        {
            var mappedEntity = (TMap)Activator.CreateInstance(typeof(TMap));
            var addEntityOperation = new AddEntityOperation<TMap>(mappedEntity, DataContext);
            var mappedAdd = new QueuedAddEntityOperation<TMap>(
                addEntityOperation,
                new AddEntityResult<TMap>(true, addEntityOperation));

            var properties = DataContext.EntityConfigurationContext.EntityType<TEntity>().Properties;
            for (var i = 0; i < properties.Count; i++)
            {
                var property = properties[i];
                var mappedProperty = definition.ResolvePropertyMap(property.PropertyName);
                mappedProperty.SetValue(mappedEntity,
                    property.GetValue(add.Operation.Entity));
            }

            var saveChangesResult = new SaveChangesResult(true);
            var mappedResult = mappedAdd.Result;
            await PerformAsync<TMap>(mappedAdd, saveChangesResult);
            var unmappedResult = add.Result;
            unmappedResult.Success = mappedResult.Success;
            // TODO: Map validation results correctly
            unmappedResult.EntityValidationResults = new Dictionary<object, IEntityValidationResult>();
            foreach (var validationResult in mappedResult.EntityValidationResults)
            {
                if (validationResult.Key == mappedEntity)
                {
                    unmappedResult.EntityValidationResults.Add(add.Operation.Entity, validationResult.Value);
                }
            }

            if (mappedResult.RemoteEntity != null)
            {
                var remoteEntity = (TEntity)Activator.CreateInstance(typeof(TEntity));
                for (var i = 0; i < properties.Count; i++)
                {
                    var property = properties[i];
                    var mappedProperty = definition.ResolvePropertyMap(property.PropertyName);
                    property.SetValue(remoteEntity,
                        mappedProperty.GetValue(mappedResult.RemoteEntity));
                }
                unmappedResult.RemoteEntity = remoteEntity;
            }

            return unmappedResult;
        }

        private async Task<DeleteEntityResult<TEntity>> PerformMappedDeleteAsync<TEntity, TMap>(
            QueuedDeleteEntityOperation<TEntity> deleteOperation,
            SpecialTypeDefinition definition)
            where TMap : class
        {
            var mappedEntity = (TMap)Activator.CreateInstance(typeof(TMap));
            var operationKey = deleteOperation.Operation.Key;
            var remappedCompositeKey = new CompositeKey(operationKey.Keys.Length);
            for (var i = 0; i < operationKey.Keys.Length; i++)
            {
                remappedCompositeKey.Keys[i] = new KeyValue(
                    definition.ResolvePropertyMap(operationKey.Keys[i].Name).PropertyName,
                    operationKey.Keys[i].Value,
                    operationKey.Keys[i].ValueType);
                mappedEntity.SetPropertyValueByName(remappedCompositeKey.Keys[i].Name,
                    remappedCompositeKey.Keys[i].Value);
            }
            var deleteEntityOperation = new DeleteEntityOperation<TMap>(remappedCompositeKey, mappedEntity, DataContext);
            var mappedDelete = new QueuedDeleteEntityOperation<TMap>(
                deleteEntityOperation,
                new DeleteEntityResult<TMap>(true, deleteEntityOperation));
            var mappedDeleteResult = mappedDelete.Result;
            await PerformAsync<TMap>(mappedDelete, new SaveChangesResult(true));

            deleteOperation.Result.Success = mappedDeleteResult.Success;
            return deleteOperation.Result;
        }

        private async Task<UpdateEntityResult<TEntity>> PerformMappedUpdateAsync<TEntity, TMap>(
            QueuedUpdateEntityOperation<TEntity> update,
            SpecialTypeDefinition definition)
            where TMap : class
        {
            var mappedEntity = (TMap)Activator.CreateInstance(typeof(TMap));
            var updateEntityOperation = new UpdateEntityOperation<TMap>(mappedEntity, DataContext);
            var mappedUpdate = new QueuedUpdateEntityOperation<TMap>(
                updateEntityOperation,
                new UpdateEntityResult<TMap>(true, updateEntityOperation));

            var dummyEntityState = new EntityState<TMap>(mappedEntity, typeof(TMap), DataContext,
                DataContext.EntityConfigurationContext.EntityType<TMap>());
            updateEntityOperation.EntityState = dummyEntityState;
            for (var i = 0; i < update.Operation.EntityState.PropertyStates.Length; i++)
            {
                var sourcePropertyState = update.Operation.EntityState.PropertyStates[i];
                var mappedProperty = definition.ResolvePropertyMap(sourcePropertyState.Property.PropertyName);
                var targetPropertyState = dummyEntityState.PropertyStates.Single(p => p.Property == mappedProperty);
                targetPropertyState.RemoteValue = sourcePropertyState.RemoteValue;
                targetPropertyState.LocalValue = sourcePropertyState.LocalValue;
                mappedEntity.SetPropertyValueByName(
                    mappedProperty.PropertyName,
                    sourcePropertyState.LocalValue);
            }

            var mappedResult = mappedUpdate.Result;
            await PerformAsync<TMap>(mappedUpdate, new SaveChangesResult(true));
            var unmappedResult = new UpdateEntityResult<TEntity>(mappedResult.Success,
                update.Operation);
            unmappedResult.EntityValidationResults = new Dictionary<object, IEntityValidationResult>();
            foreach (var validationResult in mappedResult.EntityValidationResults)
            {
                if (validationResult.Key == mappedEntity)
                {
                    unmappedResult.EntityValidationResults.Add(update.Operation.Entity, validationResult.Value);
                }
            }

            return unmappedResult;
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