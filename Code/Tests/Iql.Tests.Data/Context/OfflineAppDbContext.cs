using Iql.Data.DataStores.InMemory;
using Iql.OData;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Context
{
    public class OfflineAppDbContext : AppDbContext
    {
        public bool IsOffline
        {
            get => OfflinableDataStore.IsOffline;
            set => OfflinableDataStore.IsOffline = value;
        }

        public OfflineAppDbContext() : base(new OfflinableInMemoryDataStore(new InMemoryDataStore()))
        {
            // Load up with data
            OfflinableDataStore.GetDataSource<Client>().Add(new Client
            {
                Id = 1,
                AverageIncome = 12,
                Name = "Coca-Cola",
                TypeId = 1
            });
            OfflinableDataStore.GetDataSource<Client>().Add(new Client
            {
                Id = 2,
                AverageIncome = 33,
                Name = "Pepsi",
                TypeId = 1

            });
            OfflinableDataStore.GetDataSource<Client>().Add(new Client
            {
                Id = 3,
                AverageIncome = 97,
                Name = "Microsoft",
                TypeId = 2

            });
            OfflinableDataStore.GetDataSource<ClientType>().Add(new ClientType
            {
                Id = 1,
                Name = "Beverages"
            });
            OfflinableDataStore.GetDataSource<ClientType>().Add(new ClientType
            {
                Id = 2,
                Name = "Software"
            });
        }

        public InMemoryDataStore OfflineDataStore => (InMemoryDataStore)DataStore.OfflineDataStore;
        public OfflinableInMemoryDataStore OfflinableDataStore => (OfflinableInMemoryDataStore)DataStore;
    }
}