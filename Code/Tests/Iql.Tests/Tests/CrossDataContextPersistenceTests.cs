using System.Threading.Tasks;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;

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

            var client = new Client();
            client.Name = "My client";

            db1.Clients.Add(client);
            await db1.SaveChanges();

            var db1Client = await db1.Clients.Single(c => c.Id == 1);
            var db1Clientb = await db1.Clients.Single(c => c.Id == 1);
            var db2Client = await db2.Clients.Single(c => c.Id == 1);

            Assert.AreEqual(client.Name, db1Client.Name);
            Assert.AreEqual(client.Name, db2Client.Name);
            Assert.AreSame(client, db1Client);
            Assert.AreSame(client, db1Clientb);
            Assert.AreNotSame(client, db2Client);

            client.Name = "My modified client";

            await db1.SaveChanges();

            Assert.AreEqual(client.Name, db1Client.Name);
            Assert.AreEqual(client.Name, db2Client.Name);
        }
        [TestMethod]
        public async Task GettingTheLatestVersionOfAnEntityInOneDataContextShouldResetItAcrossAllDataContexts()
        {
            var db1 = new AppDbContext();
            var db2 = new AppDbContext();

            var client = new Client();
            client.Name = "My client";

            db1.Clients.Add(client);
            await db1.SaveChanges();

            var db1Client = await db1.Clients.Single(c => c.Id == 1);
            var db1Clientb = await db1.Clients.Single(c => c.Id == 1);
            var db2Client = await db2.Clients.Single(c => c.Id == 1);

            Assert.AreEqual(client.Name, db1Client.Name);
            Assert.AreEqual(client.Name, db2Client.Name);
            Assert.AreSame(client, db1Client);
            Assert.AreSame(client, db1Clientb);
            Assert.AreNotSame(client, db2Client);

            var newName = "My modified client";
            AppDbContext.InMemoryDb.Clients[0].Name = newName;
            await db1.Clients.Single(c => c.Id == 1);

            Assert.AreEqual(newName, db1Client.Name);
            Assert.AreEqual(newName, db2Client.Name);
            Assert.AreEqual(newName, client.Name);
        }

        [TestMethod]
        public async Task
            ModifyingARelationshipShouldUpdateInMemoryDatabaseRelationships()
        {
            // Set up
            var inMemoryDbClient1 = new Client {Id = 1, Name = "Test", TypeId = 1};
            AppDbContext.InMemoryDb.Clients.Add(inMemoryDbClient1);
            AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 2, Name = "Test 2", TypeId = 2});
            AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType { Id = 1, Name = "Type 1" });
            AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType { Id = 2, Name = "Type 2" });

            var clients = await Db.Clients.ToList();
            var clientTypes = await Db.ClientTypes.ToList();

            clients[0].TypeId = clientTypes[1].Id;

            Assert.AreEqual(1, inMemoryDbClient1.TypeId);
            await Db.SaveChanges();
            Assert.AreEqual(2, inMemoryDbClient1.TypeId);
            Assert.AreEqual(0, clientTypes[0].Clients.Count);
            Assert.AreEqual(2, clientTypes[1].Clients.Count);
        }

        [TestMethod]
        public async Task GettingTheLatestVersionOfAnEntityViaExpandInOneDataContextShouldResetItAcrossAllDataContexts()
        {
            var db1 = new AppDbContext();
            var db2 = new AppDbContext();

            var clienType = new ClientType();
            var client = new Client();
            client.Type = clienType;
            client.Name = "My client";

            db1.Clients.Add(client);
            await db1.SaveChanges();

            var db2Clients = await db2.Clients.ToList();
            var newName = "My modified client";
            AppDbContext.InMemoryDb.Clients[0].Name = newName;
            //var db1Clients = await db1.Clients.ToList();
            var db1ClientTypes = await db1.ClientTypes.Expand(c => c.Clients).ToList();

            Assert.AreEqual(client.Name, client.Name);
            Assert.AreEqual(client.Name, db2Clients[0].Name);
            Assert.AreEqual(client.Name, db1ClientTypes[0].Clients[0].Name);
            //Assert.AreSame(client, db1Client);
            //Assert.AreSame(client, db1Clientb);
            //Assert.AreNotSame(client, db2Client);

            //var newName = "My modified client";
            //AppDbContext.InMemoryDb.Clients[0].Name = newName;
            //await db1.Clients.Single(c => c.Id == 1);

            //Assert.AreEqual(newName, db1Client.Name);
            //Assert.AreEqual(newName, db2Client.Name);
            //Assert.AreEqual(newName, client.Name);
        }
    }
}