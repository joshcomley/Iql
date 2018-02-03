using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Brandless.ObjectSerializer;
using Haz.App.Data.Entities;
using Iql.Queryable;
using Iql.Queryable.Events;
using Iql.Tests.Context;

namespace Iql.Tests.ConsoleApp
{
    class Program
    {
        private static async Task Main()
        {
            //await RefreshDataSetAsync();
            //return;

            Console.WriteLine("Started");
            var stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            //var db = new HazceptionDataContext();
            new HazceptionDataStore().GetData();
            stopwatch1.Stop();
            Console.WriteLine($"Create data context: {stopwatch1.Elapsed}");
            //var stopwatch = new Stopwatch();
            //stopwatch.Start();
            ////var clientTypes =
            ////    await db
            ////        .ClientTypes
            ////        .ToList();

            //var examCandidateResults =
            //    await db
            //        .ExamCandidateResults
            //        //.Take(6)
            //        .Expand(e => e.Client)
            //        .Expand(e => e.Candidate)
            //        .Expand(e => e.CreatedByUser)
            //        .Expand(e => e.ExamCandidate)
            //        .Expand(e => e.Video)
            //        .Expand(e => e.Exam)
            //        .Expand(e => e.Results)
            //        //.ExpandAll()
            //        .ToList();

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
            var options = new CSharpSerializeToClassParameters(nameof(HazceptionDataStore));
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
