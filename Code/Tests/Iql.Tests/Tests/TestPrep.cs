using System.Linq;
using Iql.Queryable.Data.Queryable;
using Iql.Tests.Context;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests
{
    public static class TestPrep
    {
        public static DbQueryable<Person> PrepFilterCollectionTest()
        {
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
            {
                Id = 2
            });
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
            {
                Id = 3
            });
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
            {
                Id = 4
            });
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 2
            });
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 3,
                Title = "Test"
            });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap
            {
                PersonId = 1,
                TypeId = 1,
                Description = "Abc"
            });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap
            {
                PersonId = 1,
                TypeId = 2,
                Description = "Abc"
            });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap
            {
                PersonId = 2,
                TypeId = 1,
                Description = "Abc"
            });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap
            {
                PersonId = 2,
                TypeId = 3,
                Description = "Kettle"
            });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap
            {
                PersonId = 3,
                TypeId = 4,
                Description = "Def"
            });
            return TestsBase.Db.People.Where(p =>
                p.Title == "Test" || p.Types.Any(t => t.TypeId == 4 || t.Description == "Kettle"));
        }

    }
}