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
    }
}