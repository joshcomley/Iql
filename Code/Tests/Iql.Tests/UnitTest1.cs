using System.Linq;
using System.Threading.Tasks;
using Iql.DotNet;
using Iql.Queryable;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private static readonly AppDbContext Db = new AppDbContext();

        [ClassInitialize]
        public static async Task SetUp(TestContext textContext)
        {
            IqlQueryableAdapter.ExpressionConverter = () => new ExpressionToIqlConverter();
        }

        [ClassCleanup]
        public static void CleanUp()
        {

        }

        [TestMethod]
        public async Task AddingAnEntityPersistsToDb()
        {
            Assert.AreEqual(0, Db.InMemoryDb.ClientTypes.Count);
            Db.ClientTypes.Add(new ClientType() { Id = 1 });
            Assert.AreEqual(1, Db.DataStore.Queue.Count);
            await Db.SaveChanges();
            Assert.AreEqual(0, Db.DataStore.Queue.Count);
            Assert.AreEqual(1, Db.InMemoryDb.ClientTypes.Count);
        }

        [TestMethod]
        public async Task ChangingAnEntity()
        {
            Db.ClientTypes.Add(new ClientType() { Id = 2 });
            await Db.SaveChanges();
            var clientType = await Db.ClientTypes.First(ct => ct.Id == 2);
        }
    }
}
