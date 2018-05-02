using System.Linq;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests.EntityConfiguration
{
    [TestClass]
    public class EntityConfigurationTests : TestsBase
    {
        [TestMethod]
        public void ResolveTypeFromTypeName()
        {
            var type = EntityConfigurationBuilder.FindEntityTypeFromName(nameof(ApplicationUser));
            Assert.AreEqual(typeof(ApplicationUser), type);
        }

        [TestMethod]
        public void GetDeepPropertyFromString()
        {
                var propertyPath = string.Join("/", new[]
                {
                    nameof(Person.Type),
                    nameof(PersonType.CreatedByUser),
                    nameof(ApplicationUser.Client),
                    nameof(Client.AverageSales)
                });
            var property = Db.EntityConfigurationContext.EntityType<Person>()
                .FindProperty(
                    propertyPath);
            Assert.AreEqual(nameof(Client.AverageSales), property.Name);
        }

        [TestMethod]
        public void GetPrimarySearchProperties()
        {
            var searchProperties = Db.EntityConfigurationContext.EntityType<Person>()
                .ResolveSearchProperties();
            Assert.AreEqual(1, searchProperties.Length);
            Assert.IsNotNull(searchProperties.SingleOrDefault(p => p.Name == nameof(Person.Title)));
        }

        [TestMethod]
        public void GetSecondarySearchProperties()
        {
            var searchProperties = Db.EntityConfigurationContext.EntityType<Person>()
                .ResolveSearchProperties(PropertySearchKind.Secondary);
            Assert.AreEqual(3, searchProperties.Length);
            Assert.IsNotNull(searchProperties.SingleOrDefault(p => p.Name == nameof(Person.Key)));
            Assert.IsNotNull(searchProperties.SingleOrDefault(p => p.Name == nameof(Person.Title)));
            Assert.IsNotNull(searchProperties.SingleOrDefault(p => p.Name == nameof(Person.Description)));
        }

        [TestMethod]
        public void TestSetNameIsResolvedFromPropertyName()
        {
            Assert.AreEqual(nameof(AppDbContext.People), Db.EntityConfigurationContext.EntityType<Person>().SetName);
        }
    }
}