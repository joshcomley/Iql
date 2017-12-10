using System.Linq;
using System.Threading.Tasks;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class OneToManyRelationshipTests : TestsBase
    {
        [TestMethod]
        public async Task ExpandShouldIncludeExpandedEntities()
        {
            InsertData();
            var clientTypes = await Db.ClientTypes.Expand(ct => ct.Clients).ToList();
            var clientType1 = clientTypes.Single(ct => ct.Id == 72);
            var clientType2 = clientTypes.Single(ct => ct.Id == 73);
            Assert.AreEqual(2, clientTypes.Count);
            Assert.AreEqual(2, clientType1.Clients.Count);
            Assert.AreEqual(0, clientType2.Clients.Count);
        }

        [TestMethod]
        public async Task FetchingAChangedTargetShouldPersistChangesToChildCollections()
        {
            InsertData();
            var clientTypes = await Db.ClientTypes.Expand(ct => ct.Clients).ToList();
            var clientType1 = clientTypes.Single(ct => ct.Id == 72);
            var clientType2 = clientTypes.Single(ct => ct.Id == 73);
            var client = clientType1.Clients[0];
            Assert.AreEqual(2, clientTypes.Count);
            Assert.AreEqual(2, clientType1.Clients.Count);
            Assert.AreEqual(0, clientType2.Clients.Count);

            // Modify DB
            AppDbContext.InMemoryDb.Clients.Single(c => c.Id == 7).TypeId = clientType2.Id;

            await Db.RefreshEntity(client, typeof(Client));

            Assert.AreEqual(clientType2.Id, client.TypeId);
            Assert.AreEqual(1, clientType1.Clients.Count);
            Assert.AreEqual(1, clientType2.Clients.Count);

            var updatedClient = await Db.Clients.WithKey(7);
            Assert.AreEqual(updatedClient, client);
        }

        public enum ChangeOneToManySourceType
        {
            Key,
            Reference,
            Assign
        }

        [TestMethod]
        public async Task ChangingTheSourceOfAChildEntityShouldUpdatedAllRelatedCollections()
        {
            await ApplyToLiveObjects(ChangeOneToManySourceType.Assign);
            TestCleanUp();
            await ApplyToLiveObjects(ChangeOneToManySourceType.Key);
            TestCleanUp();
            await ApplyToLiveObjects(ChangeOneToManySourceType.Assign);
            TestCleanUp();
            await ApplyToLiveObjects(ChangeOneToManySourceType.Reference);
        }

        public async Task ApplyToLiveObjects(ChangeOneToManySourceType type)
        {
            InsertData();
            var clientTypes = await Db.ClientTypes.Expand(ct => ct.Clients).ToList();
            var clientType1 = clientTypes.Single(ct => ct.Id == 72);
            var clientType2 = clientTypes.Single(ct => ct.Id == 73);
            var client = clientType1.Clients[0];
            Assert.AreEqual(2, clientTypes.Count);
            Assert.AreEqual(2, clientType1.Clients.Count);
            Assert.AreEqual(0, clientType2.Clients.Count);
            var isTracked = Db.DataStore.GetTracking().IsTracked(client, typeof(Client));
            // Modify local
            switch (type)
            {
                case ChangeOneToManySourceType.Assign:
                    // I think expand is giving us new, duplicated entities...
                    // ...perhaps when we clone the expand items we need to look up
                    // their tracked version, too
                    // clientType2.Clients.Owner != clientType2
                    clientType2.Clients.AssignRelationship(clientType1.Clients[0]);
                    break;
                case ChangeOneToManySourceType.Key:
                    clientType1.Clients[0].TypeId = clientType2.Id;
                    break;
                case ChangeOneToManySourceType.Reference:
                    clientType1.Clients[0].Type = clientType2;
                    break;
            }

            Assert.AreEqual(clientType2.Id, client.TypeId);
            Assert.AreEqual(1, clientType1.Clients.Count);
            Assert.AreEqual(1, clientType2.Clients.Count);
        }

        [TestMethod]
        public void AddingLocalObjectsShouldPersistSourceToTargetCollection()
        {
            ApplyToLocalObjects();
        }

        public void ApplyToLocalObjects()
        {
            var client1 = new Client();
            var client2 = new Client();
            var clientType1 = new ClientType();
            var clientType2 = new ClientType();
            client1.Type = clientType1;
            client2.Type = clientType2;

            Assert.AreEqual(0, clientType1.Clients.Count);
            Assert.AreEqual(0, clientType2.Clients.Count);

            Db.Clients.Add(client1);
            Db.Clients.Add(client2);

            Assert.AreEqual(1, clientType1.Clients.Count);
            Assert.AreEqual(1, clientType2.Clients.Count);
        }

        private void InsertDataLocal()
        {
            var client1 = new Client();
            var client2 = new Client();
            var clientType1 = new ClientType();
            var clientType2 = new ClientType();
            client1.Type = clientType1;
            client2.Type = clientType2;
        }

        private static void InsertData()
        {
            AppDbContext.InMemoryDb.Clients.Add(
                new Client
                {
                    Id = 7,
                    TypeId = 72
                });
            AppDbContext.InMemoryDb.Clients.Add(
                new Client
                {
                    Id = 8,
                    TypeId = 72
                });
            AppDbContext.InMemoryDb.ClientTypes.Add(
                new ClientType
                {
                    Id = 72,
                });
            AppDbContext.InMemoryDb.ClientTypes.Add(
                new ClientType
                {
                    Id = 73,
                });
        }

        [TestMethod]
        public async Task RelationshipsShouldBeMatchedEvenWithUnrelatedQueries()
        {
            AppDbContext.InMemoryDb.People.Add(new Person { Id = 62, TypeId = 52 });
            AppDbContext.InMemoryDb.People.Add(new Person { Id = 63 });
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType { Id = 53 });
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType { Id = 52 });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap { PersonId = 62, TypeId = 53 });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap { PersonId = 63, TypeId = 52 });

            var people = await Db.People.ToList();
            var person = people.Single(p => p.Id == 62);

            Assert.IsNull(person.Type);
            var personType = await Db.PersonTypes.Where(pt => pt.Id == 52).Single();
            Assert.AreEqual(52, person.Type.Id);
            Assert.AreEqual(personType, person.Type);
            Assert.IsTrue(personType.People.Contains(person));
        }
    }
}