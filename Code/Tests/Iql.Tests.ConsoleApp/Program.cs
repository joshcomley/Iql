using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Haz.App.Data.Entities;
using Iql.Tests.Context;

namespace Iql.Tests.ConsoleApp
{
    public class Program
    {
        private static async Task Main()
        {
            //await RefreshDataSetAsync();
            //return;

            new HazceptionDataStore().GetData();
            var db2 = new HazceptionDataContext();
            //await Run();
            Console.WriteLine("Started");
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var examCandidateResults =
                await db2
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
            stopwatch.Stop();
            Console.WriteLine($"Fetch data: {stopwatch.Elapsed}");
        }
    }
}
