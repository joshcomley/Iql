using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class CrossDataContextPersistenceTests : TestsBase
    {
        [TestMethod]
        public async Task UpdatingAndSavingAnEntityInOneDataContextShouldResetItAcrossAllDataContexts()
        {
            var db1 = new AppDbContext();
            var db2 = new AppDbContext();
            db1.SynchronicityKey = "db";
            db2.SynchronicityKey = db1.SynchronicityKey;

            var client = new Client();
            client.Name = "My client";
            client.TypeId = 7;

            db1.Clients.Add(client);
            await db1.SaveChangesAsync();

            var db1Client = await db1.Clients.SingleAsync(c => c.Id == 1);
            var db1Clientb = await db1.Clients.SingleAsync(c => c.Id == 1);
            var db2Client = await db2.Clients.SingleAsync(c => c.Id == 1);

            Assert.AreEqual(client.Name, db1Client.Name);
            Assert.AreEqual(client.Name, db2Client.Name);
            Assert.AreSame(client, db1Client);
            Assert.AreSame(client, db1Clientb);
            Assert.AreNotSame(client, db2Client);

            client.Name = "My modified client";

            await db1.SaveChangesAsync();

            Assert.AreEqual(client.Name, db1Client.Name);
            Assert.AreEqual(client.Name, db2Client.Name);
        }

        [TestMethod]
        public async Task UpdatingAnEntityShouldFindRemoteEntity()
        {
            var db1 = new AppDbContext();
            var db2 = new AppDbContext();
            db1.SynchronicityKey = "db";
            db2.SynchronicityKey = db1.SynchronicityKey;

            var client = new Client();
            client.Name = "My client";
            client.TypeId = 7;

            db1.Clients.Add(client);
            var result = await db1.SaveChangesAsync();
            Assert.IsTrue(result.Success);

            client.Name = null;
            Assert.IsTrue(db1.HasChanges);
            var applicator = new SaveChangesApplicator(db1);
            await applicator.RemoveEntityIfEntityDoesNotExistInOnlineRemoteStoreAsync(client);
            Assert.IsTrue(db1.HasChanges);
        }

        [TestMethod]
        public async Task GettingTheLatestVersionOfAnEntityInOneDataContextShouldResetItAcrossAllDataContexts()
        {
            var db1 = new AppDbContext();
            var db2 = new AppDbContext();
            db1.SynchronicityKey = "db";
            db2.SynchronicityKey = db1.SynchronicityKey;

            var client = new Client();
            client.Name = "My client";
            client.TypeId = 7;

            db1.Clients.Add(client);
            await db1.SaveChangesAsync();

            var db1Client = await db1.Clients.SingleAsync(c => c.Id == 1);
            var db1Clientb = await db1.Clients.SingleAsync(c => c.Id == 1);
            var db2Client = await db2.Clients.SingleAsync(c => c.Id == 1);

            Assert.AreEqual(client.Name, db1Client.Name);
            Assert.AreEqual(client.Name, db2Client.Name);
            Assert.AreSame(client, db1Client);
            Assert.AreSame(client, db1Clientb);
            Assert.AreNotSame(client, db2Client);

            var newName = "My modified client";
            AppDbContext.InMemoryDb.Clients[0].Name = newName;
            await db1.Clients.SingleAsync(c => c.Id == 1);

            Assert.AreEqual(newName, db1Client.Name);
            Assert.AreEqual(newName, db2Client.Name);
            Assert.AreEqual(newName, client.Name);
        }

        [TestMethod]
        public async Task
            ModifyingARelationshipShouldUpdateInMemoryDatabaseRelationships()
        {
            // Set up
            var inMemoryDbClient1 = new Client { Id = 1, Name = "Test", TypeId = 1 };
            AppDbContext.InMemoryDb.Clients.Add(inMemoryDbClient1);
            AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 2, Name = "Test 2", TypeId = 2 });
            AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType { Id = 1, Name = "Type 1" });
            AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType { Id = 2, Name = "Type 2" });

            var clients = await Db.Clients.ToListAsync();
            var clientTypes = await Db.ClientTypes.ToListAsync();

            clients[0].TypeId = clientTypes[1].Id;

            Assert.AreEqual(1, inMemoryDbClient1.TypeId);
            await Db.SaveChangesAsync();
            Assert.AreEqual(2, inMemoryDbClient1.TypeId);
            Assert.AreEqual(0, clientTypes[0].Clients.Count);
            Assert.AreEqual(2, clientTypes[1].Clients.Count);
        }

        [TestMethod]
        public async Task GettingTheLatestVersionOfAnEntityViaExpandInOneDataContextShouldResetItAcrossAllDataContexts()
        {
            var db1 = new AppDbContext();
            var db2 = new AppDbContext();
            db1.SynchronicityKey = "db";
            db2.SynchronicityKey = db1.SynchronicityKey;

            var clienType = new ClientType();
            var client = new Client();
            client.Type = clienType;
            client.Name = "My client";

            db1.Clients.Add(client);
            await db1.SaveChangesAsync();

            var db2Clients = await db2.Clients.ToListAsync();
            var newName = "My modified client";
            AppDbContext.InMemoryDb.Clients[0].Name = newName;
            //var db1Clients = await db1.Clients.ToList();
            var db1ClientTypes = await db1.ClientTypes.Expand(c => c.Clients).ToListAsync();

            Assert.AreEqual(client.Name, client.Name);
            Assert.AreEqual(client.Name, db2Clients[0].Name);
            Assert.AreEqual(client.Name, db1ClientTypes[0].Clients[0].Name);
        }

        [TestMethod]
        public async Task PersistingAnEntityDeletionFromOneSycnrhonisedDataContextShouldRemoveFromRelatedDataContexts()
        {
            var db1 = new AppDbContext();
            var db2 = new AppDbContext();
            db1.SynchronicityKey = "db";
            db2.SynchronicityKey = db1.SynchronicityKey;

            var clientType = new ClientType();
            clientType.Name = "My new type";

            db1.ClientTypes.Add(clientType);

            await db1.SaveChangesAsync();

            var db1Clients = await db1.ClientTypes.ToListAsync();
            var db2Clients = await db2.ClientTypes.ToListAsync();

            Assert.AreEqual(1, db1Clients.Count);
            Assert.AreEqual(db1Clients.Count, db2Clients.Count);

            db1.ClientTypes.Delete(clientType);

            await db1.SaveChangesAsync();

            Assert.AreEqual(0, db1Clients.Count);
            Assert.AreEqual(db1Clients.Count, db2Clients.Count);
        }
    }
}