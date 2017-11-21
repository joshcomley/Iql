using System.Collections.Generic;
using Iql.DotNet.Queryable;
using Iql.Queryable.Data.DataStores.InMemory;

namespace Iql.Tests.Context
{
    public class AppDbContext : ISiteDataContextBase
    {
        public AppDbContext() : base(new InMemoryDataStore(new DotNetQueryableAdapter()))
        {
            var inMemoryDataStoreConfiguration = new InMemoryDataStoreConfiguration();
            RegisterConfiguration(inMemoryDataStoreConfiguration);

            var inMemoryDb = new InMemoryDataBase();
            this.InMemoryDb = inMemoryDb;
            inMemoryDataStoreConfiguration.RegisterSource(() => inMemoryDb.ClientTypes);
        }

        public InMemoryDataBase InMemoryDb { get; set; }
    }
}
