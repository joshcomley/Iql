using System;
using System.Threading.Tasks;
using Iql.Entities.SpecialTypes;
using Iql.OData.Extensions;
using Iql.Tests.Context;
using Iql.Tests.Tests.OData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests.DataContext
{
    [TestClass]
    public class SpecialTypes : TestsBase
    {
        [TestMethod]
        public async Task GetCustomReportsNormally()
        {
            AppDbContext.InMemoryDb.MyCustomReports.Clear();
            var firstRemote = new MyCustomReport
            {
                MyName = "abc",
                MyId = new Guid("50c5fd1b-4746-4ca2-9ba5-dfa93f2a2c1b"),
                MyEntityType = "MyType"
            };
            AppDbContext.InMemoryDb.MyCustomReports.Add(firstRemote);
            AppDbContext.InMemoryDb.MyCustomReports.Add(new MyCustomReport
            {
                MyName = "def",
                MyId = new Guid("3220042c-3fb4-4ffd-a3cc-11d06c72bc3e"),
                MyEntityType = "MyOtherType"
            });
            var allCustomReportsLocal = await Db.MyCustomReports.ToListAsync();
            Assert.AreEqual(2, allCustomReportsLocal.Count);
            var firstLocal = await Db.MyCustomReports.GetWithKeyAsync(new Guid("50c5fd1b-4746-4ca2-9ba5-dfa93f2a2c1b"));
            Assert.AreEqual("abc", firstLocal.MyName);
            firstLocal.MyName = "abc2";
            await Db.SaveChangesAsync();
            Assert.AreEqual("abc2", firstRemote.MyName);
            //var allCustomReportsInternal = await Db.CustomReportsManager.Set.ToListAsync();
            //Assert.AreEqual(2, allCustomReportsInternal.Count);
        }

        [TestMethod]
        public async Task GetCustomReportsWithKeyOData()
        {
            AppDbContext.InMemoryDb.MyCustomReports.Clear();
            var firstRemote = new MyCustomReport
            {
                MyName = "abc",
                MyId = new Guid("9cac910f-6b7c-46b8-9de6-d4373a0063d8"),
                MyEntityType = "MyType"
            };
            AppDbContext.InMemoryDb.MyCustomReports.Add(firstRemote);
            AppDbContext.InMemoryDb.MyCustomReports.Add(new MyCustomReport
            {
                MyName = "def",
                MyId = new Guid("571202dc-057f-49b8-8681-8450695fc079"),
                MyEntityType = "MyOtherType"
            });
            var allCustomReportsLocal = await Db.CustomReportsManager.Set.ToListAsync();
            Assert.AreEqual(2, allCustomReportsLocal.Count);
            var query =
                Db.CustomReportsManager.Set.WithKey(new Guid("9cac910f-6b7c-46b8-9de6-d4373a0063d8"));
            var odataUri = await query.ResolveODataUriAsync();
            Assert.AreEqual(@"http://localhost:28000/odata/MyCustomReports(9cac910f-6b7c-46b8-9de6-d4373a0063d8)", odataUri);
        }

        [TestMethod]
        public async Task GetCustomReportsWithMappingOfPropertyNames()
        {
            AppDbContext.InMemoryDb.MyCustomReports.Clear();
            var firstRemote = new MyCustomReport
            {
                MyName = "abc",
                MyId = new Guid("9cac910f-6b7c-46b8-9de6-d4373a0063d8"),
                MyEntityType = "MyType"
            };
            AppDbContext.InMemoryDb.MyCustomReports.Add(firstRemote);
            AppDbContext.InMemoryDb.MyCustomReports.Add(new MyCustomReport
            {
                MyName = "def",
                MyId = new Guid("571202dc-057f-49b8-8681-8450695fc079"),
                MyEntityType = "MyOtherType"
            });
            var allCustomReportsLocal = await Db.CustomReportsManager.Set.ToListAsync();
            Assert.AreEqual(2, allCustomReportsLocal.Count);
            var firstLocal = await Db.CustomReportsManager.Set.GetWithKeyAsync(new Guid("9cac910f-6b7c-46b8-9de6-d4373a0063d8"));
            Assert.AreEqual("abc", firstLocal.Name);
            firstLocal.Name = "abc2";
            var changes = Db.GetChanges();
            Assert.AreEqual(1, changes.Length);
            var result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Length);
            Assert.AreEqual("abc2", firstRemote.MyName);
            Db.CustomReportsManager.Set.Delete(firstLocal);
            var deleteResult = await Db.SaveChangesAsync();
            Assert.IsTrue(deleteResult.Success);
            Assert.AreEqual(1, AppDbContext.InMemoryDb.MyCustomReports.Count);
            var newEntity = new IqlCustomReport();
            Db.CustomReportsManager.Set.Add(newEntity);
            newEntity.Name = "Hey ho";
            await Db.SaveChangesAsync();
            Assert.AreEqual(2, AppDbContext.InMemoryDb.MyCustomReports.Count);
            await Db.SaveChangesAsync();
            Assert.AreEqual(2, AppDbContext.InMemoryDb.MyCustomReports.Count);
            // Test insert and delete
            //var allCustomReportsInternal = await Db.CustomReportsManager.Set.ToListAsync();
            //Assert.AreEqual(2, allCustomReportsInternal.Count);
        }

        [TestMethod]
        public async Task GetCustomReportsWithMappingOfPropertyNamesAndComplexFilter()
        {
            AppDbContext.InMemoryDb.MyCustomReports.Clear();
            var firstRemote = new MyCustomReport
            {
                MyName = "abc",
                MyId = new Guid("533dc6a4-cb5c-4071-95a4-f8555df0efec"),
                MyEntityType = "MyType"
            };
            AppDbContext.InMemoryDb.MyCustomReports.Add(firstRemote);
            AppDbContext.InMemoryDb.MyCustomReports.Add(new MyCustomReport
            {
                MyName = "def",
                MyId = new Guid("b09af00b-3f8f-44fc-9cfc-7b04a509f72b"),
                MyEntityType = "MyOtherType"
            });
            var allCustomReportsLocal = await Db.CustomReportsManager.Set.Where(c => c.Name == "def" && c.EntityType.Length > 5).ToListAsync();
            Assert.AreEqual(1, allCustomReportsLocal.Count);
            Assert.AreEqual("def", allCustomReportsLocal[0].Name);
        }
    }
}