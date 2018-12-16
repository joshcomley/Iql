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
        public async Task TestGetCustomDetailExact()
        {
            var site = new Site();
            var siteEntityConfiguration = Db.EntityConfigurationContext.EntityType<Site>();
            var detail = PropertyDetail.For(siteEntityConfiguration);
            var instance = await detail.GetSnapshotAsync(site, Db, DisplayConfigurationKind.Edit, SnapshotOrdering.Default, false);
            Assert.AreEqual(7, instance.ChildProperties.Length);
            AssertProperty(instance, nameof(Site.Client), true);
            AssertProperty(instance, nameof(Site.Name), true);
            AssertProperty(instance, nameof(Site.Parent), true);
            AssertProperty(instance, "Site Address", true);
            var siteAddress = instance.ChildProperties.FirstOrDefault(_ => _.PropertyName == "Site Address");
            AssertProperty(siteAddress, nameof(Site.Address), true);
            AssertProperty(siteAddress, nameof(Site.PostCode), true);
            AssertProperty(instance, nameof(Site.Parent), true);
            AssertProperty(instance, nameof(Site.Key), false);
            AssertProperty(instance, nameof(Site.Location), true);
        }

        [TestMethod]
        public async Task TestGetCustomDetailExactOrdered()
        {
            var site = new Site();
            var siteEntityConfiguration = Db.EntityConfigurationContext.EntityType<Site>();
            var detail = PropertyDetail.For(siteEntityConfiguration);
            var instance = await detail.GetSnapshotAsync(site, Db, DisplayConfigurationKind.Edit, SnapshotOrdering.ReadOnlyFirst, false);
            Assert.AreEqual(7, instance.ChildProperties.Length);
            AssertProperty(instance, nameof(Site.Key), false);
            AssertProperty(instance, nameof(Site.Client), true);
            AssertProperty(instance, nameof(Site.Name), true);
            AssertProperty(instance, nameof(Site.Parent), true);
            AssertProperty(instance, "Site Address", true);
            var siteAddress = instance.ChildProperties.FirstOrDefault(_ => _.PropertyName == "Site Address");
            AssertProperty(siteAddress, nameof(Site.Address), true);
            AssertProperty(siteAddress, nameof(Site.PostCode), true);
            AssertProperty(instance, nameof(Site.Parent), true);
            AssertProperty(instance, nameof(Site.Location), true);
        }

        [TestMethod]
        public async Task TestGetCustomDetail()
        {
            var site = new Site();
            var siteEntityConfiguration = Db.EntityConfigurationContext.EntityType<Site>();
            var detail = PropertyDetail.For(siteEntityConfiguration);
            var instance = await detail.GetSnapshotAsync(site, Db, DisplayConfigurationKind.Edit);
            Assert.AreEqual(18, instance.ChildProperties.Length);
            AssertProperty(instance, nameof(Site.Client), true);
            AssertProperty(instance, nameof(Site.Name), true);
            AssertProperty(instance, nameof(Site.Parent), true);
            AssertProperty(instance, "Site Address", true);
            var siteAddress = instance.ChildProperties.FirstOrDefault(_ => _.PropertyName == "Site Address");
            AssertProperty(siteAddress, nameof(Site.Address), true);
            AssertProperty(siteAddress, nameof(Site.PostCode), true);
            AssertProperty(instance, nameof(Site.Parent), true);
            AssertProperty(instance, nameof(Site.Key), false);
            AssertProperty(instance, nameof(Site.Location), true);
            AssertProperty(instance, nameof(Site.Id), false);
            AssertProperty(instance, nameof(Site.FullAddress), false);
            AssertProperty(instance, nameof(Site.RevisionKey), false);
            AssertProperty(instance, nameof(Site.CreatedByUser), false);
            AssertProperty(instance, nameof(Site.Area), true);
            AssertProperty(instance, nameof(Site.Line), true);
            AssertProperty(instance, nameof(Site.Left), true);
            AssertProperty(instance, nameof(Site.Right), true);
            AssertProperty(instance, nameof(Site.Guid), false);
            AssertProperty(instance, nameof(Site.CreatedDate), false);
            AssertProperty(instance, nameof(Site.PersistenceKey), false);
        }

        [TestMethod]
        public async Task TestGetCustomDetailOrdered()
        {
            var site = new Site();
            var siteEntityConfiguration = Db.EntityConfigurationContext.EntityType<Site>();
            var detail = PropertyDetail.For(siteEntityConfiguration);
            var instance = await detail.GetSnapshotAsync(site, Db, DisplayConfigurationKind.Edit, SnapshotOrdering.ReadOnlyFirst);
            Assert.AreEqual(18, instance.ChildProperties.Length);
            AssertProperty(instance, nameof(Site.Key), false);
            AssertProperty(instance, nameof(Site.Id), false);
            AssertProperty(instance, nameof(Site.FullAddress), false);
            AssertProperty(instance, nameof(Site.RevisionKey), false);
            AssertProperty(instance, nameof(Site.CreatedByUser), false);
            AssertProperty(instance, nameof(Site.Guid), false);
            AssertProperty(instance, nameof(Site.CreatedDate), false);
            AssertProperty(instance, nameof(Site.PersistenceKey), false);
            AssertProperty(instance, nameof(Site.Client), true);
            AssertProperty(instance, nameof(Site.Name), true);
            AssertProperty(instance, nameof(Site.Parent), true);
            AssertProperty(instance, "Site Address", true);
            var siteAddress = instance.ChildProperties.FirstOrDefault(_ => _.PropertyName == "Site Address");
            AssertProperty(siteAddress, nameof(Site.Address), true);
            AssertProperty(siteAddress, nameof(Site.PostCode), true);
            AssertProperty(instance, nameof(Site.Parent), true);
            AssertProperty(instance, nameof(Site.Location), true);
            AssertProperty(instance, nameof(Site.Area), true);
            AssertProperty(instance, nameof(Site.Line), true);
            AssertProperty(instance, nameof(Site.Left), true);
            AssertProperty(instance, nameof(Site.Right), true);
        }

        private static string GetAsserts(EntityPropertySnapshot[] children)
        {
            var str = "";
            for (var i = 0; i < children.Length; i++)
            {
                var child = children[i];
                str +=
                    $"AssertProperty(instance, nameof(Site.{child.PropertyName}), {child.CanEdit.ToString().ToLower()});\n";
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
            Assert.AreEqual(18, instance.ChildProperties.Length);

            AssertProperty(instance, nameof(Person.Id), false);
            AssertProperty(instance, nameof(Person.Key), true);
            AssertProperty(instance, nameof(Person.Title), true);
            AssertProperty(instance, nameof(Person.Description), true);
            AssertProperty(instance, nameof(Person.RevisionKey), false);
            AssertProperty(instance, nameof(Person.Client), true);
            AssertProperty(instance, nameof(Person.Site), true);
            AssertProperty(instance, nameof(Person.SiteArea), true);
            AssertProperty(instance, nameof(Person.Type), true);
            AssertProperty(instance, nameof(Person.Loading), true);
            AssertProperty(instance, nameof(Person.CreatedByUser), false);
            AssertProperty(instance, nameof(Person.Reports), true);
            AssertProperty(instance, nameof(Person.Location), true);
            AssertProperty(instance, nameof(Person.Skills), true);
            AssertProperty(instance, nameof(Person.Category), true);
            AssertProperty(instance, nameof(Person.Guid), false);
            AssertProperty(instance, nameof(Person.CreatedDate), false);
            AssertProperty(instance, nameof(Person.PersistenceKey), false);
        }

        static void AssertProperty(EntityPropertySnapshot snapshot, string name, bool canEdit)
        {
            var prop = snapshot.ChildProperties.FirstOrDefault(p => p.PropertyName == name);
            Assert.IsNotNull(prop);
            Assert.AreEqual(name, prop.PropertyName);
            Assert.AreEqual(canEdit, prop.CanEdit);
        }
    }
}