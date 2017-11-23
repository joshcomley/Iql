using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Parsing;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Extensions;

namespace Iql.Queryable.Data
{
    public class DataContext : IDataContext
    {
        private readonly Dictionary<string, object> _configurations =
            new Dictionary<string, object>();

        public DataContext(
            IDataStore dataStore,
            EvaluateContext evaluateContext = null
        )
        {
            DataStore = dataStore;
            EvaluateContext = evaluateContext;
            DataStore.DataContext = this;
            EntityConfigurationContext = new EntityConfigurationBuilder();
            Initialize();
        }

        private void Initialize()
        {
            if (!_initialized)
            {
                _initialized = true;
                Configure(EntityConfigurationContext);
                var properties = GetType().GetProperties()
                    .Where(p => typeof(IDbSet).IsAssignableFrom(p.PropertyType))
                    .ToList();
                foreach (var property in properties)
                {
                    var asDbSetByType = AsDbSetByType(property.PropertyType.GenericTypeArguments[0]);
                    property.SetValue(this, asDbSetByType);
                }
            }
        }

        public IDataStore DataStore { get; set; }
        public EvaluateContext EvaluateContext { get; set; }
        public EntityConfigurationBuilder EntityConfigurationContext { get; set; }

        public void RegisterConfiguration<T>(T configuration)
            where T : class
        {
            _configurations.Add(ConfigurationNameByType(configuration.GetType()), configuration);
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
                .MakeGenericMethod(
                    entityType,
                    keyType
                    )
                .Invoke(this, new object[]
                {
#if TypeScript
                    entityType,
                    keyType
#endif
                });
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
            if (new[] { left, right }.Count(i => i == null) == 1)
            {
                return false;
            }
            if (left.GetType() != right.GetType())
            {
                return false;
            }
            if (left == right)
            {
                return true;
            }
            var configuration = EntityConfigurationContext.GetEntityByType(type);
            var isMatch = true;
            foreach (var id in configuration.Key.Properties)
            {
                if (!Equals(left.GetPropertyValue(id.PropertyName), right.GetPropertyValue(id.PropertyName)))
                {
                    isMatch = false;
                    break;
                }
            }
            return isMatch;
        }

        public async Task<T> RefreshEntity<T>(T entity)
            where T : class
        {
            if (this.IsEntityNew(entity, typeof(T)))
            {
                return null;
            }
            var identityWhereOperation =
                this.ResolveWithKeyOperationFromEntity(entity);
            var queryable = AsDbSetByType(typeof(T));
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

        public T EnsureTypedEntity<T>(object entity)
        {
            return (T)EnsureTypedEntityByType(entity, typeof(T));
        }

        public object EnsureTypedEntityByType(object entity, Type type)
        {
            if (entity != null)
            {
                var entityConfiguration = EntityConfigurationContext.GetEntityByType(type);
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
                                    EnsureTypedEntityByType(
                                        entity.GetPropertyValue(propertyName),
                                        relationship.Target.Type
                                    ));
                                break;
                            case RelationshipType.ManyToMany:
                                typedEntity.SetPropertyValue(propertyName,
                                    EnsureTypedListByType((IEnumerable)entity.GetPropertyValue(propertyName), relationship.Target.Type));
                                break;
                        }
                    }
                    else
                    {
                        switch (relationship.Type)
                        {
                            case RelationshipType.OneToOne:
                                typedEntity.SetPropertyValue(propertyName,
                                    EnsureTypedEntityByType(
                                        entity.GetPropertyValue(propertyName),
                                        relationship.Source.Type)
                                );
                                break;
                            case RelationshipType.OneToMany:
                            case RelationshipType.ManyToMany:
                                typedEntity.SetPropertyValue(propertyName,
                                    EnsureTypedListByType((IEnumerable)entity.GetPropertyValue(propertyName), relationship.Source.Type));
                                break;
                        }
                    }
                }
                entity = typedEntity;
            }
            return entity;
        }

        public IList<T> EnsureTypedList<T>(IEnumerable responseData, bool forceNotNull = false)
        {
            return (IList<T>)EnsureTypedListByType(responseData, typeof(T), forceNotNull);
        }

        public IList EnsureTypedListByType(IEnumerable responseData, Type type, bool forceNotNull = false)
        {
            var list = responseData != null || forceNotNull ?
                (IList)Activator.CreateInstance(typeof(DbList<>).MakeGenericType(type))
                : null;
            if (responseData != null)
            {
                foreach (var entity in responseData)
                {
                    var typedEntity = EnsureTypedEntityByType(entity, type);
                    list.Add(typedEntity);
                }
            }
            return list;
        }
    }
}