using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Data.Crud;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.Crud.Operations.Results;
using Iql.Data.DataStores;
using Iql.Data.DataStores.InMemory;
using Iql.Data.DataStores.NestedSets;
using Iql.Data.Evaluation;
using Iql.Data.Extensions;
using Iql.Data.Lists;
using Iql.Data.Operations;
using Iql.Data.Paging;
using Iql.Data.Queryable;
using Iql.Data.SpecialTypes;
using Iql.Data.Tracking;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Entities.Events;
using Iql.Entities.Extensions;
using Iql.Entities.InferredValues;
using Iql.Entities.Relationships;
using Iql.Entities.Services;
using Iql.Entities.Validation;
using Iql.Entities.Validation.Validation;
using Iql.Events;
using Iql.Extensions;
using Iql.Parsing;
using Iql.Parsing.Types;
using Iql.Queryable;
using Iql.Queryable.Operations;

namespace Iql.Data.Context
{
    public class DataContext : IDataContext
    {
        private class SynchronisedDataContextConfiguration
        {
            private DataTracker _offlineDataTracker;
            public DataTracker OfflineDataTracker => _offlineDataTracker;
            private EventEmitter<OfflineChangeStateChangedEvent> _offlineStateChanged;
            public EventEmitter<OfflineChangeStateChangedEvent> OfflineStateChanged => _offlineStateChanged = _offlineStateChanged ?? new EventEmitter<OfflineChangeStateChangedEvent>();

            public IEntityConfigurationBuilder EntityConfigurationBuilder { get; set; }
            public string SynchronicityKey { get; set; }

            public SynchronisedDataContextConfiguration(IEntityConfigurationBuilder entityConfigurationBuilder, string synchronicityKey)
            {
                EntityConfigurationBuilder = entityConfigurationBuilder;
                SynchronicityKey = synchronicityKey;
                _offlineDataTracker = new DataTracker(DataTrackerKind.Offline, entityConfigurationBuilder, "Offline", true);
            }
        }
        private bool _offlineDataStoreDelayedInitialized;
        private IOfflineDataStore _offlineDataStoreDelayed;

        private IOfflineDataStore _offlineDataStore { get { if (!_offlineDataStoreDelayedInitialized) { _offlineDataStoreDelayedInitialized = true; _offlineDataStoreDelayed = new InMemoryDataStore("OfflineData", AutoIntegerIdStrategy.Negative); } return _offlineDataStoreDelayed; } set { _offlineDataStoreDelayedInitialized = true; _offlineDataStoreDelayed = value; } }

        public DataContextEvents Events => _events = _events ?? new DataContextEvents();

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
        private bool _configurationsDelayedInitialized;
        private Dictionary<string, object> _configurationsDelayed;


        //#if !TypeScript
        //        public IEntityStateBase Delete(object entity)
        //        {
        //            return (IEntityStateBase)DeleteInternalMethod.InvokeGeneric(this, new[] { entity }, entity.GetType());
        //        }
        //#endif

        private Dictionary<string, object> _configurations { get { if (!_configurationsDelayedInitialized) { _configurationsDelayedInitialized = true; _configurationsDelayed = new Dictionary<string, object>(); } return _configurationsDelayed; } set { _configurationsDelayedInitialized = true; _configurationsDelayed = value; } }
        private static bool EntityConfigurationsBuildersDelayedInitialized;
        private static Dictionary<Type, EntityConfigurationBuilder> EntityConfigurationsBuildersDelayed;
        private static Dictionary<Type, EntityConfigurationBuilder> EntityConfigurationsBuilders { get { if (!EntityConfigurationsBuildersDelayedInitialized) { EntityConfigurationsBuildersDelayedInitialized = true; EntityConfigurationsBuildersDelayed = new Dictionary<Type, EntityConfigurationBuilder>(); } return EntityConfigurationsBuildersDelayed; } set { EntityConfigurationsBuildersDelayedInitialized = true; EntityConfigurationsBuildersDelayed = value; } }

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
                if (tracker.DataTracker is DataContextDataTracker noTrackingDataTracker)
                {
                    return noTrackingDataTracker.DataContext;
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

        public static IEntityConfiguration FindConfigurationForEntity(object entity)
        {
            var tracker = FindTrackingForEntity(entity);
            if (tracker != null)
            {
                return tracker.EntityConfiguration;
            }
            return null;
        }

        public static IEntityStateBase FindEntity(object entity)
        {
            var tracker = FindTrackingForEntity(entity);
            return tracker?.FindMatchingEntityState(entity);
        }

        private DataTracker _temporalDataTracker;
        public DataTracker TemporalDataTracker
        {
            get
            {
                if (_temporalDataTracker == null)
                {
                    _temporalDataTracker = new DataContextDataTracker(this, DataTrackerKind.Temporal, EntityConfigurationContext, "Temporal");
                    _temporalDataTracker.RelationshipObserver.UntrackedEntityAdded.Subscribe(_ => { AddEntity(_.Entity); });
                    //TemporalDataTracker.DataContext = this;
                }

                return _temporalDataTracker;
            }
        }
        private static bool SynchronisedDataContextConfigurationsDelayedInitialized;
        private static Dictionary<IEntityConfigurationBuilder, Dictionary<string, SynchronisedDataContextConfiguration>> SynchronisedDataContextConfigurationsDelayed;

        private static Dictionary<IEntityConfigurationBuilder, Dictionary<string, SynchronisedDataContextConfiguration>> SynchronisedDataContextConfigurations
        {
            get
            {
                if (!SynchronisedDataContextConfigurationsDelayedInitialized)
                {
                    SynchronisedDataContextConfigurationsDelayedInitialized = true; SynchronisedDataContextConfigurationsDelayed = new Dictionary<IEntityConfigurationBuilder, Dictionary<string, SynchronisedDataContextConfiguration>
>();
                }
                return SynchronisedDataContextConfigurationsDelayed;
            }
            set { SynchronisedDataContextConfigurationsDelayedInitialized = true; SynchronisedDataContextConfigurationsDelayed = value; }
        }
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
        private static List<IDataContext> _allDataContexts;
        internal static List<IDataContext> AllDataContexts => _allDataContexts = _allDataContexts ?? new List<IDataContext>();
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
            var saveChangesOperation = new SaveChangesOperation(this);

            if (OfflineDataTracker == null)
            {
                return new SaveChangesResult(saveChangesOperation, SaveChangeKind.NoAction);
            }

            var changes = OfflineDataTracker.GetChanges(saveChangesOperation);
            if (changes == null || changes.Count == 0)
            {
                return new SaveChangesResult(saveChangesOperation, SaveChangeKind.NoAction);
            }

            return await CommitQueueAsync(saveChangesOperation, changes.AllChanges, true);
        }

        public bool HasOfflineChanges => OfflineDataTracker != null && OfflineDataTracker.HasChanges;

        public EventEmitter<ValueChangedEvent<bool, DataTrackerState>> HasOfflineChangesChanged
        {
            get
            {
                if (_hasOfflineChangesChanged == null)
                {
                    if (SynchronisedConfiguration != null && SynchronisedConfiguration.OfflineDataTracker != null)
                    {
                        _hasOfflineChangesChanged = SynchronisedConfiguration.OfflineDataTracker.HasChangesChanged;
                    }
                    else
                    {
                        _hasOfflineChangesChanged = new EventEmitter<ValueChangedEvent<bool, DataTrackerState>>();
                    }
                }
                return _hasOfflineChangesChanged;
            }
        }

        public bool TrackEntities { get; set; } = true;
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
            var key = entity as CompositeKey;
            if (key != null)
            {
                entityType = EntityConfigurationContext.GetEntityByTypeName(key.TypeName)?.Type;
            }
            else
            {
                entityType = entityType ?? entity.GetType();
            }
            IEntityConfiguration entityConfiguration = null;
            if (entityType != null)
            {
                entityConfiguration = EntityConfigurationContext.GetEntityByType(entityType);
            }

            if (entityConfiguration == null)
            {
                return null;
            }
            return TemporalDataTracker.TrackingSetByType(entityType).FindMatchingEntityState(entity) ?? EntityStates.Find(entity);
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
                return _dbSetsByEntityType[set.ItemType].Name;
            }
            return null;
        }
        private bool _dbSetsByEntityTypeDelayedInitialized;
        private Dictionary<Type, PropertyInfo> _dbSetsByEntityTypeDelayed;

        private Dictionary<Type, PropertyInfo> _dbSetsByEntityType { get { if (!_dbSetsByEntityTypeDelayedInitialized) { _dbSetsByEntityTypeDelayedInitialized = true; _dbSetsByEntityTypeDelayed = new Dictionary<Type, PropertyInfo>(); } return _dbSetsByEntityTypeDelayed; } set { _dbSetsByEntityTypeDelayedInitialized = true; _dbSetsByEntityTypeDelayed = value; } }

        public IDbQueryable GetDbSetByEntityType(Type entityType)
        {
            var map = EntityConfigurationContext.GetEntityByType(entityType).SpecialTypeDefinition;
            if (map != null && entityType == map.InternalType)
            {
                return GetDbSetByEntityType(map.EntityConfiguration.Type);
            }
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

        public IDbQueryable RelationshipPropertyQuery(object entity, IPropertyGroup property)
        {
            IRelationshipDetail relationship;
            if (property is IProperty p)
            {
                relationship = p.Relationship.ThisEnd;
            }
            else
            {
                relationship = property as IRelationshipDetail;
            }

            if (relationship == null)
            {
                return null;
            }
            var thisEndConstraints = relationship.Constraints.ToArray();
            var otherEndConstraints = relationship.OtherSide.Constraints.ToArray();
            var compositeKey = entity as CompositeKey;
            if (compositeKey == null)
            {
                compositeKey = new CompositeKey(relationship.EntityConfiguration.TypeName, thisEndConstraints.Length);
                for (var i = 0; i < thisEndConstraints.Length; i++)
                {
                    compositeKey.Keys[i] = new KeyValue(otherEndConstraints[i].Name,
                        thisEndConstraints[i].GetValue(entity),
                        thisEndConstraints[i].TypeDefinition);
                }
            }
            var otherDbSet = GetDbSetByEntityType(relationship.OtherSide.Type);
            var root = new IqlRootReferenceExpression();
            var expressions = new List<IqlExpression>();
            for (var i = 0; i < compositeKey.Keys.Length; i++)
            {
                expressions.Add(new IqlIsEqualToExpression(
                    new IqlPropertyExpression(compositeKey.Keys[i].Name, root),
                    new IqlLiteralExpression(compositeKey.Keys[i].Value)
                ));
            }
            var iqlLambdaExpression = new IqlLambdaExpression
            {
                Body = expressions.And(),
                Parameters = new List<IqlRootReferenceExpression>()
            };
            iqlLambdaExpression.Parameters.Add(new IqlRootReferenceExpression());
            var query = (IDbQueryable)otherDbSet.WhereEquals(iqlLambdaExpression);
            return query;
        }

        public IDbQueryable RelationshipQuery<T>(T entity, Expression<Func<T, object>> relationship)
        {
            return RelationshipPropertyQuery(
                entity,
                EntityConfigurationContext.GetEntityByType(entity.GetType())
                    .FindPropertyByLambdaExpression(relationship)
            );
        }
        private bool _dbSetsBySetTypeDelayedInitialized;
        private Dictionary<Type, PropertyInfo> _dbSetsBySetTypeDelayed;

        private Dictionary<Type, PropertyInfo> _dbSetsBySetType { get { if (!_dbSetsBySetTypeDelayedInitialized) { _dbSetsBySetTypeDelayedInitialized = true; _dbSetsBySetTypeDelayed = new Dictionary<Type, PropertyInfo>(); } return _dbSetsBySetTypeDelayed; } set { _dbSetsBySetTypeDelayedInitialized = true; _dbSetsBySetTypeDelayed = value; } }
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

        //public void RevertChanges()
        //{
        //    if (HasSnapshot)
        //    {
        //        if (HasChangesSinceSnapshot())
        //        {
        //            RestoreToPreviousSnapshot();
        //        }
        //    }
        //    else
        //    {
        //        AbandonChanges();
        //    }
        //}

        public void AbandonChanges()
        {
            AbandonChangesInternal();
        }

        private void AbandonChangesInternal()
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

        public async Task<SaveChangesResult> SaveChangesAsync(
            IEnumerable<object> entities = null, IEnumerable<IProperty> properties = null)
        {
            return await ApplySaveChangesAsync(
                new SaveChangesOperation(this, entities?.ToArray(), properties?.ToArray()));
        }

        public virtual Task<SaveChangesResult> ApplySaveChangesAsync(
            SaveChangesOperation operation)
        {
            // Sets could be added to whilst detecting changes
            // so get a copy now
            //var observable = this.Observable<SaveChangesResult>();
            return CommitQueueAsync(
                operation,
                TemporalDataTracker.GetChanges(operation).AllChanges,
                false);
        }

        //protected virtual async Task<SaveChangesResult> CommitQueueAsync(
        //    IEnumerable<IQueuedOperation> queue)
        //{
        //    return await CommitQueueInternalAsync(new SaveChangesOperation(this), queue, false);
        //}

        private async Task<SaveChangesResult> CommitQueueAsync(
            SaveChangesOperation saveChangesOperation,
            IEnumerable<IQueuedEntityCrudOperation> queue,
            bool forceOnline)
        {
            var isOffline = !(forceOnline || !HasOfflineChanges);
            await Events.ContextEvents.EmitStartedAsync(() => saveChangesOperation);
            await saveChangesOperation.Events.EmitStartedAsync(() => saveChangesOperation);
#if !TypeScript
            var operations = queue as IQueuedEntityCrudOperation[] ?? queue.ToArray();
#else
            var operations = queue;
#endif
            foreach (var queuedOperation in operations)
            {
                var state = queuedOperation.Operation.EntityState;
                await state.StatefulSaveEvents.EmitStartedAsync(() => queuedOperation);
                await state.SaveEvents.EmitStartedAsync(() => queuedOperation);
            }

            var offlineChangesBefore = OfflineDataTracker?.SerializeToJson();
            var saveChangesResult = new SaveChangesResult(saveChangesOperation, SaveChangeKind.NoAction);
            var hasAny = false;
            var queuedOperations = queue as IQueuedEntityCrudOperation[] ?? operations.ToArray();
            for (var i = 0; i < queuedOperations.Length; i++)
            {
                var queuedOperation = queuedOperations[i];
                hasAny = true;
                var task = GetType()
                    .GetMethod(nameof(PerformAsync), BindingFlags.Instance | BindingFlags.NonPublic)
                    .InvokeGeneric(this, new object[]
                        {
                            queuedOperation,
                            saveChangesResult,
                            forceOnline
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

            var failedEntitySaves = new List<IEntityStateBase>();
            for (var i = 0; i < saveChangesResult.Results.Count; i++)
            {
                var result = saveChangesResult.Results[i];
                if (result.Success)
                {
                    await result.EntityState.StatefulSaveEvents.EmitSuccessAsync(() => result);
                    await result.EntityState.SaveEvents.EmitSuccessAsync(() => result);
                }
                else
                {
                    failedEntitySaves.Add(result.EntityState);
                }
            }

            if (saveChangesResult.Results.Any(_ => _.Success))
            {
                await saveChangesOperation.Events.EmitSuccessAsync(() => saveChangesResult);
                await Events.ContextEvents.EmitSuccessAsync(() => saveChangesResult);
            }

            await saveChangesOperation.Events.EmitCompletedAsync(() => saveChangesResult);
            await Events.ContextEvents.EmitCompletedAsync(() => saveChangesResult);
            for (var i = 0; i < saveChangesResult.Results.Count; i++)
            {
                var result = saveChangesResult.Results[i];
                await result.EntityState.StatefulSaveEvents.EmitCompletedAsync(() => result);
                await result.EntityState.SaveEvents.EmitCompletedAsync(() => result);
                result.EntityState.ClearStatefulEvents();
            }

            //var dataTracker = isOffline ? OfflineDataTracker : TemporalDataTracker;

            TemporalDataTracker.NotifySaveApplied(
                saveChangesOperation.Entities,
                saveChangesOperation.Properties,
                failedEntitySaves);

            return saveChangesResult;
        }

        protected virtual void EmitOfflineChangeStateEvent()
        {
            OfflineStateChanged.EmitIfExists(() => new OfflineChangeStateChangedEvent(this, OfflineDataTracker, HasOfflineChanges));
        }

        protected virtual Task PerformAsync<TEntity>(
            IQueuedCrudOperation operation,
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
            Type entityType = null;
#endif
            entityType = entityType ?? entity.GetType();
            var key = entity as CompositeKey;
            IEntityConfiguration entityConfiguration = null;
            if (key != null)
            {
                entityConfiguration = EntityConfigurationContext.GetEntityByTypeName(key.TypeName);
                if (entityConfiguration != null)
                {
                    entityType = entityConfiguration.Type;
                    var entityState = GetEntityState(key, entityType);
                    if (entityState == null)
                    {
                        var trackingSet = TemporalDataTracker.TrackingSetByType(entityType);
                        entity = Activator.CreateInstance(entityType);
                        key.ApplyTo(entity);
                        trackingSet.Synchronise(entity, true, true, null);
                        entityState = GetEntityState(key, entityType);
                        if (entityState != null)
                        {
                            entity = entityState.Entity;
                        }
                    }
                    else
                    {
                        entity = entityState.Entity;
                    }
                }
            }

            entityConfiguration = entityConfiguration ?? EntityConfigurationContext.GetEntityByType(entityType);
            if (entityConfiguration == null)
            {
                throw new Exception("Cannot delete entity: Unable to resolve type for entity.");
            }
            return (IEntityStateBase)DeleteInternalMethod.InvokeGeneric(this, new[] { entity }, entityType);
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

        public async Task<T> RefreshEntityAsync<T>(T entity
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
        private static bool DefaultValuePlaceholderInstanceDelayedInitialized;
        private static DefaultValuePlaceholder DefaultValuePlaceholderInstanceDelayed;

        private static DefaultValuePlaceholder DefaultValuePlaceholderInstance { get { if (!DefaultValuePlaceholderInstanceDelayedInitialized) { DefaultValuePlaceholderInstanceDelayedInitialized = true; DefaultValuePlaceholderInstanceDelayed = new DefaultValuePlaceholder(); } return DefaultValuePlaceholderInstanceDelayed; } set { DefaultValuePlaceholderInstanceDelayedInitialized = true; DefaultValuePlaceholderInstanceDelayed = value; } }

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
        private DataContextEvents _events;
        private EventEmitter<ValueChangedEvent<bool, DataTrackerState>> _hasOfflineChangesChanged;

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
            return OfflineDataTracker?.GetChanges(new SaveChangesOperation(this, entities, properties)) ?? new IqlDataChanges(new SaveChangesOperation(this), null);
        }

        public SaveChangesOperation GetSaveChangesOperation(object[] entities = null, IProperty[] properties = null)
        {
            return new SaveChangesOperation(this, entities, properties);
        }

        public IqlDataChanges GetChanges(object[] entities = null, IProperty[] properties = null)
        {
            return TemporalDataTracker.GetChanges(new SaveChangesOperation(this, entities, properties));
        }

        //private DataSnapshotChain _snapshotChain = null;
        //public DataSnapshotChain SnapshotChain
        //{
        //    get
        //    {
        //        if (_snapshotChain == null)
        //        {
        //            _snapshotChain = new DataSnapshotChain(_snapshotCreatorId);
        //        }
        //        return _snapshotChain;
        //    }
        //}

        //public bool HasSnapshot => SnapshotChain.Latest.Snapshot != null;
        //private Guid _snapshotCreatorId = Guid.NewGuid();
        //public DataSnapshot GetSnapshot()
        //{
        //    return new DataSnapshot(_snapshotCreatorId, TemporalDataTracker.SerializeToJson());
        //}

        //public DataSnapshotChain RecordSnapshot()
        //{
        //    if(CurrentSnapshot == null)
        //    {
        //        if (!HasChanges())
        //        {
        //            return null;
        //        }
        //    }
        //    else if(!HasChangesSinceSnapshot())
        //    {
        //        return null;
        //    }
        //    var snapshot = GetSnapshot();
        //    var newChainPoint = new DataSnapshotChain(_snapshotCreatorId, snapshot);
        //    (CurrentSnapshot ?? SnapshotChain).Next = newChainPoint;
        //    _lastRestoredSnapshot = newChainPoint;
        //    return newChainPoint;
        //}


        //public DataSnapshotChain CurrentSnapshot
        //{
        //    get
        //    {
        //        if (_lastRestoredSnapshot != null)
        //        {
        //            return _lastRestoredSnapshot;
        //        }
        //        if (HasSnapshot)
        //        {
        //            return SnapshotChain.Latest;
        //        }
        //        return null;
        //    }
        //}

        //public bool RestoreToNextSnapshot()
        //{
        //    var currentSnapshot = CurrentSnapshot;
        //    if (currentSnapshot != null && currentSnapshot.Next != null)
        //    {
        //        return RestoreSnapshot(currentSnapshot.Next);
        //    }
        //    return false;
        //}

        //public bool RestoreToPreviousSnapshot()
        //{
        //    if (_lastRestoredSnapshot != null)
        //    {
        //        if (HasChangesSinceSnapshot())
        //        {
        //            return RestoreSnapshot(_lastRestoredSnapshot);
        //        }
        //        return RestoreSnapshot(_lastRestoredSnapshot.Previous);
        //    }
        //    if (HasSnapshot)
        //    {
        //        return RestoreToLatestSnapshot();
        //    }
        //    return false;
        //}

        //public bool RestoreToLatestSnapshot()
        //{
        //    if (HasSnapshot)
        //    {
        //        return RestoreSnapshot(SnapshotChain.Latest);
        //    }
        //    return false;
        //}

        //public bool RevertToFirstSnapshot()
        //{
        //    if (HasSnapshot)
        //    {
        //        return RestoreSnapshot(SnapshotChain.Next);
        //    }
        //    return false;
        //}

        //public bool HasChangesSinceSnapshot()
        //{
        //    var currentSnapshot = CurrentSnapshot;
        //    if (currentSnapshot != null && currentSnapshot.Snapshot != null)
        //    {
        //        var state = TemporalDataTracker.SerializeToJson();
        //        return state != currentSnapshot.Snapshot.Snapshot;
        //    }
        //    return false;
        //}

        //private DataSnapshotChain _lastRestoredSnapshot = null;
        //public bool RestoreSnapshot(DataSnapshotChain chain)
        //{
        //    if (chain == null)
        //    {
        //        return false;
        //    }
        //    if (chain.CreatorId != _snapshotCreatorId)
        //    {
        //        return false;
        //    }
        //    if (chain.IsInvalid)
        //    {
        //        return false;
        //    }
        //    var success = false;
        //    _lastRestoredSnapshot = chain;
        //    AbandonChangesInternal();
        //    if(chain.Snapshot != null)
        //    {
        //        TemporalDataTracker.RestoreFromJson(chain.Snapshot.Snapshot);
        //    }
        //    return success;
        //}

        //public bool RestoreSnapshotById(Guid id)
        //{
        //    var snapshot = SnapshotChain.FindById(id);
        //    if (snapshot == null)
        //    {
        //        return false;
        //    }
        //    return RestoreSnapshot(snapshot);
        //}

        public IQueuedAddEntityOperation[] GetAdditions(object[] entities = null)
        {
            return GetChanges(entities).Additions;
        }

        public IQueuedUpdateEntityOperation[] GetUpdates(object[] entities = null, IProperty[] properties = null)
        {
            return GetChanges(entities, properties).Updates;
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
                    entity = (T)entity.Clone(EntityConfigurationContext, entityType);
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
            if (entity == null)
            {
                return null;
            }
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
            Expression<Func<object, TProperty>> expression, bool? ensureIntegrityForSubmission = null)
        {
            var entityConfiguration = EntityConfigurationContext.GetEntityByType(typeof(T) ?? entity.GetType());
            var property = entityConfiguration.FindPropertyByLambdaExpression(expression);
            var task = (Task<IPropertyValidationResult>)ValidateEntityPropertyInternalAsyncMethod.InvokeGeneric(this,
                new object[] { entity, property, ensureIntegrityForSubmission },
                entity.GetType());
            var result = await task;
            return result;
        }

        async Task<IPropertyValidationResult> IDataContext.ValidateEntityPropertyByNameAsync(object entity, string property, bool? ensureIntegrityForSubmission = null)
        {
            var entityConfiguration = EntityConfigurationContext.GetEntityByType(entity.GetType());
            var task = (Task<IPropertyValidationResult>)ValidateEntityPropertyInternalAsyncMethod.InvokeGeneric(this,
                new object[] { entity, entityConfiguration.FindProperty(property), ensureIntegrityForSubmission },
                entity.GetType());
            var result = await task;
            return result;
        }

        async Task<IPropertyValidationResult> IDataContext.ValidateEntityPropertyAsync(object entity, IProperty property, bool? ensureIntegrityForSubmission = null)
        {
            var task = (Task<IPropertyValidationResult>)ValidateEntityPropertyInternalAsyncMethod.InvokeGeneric(this,
                new object[] { entity, property, ensureIntegrityForSubmission },
                entity.GetType());
            var result = await task;
            return result;
        }

        public async Task<EntityValidationResult<T>> ValidateEntityAsync<T>(T entity, bool? ensureIntegrityForSubmissions = false)
            where T : class
        {
            return (EntityValidationResult<T>)(await ValidateEntityInternalAsync(entity, ensureIntegrityForSubmissions));
        }

        async Task<IEntityValidationResult> IDataContext.ValidateEntityBaseAsync(object entity, bool? ensureIntegrityForSubmissions = null)
        {
            var task = (Task<IEntityValidationResult>)ValidateEntityInternalAsyncMethod.InvokeGeneric(this,
                new object[] { entity, ensureIntegrityForSubmissions },
                entity.GetType());
            var result = await task;
            return result;
        }

        private async Task<IEntityValidationResult> ValidateEntityInternalAsync<T>(T entity, bool? ensureIntegrityForSubmissions = null) where T : class
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
                var result = await ValidateEntityPropertyAsync(entity, property, ensureIntegrityForSubmissions);
                if (result.HasValidationFailures())
                {
                    validationResult.AddPropertyValidationResult(result);
                }
            }

            return validationResult;
        }

        public async Task<PropertyValidationResult<T>> ValidateEntityPropertyByExpressionAsync<T, TProperty>(T entity, Expression<Func<T, TProperty>> property, bool? ensureIntegrityForSubmission = null)
            where T : class
        {
            return (PropertyValidationResult<T>)(await ValidateEntityPropertyByExpressionInternalAsync(entity, property, ensureIntegrityForSubmission));
        }

        private Task<IPropertyValidationResult> ValidateEntityPropertyByExpressionInternalAsync<T, TProperty>(T entity, Expression<Func<T, TProperty>> property, bool? ensureIntegrityForSubmission = null)
            where T : class
        {
            var entityConfiguration = EntityConfigurationContext.GetEntityByType(typeof(T) ?? entity.GetType());
            return ValidateEntityPropertyInternalAsync(entity, entityConfiguration.FindPropertyByLambdaExpression(property), ensureIntegrityForSubmission);
        }

        public async Task<PropertyValidationResult<T>> ValidateEntityPropertyByNameAsync<T>(T entity, string property, bool? ensureIntegrityForSubmission = null)
            where T : class
        {
            return (PropertyValidationResult<T>)(await ValidateEntityPropertyByNameInternalAsync(entity, property, ensureIntegrityForSubmission));
        }

        private Task<IPropertyValidationResult> ValidateEntityPropertyByNameInternalAsync<T>(T entity, string property, bool? ensureIntegrityForSubmissions = null) where T : class
        {
            var entityConfiguration = EntityConfigurationContext.GetEntityByType(typeof(T) ?? entity.GetType());
            return ValidateEntityPropertyInternalAsync(entity, entityConfiguration.FindProperty(property), ensureIntegrityForSubmissions);
        }

        public async Task<PropertyValidationResult<T>> ValidateEntityPropertyAsync<T>(T entity, IProperty property, bool? ensureIntegrityForSubmission = null)
            where T : class
        {
            return (PropertyValidationResult<T>)(await ValidateEntityPropertyInternalAsync(entity, property, ensureIntegrityForSubmission));
        }

        private async Task<IPropertyValidationResult> ValidateEntityPropertyInternalAsync<T>(T entity, IProperty property, bool? ensureIntegrityForSubmission = null)
            where T : class
        {
            var ensureIntegrityForSubmissionResolved = ensureIntegrityForSubmission ?? false;
            var validationResult = new PropertyValidationResult<T>(entity, property);
            if (property.HasInferredWith)
            {
                var inferredWithIgnored = false;
                if (property.HasInferredWithCondition)
                {
                    var oldEntity = GetEntityState(entity)?.EntityBeforeChanges();
                    for (var i = 0; i < property.InferredValueConfigurations.Count; i++)
                    {
                        var inferredWith = property.InferredValueConfigurations[i];
                        if (inferredWith.HasCondition)
                        {
                            var result = await new EvaluationSession().EvaluateIqlAsync(
                                inferredWith.InferredWithConditionIql,
                                new InferredValueContext<T>((T)oldEntity, entity, false),
                                this,
                                null,
                                typeof(InferredValueContext<T>));
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

            if (!property.Kind.HasFlag(IqlPropertyKind.Count) && (!property.Kind.HasFlag(IqlPropertyKind.Key) || property.Kind.HasFlag(IqlPropertyKind.RelationshipKey)))
            {
                var propertyValue = property.GetValue(entity);
                if (!validationResult.HasValidationFailures() &&
                PropertyValueIsIllegallyEmpty(property, entity, propertyValue, ensureIntegrityForSubmissionResolved)
            )
                {
                    if (ensureIntegrityForSubmissionResolved)
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
                            return await ValidateEntityPropertyInternalAsync(entity, property, false);
                        }
                    }
                    validationResult.AddFailure(
                        ValidationDefaults.DefaultRequiredAutoValidationFailureKey,
                        ValidationDefaults.DefaultRequiredAutoValidationFailureMessage);
                }
            }
            return validationResult;
        }

        private static bool PropertyValueIsIllegallyEmpty(IProperty property, object entity, object propertyValue, bool ensureIntegrityForSubmissionResolved)
        {
            if (!property.CanWrite)
            {
                return false;
            }

            if (property.TypeDefinition.Nullable && property.Nullable != false)
            {
                return false;
            }

            if (property.Kind.HasFlag(IqlPropertyKind.Relationship) &&
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
                if (ensureIntegrityForSubmissionResolved &&
                    (!property.Kind.HasFlag(IqlPropertyKind.RelationshipKey) ||
                     property.Relationship.ThisEnd.Property.GetValue(entity) == null)
                )
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

        public virtual async Task<CountDataResult<TEntity>> CountAsync<TEntity>(GetDataOperation<TEntity> operation)
            where TEntity : class
        {
            var response = new FlattenedGetDataResult<TEntity>(null, null, operation, true);
            var queuedGetDataOperation = new QueuedGetDataOperation<TEntity>(
                operation,
                response);

            await DataStore.PerformCountAsync(queuedGetDataOperation);

            if (response.RequestStatus == RequestStatus.Offline)
            {
                // Magic happens here...
                if (SupportsOffline && response.Queryable.AllowOffline != false)
                {
                    response.IsOffline = true;
                    await OfflineDataStore.PerformCountAsync(queuedGetDataOperation);
                }
            }

            return new CountDataResult<TEntity>(response.IsOffline, operation, response.TotalCount, response.IsSuccessful());
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

            var localDataTracker = ResolveDataTracker(operation.Queryable, this);
            var response = new FlattenedGetDataResult<TEntity>(localDataTracker, null, operation, true);
            response.Queryable = operation.Queryable;
            // perform get and set up tracking on the objects
            var queuedGetDataOperation = new QueuedGetDataOperation<TEntity>(
                operation,
                response);

            // Prevents changes triggering changed state detection
            TemporalDataTracker.Lock();
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
            var all = EntityConfigurationContext.FlattenObjectGraphs(typeof(TEntity), dbList);
            // Restore change state detection
            TemporalDataTracker.Unlock();

            foreach (var grouping in all)
            {
                var list = grouping.Value;
                foreach (var item in list)
                {
                    var entityStateBase = GetEntityState(item);
                    if (entityStateBase != null)
                    {
                        entityStateBase.UpdateHasChanges();
                    }
                }
            }

            var getDataResult =
                new GetDataResult<TEntity>(response.IsOffline, localDataTracker, dbList, operation, response.IsSuccessful())
                {
                    TotalCount = response.TotalCount
                };

            ApplyPaging(dbList, response);

            return getDataResult;
        }

        public static DataTracker ResolveDataTracker(IQueryableBase sourceQueryable, IDataContext dataContext)
        {
            bool shouldTrackResults = dataContext.TrackEntities;
            if (sourceQueryable != null && sourceQueryable.TrackEntities.HasValue)
            {
                shouldTrackResults = sourceQueryable.TrackEntities.Value;
            }

            var localDataTracker = dataContext.TemporalDataTracker;
            if (!shouldTrackResults)
            {
                localDataTracker = new DataContextDataTracker(dataContext, DataTrackerKind.Temporal, dataContext.EntityConfigurationContext,
                    "No Tracking", false, true);
            }

            return localDataTracker;
        }

        private async Task RunGetAsync<TEntity>(QueuedGetDataOperation<TEntity> queuedGetDataOperation, FlattenedGetDataResult<TEntity> response)
            where TEntity : class
        {
            if (!HasOfflineChanges)
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
            var sourceQueryable = (DbQueryable<TEntity>)response.Queryable;
            var localDataTracker = response.DataTracker;
            var dbList = new DbList<TEntity>(localDataTracker);
            dbList.SourceQueryable = sourceQueryable;
            // Flatten before we merge because the merge will update the result data set with
            // tracked data
            dbList.Success = response.IsSuccessful();
            if (dbList.Success)
            {
                void TrackResponse(DataTracker dataTracker)
                {
                    if (
                        dataTracker == localDataTracker ||
                        dataTracker.EntityConfigurationBuilder == EntityConfigurationContext
                        )
                    {
                        var dealtWith = new Dictionary<object, object>();
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

                if (localDataTracker.LiveTracking && !response.IsOffline)
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

        private IEntityStateBase DeleteInternal<T>(T entity)
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
            return EntityStates.Find(entity);
        }

        public Task<bool> QueryAnyAsync(IqlDataSetQueryExpression query, ITypeResolver typeResolver = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            typeResolver = typeResolver ?? EntityConfigurationContext;
            var entityType = typeResolver.ResolveTypeFromTypeName(query.EntityTypeName).Type;
            return GetDbSetByEntityType(entityType).AnyQueryAsync(query.Filter as IqlLambdaExpression
#if TypeScript
            , evaluateContext
#endif
            );
        }

        public Task<bool> QueryAllAsync(IqlDataSetQueryExpression query, ITypeResolver typeResolver = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            typeResolver = typeResolver ?? EntityConfigurationContext;
            var entityType = typeResolver.ResolveTypeFromTypeName(query.EntityTypeName).Type;
            return GetDbSetByEntityType(entityType).AllQueryAsync(query.Filter as IqlLambdaExpression
#if TypeScript
            , evaluateContext
#endif
            );
        }

        public Task<long> QueryCountAsync(IqlDataSetQueryExpression query, ITypeResolver typeResolver = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            typeResolver = typeResolver ?? EntityConfigurationContext;
            var entityType = typeResolver.ResolveTypeFromTypeName(query.EntityTypeName).Type;
            return GetDbSetByEntityType(entityType).CountQueryAsync(query.Filter as IqlLambdaExpression
#if TypeScript
            , evaluateContext
#endif
            );
        }

        public Task<object> GetEntityByKeyAsync(IEntityConfiguration entityConfiguration, CompositeKey key, string[] expandPaths, bool trackResult)
        {
            var set = GetDbSetByEntityType(entityConfiguration.Type);
            if (!trackResult)
            {
                set = set.NoTracking();
            }
            for (var i = 0; i < expandPaths.Length; i++)
            {
                set = set.ExpandRelationship(expandPaths[i]);
            }
            return set.GetWithKeyAsync(key);
        }

        public string EntityStateKey(object entity, IEntityConfiguration entityConfiguration = null)
        {
            var state = GetEntityState(entity, entityConfiguration?.Type ?? entity?.GetType());
            return state.StateKey;
        }

        public IqlEntityStatus EntityStatus(object entity, IEntityConfiguration entityConfiguration = null)
        {
            var result = IsEntityNew(entity
#if TypeScript
            , entityConfiguration?.Type
#endif
            );
            if (result == null)
            {
                return IqlEntityStatus.NotTracked;
            }

            return result.Value ? IqlEntityStatus.New : IqlEntityStatus.Existing;
        }

        public void Dispose()
        {
            TemporalDataTracker?.Dispose();
            OfflineDataTracker?.Dispose();
        }

        public TrackerSnapshot CurrentSnapshot => TemporalDataTracker.CurrentSnapshot;

        public void ClearSnapshots()
        {
            TemporalDataTracker.ClearSnapshots();
        }

        public void EmptySnapshots()
        {
            TemporalDataTracker.EmptySnapshots();
        }

        public TrackerSnapshot AddSnapshot(bool? nullIfEmpty = null)
        {
            return TemporalDataTracker.AddSnapshot(nullIfEmpty);
        }

        public bool UndoChanges(object[] entities = null, object[] properties = null)
        {
            return TemporalDataTracker.UndoChanges(entities, properties);
        }

        public bool RemoveLastSnapshot(SnapshotRemoveKind? kind = null)
        {
            return TemporalDataTracker.RemoveLastSnapshot(kind);
        }

        public bool RevertToSnapshot()
        {
            return TemporalDataTracker.RevertToSnapshot();
        }

        public bool HasChangesSinceSnapshot => TemporalDataTracker.HasChangesSinceSnapshot;
        public bool HasChanges => TemporalDataTracker.HasChanges;
        public EventEmitter<ValueChangedEvent<bool, DataTrackerState>> HasChangesSinceSnapshotChanged => TemporalDataTracker.HasChangesSinceSnapshotChanged;
        public EventEmitter<ValueChangedEvent<bool, DataTrackerState>> HasChangesChanged => TemporalDataTracker.HasChangesChanged;
        public TrackerSnapshot[] Snapshots => TemporalDataTracker.Snapshots;

        public int SnapshotsCount => TemporalDataTracker.SnapshotsCount;

        public TrackerSnapshot[] RestorableSnapshots => TemporalDataTracker.RestorableSnapshots;

        public int RestorableSnapshotsCount => TemporalDataTracker.RestorableSnapshotsCount;

        public bool RestoreNextAbandonedSnapshot()
        {
            return TemporalDataTracker.RestoreNextAbandonedSnapshot();
        }

        public TrackerSnapshot ReplaceLastSnapshot()
        {
            return TemporalDataTracker.ReplaceLastSnapshot();
        }

        public bool HasSnapshot => TemporalDataTracker.HasSnapshot;

        public bool HasRestorableSnapshot => TemporalDataTracker.HasRestorableSnapshot;
    }
}
