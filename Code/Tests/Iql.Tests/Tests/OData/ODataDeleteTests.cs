using System;
using System.Linq;
using System.Threading.Tasks;
using Haz.App.Data.Entities;
using Iql.OData;
using Iql.Tests.Context;
using Iql.Tests.Data.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Tests.OData
{
    [TestClass]
    public class ODataDeleteTests : ODataTestsBase
    {
        [TestMethod]
        public async Task DeleteCustomReportsWithKeyOData()
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
            await RequestLog.LogSessionAsync(async log =>
            {
                var db = new AppDbContext(new ODataDataStore());
                var customReport =
                    await db.CustomReportsManager.Set.GetWithKeyAsync(new Guid("571202dc-057f-49b8-8681-8450695fc079"));
                db.DeleteEntity(customReport);
                var result = await db.SaveChangesAsync();
                Assert.AreEqual(true, result.Success);
                var request = log.Deletes.Pop();
                Assert.AreEqual(@"http://localhost:28000/odata/MyCustomReports('571202dc-057f-49b8-8681-8450695fc079')", request.Uri);
            });
        }

        [TestMethod]
        public async Task TestDeleteSingleEntity()
        {
            await RequestLog.LogSessionAsync(async log =>
            {
                var db = NewHazDb();
                var client = EntityHelper.NewHazClient();
                db.Clients.Add(client);
                client.Name = "New client 123";
                await db.SaveChangesAsync();
                db.Clients.Delete(client);
                await db.SaveChangesAsync();
                var request = log.Deletes.Pop();
                Assert.AreEqual(@"http://localhost:58000/odata/Clients(0)", request.Uri);
                Assert.IsNull(request.Body);
            });
        }

        //[TestMethod]
        //public async Task TestDeleteRelatedEntity()
        //{
        //    await RequestLog.LogSessionAsync(async log =>
        //    {
        //        AppDbContext.InMemoryDb.People.Add(new Person { Id = 7 });
        //        AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType { Id = 2 });
        //        AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap { PersonId = 7, TypeId = 2 });
        //        var db = NewDb();
        //        var person = db.ExamManagers
        //        var request = log.Deletes.Pop().Single();
        //        Assert.AreEqual(@"http://localhost:58000/odata/Clients(0)", request.Uri);
        //        Assert.IsNull(request.Body);
        //    });
        //}
    }
}