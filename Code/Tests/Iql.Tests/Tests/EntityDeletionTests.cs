using System.Linq;
using System.Threading.Tasks;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class EntityDeletionTests : TestsBase
    {
        [TestMethod]
        public async Task DeletingAnEntityShouldRemoveEntityFromDbLists()
        {
            AppDbContext.InMemoryDb.People.Add(new Person { Id = 62, TypeId = 52 });
            AppDbContext.InMemoryDb.People.Add(new Person { Id = 63 });

            var people = await Db.People.ToListAsync();
            var people2 = await Db.People.Where(p => p.Id == 62).ToListAsync();
            Assert.AreEqual(2, people.Count);
            Assert.AreEqual(1, people2.Count);
            var person = people[0];
            Db.People.Delete(person);
            Assert.AreEqual(2, people.Count);
            Assert.AreEqual(1, people2.Count);
            await Db.SaveChangesAsync();
            Assert.AreEqual(1, people.Count);
            Assert.AreEqual(0, people2.Count);
            Assert.IsFalse(people.Contains(person));
            Assert.IsFalse(people2.Contains(person));
        }

        [TestMethod]
        public async Task DeletingARecentlyCreatedEntityShouldRemoveEntityFromDbLists()
        {
            var person1 = new Person();
            var person2 = new Person();
            person1.Title = "A long enough title";
            person1.Description = "A long enough title";
            person2.Title = "A long enough title";
            person2.Description = "A long enough title";
            Db.People.Add(person1);
            Db.People.Add(person2);
            await Db.SaveChangesAsync();

            var people = await Db.People.ToListAsync();
            var people2 = await Db.People.Where(p => p.Id == 1).ToListAsync();
            Assert.AreEqual(2, people.Count);
            Assert.AreEqual(1, people2.Count);
            Db.People.Delete(person1);
            Assert.AreEqual(2, people.Count);
            Assert.AreEqual(1, people2.Count);
            await Db.SaveChangesAsync();
            Assert.AreEqual(1, people.Count);
            Assert.AreEqual(0, people2.Count);
            Assert.IsFalse(people.Contains(person1));
            Assert.IsFalse(people2.Contains(person1));
        }

        [TestMethod]
        public async Task DeletingAnEntityCreatedWithADifferentDataContextShouldRemoveEntityFromDbLists()
        {
            var db1 = new AppDbContext();
            var person1 = new Person();
            person1.Title = "A long enough title";
            person1.Description = "A long enough title";
            var person2 = new Person();
            person2.Title = "A long enough title";
            person2.Description = "A long enough title";
            db1.People.Add(person1);
            db1.People.Add(person2);
            var result = await db1.SaveChangesAsync();
            var people1 = await db1.People.ToListAsync();

            var db2 = new AppDbContext();
            db1.SynchronicityKey = "db";
            db2.SynchronicityKey = db1.SynchronicityKey;
            var people2 = await db2.People.ToListAsync();
            var personQueryList = await db2.People.Where(p => p.Id == 1).ToListAsync();
            var localPerson1 = people2[0];
            Assert.AreEqual(2, people1.Count);
            Assert.AreEqual(2, people2.Count);
            Assert.AreEqual(1, personQueryList.Count);
            db2.People.Delete(localPerson1);
            Assert.AreEqual(2, people1.Count);
            Assert.AreEqual(2, people2.Count);
            Assert.AreEqual(1, personQueryList.Count);
            await db2.SaveChangesAsync();
            Assert.AreEqual(1, people1.Count);
            Assert.AreEqual(1, people2.Count);
            Assert.AreEqual(0, personQueryList.Count);
            Assert.IsFalse(people2.Contains(person1));
            Assert.IsFalse(personQueryList.Contains(person1));
        }

        [TestMethod]
        public async Task DeletingAnEntityShouldRemoveEntityFromRelatedLists()
        {
            AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType { Id = 13 });
            AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType { Id = 14 });
            AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 3, TypeId = 14 });
            AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 4, TypeId = 13 });
            AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 5, TypeId = 13 });

            var clientTypes = await Db.ClientTypes.Expand(c => c.Clients).ToListAsync();
            var clientToDelete = await Db.Clients.Where(p => p.Id == 4).SingleAsync();
            var clientType = clientTypes.Single(c => c.Id == 13);
            Assert.AreEqual(2, clientType.Clients.Count);
            Db.Clients.Delete(clientToDelete);
            Assert.AreEqual(1, clientType.Clients.Count);
            Assert.IsFalse(clientType.Clients.Contains(clientToDelete));
            await Db.SaveChangesAsync();
            Assert.AreEqual(1, clientType.Clients.Count);
            Assert.IsFalse(clientType.Clients.Contains(clientToDelete));
        }

    }
}