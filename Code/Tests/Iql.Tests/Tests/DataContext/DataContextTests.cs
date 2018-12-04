using System.Threading.Tasks;
using Iql.Tests.Context;
using IqlSampleApp.ApiContext.Base;
using IqlSampleApp.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Tests.DataContext
{
    [TestClass]
    public class DataContextTests : TestsBase
    {
        [TestMethod]
        public async Task SavingChangesToASingleEntityWithMultipleEntitiesChanged()
        {
            var  clientType = new ClientType();
            clientType.Name = "Test 1";
            var documentCategory = new DocumentCategory();
            documentCategory.Name = "Test 2";
            Db.ClientTypes.Add(clientType);
            Db.DocumentCategories.Add(documentCategory);
            var result = await Db.SaveChangesAsync(new[] {clientType});
            Assert.AreEqual(1, result.Results.Count);
            var primaryResult = result.Results[0];
            Assert.AreEqual(primaryResult.LocalEntity, clientType);
        }

        [TestMethod]
            public async Task UpdatingADeletedEntityShouldUntrackTheEntity()
            {
            var id = 156187;
            var clientRemote = new Client
            {
                Name = "abc",
                Description = "blah",
                Id = id,
                TypeId = 7
            };
            AppDbContext.InMemoryDb.Clients.Add(clientRemote);
            var client = await Db.Clients.GetWithKeyAsync(id);
            Assert.IsTrue(Db.IsTracked(client));
            Assert.IsNotNull(client);
            AppDbContext.InMemoryDb.Clients.Remove(clientRemote);
            client.Name = "def";
            var changes = Db.DataStore.GetChanges();
            Assert.AreEqual(1, changes.Length);
            await Db.SaveChangesAsync();
            Assert.IsFalse(Db.IsTracked(client));
            changes = Db.DataStore.GetChanges();
            Assert.AreEqual(0, changes.Length);
        }

        [TestMethod]
        public async Task DeletingADeletedEntityShouldUntrackTheEntity()
        {
            var id = 156187;
            var clientRemote = new Client
            {
                Name = "abc",
                Id = id
            };
            AppDbContext.InMemoryDb.Clients.Add(clientRemote);
            var client = await Db.Clients.GetWithKeyAsync(id);
            Assert.IsTrue(Db.IsTracked(client));
            Assert.IsNotNull(client);
            AppDbContext.InMemoryDb.Clients.Remove(clientRemote);
            Db.DeleteEntity(client);
            var changes = Db.DataStore.GetChanges();
            Assert.AreEqual(1, changes.Length);
            await Db.SaveChangesAsync();
            Assert.IsFalse(Db.IsTracked(client));
            changes = Db.DataStore.GetChanges();
            Assert.AreEqual(0, changes.Length);
        }

        [TestMethod]
        public void GetDbSetBySet()
        {
            var someSet = Db.GetDbSetBySet<PersonInspectionSet>();
            Assert.AreEqual(Db.PersonInspections, someSet);
        }

        [TestMethod]
        public void GetDbSet()
        {
            var someSet = Db.GetDbSet<PersonInspection, int>();
            Assert.AreEqual(Db.PersonInspections, someSet);
        }

        [TestMethod]
        public void GetDbSetByLowerCaseName()
        {
            var someSet = Db.GetDbSetBySetName(nameof(IqlSampleAppDataContextBase.PersonInspections).ToLower());
            Assert.AreEqual(Db.PersonInspections, someSet);
        }

        [TestMethod]
        public void GetDbSetByEntityType()
        {
            var someSet = Db.GetDbSetByEntityType(typeof(PersonInspection));
            Assert.AreEqual(Db.PersonInspections, someSet);
        }

        [TestMethod]
        public void GetDbSetBySetType()
        {
            var someSet = Db.GetDbSetBySetType(typeof(PersonInspectionSet));
            Assert.AreEqual(Db.PersonInspections, someSet);
        }

        [TestMethod]
        public void GetDbQueryable()
        {
            var someSet = Db.GetDbQueryable<PersonInspection>();
            Assert.AreEqual(Db.PersonInspections, someSet);
        }


        [TestMethod]
        public void GetDbSetNameByEntity()
        {
            var someSet = Db.GetDbSetPropertyNameByEntityType(typeof(PersonInspection));
            Assert.AreEqual(nameof(Db.PersonInspections), someSet);
        }

        [TestMethod]
        public void GetDbSetNameBySet()
        {
            var someSet = Db.GetDbSetPropertyNameBySetType(typeof(PersonInspectionSet));
            Assert.AreEqual(nameof(Db.PersonInspections), someSet);
        }


        [TestMethod]
        public void GetDbSetNameByEntityType()
        {
            var someSet = Db.GetDbSetPropertyNameByEntityType(typeof(PersonInspection));
            Assert.AreEqual(nameof(Db.PersonInspections), someSet);
        }

        [TestMethod]
        public void GetDbSetNameBySetType()
        {
            var someSet = Db.GetDbSetPropertyNameBySetType(typeof(PersonInspectionSet));
            Assert.AreEqual(nameof(Db.PersonInspections), someSet);
        }
    }
}