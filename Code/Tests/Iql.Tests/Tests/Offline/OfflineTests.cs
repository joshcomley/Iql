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
            var clients = await Db.Clients.ToListAsync();
            Db.IsOffline = true;
            var clientsOffline = await Db.Clients.ToListAsync();
        }
    }
}
