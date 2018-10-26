using System;
using System.Linq.Expressions;
using Iql.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Tests.Properties
{
    [TestClass]
    public class IqlPropertyPathTests : TestsBase
    {
        [TestMethod]
        public void RelationshipPropertyPath()
        {
            Expression<Func<ApplicationUser, object>> exp = c => c.Client.Type.Name;
            var path = IqlPropertyPath.FromLambdaExpression(exp, Db.EntityConfigurationContext.EntityType<ApplicationUser>());
            path.Separator = ".";
            Assert.AreEqual("Client.Type", path.RelationshipPathToHere);
        }

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