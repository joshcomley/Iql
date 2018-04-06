using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Crud.Operations.Queued;
using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Data.Queryable;
using Iql.Queryable.Data.Relationships;
using Iql.Queryable.Data.Tracking;
using Iql.Queryable.Extensions;

namespace Iql.Queryable.Data.DataStores.InMemory
{
    public class InMemoryDataStore : DataStore
    {
        private RelationshipObserver _inMemoryRelationshipObserver;
        private TrackingSetCollection _inMemoryTrackingSetCollection;

        private TrackingSetCollection InMemoryTrackingSetCollection
        {
            get { return _inMemoryTrackingSetCollection = _inMemoryTrackingSetCollection ?? new TrackingSetCollection(this); }
        }

        private RelationshipObserver InMemoryRelationshipObserver
        {
            get { return _inMemoryRelationshipObserver = _inMemoryRelationshipObserver ?? new RelationshipObserver(DataContext, InMemoryTrackingSetCollection, true); }
        }

        public InMemoryDataStore(IQueryableAdapterBase queryableAdapter)
        {
            QueryableAdapter = queryableAdapter;
        }

        public IQueryableAdapterBase QueryableAdapter { get; }

        private readonly Dictionary<object, object> _cloneMap = new Dictionary<object, object>();

        public override Task<AddEntityResult<TEntity>> PerformAdd<TEntity>(
            QueuedAddEntityOperation<TEntity> operation)
        {
            var data = operation.Operation.DataContext.GetConfiguration<InMemoryDataStoreConfiguration>()
                .GetSourceByType(operation.Operation.EntityType);

            var clone = operation.Operation.Entity.CloneAs(
                DataContext,
                typeof(TEntity),
                RelationshipCloneMode.Full,
                null,
                _cloneMap);

            var configuration = operation
                .Operation
                .DataContext
                .EntityConfigurationContext
                .GetEntityByType(operation.Operation.EntityType);

            var rootTrackingSet = InMemoryTrackingSetCollection.TrackingSet<TEntity>();
            rootTrackingSet.SetKey(clone,
                () =>
                {
                    if (!rootTrackingSet.IsTracked(clone))
                    {
                        rootTrackingSet.TrackEntity(clone, null, false);
                    }
                    else
                    {
                        rootTrackingSet.GetEntityState(clone).IsNew = false;
                    }
                    foreach (var property in configuration.Key.Properties)
                    {
                        if (property.Kind.HasFlag(PropertyKind.Key))
                        {
                            //var oldId = clone.GetPropertyValue(property);
                            if (property.TypeDefinition.ElementType == typeof(int))
                            {
                                clone.SetPropertyValue(property, NextIdInteger(data, property));
                            }
                            else if (property.TypeDefinition.ElementType == typeof(string))
                            {
                                clone.SetPropertyValue(property, NextIdString(data, property));
                            }
                            //if (!oldId.IsDefaultValue())
                            //{
                            //    var newId = clone.GetPropertyValue(property);
                            //    foreach (var relationship in configuration.Relationships)
                            //    {
                            //        switch (relationship.Kind)
                            //        {
                            //            case RelationshipKind.OneToOne:
                            //                break;
                            //            case RelationshipKind.OneToMany:
                            //                break;
                            //        }
                            //    }
                            //}
                        }
                    }
                });
            var flattenObjectGraph = DataContext.EntityConfigurationContext.FlattenObjectGraph(clone, typeof(TEntity));
            foreach (var grouping in flattenObjectGraph)
            {
                var trackingSet = InMemoryTrackingSetCollection.TrackingSetByType(grouping.Key);
                foreach (var entity in grouping.Value)
                {
                    if (!trackingSet.IsTracked(entity))
                    {
                        trackingSet.TrackEntity(entity, null, entity != clone);
                    }
                }
            }
            InMemoryRelationshipObserver.ObserveAll(flattenObjectGraph);
            data.Add(clone);
            operation.Result.Success = true;
            operation.Result.RemoteEntity = (TEntity)clone;
            return Task.FromResult(operation.Result);
        }

        public int NextIdInteger(IList data, IProperty property)
        {
            int max = 0;
            foreach (var existingEntity in data)
            {
                var value = (int)existingEntity.GetPropertyValue(property);
                if (value > max)
                {
                    max = value;
                }
            }
            return ++max;
        }

        public string NextIdString(IList data, IProperty property)
        {
            return Guid.NewGuid().ToString();
        }

        private static IList<TEntity> DataSet<TEntity>(ICrudOperation operation)
        {
            return operation.DataContext.GetConfiguration<InMemoryDataStoreConfiguration>().GetSource<TEntity>();
        }

        private int FindEntityIndexFromOperation<TEntity>(EntityCrudOperation<TEntity> operation) where TEntity : class
        {
            return FindEntityIndex(
                operation.EntityType,
                operation.Entity,
                DataSet<TEntity>(operation)
            );
        }

        public override Task<UpdateEntityResult<TEntity>> PerformUpdate<TEntity>(
            QueuedUpdateEntityOperation<TEntity> operation)
        {
            var index = FindEntityIndexFromOperation(operation.Operation);
            if (index != -1)
            {
                var entity = DataSet<TEntity>(operation.Operation)[index];
                new SimplePropertyMerger(DataContext.EntityConfigurationContext.EntityType<TEntity>())
                    .Merge(entity, operation.Operation.Entity);
            }
            return Task.FromResult(operation.Result);
        }

        public override Task<DeleteEntityResult<TEntity>> PerformDelete<TEntity>(
            QueuedDeleteEntityOperation<TEntity> operation)
        {
            var index = FindEntityIndexFromOperation(operation.Operation);
            if (index != -1)
            {
                DataSet<TEntity>(operation.Operation).RemoveAt(index);
                operation.Result.Success = true;
            }
            return Task.FromResult(operation.Result);
        }

        public override Task<FlattenedGetDataResult<TEntity>> PerformGet<TEntity>(QueuedGetDataOperation<TEntity> operation)
        {
            var q = (IInMemoryResult)
                operation.Operation.Queryable.ToQueryWithAdapterBase(
                QueryableAdapter,
                DataContext,
                null,
                null);
            var lists = q.GetResults();
            var dictionary = new Dictionary<Type, IList>();
            foreach (var item in lists.AllData)
            {
                dictionary[item.Key] = item.Value.CloneAs(DataContext, item.Key, RelationshipCloneMode.DoNotClone);
            }

            lists.Root = lists.Root.CloneAs(DataContext, typeof(TEntity), RelationshipCloneMode.DoNotClone);
            operation.Result.Data = dictionary;
            operation.Result.Root = (List<TEntity>)lists.Root;
            return Task.FromResult(operation.Result);
        }
    }
}