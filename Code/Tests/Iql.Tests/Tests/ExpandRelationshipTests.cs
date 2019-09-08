using System.Linq;
using System.Threading.Tasks;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class ExpandRelationshipTests : TestsBase
    {
        [TestMethod]
        public async Task SimpleMultiExpand()
        {
            AppDbContext.InMemoryDb.People.Add(new Person { Id = 62, TypeId = 52, ClientId = 77 });
            AppDbContext.InMemoryDb.People.Add(new Person { Id = 63, TypeId = 53, ClientId = 78 });
            AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 77 });
            AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 78 });
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType { Id = 52 });
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType { Id = 53 });

            var query = Db.People.Expand(_ => _.Client).Expand(_ => _.Type);
            var people = await query.ToListAsync();
            var person1 = people.First(_ => _.Id == 62);
            Assert.IsNotNull(person1.Type);
            Assert.AreEqual(52, person1.Type.Id);
            Assert.AreEqual(52, person1.TypeId);
            Assert.IsNotNull(person1.Client);
            Assert.AreEqual(77, person1.Client.Id);
            Assert.AreEqual(77, person1.ClientId);

            var person2 = people.First(_ => _.Id == 63);
            Assert.IsNotNull(person2.Type);
            Assert.AreEqual(53, person2.Type.Id);
            Assert.AreEqual(53, person2.TypeId);
            Assert.IsNotNull(person2.Client);
            Assert.AreEqual(78, person2.Client.Id);
            Assert.AreEqual(78, person2.ClientId);
        }

        [TestMethod]
        public async Task BuildExpandOperationFromIqlExpression()
        {
            AppDbContext.InMemoryDb.People.Add(new Person { Id = 62, TypeId = 52 });
            AppDbContext.InMemoryDb.People.Add(new Person { Id = 63 });
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType { Id = 53 });
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType { Id = 52 });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap { PersonId = 62, TypeId = 53 });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap { PersonId = 63, TypeId = 52 });

            var query = Db.People.ExpandRelationship(
                string.Join("/", new[]
                {
                    nameof(Person.Types),
                    nameof(PersonTypeMap.Type),
                    nameof(PersonType.CreatedByUser),
                }));
            var people = await query.ToListAsync();
            var person = people.First(_ => _.Id == 62);
            Assert.IsNotNull(person.Type);
            Assert.AreEqual(52, person.Type.Id);
            Assert.AreEqual(52, person.TypeId);
        }

        [TestMethod]
        public async Task ExpandCollectionCount()
        {
            AppDbContext.InMemoryDb.People.Add(new Person { Id = 62, TypeId = 52 });
            AppDbContext.InMemoryDb.People.Add(new Person { Id = 63 });
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType { Id = 53 });
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType { Id = 52 });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap { PersonId = 62, TypeId = 53 });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap { PersonId = 63, TypeId = 52 });

            var people = await Db.People.ExpandCollectionCount(s => s.Types).ToListAsync();
            foreach(var person in people)
            {
                Assert.AreEqual(1, person.TypesCount);
            }
        }

        [TestMethod]
        public async Task ExpandCollectionAndNestedExpandSingleShouldIncludeExpandedEntities()
        {
            AppDbContext.InMemoryDb.People.Add(new Person { Id = 62, TypeId = 52 });
            AppDbContext.InMemoryDb.People.Add(new Person { Id = 63 });
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType { Id = 53 });
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType { Id = 52 });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap { PersonId = 62, TypeId = 53 });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap { PersonId = 63, TypeId = 52 });

            var people = await Db.People.ExpandCollection(s => s.Types, q => q.Expand(r => r.Type)).ToListAsync();
            Assert.IsNotNull(people[0].Types[0].Type);
            Assert.AreEqual(people.Single(p => p.TypeId == 52), people.Single(p => p.Types[0].TypeId == 52).Types[0].Type.People[0]);
        }

        [TestMethod]
        public async Task ExpandShouldApplyMatchingResultsToNonExpandedRelationships()
        {
            AppDbContext.InMemoryDb.People.Add(new Person { Id = 62, TypeId = 52 });
            AppDbContext.InMemoryDb.People.Add(new Person { Id = 63 });
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType { Id = 53 });
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType { Id = 52 });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap { PersonId = 62, TypeId = 53 });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap { PersonId = 63, TypeId = 52 });

            var people = await Db.People.ExpandCollection(s => s.Types, q => q.Expand(r => r.Type)).ToListAsync();
            var person = people.Single(p => p.Id == 62);

            Assert.AreEqual(52, person.Type.Id);
        }

    }
}