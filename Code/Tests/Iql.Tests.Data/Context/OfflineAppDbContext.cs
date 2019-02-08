using Iql.Data.DataStores.InMemory;
using Iql.OData;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Context
{
    public class OfflineAppDbContext : AppDbContext
    {
        public override bool UseStaticInMemoryData { get; } = false;

        public bool IsOffline
        {
            get => OfflinableDataStore?.IsOffline == true;
            set => OfflinableDataStore.IsOffline = value;
        }

        public OfflineAppDbContext()
        {
            DataStore = new OfflinableInMemoryDataStore(new InMemoryDataStore());
            // Load up with data
            var clients = OfflinableDataStore.GetDataSource<Client>();
            clients.Add(new Client
            {
                Id = 1,
                AverageIncome = 12,
                Name = "Coca-Cola",
                TypeId = 1
            });
            clients.Add(new Client
            {
                Id = 2,
                AverageIncome = 33,
                Name = "Pepsi",
                TypeId = 1
            });
            clients.Add(new Client
            {
                Id = 3,
                AverageIncome = 97,
                Name = "Microsoft",
                TypeId = 2
            });
            var otherClients = InMemoryDb.Clients;
            var clientTypes = OfflinableDataStore.GetDataSource<ClientType>();
            clientTypes.Add(new ClientType
            {
                Id = 1,
                Name = "Beverages"
            });
            clientTypes.Add(new ClientType
            {
                Id = 2,
                Name = "Software"
            });
        }

        public InMemoryDataStore OfflineDataStore => (InMemoryDataStore)DataStore?.OfflineDataStore;
        public OfflinableInMemoryDataStore OfflinableDataStore => (OfflinableInMemoryDataStore)DataStore;
    }
}