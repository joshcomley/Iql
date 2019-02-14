using System;
using System.Threading.Tasks;
using Iql.Entities;
using Iql.Tests.Context;
using Iql.Tests.Tests.OData;
using IqlSampleApp.ApiContext.Base;
using IqlSampleApp.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Tests.DataContextTests
{
    [TestClass]
    public class DataContextTests : TestsBase
    {
        [TestMethod]
        public void AttachEntityTracksCorrectly()
        {
            var client = new Client
            {
                Name = "abc",
                TypeId = 7
            };
            Assert.AreEqual(0, Db.GetChanges().Length);
            client.Name = "def";
            Assert.AreEqual(0, Db.GetChanges().Length);
            client = Db.AttachEntity(client);
            client.Name = "abc";
            Assert.AreEqual(1, Db.GetChanges().Length);
        }

        [TestMethod]
        public async Task CheckEntityIsNew()
        {
            var client = new Client
            {
                Name = "abc",
                TypeId = 7
            };
            Assert.IsNull(Db.IsEntityNew(client));
            Assert.IsNull(Db.IsEntityNew(client));
            Db.AddEntity(client);
            Assert.IsTrue(Db.IsEntityNew(client) == true);
            Assert.IsTrue(Db.IsEntityNew(client) == true);
            await Db.SaveChangesAsync();
            Assert.IsFalse(Db.IsEntityNew(client) == true);
        }

        [TestMethod]
        public async Task GetEntityWithEntityAsKeyFromDbSet()
        {
            var id = 156187;
            var clientRemote = new Client
            {
                Name = "abc",
                Id = id
            };
            AppDbContext.InMemoryDb.Clients.Add(clientRemote);
            var entity1 = await Db.Clients.SetTracking(false).GetWithKeyAsync(156187);
            Assert.AreEqual("abc", entity1.Name);
            var entity2 = (Client)await Db.Clients.GetWithKeyOrEntityAsync(entity1);
            Assert.AreNotEqual(entity1, entity2);
            Assert.AreEqual("abc", entity2.Name);
        }

        [TestMethod]
        public async Task GetEntityWithEntityAsKeyFromDataContext()
        {
            var id = 156187;
            var clientRemote = new Client
            {
                Name = "abc",
                Id = id
            };
            AppDbContext.InMemoryDb.Clients.Add(clientRemote);
            var entity1 = await Db.GetEntityAsync<Client>(156187, false);
            Assert.AreEqual("abc", entity1.Name);
            var entity2 = await Db.GetEntityAsync<Client>(entity1);
            Assert.AreNotEqual(entity1, entity2);
            Assert.AreEqual("abc", entity2.Name);
        }

        [TestMethod]
        public void IdentityObjectEntityEquivalencyTests()
        {
            var client1 = new Client();
            Assert.IsTrue(Db.AreEquivalent(client1, client1));
        }

        [TestMethod]
        public void DifferentNewEntityEquivalencyTests()
        {
            var client1 = new Client();
            var client2 = new Client();
            Assert.IsFalse(Db.AreEquivalent(client1, client2));
        }

        [TestMethod]
        public void DifferentEntityWithEquivalentIdEquivalencyTests()
        {
            var client1 = new Client();
            var client2 = new Client();
            client1.Id = 7;
            client2.Id = 7;
            Assert.IsTrue(Db.AreEquivalent(client1, client2));
        }

        [TestMethod]
        public void MatchingCompositeKeyAndEntityEquivalencyTests()
        {
            var client1 = new Client();
            client1.Id = 7;
            Assert.IsTrue(Db.AreEquivalent(client1, Db.GetCompositeKey(client1)));
        }

        [TestMethod]
        public void NonMatchingCompositeKeyAndEntityEquivalencyTests()
        {
            var client1 = new Client();
            client1.Id = 7;
            var compositeKey = Db.GetCompositeKey(client1);
            client1.Id = 8;
            Assert.IsFalse(Db.AreEquivalent(client1, compositeKey));
        }

        [TestMethod]
        public void GetDbSetByEntityTypeTest()
        {
            void AssertType(Type type)
            {
                Assert.AreEqual(Db.GetDbSetByEntityType(type).ItemType, type);
            }
            AssertType(typeof(ApplicationUser));
            AssertType(typeof(DocumentCategory));
            AssertType(typeof(MyCustomReport));
            AssertType(typeof(ClientType));
            AssertType(typeof(Client));
            AssertType(typeof(Site));
            AssertType(typeof(Person));
            AssertType(typeof(PersonType));
            AssertType(typeof(PersonTypeMap));
            AssertType(typeof(PersonInspection));
            AssertType(typeof(ReportCategory));
            AssertType(typeof(SiteInspection));
            AssertType(typeof(RiskAssessment));
            AssertType(typeof(RiskAssessmentSolution));
        }

        [TestMethod]
        public async Task SavingChangesToASingleEntityWithMultipleEntitiesChanged()
        {
            var clientType = new ClientType();
            clientType.Name = "Test 1";
            var documentCategory = new DocumentCategory();
            documentCategory.Name = "Test 2";
            Db.ClientTypes.Add(clientType);
            Db.DocumentCategories.Add(documentCategory);
            var result = await Db.SaveChangesAsync(new[] { clientType });
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
            var changes = Db.GetChanges();
            Assert.AreEqual(1, changes.Length);
            await Db.SaveChangesAsync();
            Assert.IsFalse(Db.IsTracked(client));
            changes = Db.GetChanges();
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
            var changes = Db.GetChanges();
            Assert.AreEqual(1, changes.Length);
            await Db.SaveChangesAsync();
            Assert.IsFalse(Db.IsTracked(client));
            changes = Db.GetChanges();
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