using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Brandless.ObjectSerializer;
using Hazception.ApiContext.Base;
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
        public async Task TestGetHazception()
        {
            var db = new HazceptionDataContext();
            var examCandidateResults =
                await db
                .ExamCandidateResults
                //.Take(10)
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
        //[TestMethod]
        public async Task GetHazception()
        {
            //var db = new HazceptionDataContext();
            var inMemoryDb = new HazceptionInMemoryDataBase();
            inMemoryDb.ClientTypes = (await new HazceptionDataContext().ClientTypes.SetTracking(false).ToList()).ToList();
            inMemoryDb.Clients = (await new HazceptionDataContext().Clients.SetTracking(false).ToList()).ToList();
            inMemoryDb.Users = (await new HazceptionDataContext().Users.SetTracking(false).ToList()).ToList();
            inMemoryDb.Videos = (await new HazceptionDataContext().Videos.SetTracking(false).ToList()).ToList();
            inMemoryDb.Exams = (await new HazceptionDataContext().Exams.SetTracking(false).ToList()).ToList();
            inMemoryDb.ExamManagers = (await new HazceptionDataContext().ExamManagers.SetTracking(false).ToList()).ToList();
            inMemoryDb.ExamResults = (await new HazceptionDataContext().ExamResults.SetTracking(false).ToList()).ToList();
            inMemoryDb.ExamCandidateResults = (await new HazceptionDataContext().ExamCandidateResults.SetTracking(false).ToList()).ToList();
            inMemoryDb.ExamCandidates = (await new HazceptionDataContext().ExamCandidates.SetTracking(false).ToList()).ToList();
            inMemoryDb.Hazards = (await new HazceptionDataContext().Hazards.SetTracking(false).ToList()).ToList();

            //var examCandidateResults =
            //    await db
            //    .ExamCandidateResults
            //    //.Expand(e => e.Client)
            //    //.Expand(e => e.Candidate)
            //    //.Expand(e => e.CreatedByUser)
            //    //.Expand(e => e.ExamCandidate)
            //    //.Expand(e => e.Video)
            //    //.Expand(e => e.Exam)
            //    //.Expand(e => e.Results)
            //    .ExpandAll()
            //    .ToList();
            var options = new CSharpSerializeToClassParameters("MyClass");
            var serializer = new CSharpObjectSerializer(options);
            var code = serializer.Serialize(inMemoryDb);
            File.WriteAllText(@"D:\Code\Iql\Code\Tests\Iql.Tests\Utility\Class1.cs", code);
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