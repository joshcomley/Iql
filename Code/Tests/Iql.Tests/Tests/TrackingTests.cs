using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Iql.OData;
using Iql.Queryable;
using Iql.Queryable.Data.Http;
using Iql.Queryable.Data.Lists;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class TrackingTests : TestsBase
    {
        [TestMethod]
        public async Task TestLocallyCreatedEntityBecomesEntityStateEntity()
        {
            var client = new Client();
            client.Name = "Locally created client";
            var clientOData = $@"  ""TypeId"": 1,
  ""Id"": 44,
  ""CreatedByUserId"": ""e7bbb8a0-c242-44f1-9e53-35b6aec9ebf3"",
  ""Name"": ""{client.Name}"",
  ""Description"": ""Description of: {client.Name}"",
  ""Guid"": ""3075f684-af2c-4d97-84f2-4fe90864216b"",
  ""CreatedDate"": ""2018-02-24T13:32:53.6865454Z"",
  ""Version"": 0,
  ""PersistenceKey"": ""baa8d299-57db-4839-8029-1c7ae30a24c1""
";
            await RequestLog.LogSessionAsync(async log =>
            {
                var db = new AppDbContext(new ODataDataStore());
                db.Clients.Add(client);
                await log.InterceptAsync((method, uri, request) =>
                {
                    return new HttpResult($@"{{
  ""@odata.context"": ""http://josh-pc:58000/odata/$metadata#Clients/$entity"",
  {clientOData}
}}", true);
                },
                    async () =>
                    {
                        await db.SaveChanges();
                    });
                DbList<Client> clients = null;
                await log.InterceptAsync((method, uri, request) =>
                    {
                        return new HttpResult($@"{{
  ""@odata.context"": ""http://localhost:28000/odata/$metadata#Clients"",
  ""@odata.count"": 1,
  ""value"": [
    {{
      {clientOData}
    }}
  ]
}}", true);
                    },
                    async () =>
                    {
                        clients = await db.Clients.ToList();
                    });
                Assert.AreEqual(1, clients.Count);
                Assert.AreSame(client, clients[0]);
            });
        }

        [TestMethod]
        public async Task TestGetHazceptionNoExpands()
        {
            var db = new HazceptionDataContext();
            var examCandidateResults =
                await db
                    .ExamCandidateResults
                    .ToList();
        }

        [TestMethod]
        public async Task TestGetHazceptionOneExpand()
        {
            var db = new HazceptionDataContext();
            var examCandidateResults =
                await db
                    .ExamCandidateResults
                    .Expand(e => e.Client)
                    .ToList();
        }

        [TestMethod]
        public async Task MultipleDataContextsShouldReturnDifferentEntitiesForTheSameRequest()
        {
            var db1 = new HazceptionDataContext();
            var db2 = new HazceptionDataContext();
            var examCandidateResults1 =
                await db1
                    .ExamCandidateResults
                    .ToList();
            var examCandidateResults2 =
                await db2
                    .ExamCandidateResults
                    .ToList();
            Assert.AreEqual(examCandidateResults1.Count, examCandidateResults2.Count);
            for (var i = 0; i < examCandidateResults2.Count; i++)
            {
                var examCandidateResult1 = examCandidateResults1[i];
                var examCandidateResult2 = examCandidateResults2[i];
                Assert.AreNotSame(examCandidateResult1, examCandidateResult2);
            }
        }

        [TestMethod]
        public async Task TestGetHazceptionAllExpands()
        {
            var db = new HazceptionDataContext();
            try
            {

                var examCandidateResults =
                    await db
                        .ExamCandidateResults
                        //.Take(50)
                        .Expand(e => e.Client)
                        .Expand(e => e.Candidate)
                        .Expand(e => e.CreatedByUser)
                        .Expand(e => e.ExamCandidate)
                        .Expand(e => e.Video)
                        .Expand(e => e.Exam)
                        .Expand(e => e.Results)
                        //.ExpandAll()
                        .ToList();
            }
            catch (Exception e)
            {

            }
        }

        [TestMethod]
        public void TestTrackingSpeed()
        {
            var date = DateTime.Now;
            for (var i = 0; i < 100; i++)
            {
                var clientType = new ClientType
                {
                    Name = $"Client type {i + 1}"
                };
                clientType.Clients.AddRange(new[]
                {
                    new Client {Name = $"Client {i + 1}"}
                });
                Db.ClientTypes.Add(clientType);
            }
            var time = DateTime.Now - date;
            var seconds = time.TotalSeconds;
        }
    }
}