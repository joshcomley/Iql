using System.Linq;
using System.Threading.Tasks;
using Iql.Tests.Context;
using IqlSampleApp.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class InMemoryTests : TestsBase
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            Cleanup();
        }

        [TestCleanup]
        public override void TestCleanUp()
        {
            base.TestCleanUp();
            Cleanup();
        }

        private static void Cleanup()
        {
            Db.InMemoryDataStore.DefaultPageSize = null;
            AppDbContext.InMemoryDb.PeopleTypes.Clear();
        }

        [TestMethod]
        public async Task TestManualPagingInMemory()
        {
            for (var i = 1; i <= 300; i++)
            {
                AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType { Id = i });
            }
            var peopleTypes = await Db.PersonTypes.Take(10).ToListAsync();
            Assert.AreEqual(30, peopleTypes.PagingInfo.PageCount);
            Assert.AreEqual(10, peopleTypes.Count);
        }

        [TestMethod]
        public async Task TestAutoPagingInMemory()
        {
            for (var i = 1; i <= 300; i++)
            {
                AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType { Id = i });
            }
            Db.InMemoryDataStore.DefaultPageSize = 5;
            var peopleTypes = await Db.PersonTypes.ToListAsync();
            Assert.AreEqual(60, peopleTypes.PagingInfo.PageCount);
            Assert.AreEqual(5, peopleTypes.Count);
        }

        [TestMethod]
        public async Task TestCountInMemory()
        {
            for (var i = 1; i <= 300; i++)
            {
                var personType = new PersonType { Id = i };
                if (i >= 10 && i < 20)
                {
                    personType.Title = "abc";
                }
                AppDbContext.InMemoryDb.PeopleTypes.Add(personType);
            }
            Db.InMemoryDataStore.DefaultPageSize = 5;
            var count = await Db.PersonTypes.CountAsync(_=>_.Title == "abc");
            var count2 = await Db.PersonTypes.Where(_=>_.Title == "abc").CountAsync();
            Assert.AreEqual(10, count);
            Assert.AreEqual(10, count2);
        }

        [TestMethod]
        public async Task TestSkipInMemory()
        {
            for (var i = 1; i <= 300; i++)
            {
                AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType { Id = i });
            }
            var peopleTypes = await Db.PersonTypes.Skip(10).Take(10).ToListAsync();
            Assert.AreEqual(30, peopleTypes.PagingInfo.PageCount);
            Assert.AreEqual(10, peopleTypes.Count);
            Assert.AreEqual(11, peopleTypes.First().Id);
        }
    }
}