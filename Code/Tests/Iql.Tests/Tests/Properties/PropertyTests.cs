using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests.Properties
{
    [TestClass]
    public class PropertyTests :TestsBase
    {
        [TestMethod]
        public void TestPropertyResolveFriendlyName()
        {
            var config = Db.EntityConfigurationContext.EntityType<Client>();
            var property = config.FindPropertyByExpression(c => c.CreatedDate);
            Assert.AreEqual(nameof(Client.CreatedDate), property.Name);
            Assert.AreEqual("Created Date", property.ResolveFriendlyName());
        }
    }
}