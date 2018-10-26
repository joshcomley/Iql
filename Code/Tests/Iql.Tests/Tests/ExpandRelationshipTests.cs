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