using System;
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
        public async Task InferredWithConversionToStringTest()
        {
            var site = new Site();
            site.ClientId = 7;
            site.Address = "A\nB";
            site.PostCode = "DEF";
            Assert.IsTrue(await Db.TrySetInferredValuesAsync(site));
            Assert.AreEqual("7", site.Key);
            Assert.AreEqual("A\nB\nDEF", site.FullAddress);
        }

        [TestMethod]
        public async Task PopulateNewExistingInferredWithValueTest()
        {
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 177,
                SiteId = 87,
                Title = "My person"
            });
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
            var person = await Db.People.GetWithKeyAsync(177);
            Assert.IsTrue(await Db.TrySetInferredValuesAsync(person));
            Assert.AreEqual(null, person.CreatedByUserId);
            Assert.AreEqual(null, person.Location);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            Assert.IsTrue(await Db.TrySetInferredValuesAsync(person));
            Assert.AreEqual(null, person.CreatedByUserId);
            Assert.AreEqual(null, person.Location);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            Db.ServiceProvider.RegisterInstance(new TestCurrentUserResolver());
            Db.ServiceProvider.RegisterInstance(new TestCurrentLocationResolver());
            Assert.IsTrue(await Db.TrySetInferredValuesAsync(person));
            Assert.AreEqual(null, person.Location);
            Assert.AreEqual(null, person.CreatedByUserId);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            var currentLatitude = 51.5054597;
            TestCurrentLocationResolver.CurrentLatitude = currentLatitude;
            var currentLongitude = -0.0775452;
            TestCurrentLocationResolver.CurrentLongitude = currentLongitude;
            Db.ServiceProvider.Unregister<TestCurrentUserResolver>();

            Assert.IsTrue(await Db.TrySetInferredValuesAsync(person));
            Assert.AreEqual(null, person.CreatedByUserId);
            Assert.IsNotNull(person.Location);
            Assert.AreEqual(TestCurrentLocationResolver.CurrentLongitude, person.Location.X);
            Assert.AreEqual(TestCurrentLocationResolver.CurrentLatitude, person.Location.Y);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);
            var location = person.Location;

            TestCurrentLocationResolver.CurrentLatitude = 41.5054597;
            TestCurrentLocationResolver.CurrentLongitude = -1.0775452;

            Db.ServiceProvider.RegisterInstance(new TestCurrentUserResolver());
            Assert.IsTrue(await Db.TrySetInferredValuesAsync(person));
            Assert.AreEqual(null, person.CreatedByUserId);
            Assert.AreEqual(location, person.Location);
            Assert.AreEqual(currentLongitude, person.Location.X);
            Assert.AreEqual(currentLatitude, person.Location.Y);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            Db.ServiceProvider.Unregister<IqlCurrentUserService>();
            Assert.IsTrue(await Db.TrySetInferredValuesAsync(person));
            Assert.AreEqual(null, person.CreatedByUserId);
            Assert.AreEqual(location, person.Location);
            Assert.AreEqual(currentLongitude, person.Location.X);
            Assert.AreEqual(currentLatitude, person.Location.Y);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            Db.ServiceProvider.RegisterInstance<IqlCurrentUserService>(new TestCurrentUserResolver());
            person.Location = null;
            Assert.IsTrue(await Db.TrySetInferredValuesAsync(person));
            Assert.AreEqual(null, person.CreatedByUserId);
            Assert.IsNotNull(person.Location);
            Assert.AreEqual(TestCurrentLocationResolver.CurrentLongitude, person.Location.X);
            Assert.AreEqual(TestCurrentLocationResolver.CurrentLatitude, person.Location.Y);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            Db.ServiceProvider.Unregister<IqlCurrentUserService>();
            Assert.IsTrue(await Db.TrySetInferredValuesAsync(person));
            Assert.AreEqual(null, person.CreatedByUserId);
            Assert.IsNotNull(person.Location);
            Assert.AreEqual(TestCurrentLocationResolver.CurrentLongitude, person.Location.X);
            Assert.AreEqual(TestCurrentLocationResolver.CurrentLatitude, person.Location.Y);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);
            Db.ServiceProvider.Unregister<IqlCurrentUserService>();
            Db.ServiceProvider.Unregister<IqlCurrentLocationService>();
        }

        [TestMethod]
        public async Task PopulateNewEntityInferredWithValueTest()
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

            Assert.IsTrue(await Db.TrySetInferredValuesAsync(person));
            Assert.IsTrue(person.CreatedDate > DateTimeOffset.Now.AddSeconds(-10));
            Assert.AreEqual(null, person.CreatedByUserId);
            Assert.AreEqual(null, person.Description);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            person.Category = PersonCategory.AutoDescription;
            Assert.IsTrue(await Db.TrySetInferredValuesAsync(person));
            Assert.AreEqual("I'm \\ \"auto\"", person.Description);
            Assert.AreEqual(null, person.CreatedByUserId);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            Db.ServiceProvider.RegisterInstance(new TestCurrentUserResolver());
            Assert.IsTrue(await Db.TrySetInferredValuesAsync(person));
            Assert.AreEqual("testuserid", person.CreatedByUserId);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            Db.ServiceProvider.Unregister<TestCurrentUserResolver>();
            Assert.IsTrue(await Db.TrySetInferredValuesAsync(person));
            Assert.AreEqual("testuserid", person.CreatedByUserId);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            Db.ServiceProvider.RegisterInstance(new TestCurrentUserResolver());
            Assert.IsTrue(await Db.TrySetInferredValuesAsync(person));
            Assert.AreEqual("testuserid", person.CreatedByUserId);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            Db.ServiceProvider.Unregister<IqlCurrentUserService>();
            Assert.IsTrue(await Db.TrySetInferredValuesAsync(person));
            Assert.AreEqual("testuserid", person.CreatedByUserId);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            Db.ServiceProvider.RegisterInstance<IqlCurrentUserService>(new TestCurrentUserResolver());
            Assert.IsTrue(await Db.TrySetInferredValuesAsync(person));
            Assert.AreEqual("testuserid", person.CreatedByUserId);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            Db.ServiceProvider.Unregister<IqlCurrentUserService>();
            Assert.IsTrue(await Db.TrySetInferredValuesAsync(person));
            Assert.AreEqual("testuserid", person.CreatedByUserId);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);
            Db.ServiceProvider.Unregister<IqlCurrentUserService>();
            Db.ServiceProvider.Unregister<IqlCurrentLocationService>();
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