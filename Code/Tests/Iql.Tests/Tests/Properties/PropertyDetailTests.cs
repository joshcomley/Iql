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
        public async Task TestGetDetail()
        {
            var person = new Person();
            var personEntityConfiguration = Db.EntityConfigurationContext.EntityType<Person>();
            var detail = PropertyDetail.For(personEntityConfiguration);
            var instance = await detail.GetSnapshotAsync(person, Db, DisplayConfigurationKind.Edit);
            // Currently just check no infinite loop is created
            Assert.IsNotNull(instance);
            Assert.AreEqual(18, instance.ChildProperties.Length);

            void AssertProperty(string name, bool canEdit)
            {
                var prop = instance.ChildProperties.FirstOrDefault(p => p.PropertyName == name);
                Assert.IsNotNull(prop);
                Assert.AreEqual(name, prop.PropertyName);
                Assert.AreEqual(canEdit, prop.CanEdit);
            }

            AssertProperty(nameof(Person.Id), false);
            AssertProperty(nameof(Person.Key), true);
            AssertProperty(nameof(Person.Title), true);
            AssertProperty(nameof(Person.Description), true);
            AssertProperty(nameof(Person.RevisionKey), false);
            AssertProperty(nameof(Person.Client), true);
            AssertProperty(nameof(Person.Site), true);
            AssertProperty(nameof(Person.SiteArea), true);
            AssertProperty(nameof(Person.Type), true);
            AssertProperty(nameof(Person.Loading), true);
            AssertProperty(nameof(Person.CreatedByUser), false);
            AssertProperty(nameof(Person.Reports), true);
            AssertProperty(nameof(Person.Location), true);
            AssertProperty(nameof(Person.Skills), true);
            AssertProperty(nameof(Person.Category), true);
            AssertProperty(nameof(Person.Guid), false);
            AssertProperty(nameof(Person.CreatedDate), false);
            AssertProperty(nameof(Person.PersistenceKey), false);
        }
    }
}