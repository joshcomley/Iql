using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Results;
using Iql.Data.DataStores;
using Iql.Data.DataStores.NestedSets;
using Iql.Data.Extensions;
using Iql.Data.Lists;
using Iql.Data.SpecialTypes;
using Iql.Data.Tracking;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Entities.Relationships;
using Iql.Entities.Services;
using Iql.Entities.Validation;
using Iql.Entities.Validation.Validation;
using Iql.Extensions;
using Iql.Parsing;

namespace Iql.Data.Context
{
    public class DataContext : IDataContext
    {
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
            return tracker?.DataContext;
        }

        public static ITrackingSet FindTrackingForEntity(object entity)
        {
            var trackers = DataTracker.AllDataTrackers();
            for (var i = 0; i < trackers.Length; i++)
            {
                var tracker = trackers[i];
                var set = tracker.Tracking.GetTrackingSetForEntity(entity);
                if (set != null)
                {
                    return set;
                }
            }
            return null;
        }

        public static IEntityStateBase FindEntity(object entity)
        {
            var tracker = FindTrackingForEntity(entity);
            return tracker?.FindMatchingEntityState(entity);
        }

        public DataContext(
            IDataStore dataStore = null,
            EvaluateContext evaluateContext = null
        )
        {
            DataStore = dataStore;
            if (DataStore != null)
            {
                DataStore.DataContext = this;
            }
            EvaluateContext = evaluateContext;
            var thisType = GetType();
            if (!EntityConfigurationsBuilders.ContainsKey(thisType))
            {
                EntityConfigurationContext = new EntityConfigurationBuilder();
                EntityConfigurationsBuilders.Add(thisType, EntityConfigurationContext);
                Initialize();
            }
            else
            {
                EntityConfigurationContext = EntityConfigurationsBuilders[thisType];
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
                if (_dataStore != null)
                {
                    _dataStore.DataContext = this;
                }
            }
        }

        public bool TrackEntities { get; set; } = true;
        public string SynchronicityKey { get; set; } = Guid.NewGuid().ToString();
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
            return DataStore.Tracking.TrackingSetByType(entityType).FindMatchingEntityState(entity);
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
        private IDataStore _dataStore;

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
            for (var i = 0; i < DataStore.Tracking.Sets.Count; i++)
            {
                var set = DataStore.Tracking.Sets[i];
                set.AbandonChanges();
            }
        }

        public void AbandonChangesForEntity(object entity)
        {
            var set = DataStore.Tracking.TrackingSetByType(entity.GetType());
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
            var set = DataStore.Tracking.TrackingSetByType(state.EntityType);
            set.AbandonChangesForEntityState(state);
        }

        public void AbandonChangesForEntityStates(IEnumerable<IEntityStateBase> states)
        {
            foreach (var state in states)
            {
                AbandonChangesForEntityState(state);
            }
        }

        public async Task<SaveChangesResult> SaveChangesAsync(IEnumerable<object> entities = null)
        {
            return await DataStore.SaveChangesAsync(new SaveChangesOperation(this, entities?.ToArray()));
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

        public void DeleteEntity(object entity
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
            AsDbSetByType(entityType).DeleteEntity(entity);
        }

        public void CascadeDeleteEntity(object entity,
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
            var entityState = DataStore.Tracking.TrackingSetByType(entityType)
                .FindMatchingEntityState(entity);
            entityState.MarkForCascadeDeletion(cascadedFromEntity, cascadedFromRelationship);
            DeleteEntity(entity
#if TypeScript
                , entityType
#endif
                );
        }

        public void AddEntity(object entity
#if TypeScript
            , Type entityType
#endif
        )
        {
#if !TypeScript
            var entityType = entity.GetType();
#endif
            AsDbSetByType(entityType).AddEntity(entity);
        }

        public async Task<T> RefreshEntity<T>(T entity
#if TypeScript
            , Type entityType
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
            var isEntityNew = this.IsEntityNew(entity, entityType);
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

        public T EnsureTypedEntity<T>(object entity, bool convertRelationships)
            where T : class
        {
            return (T)EnsureTypedEntityByType(entity, typeof(T), convertRelationships);
        }

        public object EnsureTypedEntityByType(object entity, Type type, bool convertRelationships)
        {
            if (entity != null && entity.GetType() != type /* prevent infinite recursion */)
            {
                var entityConfiguration = EntityConfigurationContext.GetEntityByType(type);
                var typedEntity = Activator.CreateInstance(type);
                foreach (var property in entityConfiguration.Properties)
                {
                    if (!convertRelationships &&
                        property.Relationship != null &&
                        property.Relationship.ThisEnd.Property == property)
                    {
                        continue;
                    }
                    //var instanceValue = typedEntity.GetPropertyValue(property.Name);
                    var remoteValue = property.TypeDefinition.EnsureValueType(entity.GetPropertyValue(property));

                    if (remoteValue != null)
                    {
                        var isEnum = property.TypeDefinition.Type.IsEnum;
#if !TypeScript
                        if (!isEnum)
                        {
                            var underlyingType = Nullable.GetUnderlyingType(property.TypeDefinition.Type);
                            if (underlyingType != null && underlyingType.IsEnum)
                            {
                                isEnum = true;
                            }
                        }
#endif
                        if (isEnum && remoteValue is string)
                        {
                            try
                            {
                                remoteValue = Enum.Parse(property.TypeDefinition.Type, remoteValue as string);
                            }
                            catch
                            {
                                remoteValue = 0;
                            }
                        }
                        typedEntity.SetPropertyValue(property, remoteValue);
                    }
                }

                if (convertRelationships)
                {
                    foreach (var relationship in entityConfiguration.Relationships)
                    {
                        var isSource = relationship.Source.EntityConfiguration == entityConfiguration;
                        var propertyName = isSource
                            ? relationship.Source.Property
                            : relationship.Target.Property;
                        if (isSource)
                        {
                            switch (relationship.Kind)
                            {
                                case RelationshipKind.OneToMany:
                                case RelationshipKind.OneToOne:
                                    typedEntity.SetPropertyValue(propertyName,
                                        EnsureTypedEntityByType(
                                            entity.GetPropertyValue(propertyName),
                                            relationship.Target.Type,
                                            convertRelationships
                                        ));
                                    break;
                                case RelationshipKind.ManyToMany:
                                    typedEntity.SetPropertyValue(propertyName,
                                        EnsureTypedListByType((IEnumerable)entity.GetPropertyValue(propertyName), relationship.Source.Type, entity, relationship.Target.Type, convertRelationships));
                                    break;
                            }
                        }
                        else
                        {
                            switch (relationship.Kind)
                            {
                                case RelationshipKind.OneToOne:
                                    typedEntity.SetPropertyValue(propertyName,
                                        EnsureTypedEntityByType(
                                            entity.GetPropertyValue(propertyName),
                                            relationship.Source.Type,
                                            convertRelationships)
                                    );
                                    break;
                                case RelationshipKind.OneToMany:
                                case RelationshipKind.ManyToMany:
                                    typedEntity.SetPropertyValue(propertyName,
                                        EnsureTypedListByType(
                                            (IEnumerable)entity.GetPropertyValue(propertyName),
                                            relationship.Target.Type,
                                            entity,
                                            relationship.Source.Type,
                                            convertRelationships));
                                    break;
                            }
                        }
                    }
                }
                entity = typedEntity;
            }
            return entity;
        }

        public IList<T> EnsureTypedList<T>(IEnumerable responseData, bool forceNotNull = false)
                    where T : class
        {
            return (IList<T>)EnsureTypedListByType(responseData, typeof(T), null, null, forceNotNull);
        }

        public IList EnsureTypedListByType(IEnumerable responseData, Type type, object owner, Type childType, bool convertRelationships, bool forceNotNull = false)
        {
            IList list = null;
            if (responseData != null || forceNotNull)
            {
                if (childType == null)
                {
                    list = (IList)Activator.CreateInstance(typeof(DbList<>).MakeGenericType(type));
                }
                else
                {
                    list = (IList)Activator.CreateInstance(typeof(RelatedList<,>).MakeGenericType(childType, type), new object[] { owner });
                }
            }
            if (responseData != null)
            {
                foreach (var entity in responseData)
                {
                    var typedEntity = EnsureTypedEntityByType(entity, childType ?? type, convertRelationships);
                    list.Add(typedEntity);
                }
            }
            return list;
        }

        public bool IsTracked(object entity)
        {
            return DataStore.Tracking.IsTracked(entity);
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

        private MethodInfo ValidateEntityPropertyInternalAsyncMethod
        {
            get => _validateEntityPropertyInternalAsyncMethod
            = _validateEntityPropertyInternalAsyncMethod ?? typeof(DataContext).GetMethod(nameof(ValidateEntityPropertyInternalAsync),
                  BindingFlags.Instance | BindingFlags.NonPublic);
        }

        async Task<IEntityValidationResult> IDataContext.ValidateEntityAsync(object entity)
        {
            var task = (Task<IEntityValidationResult>)ValidateEntityInternalAsyncMethod.InvokeGeneric(this,
                new object[] { entity },
                entity.GetType());
            var result = await task;
            return result;
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
                            var result = await inferredWith.InferredWithConditionIql.EvaluateIqlAsync(entity, this, typeof(T));
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
    }
}