using Iql.DotNet.Queryable;
using Iql.Queryable.Data.DataStores.InMemory;

namespace Iql.Tests.Context
{
    public class AppDbContext : ISiteDataContextBase
    {
        static AppDbContext()
        {
            InMemoryDataStoreConfiguration = new InMemoryDataStoreConfiguration();
            var inMemoryDb = new InMemoryDataBase();
            InMemoryDb = inMemoryDb;
        }

        public static InMemoryDataStoreConfiguration InMemoryDataStoreConfiguration { get; set; }

        public AppDbContext() : base(new InMemoryDataStore(new DotNetQueryableAdapter()))
        {
            RegisterConfiguration(InMemoryDataStoreConfiguration);
            InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.ClientTypes);
            InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.Clients);
        }

        public static InMemoryDataBase InMemoryDb { get; set; }
    }
}
