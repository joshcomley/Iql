using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Brandless.ObjectSerializer;
using Iql.Queryable;
using Iql.Queryable.Data.Lists;
using Iql.Queryable.Events;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.ConsoleApp
{
    [TestClass]
    public class Runner
    {
        public Runner()
        {
            new HazceptionDataStore().GetData();
            this.Db = new HazceptionDataContext();
        }

        public HazceptionDataContext Db { get; set; }

        [TestMethod]
        public async Task Run()
        {
            //Console.WriteLine("Started");
            //var stopwatch1 = new Stopwatch();
            //stopwatch1.Start();
            //var db = new HazceptionDataContext();
            //stopwatch1.Stop();
            //Console.WriteLine($"Create data context: {stopwatch1.Elapsed}");

            //var stopwatch = new Stopwatch();
            //stopwatch.Start();
            //var clientTypes =
            //    await db
            //        .ClientTypes
            //        .ToList();

            var examCandidateResults =
                await Db
                    .ExamCandidateResults
                    //.Take(6)
                    .Expand(e => e.Client)
                    .Expand(e => e.Candidate)
                    .Expand(e => e.CreatedByUser)
                    .Expand(e => e.ExamCandidate)
                    .Expand(e => e.Video)
                    .Expand(e => e.Exam)
                    .Expand(e => e.Results)
                    //.ExpandAll()
                    .ToListAsync();

            ////var clients =
            ////    await db
            ////        .Clients
            ////        //.Take(6)
            ////        //.Expand(e => e.Client)
            ////        //.Expand(e => e.Candidate)
            ////        //.Expand(e => e.CreatedByUser)
            ////        //.Expand(e => e.ExamCandidate)
            ////        //.Expand(e => e.Video)
            ////        //.Expand(e => e.Exam)
            ////        //.Expand(e => e.Results)
            ////        //.ExpandAll()
            ////        .ToList();
            //stopwatch.Stop();
            //Console.WriteLine($"Fetch data: {stopwatch.Elapsed}");
            //Console.WriteLine($"Relationships matched: {(examCandidateResults[0].Client != null ? "Yep" : "Nope")}");
        }

        private static async Task RefreshDataSetAsync()
        {
            var stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            await GetHazception();
            stopwatch2.Stop();
            Console.WriteLine($"Done in {stopwatch2.Elapsed}");
        }

        public static async Task GetHazception()
        {
            //var db = new HazceptionDataContext();
            var inMemoryDb = new HazceptionInMemoryDataBase();
            inMemoryDb.ClientTypes = (await new HazceptionDataContext().ClientTypes.SetTracking(false).ToListAsync()).ToList();
            inMemoryDb.Clients = (await new HazceptionDataContext().Clients.SetTracking(false).ToListAsync()).ToList();
            inMemoryDb.Users = (await new HazceptionDataContext().Users.SetTracking(false).ToListAsync()).ToList();
            inMemoryDb.Videos = (await new HazceptionDataContext().Videos.SetTracking(false).ToListAsync()).ToList();
            inMemoryDb.Exams = (await new HazceptionDataContext().Exams.SetTracking(false).ToListAsync()).ToList();
            inMemoryDb.ExamManagers = (await new HazceptionDataContext().ExamManagers.SetTracking(false).ToListAsync()).ToList();
            inMemoryDb.ExamResults = (await new HazceptionDataContext().ExamResults.SetTracking(false).ToListAsync()).ToList();
            inMemoryDb.ExamCandidateResults = (await new HazceptionDataContext().ExamCandidateResults.SetTracking(false).ToListAsync()).ToList();
            inMemoryDb.ExamCandidates = (await new HazceptionDataContext().ExamCandidates.SetTracking(false).ToListAsync()).ToList();
            inMemoryDb.Hazards = (await new HazceptionDataContext().Hazards.SetTracking(false).ToListAsync()).ToList();

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
            var options = new CSharpSerializeToClassParameters(nameof(HazceptionDataStore));
            //options.AllowObjectInitializer = false;
            options.IgnoreConditions.Add(new IgnoreCondition((o, info) =>
            {
                if (typeof(IRelatedList).IsAssignableFrom(info.PropertyType) ||
                    typeof(IEventEmitterBase).IsAssignableFrom(info.PropertyType) ||
                    typeof(IEventSubscriberBase).IsAssignableFrom(info.PropertyType))
                {
                    return true;
                }
                return false;
            }));
            var serializer = new CSharpObjectSerializer(options);
            var code = serializer.Serialize(inMemoryDb);
            await File.WriteAllTextAsync(@"D:\Code\Iql\Code\Tests\Iql.Tests\Context\HazceptionDataStore.cs", code);
        }
    }
}