using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Extensions;
using Iql.Parsing;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.EntityConfiguration;

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
            return (IDbSet) GetType().GetMethod(nameof(AsDbSet))
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
    }
}