using System;
using System.Linq;
using System.Threading.Tasks;
using Haz.App.Data.Entities;
using Iql.JavaScript.Extensions;
using Iql.Tests.Context;
using Iql.Tests.Data.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests.OData
{
    [TestClass]
    public class ODataMultiTests : ODataTestsBase
    {
        [TestMethod]
        public async Task TestPostUpdateDeleteSingleEntity()
        {
            var db = NewHazDb();
            var client = EntityHelper.NewHazClient();
            await RequestLog.LogSessionAsync(async log =>
            {
                // Add
                await AssertAddClient(db, client, log);
                await AssertNoChanges(log, db);

                // Update
                await AssertUpdateClient(db, client, log);
                await AssertNoChanges(log, db);

                // Delete
                await AssertDeleteClient(db, client, log);
                await AssertNoChanges(log, db);
            });
        }

        private static async Task AssertNoChanges(RequestLog log, HazceptionDataContext db)
        {
            log.AssertEmpty();
            await db.SaveChangesAsync();
            log.AssertEmpty();
        }

        private static async Task AssertDeleteClient(HazceptionDataContext db, HazClient client, RequestLog log)
        {
            db.Clients.Delete(client);
            await db.SaveChangesAsync();
            var deleteRequest = log.Deletes.Pop().Single();
            log.AssertEmpty();
            Assert.AreEqual(@"http://localhost:58000/odata/Clients(0)", deleteRequest.Uri);
            Assert.IsNull(deleteRequest.Body);
        }

        private static async Task AssertUpdateClient(HazceptionDataContext db, HazClient client, RequestLog log)
        {
            client.Name = "Some new name";
            await db.SaveChangesAsync();
            var patch = log.Patches.Pop().Single();
            log.AssertEmpty();
            Assert.AreEqual(@"{
  ""Name"": ""Some new name"",
  ""Id"": 0
}".NormalizeJson(), patch.Body.Body.NormalizeJson());
            Assert.AreEqual(@"http://localhost:58000/odata/Clients(0)", patch.Uri);
        }

        private static async Task AssertAddClient(HazceptionDataContext db, HazClient client, RequestLog log)
        {
            db.Clients.Add(client);
            client.Name = "New client 123";
            await db.SaveChangesAsync();
            var request = log.Posts.Pop().Single();
            log.AssertEmpty();
            var changes = db.TemporalDataTracker.GetUpdates();
            Assert.AreEqual(0, changes.Count);
            Assert.AreEqual("http://localhost:58000/odata/Clients", request.Uri);
            Assert.AreEqual(@"{
  ""Id"": 0,
  ""TypeId"": 7,
  ""Name"": ""New client 123"",
  ""Guid"": ""00000000-0000-0000-0000-000000000000"",
  ""CreatedDate"": ""2018-01-01T00:00:00.0+00:00"",
  ""Version"": 0,
  ""PersistenceKey"": ""e4a693fc-1041-4dd9-9f57-7097dd7053a3""
}".NormalizeJson(), request.Body.Body.NormalizeJson());
        }
    }
}