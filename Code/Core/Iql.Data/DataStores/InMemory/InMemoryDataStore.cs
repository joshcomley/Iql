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
using Iql.Data.Serialization;
using Iql.Data.Tracking;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Extensions;
using Newtonsoft.Json;

namespace Iql.Data.DataStores.InMemory
{
    public class InMemoryDataStore : DataStore, IOfflineDataStore
    {
        private class InMemoryDatabase
        {
            public AutoIntegerIdStrategy DefaultIntegerIdStrategy { get; set; } = AutoIntegerIdStrategy.Positive;

            public virtual IList DataSetByType(Type type)
            {
                if (!Data.ContainsKey(type))
                {
                    Data.Add(type, ListExtensions.NewGenericList(type));
                }

                return Data[type];
            }

            private readonly Dictionary<Type, int> _guidCount = new Dictionary<Type, int>();
            public Guid NextIdGuid(Type entityType,  IProperty property)
            {
                if (!_guidCount.ContainsKey(entityType))
                {
                    _guidCount.Add(entityType, 0);
                }

                var data = DataSetByType(entityType);
                var guidCount = _guidCount[entityType];
                Guid guid = GuidFromId(++guidCount);
                while (true)
                {
                    var allow = true;
                    foreach (var item in data)
                    {
                        if ((Guid)property.GetValue(item) == guid)
                        {
                            allow = false;
                            break;
                        }
                    }

                    if (allow)
                    {
                        break;
                    }
                    guid = GuidFromId(++guidCount);
                }
                _guidCount[entityType] = guidCount;
                return guid;
            }

            private static Guid GuidFromId(int id)
            {
                var idStr = id.ToString();
                var remain = 12 - idStr.Length;
                for (var i = 0; i < remain; i++)
                {
                    idStr = "0" + idStr;
                }

                return new Guid($"00000000-0000-0000-0000-{idStr}");
            }

            private readonly Dictionary<Type, int> _idCount = new Dictionary<Type, int>();
            public int NextIdInteger(Type entityType,  IProperty property)
            {
                if (!_idCount.ContainsKey(entityType))
                {
                    _idCount.Add(entityType, 0);
                }
                var data = DataSetByType(entityType);

                var idCount = _idCount[entityType];
                var integerIdStrategy = GetSetConfiguration(entityType).IntegerIdStrategy ?? DefaultIntegerIdStrategy;
                if (integerIdStrategy == AutoIntegerIdStrategy.Positive)
                {
                    foreach (var existingEntity in data)
                    {
                        var value = (int)existingEntity.GetPropertyValue(property);
                        if (value > idCount)
                        {
                            idCount = value;
                        }
                    }

                    idCount++;
                }
                else
                {
                    foreach (var existingEntity in data)
                    {
                        var value = (int)existingEntity.GetPropertyValue(property);
                        if (value < idCount)
                        {
                            idCount = value;
                        }
                    }

                    idCount--;
                }
                _idCount[entityType] = idCount;
                return idCount;
            }

            public string NextIdString(Type entityType, IProperty property)
            {
                return NextIdGuid(entityType, property).ToString();
            }

            private readonly Dictionary<Type, OfflineDataStoreSetConfiguration> _setConfigurations = new Dictionary<Type, OfflineDataStoreSetConfiguration>();
            public OfflineDataStoreSetConfiguration GetSetConfiguration(Type type)
            {
                if (!_setConfigurations.ContainsKey(type))
                {
                    _setConfigurations.Add(type, new OfflineDataStoreSetConfiguration());
                }
                return _setConfigurations[type];
            }
            public Dictionary<Type, IList> Data { get; } = new Dictionary<Type, IList>();
            private DataTracker _inMemoryDataTracker;
            public DataTracker InMemoryDataTracker
            {
                get => _inMemoryDataTracker = _inMemoryDataTracker ?? new DataTracker(DataTrackerKind.Online, EntityConfigurationBuilder, "In Memory", true);
            }
            public IEntityConfigurationBuilder EntityConfigurationBuilder { get; }

            public InMemoryDatabase(IEntityConfigurationBuilder entityConfigurationBuilder)
            {
                EntityConfigurationBuilder = entityConfigurationBuilder;
            }

            public void Clear()
            {
                _guidCount.Clear();
                _idCount.Clear();
                foreach (var entry in Data)
                {
                    entry.Value.Clear();
                }
                Data.Clear();
            }
        }
        static InMemoryDataStore()
        {
            SynchroniseDataTypedMethod = typeof(InMemoryDataStore).GetMethod(nameof(SynchroniseDataTyped),
                BindingFlags.Instance | BindingFlags.NonPublic);
        }

        private static MethodInfo SynchroniseDataTypedMethod { get; }

        private static readonly Dictionary<string, InMemoryDatabase> Databases = new Dictionary<string, InMemoryDatabase>();

        private InMemoryDatabase Database => EnsureInMemoryDatabase();

        private InMemoryDatabase EnsureInMemoryDatabase()
        {
            if (!Databases.ContainsKey(SynchronicityKey))
            {
                Databases.Add(SynchronicityKey, new InMemoryDatabase(EntityConfigurationBuilder));
            }

            return Databases[SynchronicityKey];
        }

        public InMemoryDataStoreConfiguration Configuration { get; set; }
        public virtual IList DataSetByType(Type type)
        {
            var source = Configuration?.GetSourceByType(type);
            if (source != null)
            {
                return source;
            }

            if (!Database.Data.ContainsKey(type))
            {
                Database.Data.Add(type, ListExtensions.NewGenericList(type));
            }

            return Database.Data[type];
        }

        public virtual IList<TEntity> DataSet<TEntity>()
            where TEntity : class
        {
            return (List<TEntity>)DataSetByType(typeof(TEntity));
        }

        public InMemoryDataStore(string name = null, AutoIntegerIdStrategy defaultAutoIntegerIdStrategy = AutoIntegerIdStrategy.Positive) : base(name ?? nameof(InMemoryDataStore))
        {
            Name = name;
            DefaultIntegerIdStrategy = defaultAutoIntegerIdStrategy;
        }

        private DataTracker InMemoryDataTracker => Database.InMemoryDataTracker;

        private readonly Dictionary<object, object> _cloneMap = new Dictionary<object, object>();

        public override string SerializeStateToJson()
        {
            var allSets = AllDataSetMaps()
                .OrderBy(_ => _.Key.Name)
                .Select(_ =>
                    new
                    {
                        Type = _.Key.Name,
                        Entities = JsonDataSerializer.PrepareCollectionForSerialization(_.Value, _.Key, false, true)
                    }
                );
            return JsonConvert.SerializeObject(allSets);
        }

        public override Task<AddEntityResult<TEntity>> PerformAddAsync<TEntity>(
            QueuedAddEntityOperation<TEntity> operation)
        {
            var data = DataSetByType(operation.Operation.EntityType);

            var clone = operation.Operation.EntityState.Entity.CloneAs(
                EntityConfigurationBuilder,
                typeof(TEntity),
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
                    foreach (var property in configuration.Key.Properties)
                    {
                        if (property.Kind.HasFlag(IqlPropertyKind.Key))
                        {
                            //var oldId = clone.GetPropertyValue(property);
                            if (property.TypeDefinition.ElementType == typeof(int))
                            {
                                clone.SetPropertyValue(property, Database.NextIdInteger(rootTrackingSet.EntityType, property));
                            }
                            else if (property.TypeDefinition.ElementType == typeof(string))
                            {
                                clone.SetPropertyValue(property, Database.NextIdString(rootTrackingSet.EntityType, property));
                            }
                            else if (property.TypeDefinition.Kind == IqlType.Guid)
                            {
                                clone.SetPropertyValue(property, Database.NextIdGuid(rootTrackingSet.EntityType, property));
                            }
                        }
                    }
                    if (!rootTrackingSet.IsMatchingEntityTracked(clone))
                    {
                        rootTrackingSet.AttachEntity(clone, false);
                    }
                    else
                    {
                        rootTrackingSet.FindMatchingEntityState(clone).IsNew = false;
                    }
                });
            data.Add(clone);
            var entityState = rootTrackingSet.GetEntityState(clone);
            entityState.HardReset();
            operation.Result.Success = true;
            operation.Result.RemoteEntity = clone;
            return Task.FromResult(operation.Result);
        }

        private int FindEntityIndexFromOperation<TEntity>(EntityCrudOperation<TEntity> operation) where TEntity : class
        {
            return FindEntityIndex(
                operation.DataContext.EntityConfigurationContext,
                operation.EntityType,
                operation.EntityState.Entity,
                DataSet<TEntity>()
            );
        }

        public override Task<UpdateEntityResult<TEntity>> PerformUpdateAsync<TEntity>(
            QueuedUpdateEntityOperation<TEntity> operation)
        {
            var index = FindEntityIndexFromOperation(operation.Operation);
            if (index != -1)
            {
                var rootTrackingSet = InMemoryDataTracker.TrackingSet<TEntity>();
                var entity = DataSet<TEntity>()[index];
                rootTrackingSet.GetEntityState(entity)?.HardReset();
                new SimplePropertyMerger(EntityConfigurationBuilder.EntityType<TEntity>())
                    .MergeAllProperties(
                        entity,
                        operation.Operation.EntityState.Entity,
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

        public override Task<FlattenedGetDataResult<TEntity>> PerformCountAsync<TEntity>(QueuedGetDataOperation<TEntity> operation)
        {
            return PerformGetInnerAsync(operation, false);
        }

        public override Task<FlattenedGetDataResult<TEntity>> PerformGetAsync<TEntity>(QueuedGetDataOperation<TEntity> operation)
        {
            return PerformGetInnerAsync(operation, true);
        }

        private async Task<FlattenedGetDataResult<TEntity>> PerformGetInnerAsync<TEntity>(QueuedGetDataOperation<TEntity> operation, bool trackResults)
            where TEntity : class
        {
            var iql = await operation.Operation.Queryable.ToIqlAsync();
            var expression = IqlExpressionConversion.DefaultExpressionConverter()
                .ConvertIqlToExpression<TEntity>(iql, operation.Operation.DataContext.EntityConfigurationContext);
            var func = (Func<InMemoryContext<TEntity>, InMemoryContext<TEntity>>) expression.Compile();
            var inMemoryContext = new InMemoryContext<TEntity>(this);
            var result = func(inMemoryContext);
            var resultList = result.SourceList.ToList();
            inMemoryContext.Finish();
            if (trackResults)
            {
                inMemoryContext.AddMatches(typeof(TEntity), resultList);
                var cloneLookup = new Dictionary<object, object>();
                var clonedResult = new List<TEntity>();
                var pageSize =
                    iql.Take ?? Database.GetSetConfiguration(typeof(TEntity)).PageSize ?? DefaultPageSize;
                var take = 0;
                for (var i = 0; i < resultList.Count; i++)
                {
                    if (iql.Skip != null && i < iql.Skip)
                    {
                        continue;
                    }

                    if (pageSize != null && take == pageSize)
                    {
                        break;
                    }

                    take++;
                    var item = resultList[i];
                    var clone = item.Clone(EntityConfigurationBuilder, typeof(TEntity));
                    cloneLookup.Add(item, clone);
                    clonedResult.Add((TEntity)clone);
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
                            newList.Add(item.Clone(EntityConfigurationBuilder, pair.Key));
                        }
                    }

                    dictionary.Add(pair.Key, newList);
                }
                operation.Result.Root = clonedResult;
                operation.Result.Data = dictionary;
            }

            operation.Result.TotalCount = resultList.Count;
            operation.Result.Success = true;
            return operation.Result;
        }

        private string _synchronicityKey;
        private bool _synchronicityKeySet = false;
        private AutoIntegerIdStrategy _defaultIntegerIdStrategy;
        private bool _defaultIntegerIdStrategySet = false;

        public string SynchronicityKey
        {
            get => (!_synchronicityKeySet ? Name : _synchronicityKey) ?? "default";
        }

        public int? DefaultPageSize { get; set; }

        public override IEntityConfigurationBuilder EntityConfigurationBuilder 
        {
            get { return base.EntityConfigurationBuilder; }
            set
            {
                base.EntityConfigurationBuilder = value;
                if (value != null)
                {
                    EnsureInMemoryDatabase();
                    if (_defaultIntegerIdStrategySet)
                    {
                        Database.DefaultIntegerIdStrategy = _defaultIntegerIdStrategy;
                    }
                }
            }
        } 

        public AutoIntegerIdStrategy DefaultIntegerIdStrategy
        {
            get
            {
                return EntityConfigurationBuilder == null
                    ? _defaultIntegerIdStrategy
                    : Database.DefaultIntegerIdStrategy;
            }
            set
            {
                if (EntityConfigurationBuilder == null)
                {
                    _defaultIntegerIdStrategySet = true;
                    _defaultIntegerIdStrategy = value;
                }
                else
                {
                    Database.DefaultIntegerIdStrategy = value;
                }
            }
        }

        public void ConfigureSet(Type type, Action<OfflineDataStoreSetConfiguration> configure)
        {
            configure(Database.GetSetConfiguration(type));
        }

        public OfflineDataStoreSetConfiguration GetSetConfiguration(Type type)
        {
            return Database.GetSetConfiguration(type);
        }

        public Task ResetAsync()
        {
            Database.Clear();
            Configuration?.Reset();
            return Task.FromResult<object>(null);
        }

        public async Task<bool> RestoreStateAsync(IPersistState persistState)
        {
            if (persistState != null)
            {
                var state = await persistState.FetchStateAsync(PersistStateKey());
                await RestoreStateFromJsonAsync(state);
                return true;
            }

            return false;
        }

        public Task<bool> RestoreStateFromJsonAsync(string json)
        {
            DeserializedEntitySets sets;
            try
            {
                sets = JsonDataSerializer.DeserializeEntitySets(EntityConfigurationBuilder, json);
            }
            catch
            {
                return Task.FromResult(false);
            }
            return RestoreStateFromSetsAsync(sets);
        }

        public async Task<bool> RestoreStateFromSetsAsync(DeserializedEntitySets sets)
        {
            Clear();
            // Restore...
            for (var i = 0; i < sets.Types.Length; i++)
            {
                var type = sets.Types[i];
                var list = DataSetByType(type);
                list.Clear();
                var objects = sets.SetByType(type);
                for (var j = 0; j < objects.Count; j++)
                {
                    var entity = objects[j];
                    list.Add(entity);
                }
            }
            return true;
        }

        public virtual async Task<bool> ClearStateAsync(IPersistState persistState)
        {
            Clear();
            return await SaveStateAsync(persistState);
        }

        private string PersistStateKey()
        {
            return $"DataStore-{Name}";
        }

        public virtual async Task<bool> SaveStateAsync(IPersistState persistState)
        {
            if (persistState != null)
            {
                return await persistState.SaveStateAsync(PersistStateKey(), SerializeStateToJson());
            }
            return false;
        }

        public IList[] AllDataSets()
        {
            if (Configuration != null)
            {
                return Configuration.AllDataSources();
            }
            var list = new List<IList>();
            foreach (var source in Database.Data)
            {
                list.Add(source.Value);
            }
            return list.ToArray();
        }

        public Dictionary<IEntityConfiguration, IList> AllDataSetMaps()
        {
            if (Configuration != null)
            {
                return Configuration.AllDataSourceMaps();
            }
            var list = new Dictionary<IEntityConfiguration, IList>();
            foreach (var source in Database.Data)
            {
                list.Add(EntityConfigurationBuilder.GetEntityByType(source.Key), source.Value);
            }
            return list;
        }

        public void Clear()
        {
            Database.Clear();
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
                    this, new object[] { entry.Value }, entry.Key);
            }
        }

        public Task ApplyAddAsync<TEntity>(QueuedAddEntityOperation<TEntity> operation) where TEntity : class
        {
            var operationEntity = operation.Operation.EntityState.Entity;
            TrackEntity(operationEntity);
            return Task.FromResult<object>(null);
        }

        private void TrackEntity<TEntity>(TEntity operationEntity) where TEntity : class
        {
            var list = new List<TEntity>();
            list.Add(operationEntity);
            SynchroniseDataTyped(list);
        }

        public Task ApplyUpdateAsync<TEntity>(
            QueuedUpdateEntityOperation<TEntity> operation,
            IPropertyState[] changedProperties) where TEntity : class
        {
            var match = TryFindEntity(operation.Operation.EntityState.Entity, out var source);
            if (match != null)
            {
                for (var i = 0; i < changedProperties.Length; i++)
                {
                    var propertyState = changedProperties[i];
                    propertyState.Property.SetValue(match, propertyState.LocalValue);
                }
            }
            else
            {
                TrackEntity(operation.Operation.EntityState.Entity);
            }
            return Task.FromResult<object>(null);
        }

        public Task ApplyDeleteAsync<TEntity>(QueuedDeleteEntityOperation<TEntity> operation) where TEntity : class
        {
            var match = TryFindEntity(operation.Operation.EntityState.Entity, out var source);
            if (match != null)
            {
                source.Remove(match);
            }
            return Task.FromResult<object>(null);
        }

        private TEntity TryFindEntity<TEntity>(TEntity operationEntity, out IList<TEntity> source) where TEntity : class
        {
            source = DataSetByType(typeof(TEntity)) as IList<TEntity>;
            var entityConfig = EntityConfigurationBuilder.GetEntityByType(typeof(TEntity));
            var key = entityConfig.GetCompositeKey(operationEntity);
            return source.SingleOrDefault(_ => entityConfig.GetCompositeKey(_).Matches(key));
        }

        private void SynchroniseDataTyped<T>(IList<T> data)
        {
            var source = DataSetByType(typeof(T)) as IList<T>;
            var entityConfig = EntityConfigurationBuilder.GetEntityByType(typeof(T));
            for (var i = 0; i < data.Count; i++)
            {
                var entity = data[i];
                var key = entityConfig.GetCompositeKey(entity);
                var match = source.SingleOrDefault(_ => entityConfig.GetCompositeKey(_).Matches(key));
                if (match != null)
                {
                    source.Remove(match);
                }
                var clone = (T)entity.Clone(EntityConfigurationBuilder, typeof(T));
                source.Add(clone);
            }
        }
    }
}