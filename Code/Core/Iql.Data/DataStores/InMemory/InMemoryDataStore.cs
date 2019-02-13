using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.Crud.Operations.Results;
using Iql.Data.Relationships;
using Iql.Data.Tracking;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Extensions;

namespace Iql.Data.DataStores.InMemory
{
    public class InMemoryDataStore : DataStore, IOfflineDataStore
    {
        static InMemoryDataStore()
        {
            SynchroniseDataTypedMethod = typeof(InMemoryDataStore).GetMethod(nameof(SynchroniseDataTyped),
                BindingFlags.Instance | BindingFlags.NonPublic);
        }

        private static MethodInfo SynchroniseDataTypedMethod { get; }

        private readonly Dictionary<Type, IList> _sources = new Dictionary<Type, IList>();

        public InMemoryDataStoreConfiguration Configuration { get; set; }
        public virtual IList DataSetByType(Type type)
        {
            var source = Configuration?.GetSourceByType(type);
            if (source != null)
            {
                return source;
            }
            if (!_sources.ContainsKey(type))
            {
                _sources.Add(type, ListExtensions.NewGenericList(type));
            }

            return _sources[type];
        }

        public virtual IList<TEntity> DataSet<TEntity>()
            where TEntity : class
        {
            return (List<TEntity>) DataSetByType(typeof(TEntity));
        }

        public InMemoryDataStore(IOfflineDataStore offlineDataStore = null) : base(offlineDataStore)
        {
        }

        private DataTracker _inMemoryDataTracker;
        private DataTracker InMemoryDataTracker
        {
            get => _inMemoryDataTracker = _inMemoryDataTracker ?? new DataTracker(EntityConfigurationBuilder, "In Memory", true);
        }

        private readonly Dictionary<object, object> _cloneMap = new Dictionary<object, object>();

        public override Task<AddEntityResult<TEntity>> PerformAddAsync<TEntity>(
            QueuedAddEntityOperation<TEntity> operation)
        {
            var data = DataSetByType(operation.Operation.EntityType);

            var clone = operation.Operation.Entity.CloneAs(
                EntityConfigurationBuilder,
                typeof(TEntity),
                RelationshipCloneMode.DoNotClone,
                null,
                _cloneMap);

            var configuration = operation
                .Operation
                .DataContext
                .EntityConfigurationContext
                .GetEntityByType(operation.Operation.EntityType);

            var rootTrackingSet = InMemoryDataTracker.TrackingSet<TEntity>();
            rootTrackingSet.SetKey(clone,
                () =>
                {
                    if (!rootTrackingSet.IsMatchingEntityTracked(clone))
                    {
                        rootTrackingSet.AttachEntity(clone, false);
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
                            else if(property.TypeDefinition.Kind == IqlType.Guid)
                            {
                                clone.SetPropertyValue(property, NextIdGuid(data, property));
                            }
                        }
                    }
                });
            data.Add(clone);
            operation.Result.Success = true;
            operation.Result.RemoteEntity = clone;
            return Task.FromResult(operation.Result);
        }

        private int _guidCount = 0;
        private Guid NextIdGuid(IList data, IProperty property)
        {
            _guidCount++;
            var id = _guidCount.ToString();
            var remain = 12 - id.Length;
            for (var i = 0; i < remain; i++)
            {
                id = "0" + id;
            }
            return new Guid($"00000000-0000-0000-0000-{id}");
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

        private int FindEntityIndexFromOperation<TEntity>(EntityCrudOperation<TEntity> operation) where TEntity : class
        {
            return FindEntityIndex(
                operation.EntityType,
                operation.Entity,
                DataSet<TEntity>()
            );
        }

        public override Task<UpdateEntityResult<TEntity>> PerformUpdateAsync<TEntity>(
            QueuedUpdateEntityOperation<TEntity> operation)
        {
            var index = FindEntityIndexFromOperation(operation.Operation);
            if (index != -1)
            {
                var entity = DataSet<TEntity>()[index];
                new SimplePropertyMerger(EntityConfigurationBuilder.EntityType<TEntity>())
                    .MergeAllProperties(
                        entity, 
                        operation.Operation.Entity,
                        operation.Operation.Properties);
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
                DataSet<TEntity>().RemoveAt(index);
                operation.Result.Success = true;
            }
            return Task.FromResult(operation.Result);
        }

        private int FindEntityIndexFromDeleteOperation<TEntity>(DeleteEntityOperation<TEntity> operation) where TEntity : class
        {
            var index = FindEntityIndexFromOperation(operation);
            if (index == -1)
            {
                return FindEntityIndexByKey(typeof(TEntity), operation.Key, DataSet<TEntity>());
            }
            return index;
        }

        public override async Task<FlattenedGetDataResult<TEntity>> PerformGetAsync<TEntity>(QueuedGetDataOperation<TEntity> operation)
        {
            var iql = await operation.Operation.Queryable.ToIqlAsync();
            var expression = IqlExpressionConversion.DefaultExpressionConverter().ConvertIqlToExpression<TEntity>(iql);
            var func = (Func<InMemoryContext<TEntity>, InMemoryContext<TEntity>>)expression.Compile();
            var inMemoryContext = new InMemoryContext<TEntity>(this);
            var result = func(inMemoryContext);
            var resultList = result.SourceList.ToList();
            inMemoryContext.AddMatches(typeof(TEntity), resultList);
            var cloneLookup = new Dictionary<object, object>();
            var clonedResult = new List<TEntity>();
            foreach (var item in resultList)
            {
                var clone = item.Clone(EntityConfigurationBuilder, typeof(TEntity), RelationshipCloneMode.DoNotClone);
                cloneLookup.Add(item, clone);
                clonedResult.Add((TEntity) clone);
            }
            var dictionary = new Dictionary<Type, IList>();
            foreach (var pair in inMemoryContext.AllData)
            {
                var newList = ListExtensions.NewGenericList(pair.Key);

                foreach (var item in pair.Value)
                {
                    if (cloneLookup.ContainsKey(item))
                    {
                        newList.Add(cloneLookup[item]);
                    }
                    else
                    {
                        newList.Add(item.Clone(EntityConfigurationBuilder, pair.Key, RelationshipCloneMode.DoNotClone));
                    }
                }

                dictionary.Add(pair.Key, newList);
            }
            operation.Result.Root = clonedResult;
            operation.Result.Success = true;
            operation.Result.Data = dictionary;
            return operation.Result;
        }

        public IList[] AllDataSets()
        {
            if (Configuration != null)
            {
                return Configuration.AllDataSources();
            }
            var list = new List<IList>();
            foreach (var source in _sources)
            {
                list.Add(source.Value);
            }
            return list.ToArray();
        }

        public void Clear()
        {
            foreach (var source in AllDataSets())
            {
                source.Clear();
            }

            InMemoryDataTracker.Clear();
            _cloneMap.Clear();
        }

        public void SynchroniseData(Dictionary<Type, IList> data)
        {
            foreach (var entry in data)
            {
                SynchroniseDataTypedMethod.InvokeGeneric(
                    this, new object[] {entry.Value}, entry.Key);
            }
        }

        public Task<AddEntityResult<TEntity>> ScheduleAddAsync<TEntity>(QueuedAddEntityOperation<TEntity> operation) where TEntity : class
        {
            operation.Result.Success = true;
            return Task.FromResult(operation.Result);
        }

        public Task<UpdateEntityResult<TEntity>> ScheduleUpdateAsync<TEntity>(QueuedUpdateEntityOperation<TEntity> operation) where TEntity : class
        {
            operation.Result.Success = true;
            return Task.FromResult(operation.Result);
        }

        public Task<DeleteEntityResult<TEntity>> ScheduleDeleteAsync<TEntity>(QueuedDeleteEntityOperation<TEntity> operation) where TEntity : class
        {
            operation.Result.Success = true;
            return Task.FromResult(operation.Result);
        }

        private void SynchroniseDataTyped<T>(IList<T> data)
        {
            var source = DataSetByType(typeof(T)) as IList<T>;
            var entityConfig = EntityConfigurationBuilder.GetEntityByType(typeof(T));
            foreach (var entity in data)
            {
                var key = entityConfig.GetCompositeKey(entity);
                var match = source.SingleOrDefault(_ => entityConfig.GetCompositeKey(_).Matches(key));
                if (match != null)
                {
                    source.Remove(match);
                }
                var clone = (T) entity.Clone(EntityConfigurationBuilder, typeof(T), RelationshipCloneMode.DoNotClone);
                source.Add(clone);
            }
        }
    }
}