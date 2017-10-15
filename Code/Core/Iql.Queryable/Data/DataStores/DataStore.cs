using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Iql.Queryable.Data.Crud;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Crud.Operations.Queued;
using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Data.Tracking;
using Iql.Queryable.Data.Validation;
using Iql.Queryable.Expressions.QueryExpressions;
using Iql.Queryable.Extensions;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.DataStores
{
    public class DataStore : IDataStore
    {
        public TrackingSetCollection Tracking2;

        public IDataContext DataContext { get; set; }
        public List<IQueuedOperation> Queue { get; set; } = new List<IQueuedOperation>();

        public virtual AddEntityResult<TEntity> Add<TEntity>(AddEntityOperation<TEntity> operation)
            where TEntity : class
        {
            GetTracking().GetSet<TEntity>().Track(operation.Entity);
            var result = new AddEntityResult<TEntity>(true, operation);
            Queue.Add(new QueuedAddEntityOperation<TEntity>(
                operation,
                result));
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

        public virtual TrackingSetCollection GetTracking()
        {
            if (Tracking2 == null)
            {
                Tracking2 = new TrackingSetCollection(DataContext);
            }
            return Tracking2;
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
            response.Data = (DbList<TEntity>)EnsureTypedList(typeof(TEntity), response.Data);
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
            trackingSet.Merge(response.Data);
            return result;
        }

        private IList EnsureTypedList(Type type, IEnumerable responseData)
        {
            var list = (IList)Activator.CreateInstance(typeof(DbList<>).MakeGenericType(type));
            if (responseData != null)
            {
                foreach (var entity in responseData)
                {
                    var typedEntity = EnsureTypedEntity(type, entity);
                    list.Add(typedEntity);
                }
            }
            return list;
        }

        private object EnsureTypedEntity(Type type, object entity)
        {
            if (entity != null)
            {
                var entityConfiguration = DataContext.EntityConfigurationContext.GetEntityByType(type);
                var typedEntity = Activator.CreateInstance(type);
                foreach (var property in entityConfiguration.Properties)
                {
                    //var instanceValue = typedEntity.GetPropertyValue(property.Name);
                    var remoteValue = entity.GetPropertyValue(property.Name);
                    if (remoteValue != null)
                    {
                        typedEntity.SetPropertyValue(property.Name, remoteValue);
                    }
                }
                foreach (var relationship in entityConfiguration.Relationships)
                {
                    var isSource = relationship.Source.Configuration == entityConfiguration;
                    var propertyName = isSource
                        ? relationship.Source.Property.PropertyName
                        : relationship.Target.Property.PropertyName;
                    if (isSource)
                    {
                        switch (relationship.Type)
                        {
                            case RelationshipType.OneToMany:
                            case RelationshipType.OneToOne:
                                typedEntity.SetPropertyValue(propertyName,
                                    EnsureTypedEntity(relationship.Target.Type,
                                        entity.GetPropertyValue(propertyName)));
                                break;
                            case RelationshipType.ManyToMany:
                                typedEntity.SetPropertyValue(propertyName,
                                    EnsureTypedList(relationship.Target.Type, (IEnumerable)entity.GetPropertyValue(propertyName)));
                                break;
                        }
                    }
                    else
                    {
                        switch (relationship.Type)
                        {
                            case RelationshipType.OneToOne:
                                typedEntity.SetPropertyValue(propertyName,
                                    EnsureTypedEntity(relationship.Source.Type,
                                        entity.GetPropertyValue(propertyName)));
                                break;
                            case RelationshipType.OneToMany:
                            case RelationshipType.ManyToMany:
                                typedEntity.SetPropertyValue(propertyName,
                                    EnsureTypedList(relationship.Source.Type, (IEnumerable)entity.GetPropertyValue(propertyName)));
                                break;
                        }
                    }
                }
                entity = typedEntity;
            }
            return entity;
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
            var trackingSets = GetTracking().Sets.ToList();
            trackingSets.ForEach(trackingSet =>
            {
                trackingSet.GetChanges().ForEach(update =>
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
                            var entitiesInObjectGraph = FlattenObjectGraph(entityOperation.Entity);
                            if (entitiesInObjectGraph.Contains(update.Entity))
                            {
                                alreadyBeingUpdatedByAnotherOperation = true;
                                break;
                            }
                        }
                    }
                    if (!alreadyBeingUpdatedByAnotherOperation)
                    {
                        var updateOperation =
                            Activator.CreateInstance(
                                typeof(QueuedUpdateEntityOperation<>).MakeGenericType(update.EntityType), update, null);
                        Queue.Add((IQueuedOperation)updateOperation);
                    }
                    //this.Queue.Add(new QueuedUpdateEntityOperation<object>(update, new UpdateEntityResult<object>(true, update)));
                    //Apply(update);
                });
                trackingSet.Reset();
            });
            //var observable = this.Observable<SaveChangesResult>();
            var saveChangesResult = new SaveChangesResult(false);
            var count = Queue.Count;
            Action decrement = () =>
            {
                count--;
                if (count == 0)
                {
                    //observable.SetData(saveChangesResult);
                }
            };
            var queue = Queue;
            Queue = new List<IQueuedOperation>();
            foreach (var queuedOperation in queue)
            {
                var task = GetType()
                    .GetMethod(nameof(Perform))
                    .MakeGenericMethod(queuedOperation.Operation.EntityType)
                    .Invoke(this, new object[]
                    {
                        queuedOperation, decrement, saveChangesResult
#if TypeScript // The type info
                        ,queuedOperation.Operation.EntityType
#endif
                    }) as Task;
                await task;
            }
            return saveChangesResult;
        }

        public List<object> FlattenObjectGraph(object objectGraphRoot, List<object> objects = null)
        {
            objects = objects ?? new List<object>();
            if (objects.Contains(objectGraphRoot))
            {
                // Prevent infinite recursion
                return objects;
            }
            objects.Add(objectGraphRoot);
            var graphEntityConfiguration =
                DataContext.EntityConfigurationContext.GetEntityByType(objectGraphRoot.GetType());
            foreach (var relationship in graphEntityConfiguration.Relationships)
            {
                var isSource = relationship.Source.Configuration == graphEntityConfiguration;
                var propertyName = isSource
                    ? relationship.Source.Property.PropertyName
                    : relationship.Target.Property.PropertyName;
                var relationshipValue = objectGraphRoot.GetPropertyValue(propertyName);

                if (relationshipValue != null)
                {
                    var isArray = relationshipValue is IEnumerable && !(relationshipValue is string);
                    if (isArray)
                    {
                        var list = (IList)relationshipValue;
                        foreach (var item in list)
                        {
                            FlattenObjectGraph(item, objects);
                        }
                    }
                    else
                    {
                        FlattenObjectGraph(relationshipValue, objects);
                    }
                }
            }
            return objects;
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
            Action decrement,
            SaveChangesResult saveChangesResult) where TEntity : class
        {
            //var ctor: { new(entityType: { new(): any }, success: boolean, entity: any): any };
            ICrudResult result = null;            
            switch (operation.Operation.Type)
            {
                case OperationType.Add:
                    var addEntityOperation = (QueuedAddEntityOperation<TEntity>)operation;
                    var localEntity = addEntityOperation.Operation.Entity;
                    EnsurePersistenceKeyInNewEntities(localEntity);
                    result = await PerformAdd(addEntityOperation);
                    var remoteEntity = addEntityOperation.Result.RemoteEntity;
                    //ObjectMerger.Merge(DataContext, localEntity, remoteEntity);
                    foreach (var keyProperty in DataContext.EntityConfigurationContext.GetEntity<TEntity>().Key.Properties)
                    {
                        localEntity.SetPropertyValue(keyProperty.PropertyName, remoteEntity.GetPropertyValue(keyProperty.PropertyName));
                    }
                    await RefreshEntity(localEntity);
                    GetTracking().GetSet<TEntity>().Track(addEntityOperation.Operation.Entity);
                    break;
                case OperationType.Update:
                    var updateEntityOperation = (QueuedUpdateEntityOperation<TEntity>)operation;
                    EnsurePersistenceKeyInNewEntities(updateEntityOperation.Operation.Entity);
                    result = await PerformUpdate(updateEntityOperation);
                    var operationEntity = updateEntityOperation.Operation
                        .Entity;
                    await RefreshEntity(operationEntity);
                    GetTracking().GetSet<TEntity>().Track(operationEntity);
                    //.WithKey(identityWhereOperation.Key);
                    //Merge(updateEntityOperation.Operation.Entity, refreshResult);
                    break;
                case OperationType.Delete:
                    var deleteEntityOperation = (QueuedDeleteEntityOperation<TEntity>)operation;
                    result = await PerformDelete(deleteEntityOperation);
                    GetTracking().GetSet<TEntity>().Untrack(deleteEntityOperation.Operation.Entity);
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
            decrement();
        }

        private static void ParseEntityResult(IDictionary<object, EntityValidationResult> resultsDictionary, object entity,
            EntityValidationResult entityValidationResult)
        {
            if (resultsDictionary.ContainsKey(entity))
            {
                return;
            }
            resultsDictionary.Add(entity, entityValidationResult);
            entityValidationResult.LocalEntity = entity;
            foreach (var collectionValidationResultSet in entityValidationResult
                .RelationshipCollectionValidationResults)
            {
                foreach(var validationResult in collectionValidationResultSet.RelationshipValidationResults)
                {
                    var index = validationResult.Key;
                    var collectionEntity =
                        ((IList) entity.GetPropertyValue(validationResult.Value.PropertyName))[index];
                    ParseEntityResult(resultsDictionary, collectionEntity, validationResult.Value.EntityValidationResult);
                }
            }
            foreach (var relationshipResult in entityValidationResult.RelationshipValidationResults)
            {
                var relationshipEntity = entity.GetPropertyValue(relationshipResult.PropertyName);
                ParseEntityResult(resultsDictionary, relationshipEntity, relationshipResult.EntityValidationResult);
            }
        }

        private void EnsurePersistenceKeyInNewEntities(object localEntity, List<object> entitiesAlreadyChecked = null)
        {
            entitiesAlreadyChecked = entitiesAlreadyChecked ?? new List<object>();
            // Avoid infinite recursion
            if (entitiesAlreadyChecked.Contains(localEntity))
            {
                return;
            }
            var type = localEntity.GetType();
            if (DataContext.IsEntityNew(localEntity))
            {
                var persistenceKey = DataContext.EntityConfigurationContext.GetEntityByType(type).Properties
                    .FirstOrDefault(p => p.Name == "PersistenceKey");
                if (persistenceKey != null)
                {
                    localEntity.SetPropertyValue("PersistenceKey", Guid.NewGuid());
                }
                var tracking = GetTracking();
                var trackedEntity = tracking.FindEntity(localEntity);
                if (trackedEntity == null)
                {
                    tracking.Track(localEntity);
                }
            }
            var entityConfiguration = DataContext.EntityConfigurationContext.GetEntityByType(type);
            foreach (var relationship in entityConfiguration.Relationships)
            {
                var isSource = relationship.Source.Configuration == entityConfiguration;
                var propertyName = isSource
                    ? relationship.Source.Property.PropertyName
                    : relationship.Target.Property.PropertyName;
                var relationshipValue = localEntity.GetPropertyValue(propertyName);

                if (relationshipValue != null)
                {
                    var isArray = relationshipValue is IEnumerable && !(relationshipValue is string);
                    if (isArray)
                    {
                        var list = (IList)relationshipValue;
                        foreach (var item in list)
                        {
                            EnsurePersistenceKeyInNewEntities(item);
                        }
                    }
                    else
                    {
                        EnsurePersistenceKeyInNewEntities(relationshipValue);
                    }
                }
            }
        }

        private async Task RefreshEntity<TEntity>(TEntity entity)
            where TEntity : class
        {
            var identityWhereOperation =
                DataContext.ResolveWithKeyOperationFromEntity(entity);
            var queryable = DataContext.AsDbSetByType(typeof(TEntity));
            //var refreshConfiguration = DataContext.GetConfiguration<EntityDefaultQueryConfiguration>();
            //if (refreshConfiguration != null)
            //{
            //    queryable = refreshConfiguration.GetQueryable<TEntity>()();
            //}
            //else
            //{
            //    queryable =
            //        DataContext.AsDbSetByType(typeof(TEntity));
            //}
            // This will trigger a merge in the tracking store
            await queryable.WithKey(identityWhereOperation.Key);
        }
    }
}