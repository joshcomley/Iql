﻿using System.Linq;
using System.Threading.Tasks;
using Iql.Conversion.State;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

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
            client.TypeId = 2;
            client.Type.Name = "A new name";
            var entityState = Db.GetEntityState(client);
            var json = Db.Tracking.SerializeToJson();
            var state = JsonConvert.DeserializeObject(json, typeof(TrackingState));
        }

        [TestMethod]
        public async Task GetDataWhenOnlineAndRefetchWhenOffline()
        {
            Db.OfflineDataStore.Clear();
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
            Db.OfflineDataStore.Clear();
            Db.IsOffline = false;
            var clients = await Db.Clients.Expand(_ => _.Type).ToListAsync();
            Assert.AreEqual(3, clients.Count);

            Db.IsOffline = true;
            var client = clients.First();
            client.Name = "Changed";
            var result = await Db.SaveChangesAsync();
            Assert.AreEqual(true, result.Success);
            var changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Length);
            var offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(1, offlineChanges.Length);
            client.TypeId = 2;
            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(1, offlineChanges.Length);
            changes = Db.GetChanges();
            Assert.AreEqual(1, changes.Length);
            result = await Db.SaveChangesAsync();
            Assert.AreEqual(true, result.Success);
            changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Length);
            Assert.AreEqual(2, offlineChanges.Length);
            //Db.IsOffline = true;
            //var clientsOffline2 = await Db.Clients.Expand(_ => _.Type).ToListAsync();
            //Assert.AreEqual(3, clientsOffline2.Count);
        }
    }
}