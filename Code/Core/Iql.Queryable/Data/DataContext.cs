using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var properties = GetType().GetProperties()
                .Where(p => typeof(IDbSet).IsAssignableFrom(p.PropertyType))
                .ToList();
            foreach (var property in properties)
            {
                property.SetValue(this, AsDbSetByType(property.PropertyType.GenericTypeArguments[0]));
            }
        }

        public IDataStore DataStore { get; set; }
        public EvaluateContext EvaluateContext { get; set; }
        public EntityConfigurationBuilder EntityConfigurationContext { get; set; }

        public void RegisterConfiguration<T>(T configuration)
            where T : class
        {
            _configurations.Add(ConfigurationName<T>(), configuration);
        }

        public T GetConfiguration<T>() where T : class
        {
            if (!_configurations.ContainsKey(ConfigurationName<T>()))
            {
                throw new Exception($"No configuration found for \"{ConfigurationName<T>()}\"");
            }
            return _configurations[ConfigurationName<T>()] as T;
        }

        public IDbSet AsDbSetByType(Type entityType)
        {
            return (IDbSet) GetType().GetMethod(nameof(AsDbSet))
                .MakeGenericMethod(entityType)
                .Invoke(this, null);
        }

        private bool _configured;
        public DbSet<T, TKey> AsDbSet<T, TKey>() where T : class
        {
            if (!_configured)
            {
                _configured = true;
                Configure(EntityConfigurationContext);
            }
            return new DbSet<T, TKey>(
                EntityConfigurationContext,
                () => DataStore,
                EvaluateContext,
                this);
        }

        private static string ConfigurationName<T>() where T : class
        {
            return typeof(T).Name;
        }

        public virtual void Configure(EntityConfigurationBuilder builder)
        {
        }

        public void RegisterConfig<T>(Type configurationType, T config)
            where T : IEntityConfiguration
        {
            _configurations[configurationType.Name] = config;
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