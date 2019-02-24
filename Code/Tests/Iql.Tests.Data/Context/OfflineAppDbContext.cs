using Iql.Data.DataStores.InMemory;
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
            DataStore = new OfflinableInMemoryDataStore();
            RefreshDisabled = true;
            EnableOffline = true;
            Reset();
        }

        public void Reset()
        {
            OfflineDataStore.Clear();
            OfflinableDataStore.Clear();
            OfflineDataTracker.Clear();
            TemporalDataTracker.Clear();
            
            // Load up with data
            var users = OfflinableDataStore.DataSet<ApplicationUser>();
            users.Clear();
            users.Add(new ApplicationUser
            {
                Id = "user1",
                ClientId = 1,
                UserName = "User 1"
            });
            users.Add(new ApplicationUser
            {
                Id = "user2",
                ClientId = 2,
                UserName = "User 2"
            });
            users.Add(new ApplicationUser
            {
                Id = "user3",
                ClientId = 3,
                UserName = "User 3"
            });
            var clients = OfflinableDataStore.DataSet<Client>();
            clients.Clear();
            clients.Add(new Client
            {
                Id = 1,
                AverageIncome = 12,
                Name = Client1Name,
                TypeId = Client1TypeId,
                CreatedByUserId = "user1"
            });
            clients.Add(new Client
            {
                Id = 2,
                AverageIncome = 33,
                Name = Client2Name,
                TypeId = Client2TypeId,
                CreatedByUserId = "user2"
            });
            clients.Add(new Client
            {
                Id = 3,
                AverageIncome = 97,
                Name = "Microsoft",
                TypeId = 2,
                CreatedByUserId = "user3"
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

        public InMemoryDataStore OfflineInMemoryDataStore => (InMemoryDataStore)OfflineDataStore;
        public OfflinableInMemoryDataStore OfflinableDataStore => (OfflinableInMemoryDataStore)DataStore;
    }
}