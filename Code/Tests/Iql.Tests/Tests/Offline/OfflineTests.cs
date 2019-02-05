using System.Linq;
using System.Threading.Tasks;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests.Offline
{
    [TestClass]
    public class OfflineTests
    {
        private static OfflineAppDbContext _db = new OfflineAppDbContext();

        public static OfflineAppDbContext Db => _db;

        [TestMethod]
        public async Task SerializeState()
        {
            Db.IsOffline = false;
            var clients = await Db.Clients.Expand(_ => _.Type).ToListAsync();
            var client = clients.First();
            client.Name = "Changed";
            var entityState = Db.GetEntityState(client);
            var json = Db.DataStore.Tracking.SerializeToJson();
        }

        [TestMethod]
        public async Task GetDataWhenOnlineAndRefetchWhenOffline()
        {
            Db.IsOffline = true;
            var clientsOffline1 = await Db.Clients.ToListAsync();
            Assert.AreEqual(0, clientsOffline1.Count);

            Db.IsOffline = false;
            var clients = await Db.Clients.Expand(_ => _.Type).ToListAsync();
            Assert.AreEqual(3, clients.Count);

            Db.IsOffline = true;
            var clientsOffline2 = await Db.Clients.Expand(_ => _.Type).ToListAsync();
            Assert.AreEqual(3, clientsOffline2.Count);
        }

        [TestMethod]
        public async Task SaveChangeWhenOfflineAndResyncWhenOnline()
        {
            Db.IsOffline = false;
            var clients = await Db.Clients.Expand(_ => _.Type).ToListAsync();
            Assert.AreEqual(3, clients.Count);

            Db.IsOffline = true;
            var client = clients.First();
            client.Name = "Changed";
            var result = await Db.SaveChangesAsync();

            Assert.AreEqual(true, result.Success);

            //Db.IsOffline = true;
            //var clientsOffline2 = await Db.Clients.Expand(_ => _.Type).ToListAsync();
            //Assert.AreEqual(3, clientsOffline2.Count);
        }
    }
}
