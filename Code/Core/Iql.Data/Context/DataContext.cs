using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Iql.Data.Crud;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.Crud.Operations.Results;
using Iql.Data.DataStores;
using Iql.Data.DataStores.InMemory;
using Iql.Data.DataStores.NestedSets;
using Iql.Data.Extensions;
using Iql.Data.Lists;
using Iql.Data.Operations;
using Iql.Data.Paging;
using Iql.Data.Queryable;
using Iql.Data.SpecialTypes;
using Iql.Data.Tracking;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Entities.InferredValues;
using Iql.Entities.Relationships;
using Iql.Entities.Services;
using Iql.Entities.Validation;
using Iql.Entities.Validation.Validation;
using Iql.Events;
using Iql.Extensions;
using Iql.Parsing;
using Iql.Queryable.Operations;

namespace Iql.Data.Context
{
    public class DataContext : IDataContext
    {
        private class SynchronisedDataContextConfiguration
        {
            private DataTracker _offlineDataTracker;
            public DataTracker OfflineDataTracker => _offlineDataTracker;
            public EventEmitter<OfflineChangeStateChangedEvent> OfflineStateChanged { get; } = new EventEmitter<OfflineChangeStateChangedEvent>();

            public IEntityConfigurationBuilder EntityConfigurationBuilder { get; set; }
            public string SynchronicityKey { get; set; }

            public SynchronisedDataContextConfiguration(IEntityConfigurationBuilder entityConfigurationBuilder, string synchronicityKey)
            {
                EntityConfigurationBuilder = entityConfigurationBuilder;
                SynchronicityKey = synchronicityKey;
                _offlineDataTracker = new DataTracker(DataTrackerKind.Offline, entityConfigurationBuilder, "Offline", true);
            }
        }

        private IOfflineDataStore _offlineDataStore = new InMemoryDataStore("OfflineData", AutoIntegerIdStrategy.Negative);

        public EventEmitter<OfflineChangeStateChangedEvent> OfflineStateChanged =>
            SynchronisedConfiguration.OfflineStateChanged;

        public bool EnableOffline { get; set; }

        public bool SupportsOffline => EnableOffline && OfflineDataStore != null;

        public IOfflineDataStore OfflineDataStore
        {
            get
            {
                if (_offlineDataStore != null && _offlineDataStore.EntityConfigurationBuilder == null)
                {
                    _offlineDataStore.EntityConfigurationBuilder = EntityConfigurationContext;
                }

                return _offlineDataStore;
            }
            set
            {
                _offlineDataStore = value;
                if (value != null)
                {
                    value.EntityConfigurationBuilder = EntityConfigurationContext;
                }
            }
        }

        private MethodInfo _addInternalMethod;

        private MethodInfo AddInternalMethod =>
            _addInternalMethod =
                _addInternalMethod ??
                typeof(DataContext)
                    .GetMethod(nameof(AddInternal),
                        BindingFlags.Instance |
                        BindingFlags.NonPublic);

        private MethodInfo _deleteInternalMethod;

        private MethodInfo DeleteInternalMethod =>
            _deleteInternalMethod =
                _deleteInternalMethod ??
                typeof(DataContext)
                    .GetMethod(nameof(DeleteInternal),
                        BindingFlags.Instance |
                        BindingFlags.NonPublic);

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

        private IEntityState<T> AddInternal<T>(T entity)
            where T : class
        {
            return (IEntityState<T>)TemporalDataTracker.AddEntity(entity);
        }


        //#if !TypeScript
        //        public IEntityStateBase Delete(object entity)
        //        {
        //            return (IEntityStateBase)DeleteInternalMethod.InvokeGeneric(this, new[] { entity }, entity.GetType());
        //        }
        //#endif

        private readonly Dictionary<string, object> _configurations =
            new Dictionary<string, object>();
        private static readonly Dictionary<Type, EntityConfigurationBuilder> EntityConfigurationsBuilders
            = new Dictionary<Type, EntityConfigurationBuilder>();

        public static Type FindDataContextTypeForEntityType(Type entityType)
        {
            foreach (var lookup in EntityConfigurationsBuilders)
            {
                if (lookup.Value.IsEntityType(entityType))
                {
                    return lookup.Key;
                }
            }

            return null;
        }

        public static EntityConfigurationBuilder FindBuilderForEntityType(Type entityType)
        {
            foreach (var lookup in EntityConfigurationsBuilders)
            {
                if (lookup.Value.IsEntityType(entityType))
                {
                    return lookup.Value;
                }
            }

            return null;
        }

        public static IDataContext FindDataContextForEntity(object entity)
        {
            var tracker = FindTrackingForEntity(entity);
            if (tracker != null)
            {
                foreach (var dataContext in AllDataContexts)
                {
                    if (dataContext.TemporalDataTracker.TrackingSetByType(tracker.EntityType) == tracker)
                    {
                        return dataContext;
                    }
                }
            }
            return null;
        }

        public static ITrackingSet FindTrackingForEntity(object entity)
        {
            return GlobalTracking.GetTrackingSet(entity);
            //var trackers = DataTracker.AllDataTrackers();
            //for (var i = 0; i < trackers.Length; i++)
            //{
            //    var tracker = trackers[i];
            //    var set = tracker.GetTrackingSetForEntity(entity);
            //    if (set != null)
            //    {
            //        return set;
            //    }
            //}
            //return null;
        }

        public static IEntityStateBase FindEntity(object entity)
        {
            var tracker = FindTrackingForEntity(entity);
            return tracker?.FindMatchingEntityState(entity);
        }

        private DataTracker _dataTracker;
        public DataTracker TemporalDataTracker
        {
            get
            {
                if (_dataTracker == null)
                {
                    _dataTracker = new DataTracker(DataTrackerKind.Temporal, EntityConfigurationContext, "Temporal");
                    _dataTracker.RelationshipObserver.UntrackedEntityAdded.Subscribe(_ => { AddEntity(_.Entity); });
                    //_dataTracker.DataContext = this;
                }

                return _dataTracker;
            }
        }

        private static readonly
            Dictionary<IEntityConfigurationBuilder, Dictionary<string, SynchronisedDataContextConfiguration>>
            SynchronisedDataContextConfigurations
                = new Dictionary<IEntityConfigurationBuilder, Dictionary<string, SynchronisedDataContextConfiguration>
                >();
        public DataTracker OfflineDataTracker => SupportsOffline ? SynchronisedConfiguration.OfflineDataTracker : null;

        private SynchronisedDataContextConfiguration SynchronisedConfiguration
        {
            get
            {
                if (_synchronisedConfiguration != null)
                {
                    return _synchronisedConfiguration;
                }
                if (!SynchronisedDataContextConfigurations.ContainsKey(EntityConfigurationContext))
                {
                    SynchronisedDataContextConfigurations.Add(EntityConfigurationContext, new Dictionary<string, SynchronisedDataContextConfiguration>());
                }

                var lookup = SynchronisedDataContextConfigurations[EntityConfigurationContext];
                if (!lookup.ContainsKey(OfflineSynchronicityKey))
                {
                    lookup.Add(OfflineSynchronicityKey, new SynchronisedDataContextConfiguration(EntityConfigurationContext, SynchronicityKey));
                }

                _synchronisedConfiguration = lookup[OfflineSynchronicityKey];
                return _synchronisedConfiguration;
            }
        }
        internal static List<IDataContext> AllDataContexts { get; } = new List<IDataContext>();
        public DataContext(
            IDataStore dataStore = null,
            EvaluateContext evaluateContext = null
        )
        {
            AllDataContexts.Add(this);
            EvaluateContext = evaluateContext;
            void midSetup()
            {
                DataStore = dataStore;
                int a = 0;
            }
            var thisType = GetType();
            if (!EntityConfigurationsBuilders.ContainsKey(thisType))
            {
                EntityConfigurationContext = new EntityConfigurationBuilder();
                EntityConfigurationsBuilders.Add(thisType, EntityConfigurationContext);
                midSetup();
                Initialize();
            }
            else
            {
                EntityConfigurationContext = EntityConfigurationsBuilders[thisType];
                midSetup();
                _initialized = true;
                InitializeProperties();
                InitializeSetNames();
            }
        }

        private void Initialize()
        {
            if (!_initialized)
            {
                _initialized = true;
                Configure(EntityConfigurationContext);
                InitializeProperties();
                InitializeSetNames();
            }
        }

        private void InitializeSetNames()
        {
            var allConfigs = EntityConfigurationContext.AllEntityTypes().ToArray();
            for (var i = 0; i < allConfigs.Length; i++)
            {
                var config = allConfigs[i];
                if (!config.SetNameSet)
                {
                    var propertyName = GetDbSetPropertyNameByEntityType(config.Type);
                    if (!string.IsNullOrWhiteSpace(propertyName))
                    {
                        config.SetName = propertyName;
                    }
                }
            }
        }

        protected virtual void InitializeProperties()
        {
            var allProperties = GetType().GetRuntimeProperties().ToArray();
            var properties = allProperties
                .Where(p => typeof(IDbQueryable).IsAssignableFrom(p.PropertyType))
                .ToList();
            foreach (var property in properties)
            {
                IDbQueryable asDbSetByType = null;
                var existingValue = this.GetPropertyValueByName(property.Name);
                Type entityType = null;
                var dbSetName = nameof(DbSet<object, object>);
                var propertyType = property.PropertyType;
                if (propertyType.Name == dbSetName)
                {
                    entityType = propertyType.GenericTypeArguments[0];
                    if (existingValue != null)
                    {
                        asDbSetByType = AsDbSetByType(entityType);
                    }
                }
                else
                {
                    var baseType = propertyType.BaseType;
                    while (baseType.SimpleName() != dbSetName)
                    {
                        baseType = baseType.BaseType;
                    }

                    entityType = baseType.GenericTypeArguments[0];
                    if (existingValue != null)
                    {
                        asDbSetByType = AsCustomDbSetByType(entityType, propertyType);
                    }
                }

                if (asDbSetByType != null)
                {
                    property.SetValue(this, asDbSetByType);
                }
                var configuration = EntityConfigurationContext.GetEntityByType(entityType);
                if (configuration != null && !configuration.SetNameSet)
                {
                    configuration.SetName = property.Name;
                }
            }
        }

        public IDataStore DataStore
        {
            get => _dataStore;
            set
            {
                _dataStore = value;
                if (value != null)
                {
                    value.EntityConfigurationBuilder = EntityConfigurationContext;
                }
            }
        }

        public async Task<SaveChangesResult> SaveOfflineChangesAsync()
        {
            if (OfflineDataTracker == null)
            {
                return new SaveChangesResult(SaveChangeKind.NoAction);
            }

            var changes = OfflineDataTracker.GetChanges(this);
            if (changes == null || changes.Length == 0)
            {
                return new SaveChangesResult(SaveChangeKind.NoAction);
            }

            return await CommitQueueInternalAsync(changes.AllChanges, true);
        }

        public bool HasOfflineChanges()
        {
            return OfflineDataTracker?.HasChanges() == true;
        }

        public bool TrackEntities { get; set; } = true;
        public bool AllowOffline { get; set; } = true;
        public string SynchronicityKey { get; set; } = Guid.NewGuid().ToString();
        public string OfflineSynchronicityKey { get; set; } = "offline";
        public EvaluateContext EvaluateContext { get; set; }
        public EntityConfigurationBuilder EntityConfigurationContext { get; set; }

        public void RegisterConfiguration<T>(T configuration)
            where T : class
        {
            _configurations.Add(ConfigurationNameByType(configuration.GetType()), configuration);
        }

        public IEntityStateBase GetEntityState(object entity, Type entityType = null)
        {
            entityType = entityType ?? entity.GetType();
            return TemporalDataTracker.TrackingSetByType(entityType).FindMatchingEntityState(entity);
        }

        public T GetConfiguration<T>() where T : class
        {
            if (!_configurations.ContainsKey(ConfigurationName<T>()))
            {
                //throw new Exception($"No configuration found for \"{ConfigurationName<T>()}\"");
                return null;
            }
            return _configurations[ConfigurationName<T>()] as T;
        }

        public DbSet<T, TKey> GetDbSet<T, TKey>()
            where T : class
        {
            return (DbSet<T, TKey>)GetDbSetByEntityType(typeof(T));
        }

        public DbQueryable<T> GetDbQueryable<T>()
            where T : class
        {
            return (DbQueryable<T>)GetDbSetByEntityType(typeof(T));
        }

        public TDbSet GetDbSetBySet<TDbSet>()
        where TDbSet : IDbQueryable
        {
            return (TDbSet)GetDbSetBySetType(typeof(TDbSet));
        }

        public string GetDbSetPropertyNameBySet<TDbSet>() where TDbSet : IDbQueryable
        {
            return GetDbSetPropertyNameBySetType(typeof(TDbSet));
        }

        public string GetDbSetPropertyNameByEntity<T>() where T : class
        {
            return GetDbSetPropertyNameByEntityType(typeof(T));
        }

        public string GetDbSetPropertyNameBySetType(Type setType)
        {
            var set = GetDbSetBySetType(setType);
            if (set != null)
            {
                return _dbSetsBySetType[setType].Name;
            }
            return null;
        }

        public string GetDbSetPropertyNameByEntityType(Type entityType)
        {
            var set = GetDbSetByEntityType(entityType);
            if (set != null)
            {
                return _dbSetsByEntityType[entityType].Name;
            }
            return null;
        }

        private readonly Dictionary<Type, PropertyInfo> _dbSetsByEntityType = new Dictionary<Type, PropertyInfo>();
        public IDbQueryable GetDbSetByEntityType(Type entityType)
        {
            if (!_dbSetsByEntityType.ContainsKey(entityType))
            {
                foreach (var property in GetType().GetRuntimeProperties())
                {
                    if (!property.GetMethod.IsPublic)
                    {
                        continue;
                    }
                    var value = this.GetPropertyValueByName(property.Name);
                    if (value is IDbQueryable)
                    {
                        var dbSet = value as IDbQueryable;
                        if (dbSet.ItemType == entityType)
                        {
                            _dbSetsByEntityType.Add(entityType, property);
                            return dbSet;
                        }
                    }
                }
            }
            else
            {
                return this.GetPropertyValueByName(_dbSetsByEntityType[entityType].Name) as IDbQueryable;
            }
            return null;
        }

        public IDbQueryable GetDbSetBySetName(string name)
        {
            name = name.ToLower();
            foreach (var property in GetType().GetRuntimeProperties())
            {
                if (property.Name.ToLower() == name)
                {
                    var value = this.GetPropertyValueByName(property.Name);
                    if (value is IDbQueryable)
                    {
                        return value as IDbQueryable;
                    }
                }
            }

            return null;
        }

        public async Task<Dictionary<IProperty, IList>> LoadAllRelationshipsAsync(object entity, LoadRelationshipMode mode = LoadRelationshipMode.Both, Type entityType = null)
        {
            return await GetDbSetByEntityType(entityType ?? entity.GetType())
                .LoadAllRelationshipsAsync(entity, mode);
        }

        public async Task<Dictionary<IProperty, IList>> LoadRelationshipsAsync(object entity, IEnumerable<EntityRelationship> relationships, Type entityType = null)
        {
            return await GetDbSetByEntityType(entityType ?? entity.GetType())
                .LoadRelationshipsAsync(entity, relationships);
        }

        public async Task<IList> LoadRelationshipPropertyAsync(object entity, IProperty property, Func<IDbQueryable, IDbQueryable> queryFilter = null)
        {
            return await GetDbSetByEntityType(property.TypeDefinition.DeclaringType)
                .LoadRelationshipPropertyAsync(entity, property, queryFilter);
        }

        public async Task<IList> LoadRelationshipAsync<T>(T entity, Expression<Func<T, object>> relationship, Func<IDbQueryable, IDbQueryable> queryFilter = null)
        {
            return await LoadRelationshipPropertyAsync(entity,
                EntityConfigurationContext.GetEntityByType(typeof(T) ?? entity.GetType()).FindNestedPropertyByLambdaExpression(relationship),
                queryFilter);
        }

        private readonly Dictionary<Type, PropertyInfo> _dbSetsBySetType = new Dictionary<Type, PropertyInfo>();
        public IDbQueryable GetDbSetBySetType(Type setType)
        {
            if (!_dbSetsBySetType.ContainsKey(setType))
            {
                foreach (var property in GetType().GetRuntimeProperties())
                {
                    var value = this.GetPropertyValueByName(property.Name);
                    if (value != null && value is IDbQueryable && setType.IsAssignableFrom(value.GetType()))
                    {
                        _dbSetsBySetType.Add(setType, property);
                        return value as IDbQueryable;
                    }
                }
            }
            else
            {
                return this.GetPropertyValueByName(_dbSetsBySetType[setType].Name) as IDbQueryable;
            }
            return null;
        }

        public IDbQueryable AsDbSetByType(Type entityType)
        {
            Initialize();
            var entityKey = EntityConfigurationContext.GetEntityByType(entityType).Key;
            var keyType = entityKey.KeyType;
            return (IDbQueryable)GetType().GetMethod(nameof(AsDbSet))
                .InvokeGeneric(
                    this,
                    new object[] { },
                    entityType,
                    keyType
                    );
        }

        public IDbQueryable AsCustomDbSetByType(Type entityType, Type setType)
        {
            Initialize();
            var entityKey = EntityConfigurationContext.GetEntityByType(entityType).Key;
            var keyType = entityKey.KeyType;
            return (IDbQueryable)GetType().GetMethod(nameof(AsCustomDbSet))
                .InvokeGeneric(
                    this,
                    new object[] { },
                    entityType,
                    keyType,
                    setType
                );
        }

        private bool _initialized;

        public DbSet<T, TKey> AsDbSet<T, TKey>() where T : class
        {
            Initialize();
            return new DbSet<T, TKey>(
                EntityConfigurationContext,
                () => DataStore,
                EvaluateContext,
                this);
        }

        private Func<IDataStore> _dataStoreGetter = null;
        public TDbSet AsCustomDbSet<T, TKey, TDbSet>()
            where T : class
        {
            Initialize();
            if (_dataStoreGetter == null)
            {
                Func<IDataStore> dataStoreGetter = () => DataStore;
                _dataStoreGetter = dataStoreGetter;
            }
            return (TDbSet)Activator.CreateInstance(
                typeof(TDbSet),
                new object[]
                {
                    EntityConfigurationContext,
                    _dataStoreGetter,
                    EvaluateContext,
                    this
#if TypeScript
                    ,
                    typeof(T),
                    typeof(TKey)
#endif
                }
            );
        }

        private static string ConfigurationName<T>() where T : class
        {
            return ConfigurationNameByType(typeof(T));
        }

        private static string ConfigurationNameByType(Type type)
        {
            return type.Name;
        }

        public virtual void Configure(EntityConfigurationBuilder builder)
        {
        }

        public EntityConfiguration<T> GetConfig<T>(Type entityType) where T : class
        {
            return _configurations[entityType.Name] as EntityConfiguration<T>;
        }

        private UsersManager _usersManager;
        public UsersManager UsersManager
        {
            get
            {
                if (EntityConfigurationContext.UsersDefinition != null)
                {
                    _usersManager = new UsersManager(this);
                }

                return _usersManager;
            }
        }

        private CustomReportsManager _customReportsManager;
        public CustomReportsManager CustomReportsManager
        {
            get
            {
                if (EntityConfigurationContext.CustomReportsDefinition != null)
                {
                    _customReportsManager = new CustomReportsManager(this);
                }

                return _customReportsManager;
            }
        }

        private UserSettingsManager _userSettingsManager;
        public UserSettingsManager UserSettingsManager
        {
            get
            {
                if (EntityConfigurationContext.UserSettingsDefinition != null)
                {
                    _userSettingsManager = new UserSettingsManager(this);
                }

                return _userSettingsManager;
            }
        }

        public virtual INestedSetsProviderBase NestedSetsProviderForType(Type type)
        {
            return DataStore.NestedSetsProviderForType(type);
        }

        public virtual INestedSetsProvider<T> NestedSetsProviderFor<T>()
        {
            return (INestedSetsProvider<T>)NestedSetsProviderForType(typeof(T));
        }

        public void AbandonChanges()
        {
            for (var i = 0; i < TemporalDataTracker.Sets.Count; i++)
            {
                var set = TemporalDataTracker.Sets[i];
                set.AbandonChanges();
            }
        }

        public void AbandonChangesForEntity(object entity)
        {
            var set = TemporalDataTracker.TrackingSetByType(entity.GetType());
            set?.AbandonChangesForEntity(entity);
        }

        public void AbandonChangesForEntities(IEnumerable<object> entities)
        {
            foreach (var entity in entities)
            {
                AbandonChangesForEntity(entity);
            }
        }

        public void AbandonChangesForEntityState(IEntityStateBase state)
        {
            var set = TemporalDataTracker.TrackingSetByType(state.EntityType);
            set.AbandonChangesForEntityState(state);
        }

        public void AbandonChangesForEntityStates(IEnumerable<IEntityStateBase> states)
        {
            foreach (var state in states)
            {
                AbandonChangesForEntityState(state);
            }
        }

        public async Task<SaveChangesResult> SaveChangesAsync(IEnumerable<object> entities = null, IEnumerable<IProperty> properties = null)
        {
            return await ApplySaveChangesAsync(new SaveChangesOperation(this, entities?.ToArray(), properties?.ToArray()));
        }

        public virtual Task<SaveChangesResult> ApplySaveChangesAsync(
            SaveChangesOperation operation)
        {
            // Sets could be added to whilst detecting changes
            // so get a copy now
            //var observable = this.Observable<SaveChangesResult>();
            return CommitQueueAsync(GetChanges(operation.Entities, operation.Properties).AllChanges);
        }

        public virtual async Task<SaveChangesResult> CommitQueueAsync(IEnumerable<IQueuedOperation> queue)
        {
            return await CommitQueueInternalAsync(queue, false);
        }

        private async Task<SaveChangesResult> CommitQueueInternalAsync(IEnumerable<IQueuedOperation> queue, bool forceOnline)
        {
            var offlineChangesBefore = OfflineDataTracker?.SerializeToJson();
            var saveChangesResult = new SaveChangesResult(SaveChangeKind.NoAction);
            var hasAny = false;
            var queuedOperations = queue as IQueuedOperation[] ?? queue.ToArray();
            for (var i = 0; i < queuedOperations.Length; i++)
            {
                var queuedOperation = queuedOperations[i];
                hasAny = true;
                var task = GetType()
                    .GetMethod(nameof(PerformAsync))
                    .InvokeGeneric(this, new object[]
                        {
                            queuedOperation, saveChangesResult, forceOnline
                        },
                        queuedOperation.Operation.EntityType) as Task;
                await task;
            }

            if (hasAny)
            {
                saveChangesResult.Success = queuedOperations.All(_ => _.Result.Success);
            }

            if (queuedOperations.Any(_ => _.Result.Success))
            {
                var offlineChangesAfter = OfflineDataTracker?.SerializeToJson();
                if (offlineChangesBefore != offlineChangesAfter)
                {
                    EmitOfflineChangeStateEvent();
                }
            }

            return saveChangesResult;
        }

        protected virtual void EmitOfflineChangeStateEvent()
        {
            OfflineStateChanged.Emit(() => new OfflineChangeStateChangedEvent(this, OfflineDataTracker, HasOfflineChanges()));
        }

        public virtual Task PerformAsync<TEntity>(
            IQueuedOperation operation,
            SaveChangesResult saveChangesResult,
            bool forceOnline) where TEntity : class
        {
            return new SaveChangesApplicator(this)
                .PerformAsync<TEntity>(operation, saveChangesResult, forceOnline);
        }

        public bool IsIdMatch(object left, object right, Type type)
        {
            return EntityConfigurationContext.GetEntityByType(type)
                .KeysMatch(left, right);
        }

        public bool EntityPropertiesMatch(object entity, CompositeKey compositeKey)
        {
            return compositeKey.MatchesEntity(entity);
        }

        public bool EntityHasKey(object left, Type type, CompositeKey key)
        {
            return EntityConfigurationContext.GetEntityByType(type).EntityHasKey(left, key);
        }

        public IEntityStateBase DeleteEntity(object entity
#if TypeScript
            , Type entityType = null
#endif
        )
        {
#if !TypeScript
            var entityType = entity.GetType();
#else
            entityType = entityType ?? entity.GetType();
#endif
            return (IEntityStateBase)DeleteInternalMethod.InvokeGeneric(this, new[] { entity }, entity.GetType());
        }

        public IEntityStateBase CascadeDeleteEntity(object entity,
            object cascadedFromEntity,
            IRelationship cascadedFromRelationship
#if TypeScript
            , Type entityType
            , Type cascadedFromEntityType
#endif
        )
        {
#if !TypeScript
            var entityType = entity.GetType();
            var cascadedFromEntityType = cascadedFromEntity.GetType();
#endif
            var entityState = TemporalDataTracker.TrackingSetByType(entityType)
                .FindMatchingEntityState(entity);
            entityState.MarkForCascadeDeletion(cascadedFromEntity, cascadedFromRelationship);
            return DeleteEntity(entity
#if TypeScript
                , entityType
#endif
                );
        }

        public IEntityStateBase AddEntity(object entity
#if TypeScript
            , Type entityType = null
#endif
        )
        {
#if !TypeScript
            var
#endif
            entityType = entity.GetType();
            if (entityType == null || entityType == typeof(object))
            {
                entityType = entity.GetType();
            }
            return (IEntityStateBase)AddInternalMethod.InvokeGeneric(this, new[] { entity }, entityType);
        }

        public async Task<T> RefreshEntity<T>(T entity
#if TypeScript
            , Type entityType = null
#endif
            )
            where T : class
        {
#if !TypeScript
            var entityType = typeof(T);
#endif
            if (entityType == typeof(object))
            {
                entityType = entity.GetType();
            }
            var isEntityNew = this.IsEntityNew(entity
#if TypeScript
                , entityType
#endif
            );
            if (isEntityNew == null || isEntityNew == true)
            {
                return null;
            }
            return await GetEntityFromEntityAsync(entity
#if TypeScript
, entityType
#endif
            );
        }

        public async Task<T> GetEntityFromEntityAsync<T>(T entity
#if TypeScript
            , Type entityType = null
#endif
            ) where T : class
        {
#if !TypeScript
            var entityType = typeof(T);
#endif
            if (entityType == null || entityType == typeof(object))
            {
                entityType = entity.GetType();
            }
            var identityWhereOperation =
                this.ResolveWithKeyOperationFromEntity(entity
#if TypeScript
                , entityType
#endif
                );
            var queryable = AsDbSetByType(entityType);
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
            return (T)await queryable.GetWithKeyAsync(identityWhereOperation.Key);
        }

        public bool IsTracked(object entity)
        {
            return TemporalDataTracker.IsTracked(entity);
        }

        private IqlServiceProvider _serviceProvider;
        public IqlServiceProvider ServiceProvider
        {
            get
            {
                _serviceProvider = _serviceProvider ?? new IqlServiceProvider();
                if (EntityConfigurationContext != null)
                {
                    _serviceProvider.BaseProvider = EntityConfigurationContext.ServiceProvider;
                }

                return _serviceProvider;
            }
        }

        private class DefaultValuePlaceholder { }

        private static readonly DefaultValuePlaceholder DefaultValuePlaceholderInstance = new DefaultValuePlaceholder();

        private MethodInfo ValidateEntityInternalAsyncMethod
        {
            get => _validateEntityInternalAsyncMethod = _validateEntityInternalAsyncMethod ??
                                                        typeof(DataContext).GetMethod(nameof(ValidateEntityInternalAsync),
                                                            BindingFlags.Instance | BindingFlags.NonPublic);
        }

        private MethodInfo _validateEntityPropertyInternalAsyncMethod;
        private MethodInfo _validateEntityInternalAsyncMethod;
        private IDataStore _dataStore;
        private SynchronisedDataContextConfiguration _synchronisedConfiguration;

        private MethodInfo ValidateEntityPropertyInternalAsyncMethod
        {
            get => _validateEntityPropertyInternalAsyncMethod
            = _validateEntityPropertyInternalAsyncMethod ?? typeof(DataContext).GetMethod(nameof(ValidateEntityPropertyInternalAsync),
                  BindingFlags.Instance | BindingFlags.NonPublic);
        }

        public async Task<bool> ClearOfflineStateAsync()
        {
            if (PersistState == null)
            {
                return false;
            }
            var success = true;
            if (OfflineDataStore != null)
            {
                if (!await OfflineDataStore.ClearStateAsync(PersistState))
                {
                    success = false;
                }
            }
            if (OfflineDataTracker != null)
            {
                if (!await OfflineDataTracker.ClearStateAsync(PersistState))
                {
                    success = false;
                }
            }
            return success;
        }

        public async Task<bool> SaveOfflineStateAsync()
        {
            if (PersistState == null)
            {
                return false;
            }
            var success = true;
            if (OfflineDataStore != null)
            {
                if (!await OfflineDataStore.SaveStateAsync(PersistState))
                {
                    success = false;
                }
            }
            if (OfflineDataTracker != null)
            {
                if (!await OfflineDataTracker.SaveStateAsync(PersistState))
                {
                    success = false;
                }
            }
            return success;
        }

        public async Task<bool> RestoreOfflineStateAsync()
        {
            if (PersistState == null)
            {
                return false;
            }
            var success = true;
            if (OfflineDataStore != null)
            {
                if (!await OfflineDataStore.RestoreStateAsync(PersistState))
                {
                    success = false;
                }
            }
            if (OfflineDataTracker != null)
            {
                var offlineChangesBefore = OfflineDataTracker.SerializeToJson();
                if (!await OfflineDataTracker.RestoreStateAsync(PersistState))
                {
                    success = false;
                }

                var offlineChangesAfter = OfflineDataTracker.SerializeToJson();
                if (offlineChangesBefore != offlineChangesAfter)
                {
                    EmitOfflineChangeStateEvent();
                }
            }
            return success;
        }

        public IqlDataChanges GetOfflineChanges(object[] entities = null, IProperty[] properties = null)
        {
            return OfflineDataTracker?.GetChanges(this, entities, properties) ?? new IqlDataChanges(null);
        }

        public IqlDataChanges GetChanges(object[] entities = null, IProperty[] properties = null)
        {
            return TemporalDataTracker.GetChanges(this, entities, properties);
        }

        public IQueuedAddEntityOperation[] GetAdditions(object[] entities = null)
        {
            return GetChanges(entities).Additions;
        }

        public IQueuedUpdateEntityOperation[] GetUpdates(object[] entities = null, IProperty[] properties = null)
        {
            return GetChanges(entities).Updates;
        }

        public IQueuedDeleteEntityOperation[] GetDeletions(object[] entities = null)
        {
            return GetChanges(entities).Deletions;
        }

        public List<T> AttachEntities<T>(IEnumerable<T> entities, bool? cloneIfAttachedElsewhere = null)
            where T : class
        {
            var list = new List<T>();
            foreach (var entity in entities)
            {
                list.Add(AttachEntity(entity, cloneIfAttachedElsewhere));
            }

            return list;
        }

        public T AttachEntity<T>(T entity, bool? cloneIfAttachedElsewhere = null)
            where T : class
        {
            var entityType = typeof(T) ?? entity.GetType();
            var clone = cloneIfAttachedElsewhere ?? true;
            var context = DataContext.FindDataContextForEntity(entity);
            if (context != null && context != this)
            {
                if (clone)
                {
                    entity = (T)entity.Clone(EntityConfigurationContext, entityType, RelationshipCloneMode.DoNotClone);
                }
                else
                {
                    return null;
                }
            }

            var dbSet = GetDbSetByEntityType(entityType);
            dbSet.TrackingSet.AttachEntity(entity, false);
            return entity;
        }

        public bool? IsEntityNew(object entity
#if TypeScript
            , Type entityType = null
#endif
)
        {
#if !TypeScript
            var entityType = entity.GetType();
#else
            entityType = entityType ?? entity.GetType();
#endif

            var state = GetEntityState(entity, entityType);
            return state?.IsNew;
        }

        public CompositeKey GetCompositeKey(object entity)
        {
            if (entity is CompositeKey)
            {
                return (CompositeKey)entity;
            }
            var type = entity.GetType();
            var entityConfiguration = EntityConfigurationContext.GetEntityByType(type);
            return entityConfiguration?.GetCompositeKey(entity);
        }

        public async Task<T> GetEntityAsync<T>(object entityOrKey, bool? trackResult = null)
            where T : class
        {
            var set = GetDbSetByEntityType(typeof(T) ?? entityOrKey.GetType());
            if (set == null)
            {
                return null;
            }
            if (trackResult != null)
            {
                set = set.SetTracking(trackResult.Value);
            }
            var result = await set.GetWithKeyAsync(entityOrKey);
            return (T)result;
        }

        /// <summary>
        /// Check for equivalency between an entity/composite key and another entity/composite key (can be mixed).
        /// </summary>
        /// <param name="left">Left entity or composite key.</param>
        /// <param name="right">Right entity or composite key.</param>
        /// <returns>Whether the two objects represent database equivalency.</returns>
        public bool AreEquivalent(object left, object right)
        {
            if (left == null)
            {
                return false;
            }
            if (left == right)
            {
                return true;
            }

            var leftType = left.GetType();
            var rightType = right.GetType();
            if (leftType != typeof(CompositeKey) && rightType != typeof(CompositeKey) && leftType != rightType)
            {
                return false;
            }

            var leftKey = GetCompositeKey(left);
            var rightKey = GetCompositeKey(right);
            return CompositeKey.AreEquivalent(leftKey, rightKey);
        }

        async Task<IPropertyValidationResult> IDataContext.ValidateEntityPropertyByExpressionAsync<T, TProperty>(object entity,
            Expression<Func<object, TProperty>> expression)
        {
            var entityConfiguration = EntityConfigurationContext.GetEntityByType(typeof(T) ?? entity.GetType());
            var property = entityConfiguration.FindPropertyByLambdaExpression(expression);
            var task = (Task<IPropertyValidationResult>)ValidateEntityPropertyInternalAsyncMethod.InvokeGeneric(this,
                new object[] { entity, property, false },
                entity.GetType());
            var result = await task;
            return result;
        }

        async Task<IPropertyValidationResult> IDataContext.ValidateEntityPropertyByNameAsync(object entity, string property)
        {
            var entityConfiguration = EntityConfigurationContext.GetEntityByType(entity.GetType());
            var task = (Task<IPropertyValidationResult>)ValidateEntityPropertyInternalAsyncMethod.InvokeGeneric(this,
                new object[] { entity, entityConfiguration.FindProperty(property), false },
                entity.GetType());
            var result = await task;
            return result;
        }

        async Task<IPropertyValidationResult> IDataContext.ValidateEntityPropertyAsync(object entity, IProperty property)
        {
            var task = (Task<IPropertyValidationResult>)ValidateEntityPropertyInternalAsyncMethod.InvokeGeneric(this,
                new object[] { entity, property, false },
                entity.GetType());
            var result = await task;
            return result;
        }

        public async Task<EntityValidationResult<T>> ValidateEntityAsync<T>(T entity)
            where T : class
        {
            return (EntityValidationResult<T>)(await ValidateEntityInternalAsync(entity));
        }

        async Task<IEntityValidationResult> IDataContext.ValidateEntityBaseAsync(object entity)
        {
            var task = (Task<IEntityValidationResult>)ValidateEntityInternalAsyncMethod.InvokeGeneric(this,
                new object[] { entity },
                entity.GetType());
            var result = await task;
            return result;
        }

        private async Task<IEntityValidationResult> ValidateEntityInternalAsync<T>(T entity) where T : class
        {
            var entityConfiguration = EntityConfigurationContext.GetEntityByType(typeof(T) ?? entity.GetType());
            var validationResult = new EntityValidationResult<T>(entity);

            foreach (var validation in entityConfiguration.EntityValidation.All)
            {
                if (!validation.Run(entity))
                {
                    validationResult.AddFailure(validation.Key, validation.Message);
                }
            }

            var properties = entityConfiguration.Properties.ToArray();
            for (var index = 0; index < properties.Length; index++)
            {
                var property = properties[index];
                var result = await ValidateEntityPropertyAsync(entity, property);
                if (result.HasValidationFailures())
                {
                    validationResult.AddPropertyValidationResult(result);
                }
            }

            return validationResult;
        }

        public async Task<PropertyValidationResult<T>> ValidateEntityPropertyByExpressionAsync<T, TProperty>(T entity, Expression<Func<T, TProperty>> property)
            where T : class
        {
            return (PropertyValidationResult<T>)(await ValidateEntityPropertyByExpressionInternalAsync(entity, property));
        }

        private Task<IPropertyValidationResult> ValidateEntityPropertyByExpressionInternalAsync<T, TProperty>(T entity, Expression<Func<T, TProperty>> property)
            where T : class
        {
            var entityConfiguration = EntityConfigurationContext.GetEntityByType(typeof(T) ?? entity.GetType());
            return ValidateEntityPropertyInternalAsync(entity, entityConfiguration.FindPropertyByLambdaExpression(property), false);
        }

        public async Task<PropertyValidationResult<T>> ValidateEntityPropertyByNameAsync<T>(T entity, string property)
            where T : class
        {
            return (PropertyValidationResult<T>)(await ValidateEntityPropertyByNameInternalAsync(entity, property));
        }

        private Task<IPropertyValidationResult> ValidateEntityPropertyByNameInternalAsync<T>(T entity, string property) where T : class
        {
            var entityConfiguration = EntityConfigurationContext.GetEntityByType(typeof(T) ?? entity.GetType());
            return ValidateEntityPropertyInternalAsync(entity, entityConfiguration.FindProperty(property), false);
        }

        public async Task<PropertyValidationResult<T>> ValidateEntityPropertyAsync<T>(T entity, IProperty property)
            where T : class
        {
            return (PropertyValidationResult<T>)(await ValidateEntityPropertyInternalAsync(entity, property, false));
        }

        private async Task<IPropertyValidationResult> ValidateEntityPropertyInternalAsync<T>(T entity, IProperty property, bool hasSetDefaultValue)
            where T : class
        {
            var validationResult = new PropertyValidationResult<T>(entity, property);
            var oldEntity = GetEntityState(entity)?.EntityBeforeChanges();
            if (property.HasInferredWith)
            {
                var inferredWithIgnored = false;
                if (property.HasInferredWithCondition)
                {
                    for (var i = 0; i < property.InferredValueConfigurations.Count; i++)
                    {
                        var inferredWith = property.InferredValueConfigurations[i];
                        if (inferredWith.HasCondition)
                        {
                            var result = await inferredWith.InferredWithConditionIql.EvaluateIqlAsync(
                                new InferredValueContext<T>((T)oldEntity, entity),
                                this,
                                typeof(T));
                            if (!Equals(result.Result, true))
                            {
                                inferredWithIgnored = true;
                                break;
                            }
                        }
                    }

                }
                if (!inferredWithIgnored && EntityConfigurationContext.ValidateInferredWithClientSide == false)
                {
                    return validationResult;
                }
            }

            foreach (var validation in property.ValidationRules.All)
            {
                if (!validation.Run(entity))
                {
                    validationResult.AddFailure(validation.Key, validation.Message);
                }
            }

            if (!property.Kind.HasFlag(PropertyKind.Count) && (!property.Kind.HasFlag(PropertyKind.Key) || property.Kind.HasFlag(PropertyKind.RelationshipKey)))
            {
                var propertyValue = property.GetValue(entity);
                if (!validationResult.HasValidationFailures() &&
                PropertyValueIsIllegallyEmpty(property, entity, propertyValue)
            )
                {
                    if (!hasSetDefaultValue)
                    {
                        // Mimic default values for 
                        object newValue = DefaultValuePlaceholderInstance;
                        if (property.TypeDefinition.ConvertedFromType == KnownPrimitiveTypes.Guid ||
                            property.TypeDefinition.Kind == IqlType.Date ||
                            property.TypeDefinition.Kind == IqlType.Enum)
                        {
                            newValue = property.TypeDefinition.DefaultValue();
                        }
                        if (!Equals(newValue, DefaultValuePlaceholderInstance))
                        {
                            property.SetValue(entity, newValue);
                            return await ValidateEntityPropertyInternalAsync(entity, property, true);
                        }
                    }
                    validationResult.AddFailure(
                        ValidationDefaults.DefaultRequiredAutoValidationFailureKey,
                        ValidationDefaults.DefaultRequiredAutoValidationFailureMessage);
                }
            }
            return validationResult;
        }

        private static bool PropertyValueIsIllegallyEmpty(IProperty property, object entity, object propertyValue)
        {
            if (property.IsReadOnly)
            {
                return false;
            }

            if (property.TypeDefinition.Nullable && property.Nullable != false)
            {
                return false;
            }

            if (property.Kind.HasFlag(PropertyKind.Relationship) &&
                propertyValue == null)
            {
                if (!property.Relationship.ThisIsTarget)
                {
                    var properties = property.Relationship.ThisEnd.Constraints;
                    for (var i = 0; i < properties.Length; i++)
                    {
                        var constraint = properties[i];
                        var constraintValue = constraint.GetValue(entity);
                        if (Equals(null, constraintValue) ||
                            Equals(constraint.TypeDefinition.DefaultValue(), constraintValue))
                        {
                            return true;
                        }
                    }
                }
            }
            else if (Equals(null, propertyValue) && property.TypeDefinition.Nullable == false)
            {
                object defaultValue;
                switch (property.TypeDefinition.Kind)
                {
                    case IqlType.Integer:
                    case IqlType.Decimal:
                    case IqlType.Enum:
                    case IqlType.Boolean:
                    case IqlType.Date:
                        defaultValue = property.TypeDefinition.DefaultValue();
                        break;
                    default:
                        return true;
                }
                property.SetValue(entity, defaultValue);
                propertyValue = defaultValue;
            }

            if (property.TypeDefinition.Kind == IqlType.Enum)
            {
                var stringValue = Enum.ToObject(property.TypeDefinition.Type, propertyValue).ToString();
                if (string.IsNullOrWhiteSpace(stringValue) || Regex.IsMatch(stringValue, @"^\d+$"))
                {
                    return true;
                }
            }

            if (property.TypeDefinition.Kind == IqlType.Date)
            {
                if (propertyValue.IsDefaultValue(property.TypeDefinition))
                {
                    return true;
                }
            }

            if (property.TypeDefinition.Type == typeof(string) && Equals(propertyValue, ""))
            {
                return true;
            }

            return false;
        }


        public virtual async Task<GetDataResult<TEntity>> GetAsync<TEntity>(GetDataOperation<TEntity> operation)
            where TEntity : class
        {
            if (!operation.Queryable.HasDefaults)
            {
                var getConfiguration = GetConfiguration<EntityDefaultQueryConfiguration>();
                if (getConfiguration == null)
                {
                    getConfiguration = new EntityDefaultQueryConfiguration();
                    RegisterConfiguration(getConfiguration);
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

            var listenConfiguration = GetConfiguration<DataContextEventsConfiguration>();
            if (listenConfiguration != null && listenConfiguration.GetBeginListeners != null)
            {
                foreach (var listener in listenConfiguration.GetBeginListeners)
                {
                    listener(operation);
                }
            }

            var response = new FlattenedGetDataResult<TEntity>(null, operation, true);
            response.Queryable = operation.Queryable;
            // perform get and set up tracking on the objects
            var queuedGetDataOperation = new QueuedGetDataOperation<TEntity>(
                operation,
                response);

            await RunGetAsync(queuedGetDataOperation, response);

            //if (OfflineDataTracker != null)
            //{
            //    TrackGetDataResult(OfflineDataTracker, response, false);
            //    OfflineDataTracker.Reset(response.Data);
            //}
            //OfflineDataStore?.SynchroniseData(response.Data);
            //// Update "offline" repository with these results

            // Clone the queryable so any changes made in the application code
            // don't trickle down to our result
            response.Queryable = (global::Iql.Queryable.IQueryable<TEntity>)operation.Queryable.Copy();

            // In here, if we're offline, we don't want to update other trackers (I think)
            var dbList = await TrackGetDataResultAsync(response);

            var getDataResult =
                new GetDataResult<TEntity>(response.IsOffline, dbList, operation, response.IsSuccessful())
                {
                    TotalCount = response.TotalCount
                };

            ApplyPaging(dbList, response);

            return getDataResult;
        }

        private async Task RunGetAsync<TEntity>(QueuedGetDataOperation<TEntity> queuedGetDataOperation, FlattenedGetDataResult<TEntity> response)
            where TEntity : class
        {
            if (OfflineDataTracker?.HasChanges() != true)
            {
                await DataStore.PerformGetAsync(queuedGetDataOperation);

                if (response.RequestStatus == RequestStatus.Offline)
                {
                    // Magic happens here...
                    if (SupportsOffline && response.Queryable.AllowOffline != false)
                    {
                        response.IsOffline = true;
                        await OfflineDataStore.PerformGetAsync(queuedGetDataOperation);
                    }
                }
            }
            else if (SupportsOffline && response.Queryable.AllowOffline != false)
            {
                response.IsOffline = true;
                await OfflineDataStore.PerformGetAsync(queuedGetDataOperation);
            }
        }

        public IPersistState PersistState { get; set; }

        public bool RefreshDisabled { get; set; }

        public async Task<DbList<TEntity>> TrackGetDataResultAsync<TEntity>(
            FlattenedGetDataResult<TEntity> response)
            where TEntity : class
        {
            response.Root = response.Root ?? response.ResolveRoot();
#if TypeScript
            response.Data = EntityConfigurationContext.EnsureTypedResult(response.Data);
            response.Root = EntityConfigurationContext.EnsureTypedList<TEntity>(response.Root);
#endif
            var dbList = new DbList<TEntity>();
            dbList.SourceQueryable = (DbQueryable<TEntity>)response.Queryable;
            // Flatten before we merge because the merge will update the result data set with
            // tracked data
            dbList.Success = response.IsSuccessful();
            if (dbList.Success)
            {
                var shouldTrackResults = TrackEntities;
                if (dbList.SourceQueryable != null && dbList.SourceQueryable.TrackEntities.HasValue)
                {
                    shouldTrackResults = dbList.SourceQueryable.TrackEntities.Value;
                }

                var localDataTracker = TemporalDataTracker;
                if (!shouldTrackResults)
                {
                    localDataTracker = new DataTracker(DataTrackerKind.Temporal, EntityConfigurationContext, "No Tracking", false, true);
                }

                void TrackResponse(DataTracker dataTracker)
                {
                    if (
                        dataTracker == localDataTracker ||
                        dataTracker.EntityConfigurationBuilder == EntityConfigurationContext
                        )
                    {
                        Dictionary<object, object> dealtWith = new Dictionary<object, object>();
                        if (response.Root != null)
                        {
                            for (var i = 0; i < response.Root.Count; i++)
                            {
                                var item = response.Root[i];
                                var trackingSet = dataTracker.TrackingSetByType(typeof(TEntity));
                                var state = trackingSet.Synchronise(item, false, true, null);
                                if (dataTracker == localDataTracker)
                                {
                                    dbList.Add((TEntity)state.Entity);
                                }
                                dealtWith.Add(item, item);
                            }
                        }

                        foreach (var pair in response.Data)
                        {
                            for (var i = 0; i < pair.Value.Count; i++)
                            {
                                var item = pair.Value[i];
                                if (!dealtWith.ContainsKey(item))
                                {
                                    dataTracker.TrackingSetByType(pair.Key)
                                        .Synchronise(item, false, true, null);
                                }
                            }
                        }
                    }
                }

                if (SupportsOffline && response.Queryable.AllowOffline != false)
                {
                    OfflineDataStore?.SynchroniseData(response.Data);
                    if (OfflineDataStore != null && PersistState != null)
                    {
                        await OfflineDataStore?.SaveStateAsync(PersistState);
                    }
                }

                if (shouldTrackResults && !response.IsOffline)
                {
                    this.ForMatchingDataContexts(dataContext =>
                    {
                        TrackResponse(dataContext.TemporalDataTracker);
                        if (dataContext.OfflineDataTracker != null)
                        {
                            TrackResponse(dataContext.OfflineDataTracker);
                        }
                    });
                }
                else
                {
                    TrackResponse(localDataTracker);
                    if (OfflineDataTracker != null && !response.IsOffline)
                    {
                        TrackResponse(OfflineDataTracker);
                    }
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

        private IEntityState<T> DeleteInternal<T>(T entity)
            where T : class
        {
            return (IEntityState<T>)TemporalDataTracker.DeleteEntity(entity);
        }

        public static bool IsEntityTracked(object entity)
        {
            return FindDataContextForEntity(entity) != null;
        }

        public static IEntityStateBase FindEntityState(object entity)
        {
            var dataContext = FindDataContextForEntity(entity);
            return dataContext?.GetEntityState(entity);
        }
    }
}