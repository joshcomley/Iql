using Iql.Data.DataStores;
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

        public static string Client1Name = "Coca-Cola";
        public static int Client1TypeId = 1;
        public static string Client2Name = "Pepsi";
        public static int Client2TypeId = 1;
        public OfflineAppDbContext()
        {
            DataStore = new OfflinableInMemoryDataStore(new InMemoryDataStore());
            Reset();
            RefreshDisabled = true;
        }

        public void Reset()
        {
            OfflineDataStore.Clear();
            OfflinableDataStore.Clear();
            OfflineDataTracker.Clear();
            TemporalDataTracker.Clear();
            
            // Load up with data
            var clients = OfflinableDataStore.DataSet<Client>();
            clients.Clear();
            clients.Add(new Client
            {
                Id = 1,
                AverageIncome = 12,
                Name = Client1Name,
                TypeId = Client1TypeId
            });
            clients.Add(new Client
            {
                Id = 2,
                AverageIncome = 33,
                Name = Client2Name,
                TypeId = Client2TypeId
            });
            clients.Add(new Client
            {
                Id = 3,
                AverageIncome = 97,
                Name = "Microsoft",
                TypeId = 2
            });
            var clientTypes = OfflinableDataStore.DataSet<ClientType>();
            clientTypes.Clear();
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
            var sites = OfflinableDataStore.DataSet<Site>();
            sites.Clear();
            sites.Add(new Site
            {
                Name = "Berlin",
                Location = new IqlPointExpression(13.2846516, 52.5069704)
            });
        }

        public InMemoryDataStore OfflineDataStore => (InMemoryDataStore)DataStore?.OfflineDataStore;
        public OfflinableInMemoryDataStore OfflinableDataStore => (OfflinableInMemoryDataStore)DataStore;
    }
}