using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.Crud.Operations.Results;
using Iql.Data.Relationships;
using Iql.Data.Tracking;
using Iql.Entities;
using Iql.Entities.Extensions;

namespace Iql.Data.DataStores.InMemory
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

        private readonly Dictionary<object, object> _cloneMap = new Dictionary<object, object>();

        public override Task<AddEntityResult<TEntity>> PerformAddAsync<TEntity>(
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
                    if (!rootTrackingSet.IsMatchingEntityTracked(clone))
                    {
                        rootTrackingSet.TrackEntity(clone, null, false);
                    }
                    else
                    {
                        rootTrackingSet.FindMatchingEntityState(clone).IsNew = false;
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
                    if (!trackingSet.IsMatchingEntityTracked(entity))
                    {
                        trackingSet.TrackEntity(entity, null, entity != clone);
                    }
                }
            }
            InMemoryRelationshipObserver.ObserveAll(flattenObjectGraph);
            data.Add(clone);
            operation.Result.Success = true;
            operation.Result.RemoteEntity = clone;
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

        public override Task<UpdateEntityResult<TEntity>> PerformUpdateAsync<TEntity>(
            QueuedUpdateEntityOperation<TEntity> operation)
        {
            var index = FindEntityIndexFromOperation(operation.Operation);
            if (index != -1)
            {
                var entity = DataSet<TEntity>(operation.Operation)[index];
                new SimplePropertyMerger(DataContext.EntityConfigurationContext.EntityType<TEntity>())
                    .Merge(entity, operation.Operation.Entity);
                operation.Result.Success = true;
            }
            else
            {
                operation.Result.Success = false;
            }
            return Task.FromResult(operation.Result);
        }

        public override Task<DeleteEntityResult<TEntity>> PerformDeleteAsync<TEntity>(
            QueuedDeleteEntityOperation<TEntity> operation)
        {
            var index = FindEntityIndexFromDeleteOperation(operation.Operation);
            if (index != -1)
            {
                DataSet<TEntity>(operation.Operation).RemoveAt(index);
                operation.Result.Success = true;
            }
            return Task.FromResult(operation.Result);
        }

        private int FindEntityIndexFromDeleteOperation<TEntity>(DeleteEntityOperation<TEntity> operation) where TEntity : class
        {
            var index = FindEntityIndexFromOperation(operation);
            if (index == -1)
            {
                return FindEntityIndexByKey(typeof(TEntity), operation.Key, DataSet<TEntity>(operation));
            }
            return index;
        }

        public override async Task<FlattenedGetDataResult<TEntity>> PerformGetAsync<TEntity>(QueuedGetDataOperation<TEntity> operation)
        {
            var iql = await operation.Operation.Queryable.ToIqlAsync();
            var expression = IqlExpressionConversion.DefaultExpressionConverter().ConvertIqlToExpression<TEntity>(iql);
            var func = (Func<InMemoryContext<TEntity>, InMemoryContext<TEntity>>)expression.Compile();
            var inMemoryContext = new InMemoryContext<TEntity>(DataContext);
            var result = func(inMemoryContext);
            var resultList = result.SourceList.ToList();
            var clonedResult = resultList.CloneAs(DataContext, typeof(TEntity), RelationshipCloneMode.DoNotClone).ToList();
            inMemoryContext.AddMatches(typeof(TEntity), clonedResult);
            var dictionary = new Dictionary<Type, IList>();
            foreach (var item in inMemoryContext.AllData)
            {
                dictionary[item.Key] = item.Value.CloneAs(DataContext, item.Key, RelationshipCloneMode.DoNotClone);
            }
            operation.Result.Root = clonedResult;
            operation.Result.Success = true;
            operation.Result.Data = dictionary;
            return operation.Result;
            // Now convert IQL to a native expression
            // Then run that native expression
            //var q = (IInMemoryResult)
            //    operation.Operation.Queryable.ToQueryWithAdapterBase(
            //    QueryableAdapter,
            //    DataContext,
            //    null,
            //    null);
            //var lists = q.GetResults();
            //var dictionary = new Dictionary<Type, IList>();
            //foreach (var item in lists.AllData)
            //{
            //    dictionary[item.Key] = item.Value.CloneAs(DataContext, item.Key, RelationshipCloneMode.DoNotClone);
            //}

            //var cloned = lists.Root.CloneAs(DataContext, typeof(TEntity), RelationshipCloneMode.DoNotClone);
            //lists.Root = cloned;
            //operation.Result.Data = dictionary;
            //operation.Result.Root = (List<TEntity>)lists.Root;
            //return Task.FromResult(operation.Result);
        }
    }
}