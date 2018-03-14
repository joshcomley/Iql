using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Extensions;
using Iql.Parsing;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Data.Lists;
using Iql.Queryable.Data.Tracking.State;
using Iql.Queryable.Extensions;

namespace Iql.Queryable.Data.Context
{
    public class DataContext : IDataContext
    {
        private readonly Dictionary<string, object> _configurations =
            new Dictionary<string, object>();
        private static readonly Dictionary<Type, EntityConfigurationBuilder> EntityConfigurationsBuilders
            = new Dictionary<Type, EntityConfigurationBuilder>();
        public DataContext(
            IDataStore dataStore,
            EvaluateContext evaluateContext = null
        )
        {
            DataStore = dataStore;
            DataStore.DataContext = this;
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
            }
        }

        private void Initialize()
        {
            if (!_initialized)
            {
                _initialized = true;
                Configure(EntityConfigurationContext);
                InitializeProperties();
            }
        }

        private void InitializeProperties()
        {
            var properties = GetType().GetProperties()
                .Where(p => typeof(IDbSet).IsAssignableFrom(p.PropertyType))
                .ToList();
            foreach (var property in properties)
            {
                IDbSet asDbSetByType;
                var dbSetName = nameof(DbSet<object, object>);
                if (property.PropertyType.Name == dbSetName)
                {
                    asDbSetByType = AsDbSetByType(property.PropertyType.GenericTypeArguments[0]);
                }
                else
                {
                    var baseType = property.PropertyType.BaseType;
                    while (baseType.SimpleName() != dbSetName)
                    {
                        baseType = baseType.BaseType;
                    }
                    asDbSetByType = AsCustomDbSetByType(baseType.GenericTypeArguments[0], property.PropertyType);
                }
                property.SetValue(this, asDbSetByType);
            }
        }

        public IDataStore DataStore { get; set; }
        public bool TrackEntities { get; set; } = true;
        public string SynchronicityKey { get; set; } = Guid.NewGuid().ToString();
        public EvaluateContext EvaluateContext { get; set; }
        public EntityConfigurationBuilder EntityConfigurationContext { get; set; }

        public void RegisterConfiguration<T>(T configuration)
            where T : class
        {
            _configurations.Add(ConfigurationNameByType(configuration.GetType()), configuration);
        }

        public IEntityStateBase GetEntityState(object entity
#if TypeScript
            , Type entityType
#endif
            )
        {
#if !TypeScript
            var entityType = entity.GetType();
#endif
            return DataStore.Tracking.TrackingSetByType(entityType).GetEntityState(entity);
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

        public IDbSet AsDbSetByType(Type entityType)
        {
            Initialize();
            var entityKey = EntityConfigurationContext.GetEntityByType(entityType).Key;
            var keyType = entityKey.KeyType;
            return (IDbSet)GetType().GetMethod(nameof(AsDbSet))
                .InvokeGeneric(
                    this,
                    new object[] { },
                    entityType,
                    keyType
                    );
        }

        public IDbSet AsCustomDbSetByType(Type entityType, Type setType)
        {
            Initialize();
            var entityKey = EntityConfigurationContext.GetEntityByType(entityType).Key;
            var keyType = entityKey.KeyType;
            return (IDbSet)GetType().GetMethod(nameof(AsCustomDbSet))
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

        public TDbSet AsCustomDbSet<T, TKey, TDbSet>()
            where T : class
        {
            Initialize();
            Func<IDataStore> dataStoreGetter = () => DataStore;
            return (TDbSet)Activator.CreateInstance(
                typeof(TDbSet),
                new object[]
                {
                    EntityConfigurationContext,
                    dataStoreGetter,
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

        public async Task<SaveChangesResult> SaveChanges()
        {
            return await DataStore.SaveChanges(new SaveChangesOperation(this));
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
            , Type entityType
#endif
        )
        {
#if !TypeScript
            var entityType = entity.GetType();
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
                .GetEntityState(entity);
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
            return await GetEntityByMockEntity(entity
#if TypeScript
, entityType
#endif
                );
        }

        public async Task<T> GetEntityByMockEntity<T>(T entity
#if TypeScript
            , Type entityType
#endif
            ) where T : class
        {
#if !TypeScript
            var entityType = typeof(T);
#endif
            if (entityType == typeof(object))
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
            return (T)await queryable.WithKey(identityWhereOperation.Key);
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
                    var remoteValue = EnsureTypedValue(entity.GetPropertyValue(property), property);

                    if (remoteValue != null)
                    {
                        if (property.Type.IsEnum && remoteValue is string)
                        {
                            try
                            {
                                remoteValue = Enum.Parse(property.Type, remoteValue as string);
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
                        var isSource = relationship.Source.Configuration == entityConfiguration;
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

        private static object EnsureTypedValue(object value, IProperty property)
        {
            if (Equals(value, null))
            {
                if (property.Nullable)
                {
                    return null;
                }
                //if (!property.Nullable)
                //{
                //    return property.Type.DefaultValue();
                //}
                return null;
            }
            if (property.Type == typeof(String) && !(value is String))
            {
                return value.ToString();
            }
            if (property.Type == typeof(Int32) && !(value is Int32) && !(value is Double))
            {
                return Convert.ToDouble(value.ToString());
            }
            if (property.Type == typeof(DateTime) && !(value is DateTime))
            {
                if (value is Int64)
                {
                    return new DateTime((long)value);
                }
                return DateTime.Parse(value.ToString());
            }
            if (property.Type == typeof(Boolean) && !(value is Boolean))
            {
                return Boolean.Parse(value.ToString());
            }
            return value;
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
    }
}