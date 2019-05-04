using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Tests.Context;
using IqlSampleApp.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class AbandonChangesTests : TestsBase
    {
        [TestMethod]
        public async Task TestGlobalAbandonEntityEvent()
        {
            var abandonEntityChangesCount = 0;
            var db = new AppDbContext();
            StateEvents.AbandonedEntityChanges.Subscribe(e =>
            {
                Assert.AreEqual(db, e.DataContext);
                abandonEntityChangesCount++;
            });
            var clientType = new ClientType();
            var client = new Client();
            client.Type = clientType;
            client.Name = "My client";

            db.Clients.Add(client);
            db.AbandonChanges();
            Assert.AreEqual(2, abandonEntityChangesCount);
        }
    }
}