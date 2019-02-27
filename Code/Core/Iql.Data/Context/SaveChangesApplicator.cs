using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Iql.Data.Crud;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.Crud.Operations.Results;
using Iql.Data.DataStores;
using Iql.Data.Events;
using Iql.Data.Extensions;
using Iql.Data.Tracking;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Entities.SpecialTypes;
using Iql.Entities.Validation.Validation;
using Iql.Extensions;

namespace Iql.Data.Context
{
    public class SaveChangesApplicator
    {
        public EntityConfigurationBuilder EntityConfigurationContext => DataContext.EntityConfigurationContext;

        public IDataContext DataContext { get; }

        public SaveChangesApplicator(IDataContext dataContext)
        {
            DataContext = dataContext;
        }

        private bool CheckPendingDependencies<TEntity, TOperation>(
            bool isOffline,
            TOperation operation, EntityCrudResult<TEntity, TOperation> result)
            where TEntity : class
            where TOperation : EntityCrudOperation<TEntity>
        {
            var dataTracker = isOffline ? DataContext.OfflineDataTracker : DataContext.TemporalDataTracker;
            if (dataTracker.GetPendingDependencyCount(operation.Entity, operation.EntityType) > 0)
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

        public virtual async Task PerformAsync<TEntity>(
            IQueuedOperation operation,
            SaveChangesResult saveChangesResult,
            bool isOfflineResync) where TEntity : class
        {
            var allowOnline = isOfflineResync || !DataContext.HasOfflineChanges();
            //var ctor: { new(entityType: { new(): any }, success: boolean, entity: any): any };
            var isOffline = !allowOnline;
            ICrudResult result;
            var dataStore = DataContext.DataStore;
            var offlineDataStore = DataContext.OfflineDataStore;
            var offlineDataTracker = DataContext.OfflineDataTracker;
            var dataTracker = DataContext.TemporalDataTracker;
            if (!allowOnline)
            {
                dataStore = offlineDataStore ?? dataStore;
                dataTracker = offlineDataTracker ?? dataTracker;
            }
            switch (operation.Operation.Type)
            {
                case OperationType.Add:
                    var addEntityOperation = (QueuedAddEntityOperation<TEntity>)operation;
                    var addEntityValidationResult = await DataContext.ValidateEntityAsync(addEntityOperation.Operation.Entity);
                    if (!isOfflineResync && addEntityValidationResult.HasValidationFailures())
                    {
                        addEntityOperation.Result.Success = false;
                        addEntityOperation.Result.EntityValidationResults = new Dictionary<object, IEntityValidationResult>();
                        addEntityOperation.Result.EntityValidationResults.Add(addEntityOperation.Operation.Entity, addEntityValidationResult);
                    }
                    else if (CheckPendingDependencies(isOfflineResync, addEntityOperation.Operation, addEntityOperation.Result) &&
                             (isOfflineResync || await CheckNotAlreadyExistsAsync(addEntityOperation)))
                    {
                        var localEntity = addEntityOperation.Operation.Entity;

                        var specialTypeMap = EntityConfigurationContext.GetSpecialTypeMap(typeof(TEntity).Name);

                        if (specialTypeMap != null && specialTypeMap.EntityConfiguration.Type != typeof(TEntity))
                        {
                            var method = typeof(SaveChangesApplicator).GetMethod(nameof(PerformMappedAddAsync),
                                BindingFlags.NonPublic | BindingFlags.Instance);
                            result = await (Task<AddEntityResult<TEntity>>)method.InvokeGeneric(
                                this,
                                new object[] { addEntityOperation, specialTypeMap, isOfflineResync },
                                typeof(TEntity), specialTypeMap.EntityConfiguration.Type);
                            if (result.RequestStatus == RequestStatus.Offline)
                            {
                                // Magic happens here...
                                isOffline = true;
                                if (offlineDataStore != null)
                                {
                                    result = await offlineDataStore.PerformAddAsync(addEntityOperation);
                                    result.Success = true;
                                }
                            }
                            addEntityOperation.Result.Success = result.Success;
                        }
                        else
                        {
                            result = await dataStore.PerformAddAsync(addEntityOperation);
                            if (result.RequestStatus == RequestStatus.Offline)
                            {
                                // Magic happens here...
                                isOffline = true;
                                if (offlineDataStore != null)
                                {
                                    result = await offlineDataStore.PerformAddAsync(addEntityOperation);
                                    result.Success = true;
                                }
                            }
                        }

                        var remoteEntity = addEntityOperation.Result.RemoteEntity;
#if TypeScript
                        if (remoteEntity != null)
                        {
                            remoteEntity =
                                (TEntity)EntityConfigurationContext.EnsureTypedEntityByType(remoteEntity, typeof(TEntity), false);
                        }
#endif
                        if (remoteEntity != null && result.Success)
                        {
                            var temporalEntity = (TEntity)DataContext.TemporalDataTracker.TrackingSet<TEntity>()
                                .FindMatchingEntityState(localEntity)?.Entity;
                            if (!isOfflineResync)
                            {
                                DataContext.OfflineDataTracker?.ApplyAdd(addEntityOperation, isOffline);
                                DataContext.TemporalDataTracker.TrackingSet<TEntity>()
                                    .Synchronise(remoteEntity, true, true, temporalEntity);
                            }
                            else
                            {
                                DataContext.TemporalDataTracker.TrackingSet<TEntity>()
                                    .Synchronise(remoteEntity, true, true, temporalEntity);
                                DataContext.OfflineDataTracker.TrackingSet<TEntity>()
                                    .Synchronise(remoteEntity, true, true, localEntity);
                                var temporalEntityState = DataContext.TemporalDataTracker.TrackingSet<TEntity>()
                                    .FindMatchingEntityState(localEntity);
                                localEntity = (TEntity)(temporalEntityState?.Entity ??
                                                         localEntity.Clone(EntityConfigurationContext, typeof(TEntity),
                                                             RelationshipCloneMode.DoNotClone));
                            }
                            //var trackingSet = DataContext.DataTracker.TrackingSet<TEntity>();
                            //trackingSet.TrackEntity(localEntity, remoteEntity, false);
                            //trackingSet.FindMatchingEntityState(localEntity).Reset();

                            if (!DataContext.RefreshDisabled)
                            {
                                await DataContext.RefreshEntity(localEntity
#if TypeScript
                        , typeof(TEntity)
#endif
                                );
                            }
                            if (!isOffline && DataContext.SupportsOffline)
                            {
                                await DataContext.OfflineDataStore.ApplyAddAsync(addEntityOperation);
                            }
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
                    else if (CheckPendingDependencies(isOfflineResync, updateEntityOperation.Operation, updateEntityOperation.Result))
                    {
                        var updateEntityValidationResult = await DataContext.ValidateEntityAsync(updateEntityOperation.Operation.Entity);
                        if (!isOfflineResync && updateEntityValidationResult.HasValidationFailures())
                        {
                            updateEntityOperation.Result.Success = false;
                            updateEntityOperation.Result.EntityValidationResults = new Dictionary<object, IEntityValidationResult>();
                            updateEntityOperation.Result.EntityValidationResults.Add(updateEntityOperation.Operation.Entity, updateEntityValidationResult);
                        }
                        else
                        {
                            var changedProperties = updateEntityOperation.Operation.GetChangedProperties();
                            var specialTypeMap = DataContext.EntityConfigurationContext.GetSpecialTypeMap(typeof(TEntity).Name);
                            if (specialTypeMap != null && specialTypeMap.EntityConfiguration.Type != typeof(TEntity))
                            {
                                var method = typeof(SaveChangesApplicator).GetMethod(nameof(PerformMappedUpdateAsync), BindingFlags.NonPublic | BindingFlags.Instance);
                                result = await (Task<UpdateEntityResult<TEntity>>)method.InvokeGeneric(
                                    this,
                                    new object[]
                                    {
                                        updateEntityOperation,
                                        specialTypeMap,
                                        isOfflineResync
                                    },
                                    typeof(TEntity), specialTypeMap.EntityConfiguration.Type);
                                if (result.RequestStatus == RequestStatus.Offline)
                                {
                                    isOffline = true;
                                    // Magic happens here...
                                    if (offlineDataStore != null)
                                    {
                                        result = await offlineDataStore.PerformUpdateAsync(updateEntityOperation);
                                    }
                                }
                            }
                            else
                            {
                                result = await dataStore.PerformUpdateAsync(updateEntityOperation);
                                if (result.RequestStatus == RequestStatus.Offline)
                                {
                                    isOffline = true;
                                    // Magic happens here...
                                    if (offlineDataStore != null)
                                    {
                                        result = await offlineDataStore.PerformUpdateAsync(updateEntityOperation);
                                    }
                                }
                            }
                            var operationEntity = updateEntityOperation
                                .Operation
                                .Entity;
                            if (result.Success)
                            {
                                DataContext.OfflineDataTracker?.ApplyUpdate(updateEntityOperation, isOffline);
                                if (isOffline)
                                {
                                    DataContext.TemporalDataTracker?.ApplyUpdate(updateEntityOperation, false);
                                }
                                else
                                {
                                    DataContext.ForMatchingDataContexts(
                                        (dataContext) =>
                                        {
                                            dataContext.TemporalDataTracker.TrackingSet<TEntity>()
                                                .Merge(
                                                    updateEntityOperation.Operation.Entity,
                                                    updateEntityOperation.Operation.Entity,
                                                    true,
                                                    true);
                                            if (dataContext.OfflineDataTracker != null)
                                            {
                                                dataContext.OfflineDataTracker.TrackingSet<TEntity>()
                                                    .Merge(
                                                        updateEntityOperation.Operation.Entity,
                                                        updateEntityOperation.Operation.Entity,
                                                        true,
                                                        true);
                                            }
                                        });

                                }
                                // TODO: Should be able to refresh an entity yet maintain existing changes
                                if (!DataContext.RefreshDisabled)
                                {
                                    await DataContext.RefreshEntity(operationEntity
#if TypeScript
                        , typeof(TEntity)
#endif
                                    );
                                }

                                if (!isOffline && DataContext.SupportsOffline)
                                {
                                    await DataContext.OfflineDataStore.ApplyUpdateAsync(
                                        updateEntityOperation,
                                        changedProperties);
                                }
                            }
                            else
                            {
                                await RemoveEntityIfEntityDoesNotExistInOnlineRemoteStore(operationEntity);
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
                        failure.AddFailure("", "This entity has not yet been saved so it cannot be deleted.");
                        deleteEntityOperation.Result.RootEntityValidationResult = failure;
                    }
                    else if (deleteEntityOperation.Key != null)
                    {
                        var specialTypeMap = DataContext.EntityConfigurationContext.GetSpecialTypeMap(typeof(TEntity).Name);
                        if (specialTypeMap != null && specialTypeMap.EntityConfiguration.Type != typeof(TEntity))
                        {
                            var method = typeof(SaveChangesApplicator).GetMethod(nameof(PerformMappedDeleteAsync), BindingFlags.NonPublic | BindingFlags.Instance);
                            result = await (Task<DeleteEntityResult<TEntity>>)method.InvokeGeneric(
                                this,
                                new object[] { deleteEntityOperation, specialTypeMap, isOfflineResync },
                                typeof(TEntity), specialTypeMap.EntityConfiguration.Type);
                            if (result.RequestStatus == RequestStatus.Offline)
                            {
                                isOffline = true;
                                // Magic happens here...
                                if (offlineDataStore != null)
                                {
                                    result = await offlineDataStore.PerformDeleteAsync(deleteEntityOperation);
                                    result.Success = true;
                                }
                            }
                            deleteEntityOperation.Result.Success = result.Success;
                        }
                        else
                        {
                            result = await dataStore.PerformDeleteAsync(deleteEntityOperation);
                            if (result.RequestStatus == RequestStatus.Offline)
                            {
                                isOffline = true;
                                // Magic happens here...
                                if (offlineDataStore != null)
                                {
                                    result = await offlineDataStore.PerformDeleteAsync(deleteEntityOperation);
                                    result.Success = true;
                                }
                            }
                        }
                        if (result.Success)
                        {
                            DataContext.OfflineDataTracker?.ApplyDelete(deleteEntityOperation, isOffline);
                            RemoveEntityByKeyAndType(
                                deleteEntityOperation.Operation.Key,
                                typeof(TEntity),
                                isOffline);

                            if (!isOffline && DataContext.SupportsOffline)
                            {
                                await DataContext.OfflineDataStore.ApplyDeleteAsync(deleteEntityOperation);
                            }
                        }
                        else
                        {
                            await RemoveEntityIfEntityDoesNotExistInOnlineRemoteStore(deleteEntityOperation.Operation.Entity);
                        }
                    }

                    break;
            }

            var entityCrudResult = operation.Result as IEntityCrudResult;
            if (entityCrudResult != null)
            {
                saveChangesResult.Results.Add(entityCrudResult);
            }
            await DataContext.SaveOfflineStateAsync();
        }

        private async Task<AddEntityResult<TEntity>> PerformMappedAddAsync<TEntity, TMap>(
            QueuedAddEntityOperation<TEntity> add,
            SpecialTypeDefinition definition,
            bool forceOnline)
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
                mappedProperty.CustomProperty.SetValue(mappedEntity,
                    property.GetValue(add.Operation.Entity));
            }

            var saveChangesResult = new SaveChangesResult(SaveChangeKind.Success);
            var mappedResult = mappedAdd.Result;
            await PerformAsync<TMap>(mappedAdd, saveChangesResult, forceOnline);
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
                        mappedProperty.CustomProperty.GetValue(mappedResult.RemoteEntity));
                }
                unmappedResult.RemoteEntity = remoteEntity;
            }

            return unmappedResult;
        }

        private async Task<DeleteEntityResult<TEntity>> PerformMappedDeleteAsync<TEntity, TMap>(
            QueuedDeleteEntityOperation<TEntity> deleteOperation,
            SpecialTypeDefinition definition,
            bool forceOnline)
            where TMap : class
        {
            var mappedEntity = (TMap)Activator.CreateInstance(typeof(TMap));
            var operationKey = deleteOperation.Operation.Key;
            var remappedCompositeKey = new CompositeKey(operationKey.Keys.Length);
            for (var i = 0; i < operationKey.Keys.Length; i++)
            {
                remappedCompositeKey.Keys[i] = new KeyValue(
                    definition.ResolvePropertyMap(operationKey.Keys[i].Name).CustomProperty.PropertyName,
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
            await PerformAsync<TMap>(mappedDelete, new SaveChangesResult(SaveChangeKind.Success), forceOnline);

            deleteOperation.Result.Success = mappedDeleteResult.Success;
            return deleteOperation.Result;
        }

        private async Task<UpdateEntityResult<TEntity>> PerformMappedUpdateAsync<TEntity, TMap>(
            QueuedUpdateEntityOperation<TEntity> update,
            SpecialTypeDefinition definition,
            bool forceOnline)
            where TMap : class
        {
            var mappedEntity = (TMap)Activator.CreateInstance(typeof(TMap));
            var updateEntityOperation = new UpdateEntityOperation<TMap>(mappedEntity, DataContext);
            var mappedUpdate = new QueuedUpdateEntityOperation<TMap>(
                updateEntityOperation,
                new UpdateEntityResult<TMap>(true, updateEntityOperation));

            var dummyEntityState = new EntityState<TMap>(
                update.Operation.EntityState.DataTracker,
                mappedEntity, typeof(TMap),
                DataContext.EntityConfigurationContext.EntityType<TMap>());
            updateEntityOperation.EntityState = dummyEntityState;
            for (var i = 0; i < update.Operation.EntityState.PropertyStates.Length; i++)
            {
                var sourcePropertyState = update.Operation.EntityState.PropertyStates[i];
                var mappedProperty = definition.ResolvePropertyMap(sourcePropertyState.Property.PropertyName);
                var targetPropertyState = dummyEntityState.PropertyStates.Single(p => p.Property == mappedProperty.CustomProperty);
                targetPropertyState.RemoteValue = sourcePropertyState.RemoteValue;
                targetPropertyState.LocalValue = sourcePropertyState.LocalValue;
                mappedEntity.SetPropertyValueByName(
                    mappedProperty.CustomProperty.PropertyName,
                    sourcePropertyState.LocalValue);
            }

            var mappedResult = mappedUpdate.Result;
            await PerformAsync<TMap>(mappedUpdate, new SaveChangesResult(SaveChangeKind.Success), forceOnline);
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

        private async Task<bool> EntityWithKeyAlreadyExists<TEntity>(TEntity entity) where TEntity : class
        {
            var entityWithKeyAlreadyExists =
                DataContext.TemporalDataTracker.EntityWithSameKeyIsBeingTracked(entity, typeof(TEntity));
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

        private async Task RemoveEntityIfEntityDoesNotExistInOnlineRemoteStore<TEntity>(TEntity entity) where TEntity : class
        {
            // TODO: We should return NotFound from our data store implementations
            // Todoot: 159
            var result = await DataContext.GetDbSetByEntityType(typeof(TEntity))
                .SetTracking(false)
                .WithKey(
                    DataContext.EntityConfigurationContext.GetEntityByType(typeof(TEntity)).GetCompositeKey(entity)
                )
                .ToListWithResponseAsync();
            if (result.IsOffline == false && (result.Root == null || result.Root.Count == 0))
            {
                var entityType = typeof(TEntity);
#if TypeScript
                entityType = entityType ?? entity.GetType();
#endif
                var key = DataContext.EntityConfigurationContext.GetEntityByType(typeof(TEntity)).GetCompositeKey(entity);
                RemoveEntityByKeyAndType(key, entityType, false);
            }
        }

        private void RemoveEntityByKeyAndType(CompositeKey entityKey, Type entityType, bool isOfflineDelete)
        {
            void Apply(DataTracker dataTracker)
            {
                if (dataTracker.EntityConfigurationBuilder == EntityConfigurationContext)
                {
                    var trackingSet = dataTracker.TrackingSetByType(entityType);
                    var state = trackingSet.GetEntityStateByKey(entityKey);
                    dataTracker.RemoveEntityByKeyAndType(entityKey, entityType);
                    var iEntity = (IEntity)state?.Entity;
                    iEntity?.ExistsChanged?.Emit(() => new ExistsChangeEvent(state, false));
                }
            }
            if (isOfflineDelete)
            {
                Apply(DataContext.TemporalDataTracker);
            }
            else
            {
                DataContext.ForMatchingDataContexts(dataContext =>
                {
                    Apply(dataContext.TemporalDataTracker);
                    if (dataContext.OfflineDataTracker != null)
                    {
                        Apply(dataContext.OfflineDataTracker);
                    }
                });
            }
        }
    }
}