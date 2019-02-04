using System;
using System.Collections.Generic;
using System.Text;
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
        public async Task GetDataWhenOnlineAndRefetchWhenOffline()
        {
            Db.IsOffline = true;
            var clientsOffline1 = await Db.Clients.ToListAsync();
            Assert.AreEqual(0, clientsOffline1.Count);

            Db.IsOffline = false;
            var clients = await Db.Clients.ToListAsync();
            Assert.AreEqual(3, clients.Count);

            Db.IsOffline = true;
            var clientsOffline2 = await Db.Clients.ToListAsync();
            Assert.AreEqual(3, clientsOffline2.Count);
        }
    }
}
