using System.Threading.Tasks;
using Iql.Data.Extensions;
using Iql.Entities.Services;
using Iql.Tests.Context;
using Iql.Tests.Tests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Tests.Properties
{
    [TestClass]
    public class PropertyTests : TestsBase
    {
        [TestMethod]
        public async Task TestPopulateInferredValue()
        {
            var person = new Person();
            person.SiteId = 87;
            AppDbContext.InMemoryDb.Sites.Add(new Site
            {
                Id = 87,
                Name = "My site",
                ClientId = 107
            });
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 107,
                Name = "My client"
            });

            await PropertyExtensions.TrySetInferredValuesAsync(person, Db);
            Assert.AreEqual(null, person.CreatedByUserId);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            await PropertyExtensions.TrySetInferredValuesAsync(person, Db);
            Assert.AreEqual(null, person.CreatedByUserId);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            Db.ServiceProvider.RegisterInstance(new TestCurrentUserResolver());
            await PropertyExtensions.TrySetInferredValuesAsync(person, Db);
            Assert.AreEqual("testuserid", person.CreatedByUserId);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            Db.ServiceProvider.Unregister<TestCurrentUserResolver>();
            await PropertyExtensions.TrySetInferredValuesAsync(person, Db);
            Assert.AreEqual(null, person.CreatedByUserId);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            Db.ServiceProvider.RegisterInstance(new TestCurrentUserResolver());
            await PropertyExtensions.TrySetInferredValuesAsync(person, Db);
            Assert.AreEqual("testuserid", person.CreatedByUserId);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            Db.ServiceProvider.Unregister<IqlCurrentUserService>();
            await PropertyExtensions.TrySetInferredValuesAsync(person, Db);
            Assert.AreEqual(null, person.CreatedByUserId);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            Db.ServiceProvider.RegisterInstance<IqlCurrentUserService>(new TestCurrentUserResolver());
            await PropertyExtensions.TrySetInferredValuesAsync(person, Db);
            Assert.AreEqual("testuserid", person.CreatedByUserId);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            Db.ServiceProvider.Unregister<IqlCurrentUserService>();
            await PropertyExtensions.TrySetInferredValuesAsync(person, Db);
            Assert.AreEqual(null, person.CreatedByUserId);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);
        }

        [TestMethod]
        public void TestPropertyResolveFriendlyName()
        {
            var config = Db.EntityConfigurationContext.EntityType<Client>();
            var property = config.FindPropertyByExpression(c => c.CreatedDate);
            Assert.AreEqual(nameof(Client.CreatedDate), property.Name);
            Assert.AreEqual("Created Date", property.FriendlyName);
        }

        [TestMethod]
        public void TestPropertyNullability()
        {
            Assert.AreEqual(false,
                Db.EntityConfigurationContext.EntityType<Client>().FindPropertyByExpression(p => p.CreatedDate)
                    .TypeDefinition.Nullable);
        }

        [TestMethod]
        public void TestPropertyIqlTypeString()
        {
            Assert.AreEqual(IqlType.String,
                Db.EntityConfigurationContext.EntityType<Client>().FindPropertyByExpression(p => p.Name)
                    .TypeDefinition.Kind);
        }

        [TestMethod]
        public void TestPropertyIqlTypeDate()
        {
            Assert.AreEqual(IqlType.Date,
                Db.EntityConfigurationContext.EntityType<Client>().FindPropertyByExpression(p => p.CreatedDate)
                    .TypeDefinition.Kind);
        }

        [TestMethod]
        public void TestPropertyIqlTypeInteger()
        {
            Assert.AreEqual(IqlType.Integer,
                Db.EntityConfigurationContext.EntityType<Client>().FindPropertyByExpression(p => p.Id)
                    .TypeDefinition.Kind);
        }

        [TestMethod]
        public void TestPropertyIqlTypeDecimalFromDecimal()
        {
            Assert.AreEqual(IqlType.Decimal,
                Db.EntityConfigurationContext.EntityType<Client>().FindPropertyByExpression(p => p.Discount)
                    .TypeDefinition.Kind);
        }

        [TestMethod]
        public void TestPropertyIqlTypeDecimalFromFloat()
        {
            Assert.AreEqual(IqlType.Decimal,
                Db.EntityConfigurationContext.EntityType<Client>().FindPropertyByExpression(p => p.AverageSales)
                    .TypeDefinition.Kind);
        }

        [TestMethod]
        public void TestPropertyIqlTypeDecimalFromDouble()
        {
            Assert.AreEqual(IqlType.Decimal,
                Db.EntityConfigurationContext.EntityType<Client>().FindPropertyByExpression(p => p.AverageIncome)
                    .TypeDefinition.Kind);
        }

        [TestMethod]
        public void TestPropertyIqlTypeBoolean()
        {
            Assert.AreEqual(IqlType.Boolean,
                Db.EntityConfigurationContext.EntityType<ApplicationUser>().FindPropertyByExpression(p => p.EmailConfirmed)
                    .TypeDefinition.Kind);
        }
    }
}