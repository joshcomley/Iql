using System.Linq;
using System.Threading.Tasks;
using Iql.Data.Rendering;
using Iql.Entities;
using IqlSampleApp.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests.Properties
{
    [TestClass]
    public class PropertyDetailTests : TestsBase
    {
        [TestMethod]
        public async Task TestStandardOrdering()
        {
            var site = new Site();
            var siteEntityConfiguration = Db.EntityConfigurationContext.EntityType<Site>();
            var detail = PropertyDetail.For(siteEntityConfiguration);
            var instance = await detail.GetSnapshotAsync(site, Db, DisplayConfigurationKind.Edit, SnapshotOrdering.Standard);
            Assert.AreEqual(25, instance.ChildProperties.Length);
            var expectedIndex = 0;
            AssertProperty(expectedIndex++, instance, nameof(Site.Key), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Id), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.FullAddress), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.RevisionKey), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.CreatedByUser), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Guid), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.CreatedDate), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.PersistenceKey), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Documents), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.AdditionalSendReportsTo), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.People), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Children), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.SiteInspections), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Users), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Areas), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Client), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Name), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Parent), true);
            AssertProperty(expectedIndex++, instance, "Site Address", true);
            var siteAddress = instance.ChildProperties.FirstOrDefault(_ => _.PropertyName == "Site Address");
            AssertProperty(0, siteAddress, nameof(Site.Address), true);
            AssertProperty(1, siteAddress, nameof(Site.PostCode), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Parent), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Location), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Area), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Line), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Left), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Right), true);
        }

        [TestMethod]
        public async Task TestGetCustomDetailExact()
        {
            var site = new Site();
            var siteEntityConfiguration = Db.EntityConfigurationContext.EntityType<Site>();
            var detail = PropertyDetail.For(siteEntityConfiguration);
            var instance = await detail.GetSnapshotAsync(site, Db, DisplayConfigurationKind.Edit, SnapshotOrdering.Default, false);
            Assert.AreEqual(7, instance.ChildProperties.Length);
            var expectedIndex = 0;
            AssertProperty(expectedIndex++, instance, nameof(Site.Client), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Name), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Parent), true);
            AssertProperty(expectedIndex++, instance, "Site Address", true);
            var siteAddress = instance.ChildProperties.FirstOrDefault(_ => _.PropertyName == "Site Address");
            AssertProperty(0, siteAddress, nameof(Site.Address), true);
            AssertProperty(1, siteAddress, nameof(Site.PostCode), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Parent), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Key), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Location), true);
        }

        [TestMethod]
        public async Task TestGetCustomDetailExactOrdered()
        {
            var site = new Site();
            var siteEntityConfiguration = Db.EntityConfigurationContext.EntityType<Site>();
            var detail = PropertyDetail.For(siteEntityConfiguration);
            var instance = await detail.GetSnapshotAsync(site, Db, DisplayConfigurationKind.Edit, SnapshotOrdering.ReadOnlyFirst, false);
            Assert.AreEqual(7, instance.ChildProperties.Length);
            var expectedIndex = 0;
            AssertProperty(expectedIndex++, instance, nameof(Site.Key), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Client), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Name), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Parent), true);
            AssertProperty(expectedIndex++, instance, "Site Address", true);
            var siteAddress = instance.ChildProperties.FirstOrDefault(_ => _.PropertyName == "Site Address");
            AssertProperty(0, siteAddress, nameof(Site.Address), true);
            AssertProperty(1, siteAddress, nameof(Site.PostCode), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Parent), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Location), true);
        }

        [TestMethod]
        public async Task TestGetCustomDetail()
        {
            var site = new Site();
            var siteEntityConfiguration = Db.EntityConfigurationContext.EntityType<Site>();
            var detail = PropertyDetail.For(siteEntityConfiguration);
            var instance = await detail.GetSnapshotAsync(site, Db, DisplayConfigurationKind.Edit);
            Assert.AreEqual(25, instance.ChildProperties.Length);
            var expectedIndex = 0;
            AssertProperty(expectedIndex++, instance, nameof(Site.Client), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Name), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Parent), true);
            AssertProperty(expectedIndex++, instance, "Site Address", true);
            var siteAddress = instance.ChildProperties.FirstOrDefault(_ => _.PropertyName == "Site Address");
            AssertProperty(0, siteAddress, nameof(Site.Address), true);
            AssertProperty(1, siteAddress, nameof(Site.PostCode), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Parent), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Key), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Location), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Id), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.FullAddress), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.RevisionKey), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.CreatedByUser), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Documents), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.AdditionalSendReportsTo), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.People), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Children), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Areas), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.SiteInspections), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Users), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Area), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Line), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Left), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Right), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Guid), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.CreatedDate), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.PersistenceKey), false);
        }

        [TestMethod]
        public async Task TestGetCustomDetailOrdered()
        {
            var site = new Site();
            var siteEntityConfiguration = Db.EntityConfigurationContext.EntityType<Site>();
            var detail = PropertyDetail.For(siteEntityConfiguration);
            var instance = await detail.GetSnapshotAsync(site, Db, DisplayConfigurationKind.Edit, SnapshotOrdering.ReadOnlyFirst);
            Assert.AreEqual(25, instance.ChildProperties.Length);
            var expectedIndex = 0;
            AssertProperty(expectedIndex++, instance, nameof(Site.Key), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Id), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.FullAddress), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.RevisionKey), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.CreatedByUser), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Documents), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.AdditionalSendReportsTo), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.People), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Children), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.SiteInspections), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Users), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Guid), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.CreatedDate), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.PersistenceKey), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Client), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Name), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Parent), true);
            AssertProperty(expectedIndex++, instance, "Site Address", true);
            var siteAddress = instance.ChildProperties.FirstOrDefault(_ => _.PropertyName == "Site Address");
            AssertProperty(0, siteAddress, nameof(Site.Address), true);
            AssertProperty(1, siteAddress, nameof(Site.PostCode), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Parent), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Location), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Areas), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Area), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Line), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Left), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Right), true);
        }

        private static string GetAsserts<T>(EntityPropertySnapshot snapshot)
        {
            var str = "";
            for (var i = 0; i < snapshot.ChildProperties.Length; i++)
            {
                var child = snapshot.ChildProperties[i];
                str +=
                    $"AssertProperty(expectedIndex++, instance, nameof({typeof(T).Name}.{child.PropertyName}), {child.CanEdit.ToString().ToLower()});\n";
            }

            return str;
        }

        [TestMethod]
        public async Task TestGetDetail()
        {
            var person = new Person();
            var personEntityConfiguration = Db.EntityConfigurationContext.EntityType<Person>();
            var detail = PropertyDetail.For(personEntityConfiguration);
            var instance = await detail.GetSnapshotAsync(person, Db, DisplayConfigurationKind.Edit);
            // Currently just check no infinite loop is created
            Assert.IsNotNull(instance);
            Assert.AreEqual(19, instance.ChildProperties.Length);
            var expectedIndex = 0;
            AssertProperty(expectedIndex++, instance, nameof(Person.Id), false);
            AssertProperty(expectedIndex++, instance, nameof(Person.Key), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Title), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Description), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.RevisionKey), false);
            AssertProperty(expectedIndex++, instance, nameof(Person.Client), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Site), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.SiteArea), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Type), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Loading), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.CreatedByUser), false);
            AssertProperty(expectedIndex++, instance, nameof(Person.Types), false);
            AssertProperty(expectedIndex++, instance, nameof(Person.Reports), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Location), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Skills), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Category), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Guid), false);
            AssertProperty(expectedIndex++, instance, nameof(Person.CreatedDate), false);
            AssertProperty(expectedIndex++, instance, nameof(Person.PersistenceKey), false);
        }

        static void AssertProperty(int expectedIndex, EntityPropertySnapshot snapshot, string name, bool canEdit)
        {
            var prop = snapshot.ChildProperties[expectedIndex];
            Assert.IsNotNull(prop);
            Assert.AreEqual(name, prop.PropertyName);
            Assert.AreEqual(canEdit, prop.CanEdit);
        }
    }
}