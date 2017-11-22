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

        [TestCleanup]
        public void TestCleanUp()
        {
            Db.InMemoryDb.ClientTypes.Clear();
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
        public async Task TestWithKey()
        {
            var id = 1;
            Db.ClientTypes.Add(new ClientType { Id = id });
            Db.ClientTypes.Add(new ClientType { Id = id + 1 });
            await Db.SaveChanges();
            var clientType = await Db.ClientTypes.WithKey(id);
            Assert.IsNotNull(clientType);
            Assert.AreEqual(clientType.Id, id);
        }

        [TestMethod]
        public async Task TestWhere()
        {
            var id = 1;
            Db.ClientTypes.Add(new ClientType { Id = id, Name = "First" });
            var id2 = id + 1;
            Db.ClientTypes.Add(new ClientType { Id = id2, Name = "Second" });
            await Db.SaveChanges();
            var clientType = await Db.ClientTypes.Where(ct => ct.Name == "Second").Single();
            Assert.IsNotNull(clientType);
            Assert.AreEqual(clientType.Id, id2);
        }

        [TestMethod]
        public async Task TestWhereDifferentCase()
        {
            var id = 1;
            Db.ClientTypes.Add(new ClientType { Id = id, Name = "First" });
            var id2 = id + 1;
            Db.ClientTypes.Add(new ClientType { Id = id2, Name = "Second" });
            await Db.SaveChanges();
            var clientType = await Db.ClientTypes.Where(ct => ct.Name == "SECOND").Single();
            Assert.IsNotNull(clientType);
            Assert.AreEqual(clientType.Id, id2);
        }

        [TestMethod]
        public async Task ChangingAnEntity()
        {
            var entity = new ClientType { Id = 2 };
            Db.ClientTypes.Add(entity);
            await Db.SaveChanges();
            var clientType = await Db.ClientTypes.First(ct => ct.Id == entity.Id);
            Assert.AreEqual(entity.Id, clientType.Id);
            var changes = Db.DataStore.GetChanges();
            Assert.AreEqual(0, changes.Count());
            clientType.Name = "Something else";
            changes = Db.DataStore.GetChanges();
            Assert.AreEqual(1, changes.Count());
        }
    }
}
