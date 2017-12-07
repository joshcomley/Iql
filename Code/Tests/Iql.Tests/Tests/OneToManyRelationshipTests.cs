using System.Linq;
using System.Threading.Tasks;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class OneToManyRelationshipTests : TestsBase
    {
        [TestMethod]
        public async Task ExpandShouldIncludeExpandedEntities()
        {
            InsertData();
            var clientTypes = await Db.ClientTypes.Expand(ct => ct.Clients).ToList();
            var clientType1 = clientTypes.Single(ct => ct.Id == 72);
            var clientType2 = clientTypes.Single(ct => ct.Id == 73);
            Assert.AreEqual(2, clientTypes.Count);
            Assert.AreEqual(2, clientType1.Clients.Count);
            Assert.AreEqual(0, clientType2.Clients.Count);
        }

        [TestMethod]
        public async Task FetchingAChangedTargetShouldPersistChangesToChildCollections()
        {
            InsertData();
            var clientTypes = await Db.ClientTypes.Expand(ct => ct.Clients).ToList();
            var clientType1 = clientTypes.Single(ct => ct.Id == 72);
            var clientType2 = clientTypes.Single(ct => ct.Id == 73);
            var client = clientType1.Clients[0];
            Assert.AreEqual(2, clientTypes.Count);
            Assert.AreEqual(2, clientType1.Clients.Count);
            Assert.AreEqual(0, clientType2.Clients.Count);

            // Modify DB
            AppDbContext.InMemoryDb.Clients.Single(c => c.Id == 7).TypeId = clientType2.Id;

            await Db.RefreshEntity(client, typeof(Client));

            Assert.AreEqual(clientType2.Id, client.TypeId);
            Assert.AreEqual(1, clientType1.Clients.Count);
            Assert.AreEqual(1, clientType2.Clients.Count);

            var updatedClient = await Db.Clients.WithKey(7);
            Assert.AreEqual(updatedClient, client);
        }

        private static void InsertData()
        {
            AppDbContext.InMemoryDb.Clients.Add(
                new Client
                {
                    Id = 7,
                    TypeId = 72
                });
            AppDbContext.InMemoryDb.Clients.Add(
                new Client
                {
                    Id = 8,
                    TypeId = 72
                });
            AppDbContext.InMemoryDb.ClientTypes.Add(
                new ClientType
                {
                    Id = 72,
                });
            AppDbContext.InMemoryDb.ClientTypes.Add(
                new ClientType
                {
                    Id = 73,
                });
        }
    }
}