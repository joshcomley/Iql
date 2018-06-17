using Iql.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests.Properties
{
    [TestClass]
    public class IqlPropertyPathTests : TestsBase
    {
        [TestMethod]
        public void TestPropertySetterOnNestedProperty()
        {
            var user = new ApplicationUser();
            var client = new Client();
            var type = new ClientType();
            user.Client = client;
            client.Type = type;
            type.Name = "Old name";
            Assert.AreEqual("Old name", type.Name);
            var path = IqlPropertyPath.FromLambda(u => u.Client.Type.Name, Db.EntityConfigurationContext.EntityType<ApplicationUser>());
            path.SetValue(user, "Hello");
            Assert.AreEqual("Hello", type.Name);
        }
    }
}