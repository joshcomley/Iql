using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            //await Run();
            for (var i = 0; i < 10; i++)
            {
                await Run();
            }

            Console.WriteLine();
            Console.WriteLine($"Average: {new TimeSpan((long)Elapseds.Average())}");
            Console.WriteLine($"Average (normalised): {new TimeSpan((long)Elapseds.Skip(1).Average())}");
            Console.WriteLine($"Total: {new TimeSpan((long)Elapseds.Sum())}");
        }

        private static readonly List<long> Elapseds = new List<long>();
        private static async Task Run()
        {
            var dataContext = new HazceptionDataContext();
            Console.WriteLine("Started");
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var examCandidateResults =
                await dataContext
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
            //examCandidateResults[0].ClickCount = 7;
            stopwatch.Stop();
            Console.WriteLine($"Fetch data: {stopwatch.Elapsed} - {examCandidateResults.Count} entit{(examCandidateResults.Count == 1 ? "y" : "ies")}");
            Elapseds.Add(stopwatch.Elapsed.Ticks);
        }
    }
}
