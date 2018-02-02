using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Haz.App.Data.Entities;
using Iql.Tests.Context;

namespace Iql.Tests.ConsoleApp
{
    class Program
    {
        private static async Task Main()
        {
            Console.WriteLine("Started");
            var stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            var db = new HazceptionDataContext();
            stopwatch1.Stop();
            Console.WriteLine($"Create data context: {stopwatch1.Elapsed}");
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var examCandidateResults =
                await db
                    .ExamCandidateResults
                    //.Take(6)
                    //.Expand(e => e.Client)
                    //.Expand(e => e.Candidate)
                    //.Expand(e => e.CreatedByUser)
                    //.Expand(e => e.ExamCandidate)
                    //.Expand(e => e.Video)
                    //.Expand(e => e.Exam)
                    //.Expand(e => e.Results)
                    //.ExpandAll()
                    .ToList();
            var clients =
                await db
                    .Clients
                    //.Take(6)
                    //.Expand(e => e.Client)
                    //.Expand(e => e.Candidate)
                    //.Expand(e => e.CreatedByUser)
                    //.Expand(e => e.ExamCandidate)
                    //.Expand(e => e.Video)
                    //.Expand(e => e.Exam)
                    //.Expand(e => e.Results)
                    //.ExpandAll()
                    .ToList();
            stopwatch.Stop();
            Console.WriteLine($"Fetch data: {stopwatch.Elapsed}");
        }
    }
}
