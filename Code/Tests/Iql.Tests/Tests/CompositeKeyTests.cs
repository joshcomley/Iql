﻿using System.Threading.Tasks;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Queryable.Extensions;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class CompositeKeyTests : TestsBase
    {
        [TestMethod]
        public async Task EnsureCompositeKeyWithEntityConfiguration()
        {
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 7,
                Name = "Test client",
                TypeId = 2
            });
            var client = await Db.Clients.GetWithKeyAsync(7);
            var compositeKey = CompositeKey.EnsureWithBuilder(client, Db.EntityConfigurationContext);
            var expectedFullKeyString = "Client>Id:7";
            Assert.AreEqual(expectedFullKeyString, compositeKey.FullKeyString);
            Assert.AreEqual(expectedFullKeyString, CompositeKey.EnsureWithBuilder(client, Db.EntityConfigurationContext).FullKeyString);
            Assert.AreEqual(expectedFullKeyString, CompositeKey.EnsureWithBuilder(compositeKey, Db.EntityConfigurationContext).FullKeyString);
            Assert.AreEqual(expectedFullKeyString, CompositeKey.EnsureWithBuilder(expectedFullKeyString, Db.EntityConfigurationContext).FullKeyString);

        }

        [TestMethod]
        public async Task ConvertingCompositeKeyFromKeyStringWithNames()
        {
            var client = new Client();
            client.Id = 7;
            var entityConfiguration = Db.EntityConfigurationContext.EntityType<Client>();
            var keyString = entityConfiguration.GetCompositeKey(client).AsKeyString(true);
            var compositeKey = CompositeKey.FromKeyString(keyString, entityConfiguration.Builder);
            Assert.AreEqual(1, compositeKey.Keys.Length);
            Assert.AreEqual(nameof(Client.Id), compositeKey.Keys[0].Name);
            Assert.AreEqual(7, compositeKey.Keys[0].Value);
        }

        [TestMethod]
        public async Task ConvertingCompositeKeyFromFullKeyString()
        {
            var client = new Client();
            client.Id = 7;
            var entityConfiguration = Db.EntityConfigurationContext.EntityType<Client>();
            var keyString = entityConfiguration.GetCompositeKey(client).AsKeyString(true);
            var compositeKey = CompositeKey.FromKeyString(keyString, entityConfiguration.Builder);
            Assert.AreEqual(entityConfiguration.TypeName, compositeKey.TypeName);
            Assert.AreEqual(1, compositeKey.Keys.Length);
            Assert.AreEqual(nameof(Client.Id), compositeKey.Keys[0].Name);
            Assert.AreEqual(7, compositeKey.Keys[0].Value);
        }

        [TestMethod]
        public async Task ConvertingCompositeKeyFromKeyStringWithoutNames()
        {
            var client = new Client();
            client.Id = 7;
            var entityConfiguration = Db.EntityConfigurationContext.EntityType<Client>();
            var keyString = entityConfiguration.GetCompositeKey(client).AsKeyString(false);
            var compositeKey = CompositeKey.FromKeyString(keyString, entityConfiguration.Builder);
            Assert.AreEqual(1, compositeKey.Keys.Length);
            Assert.AreEqual(nameof(Client.Id), compositeKey.Keys[0].Name);
            Assert.AreEqual(7, compositeKey.Keys[0].Value);
        }
    }
}