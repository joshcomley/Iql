using System.Threading.Tasks;
using Iql.Data.Configuration;
using Iql.Data.Configuration.Extensions;
using Iql.Queryable.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class CompositeKeyTests : TestsBase
    {
        [TestMethod]
        public async Task ConvertingCompositeKeyFromKeyStringWithNames()
        {
            var client = new Client();
            client.Id = 7;
            var entityConfiguration = Db.EntityConfigurationContext.EntityType<Client>();
            var keyString = entityConfiguration.GetCompositeKey(client).AsKeyString(true);
            var compositeKey = CompositeKey.FromKeyString(keyString, entityConfiguration);
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
            var compositeKey = CompositeKey.FromKeyString(keyString, entityConfiguration);
            Assert.AreEqual(1, compositeKey.Keys.Length);
            Assert.AreEqual(nameof(Client.Id), compositeKey.Keys[0].Name);
            Assert.AreEqual(7, compositeKey.Keys[0].Value);
        }
    }
}