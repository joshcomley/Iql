using System;
using System.Linq;
using System.Threading.Tasks;
using Haz.App.Data.Entities;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests.OData
{
    [TestClass]
    public class ODataMultiTests : ODataTestsBase
    {
        [TestMethod]
        public async Task TestPostUpdateDeleteSingleEntity()
        {
            var db = NewDb();
            var client = new HazClient { PersistenceKey = new Guid("e4a693fc-1041-4dd9-9f57-7097dd7053a3") };
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
            await db.SaveChanges();
            log.AssertEmpty();
        }

        private static async Task AssertDeleteClient(HazceptionDataContext db, HazClient client, RequestLog log)
        {
            db.Clients.Delete(client);
            await db.SaveChanges();
            var deleteRequest = log.Deletes.Pop().Single();
            log.AssertEmpty();
            Assert.AreEqual(@"http://localhost:58000/odata/Clients(0)", deleteRequest.Uri);
            Assert.IsNull(deleteRequest.Body);
        }

        private static async Task AssertUpdateClient(HazceptionDataContext db, HazClient client, RequestLog log)
        {
            client.Name = "Some new name";
            await db.SaveChanges();
            var patch = log.Patches.Pop().Single();
            log.AssertEmpty();
            Assert.AreEqual(@"{
  ""Name"": ""Some new name"",
  ""Id"": 0
}", patch.Body.Body);
            Assert.AreEqual(@"http://localhost:58000/odata/Clients(0)", patch.Uri);
        }

        private static async Task AssertAddClient(HazceptionDataContext db, HazClient client, RequestLog log)
        {
            db.Clients.Add(client);
            client.Name = "New client 123";
            await db.SaveChanges();
            var request = log.Posts.Pop().Single();
            log.AssertEmpty();
            var changes = db.DataStore.Tracking.GetUpdates();
            Assert.AreEqual(0, changes.Count);
            Assert.AreEqual("http://localhost:58000/odata/Clients", request.Uri);
            Assert.AreEqual(@"{
  ""Users"": [],
  ""Id"": 0,
  ""TypeId"": 0,
  ""Name"": ""New client 123"",
  ""Guid"": ""00000000-0000-0000-0000-000000000000"",
  ""CreatedDate"": ""0001-01-01T00:00:00+00:00"",
  ""Version"": 0,
  ""PersistenceKey"": ""e4a693fc-1041-4dd9-9f57-7097dd7053a3"",
  ""UsersCount"": 0,
  ""Videos"": [],
  ""VideosCount"": 0,
  ""Exams"": [],
  ""ExamsCount"": 0,
  ""ExamManagers"": [],
  ""ExamManagersCount"": 0,
  ""ExamResults"": [],
  ""ExamResultsCount"": 0,
  ""ExamCandidateResults"": [],
  ""ExamCandidateResultsCount"": 0,
  ""ExamCandidates"": [],
  ""ExamCandidatesCount"": 0,
  ""Hazards"": [],
  ""HazardsCount"": 0
}", request.Body.Body);
        }
    }
}