using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Haz.App.Data.Entities;
using Iql.DotNet.Serialization;
using Iql.Queryable.Data;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Extensions;
using Iql.Queryable.Operations;
using Iql.Tests.Context;

namespace Iql.Tests.ConsoleApp
{
    public class Program
    {
        private static async Task Main()
        {
            //var iql = IqlSerializer.DeserializeFromXml(
            //    @"<?xml version=""1.0"" encoding=""utf-16""?> <IqlPropertyExpression xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""> <Type>Property</Type> <ReturnType>Unknown</ReturnType> <Parent xsi:type=""IqlRootReferenceExpression""> <Type>RootReference</Type> <ReturnType>Unknown</ReturnType> <VariableName>p</VariableName> </Parent> <PropertyName>Title</PropertyName> </IqlPropertyExpression>");
            //CSharpObjectSerializer serializer = new CSharpObjectSerializer();
            //var str = serializer.Serialize(iql);
            //await RefreshDataSetAsync();
            //return;
            //var db = new AppDbContext();
            //AppDbContext.InMemoryDb.RiskAssessments.a
            //var abc = await db.RiskAssessments.ToList();

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
            Console.WriteLine($"Composite Keys: {CompositeKey.All.Select(x=>x.AsKeyString()).Distinct().Count()}");
//            Console.WriteLine($"Composite Key Strings: {CompositeKeyExtensions.AsKeyStringCount}");
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
            //var clients =
            //    await dataContext
            //        .Clients
            //        //.Take(50)
            //        //.Expand(e => e.Client)
            //        //.Expand(e => e.Candidate)
            //        //.Expand(e => e.CreatedByUser)
            //        //.Expand(e => e.ExamCandidate)
            //        //.Expand(e => e.Video)
            //        //.Expand(e => e.Exam)
            //        //.Expand(e => e.Results)
            //        //.ExpandAll()
            //        .ToList();
            //var users =
            //    await dataContext
            //        .Users
            //        .ToList();
            //var results =
            //    await dataContext
            //        .ExamResults
            //        .ToList();
            //var user1 = users[0];
            //var client1 = clients[0];
            //user1.ClientId = 55555;
            //user1.Client = client1;
            //user1.ClientId = 55555;
            //var examCandidateResult1 = examCandidateResults[0];
            //var newClient = clients[3];
            //var oldClient = examCandidateResults[0].Client;
            //examCandidateResults[0].ClientId = newClient.Id;
            //var examCandidateResult2 = examCandidateResults[1];
            stopwatch.Stop();
            Console.WriteLine($"Fetch data: {stopwatch.Elapsed} - {examCandidateResults.Count} entit{(examCandidateResults.Count == 1 ? "y" : "ies")}");
            Elapseds.Add(stopwatch.Elapsed.Ticks);
        }
    }
}
