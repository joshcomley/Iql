using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Data.Extensions;
using Iql.Entities;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Tests.Properties
{
    [TestClass]
    public class IqlPropertyPathTests : TestsBase
    {
        [TestMethod]
        public async Task ResolvePropertyPathWithKeys()
        {
            var person = new Person
            {
                ClientId = 19828
            };
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 19828,
                Name = "My Client 11",
                TypeId = 77
            });
            AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType
            {
                Id = 77,
                Name = "My Client Type 11"
            });
            var path = IqlPropertyPath.FromExpression<Person>(
                p => p.Client.Type,
                Db.EntityConfigurationContext
                );
            var result = await path.EvaluateAsync(person, Db, false);
            Assert.AreEqual((result.Value as ClientType).Name, "My Client Type 11");
        }

        [TestMethod]
        public void RelationshipPropertyPath()
        {
            Expression<Func<ApplicationUser, object>> exp = c => c.Client.Type.Name;
            var path = IqlPropertyPath.FromLambdaExpression(exp, Db.EntityConfigurationContext, Db.EntityConfigurationContext.EntityType<ApplicationUser>().TypeMetadata);
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
            var path = IqlPropertyPath.FromLambda<ApplicationUser>(u => u.Client.Type.Name, Db.EntityConfigurationContext);
            path.SetValue(user, "Hello");
            Assert.AreEqual("Hello", type.Name);
        }
    }
}