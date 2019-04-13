using System.Linq;
using System.Threading.Tasks;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class OneToManyRelationshipTests : TestsBase
    {
        [TestMethod]
        public async Task TestRelationshipMaps()
        {
            // Set up
            //var inMemoryDbClient1 = new Client { Id = 1, Name = "Test", TypeId = 1 };
            //AppDbContext.InMemoryDb.Clients.Add(inMemoryDbClient1);
            //AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 2, Name = "Test 2", TypeId = 2 });
            //AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType { Id = 1, Name = "Type 1" });
            //AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType { Id = 2, Name = "Type 2" });

            var clientType1 = new ClientType();
            clientType1.Name = "Type 1";
            var clientType2 = new ClientType();
            clientType2.Name = "Type 2";
            Db.ClientTypes.Add(clientType1);
            Db.ClientTypes.Add(clientType2);
            await Db.SaveChangesAsync();
            var client = new Client();
            client.Name = "Client 1";
            Db.Clients.Add(client);

            // Set to client type 1 via key
            client.TypeId = clientType1.Id;
            Assert.AreEqual(1, clientType1.Clients.Count);
            Assert.AreEqual(0, clientType2.Clients.Count);
            Assert.AreEqual(clientType1, client.Type);
            Assert.AreEqual(clientType1.Id, client.TypeId);

            // Set to client type 2 via key
            client.TypeId = clientType2.Id;
            Assert.AreEqual(0, clientType1.Clients.Count);
            Assert.AreEqual(1, clientType2.Clients.Count);
            Assert.AreEqual(clientType2, client.Type);
            Assert.AreEqual(clientType2.Id, client.TypeId);

            // Set to client type 1 via reference
            client.Type = clientType1;
            Assert.AreEqual(1, clientType1.Clients.Count);
            Assert.AreEqual(0, clientType2.Clients.Count);
            Assert.AreEqual(clientType1, client.Type);
            Assert.AreEqual(clientType1.Id, client.TypeId);

            // Set to client type 2 via reference
            client.Type = clientType2;
            Assert.AreEqual(0, clientType1.Clients.Count);
            Assert.AreEqual(1, clientType2.Clients.Count);
            Assert.AreEqual(clientType2, client.Type);
            Assert.AreEqual(clientType2.Id, client.TypeId);

            // Assign a type that isn't saved, yet
            var clientType3 = new ClientType();
            clientType3.Name = "Type 3";
            client.Type = clientType3;
            Assert.AreEqual(0, clientType1.Clients.Count);
            Assert.AreEqual(0, clientType2.Clients.Count);
            Assert.AreEqual(1, clientType3.Clients.Count);
            Assert.AreEqual(clientType3, client.Type);
            Assert.AreEqual(clientType3.Id, client.TypeId);

            // Save the new type
            await Db.SaveChangesAsync();
            // The TypeId property should have auto-updated
            Assert.AreEqual(3, clientType3.Id);
            Assert.AreEqual(clientType3.Id, client.TypeId);

            // Assign by related list
            clientType1.Clients.Add(client);
            Assert.AreEqual(1, clientType1.Clients.Count);
            Assert.AreEqual(0, clientType2.Clients.Count);
            Assert.AreEqual(0, clientType3.Clients.Count);
        }

        [TestMethod]
        public async Task ExpandShouldIncludeExpandedEntities()
        {
            InsertData();
            var clientTypes = await Db.ClientTypes.Expand(ct => ct.Clients).ToListAsync();
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
            var clientTypes = await Db.ClientTypes.Expand(ct => ct.Clients).ToListAsync();
            var clientType1 = clientTypes.Single(ct => ct.Id == 72);
            var clientType2 = clientTypes.Single(ct => ct.Id == 73);
            var client = clientType1.Clients[0];
            Assert.AreEqual(2, clientTypes.Count);
            Assert.AreEqual(2, clientType1.Clients.Count);
            Assert.AreEqual(0, clientType2.Clients.Count);

            // Modify DB
            AppDbContext.InMemoryDb.Clients.Single(c => c.Id == 7).TypeId = clientType2.Id;

            await Db.RefreshEntityAsync(client
#if TypeScript
                , typeof(Client)
#endif
                );

            Assert.AreEqual(clientType2.Id, client.TypeId);
            Assert.AreEqual(1, clientType1.Clients.Count);
            Assert.AreEqual(1, clientType2.Clients.Count);

            var updatedClient = await Db.Clients.GetWithKeyAsync(7);
            Assert.AreEqual(updatedClient, client);
        }

        public enum ChangeOneToManySourceType
        {
            Key,
            Reference,
            Assign
        }

        [TestMethod]
        public async Task ChangingTheSourceOfAChildEntityShouldUpdatedAllRelatedCollectionsByAssign()
        {
            await ApplyToLiveObjects(ChangeOneToManySourceType.Assign);
        }

        [TestMethod]
        public async Task ChangingTheSourceOfAChildEntityShouldUpdatedAllRelatedCollectionsByAssignByReference()
        {
            await ApplyToLiveObjects(ChangeOneToManySourceType.Reference);
        }

        [TestMethod]
        public async Task ChangingTheSourceOfAChildEntityShouldUpdatedAllRelatedCollectionsByKey()
        {
            await ApplyToLiveObjects(ChangeOneToManySourceType.Key);
        }

        public async Task ApplyToLiveObjects(ChangeOneToManySourceType type)
        {
            InsertData();
            var clientTypes = await Db.ClientTypes.Expand(ct => ct.Clients).ToListAsync();
            var clientType1 = clientTypes.Single(ct => ct.Id == 72);
            var clientType2 = clientTypes.Single(ct => ct.Id == 73);
            var client = clientType1.Clients[0];
            Assert.AreEqual(2, clientTypes.Count);
            Assert.AreEqual(2, clientType1.Clients.Count);
            Assert.AreEqual(0, clientType2.Clients.Count);
            //var isTracked = Db.DataStore.GetTracking().IsTracked(client, typeof(Client));
            // Modify local
            switch (type)
            {
                case ChangeOneToManySourceType.Assign:
                    // I think expand is giving us new, duplicated entities...
                    // ...perhaps when we clone the expand items we need to look up
                    // their tracked version, too
                    // clientType2.Clients.Owner != clientType2
                    clientType2.Clients.Add(clientType1.Clients[0]);
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
            var client1 = new Client { Name = "abc" };
            var client2 = new Client { Name = "def" };
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

            var people = await Db.People.ToListAsync();
            var person = people.Single(p => p.Id == 62);

            Assert.IsNull(person.Type);
            var personType = await Db.PersonTypes.Where(pt => pt.Id == 52).SingleAsync();
            Assert.AreEqual(52, person.Type.Id);
            Assert.AreEqual(personType, person.Type);
            Assert.IsTrue(personType.People.Contains(person));
        }
    }
}