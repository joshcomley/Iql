using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Iql.OData.Data;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class TrackingTests : TestsBase
    {
        [TestMethod]
        public async Task TestGetExpand()
        {
            var db = new HazceptionDataContext(new ODataDataStore());
            var user = await
                db
                    .Users
                    .ExpandCollection(u => u.ExamResults, examResults =>
                        examResults
                            .Expand(results => results.Exam)
                            .Expand(results => results.Video)
                            .ExpandCollection(results => results.Results, results =>
                                results.Expand(examResult => examResult.Hazard))
                    )
                    .WithKey("2b2b0e44-4579-4965-8e3a-097e6684b767");
            Assert.AreEqual(1, user.ExamResults.Count);
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
        public async Task TestGetHazceptionAllExpands()
        {
            var db = new HazceptionDataContext();
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