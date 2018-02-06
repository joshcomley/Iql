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
            var runCount = 1;
            for (var i = 0; i < runCount; i++)
            {
                await Run();
            }

            Console.WriteLine();
            Console.WriteLine($"Average: {new TimeSpan((long)Elapseds.Average())}");
            if (runCount > 1)
            {
                Console.WriteLine($"Average (normalised): {new TimeSpan((long)Elapseds.Skip(1).Average())}");
            }
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
                await dataContext
                    .Clients
                    //.Take(50)
                    //.Expand(e => e.Client)
                    //.Expand(e => e.Candidate)
                    //.Expand(e => e.CreatedByUser)
                    //.Expand(e => e.ExamCandidate)
                    //.Expand(e => e.Video)
                    //.Expand(e => e.Exam)
                    //.Expand(e => e.Results)
                    //.ExpandAll()
                    .ToList();
            var results =
                await dataContext
                    .ExamResults
                    .ToList();
            examCandidateResults[0].ClientId = clients[3].Id;
            var examCandidateResult1 = examCandidateResults[0];
            var examCandidateResult2 = examCandidateResults[1];
            stopwatch.Stop();
            Console.WriteLine($"Fetch data: {stopwatch.Elapsed} - {examCandidateResults.Count} entit{(examCandidateResults.Count == 1 ? "y" : "ies")}");
            Elapseds.Add(stopwatch.Elapsed.Ticks);
        }
    }
}
