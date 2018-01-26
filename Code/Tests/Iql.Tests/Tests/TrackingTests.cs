using System;
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
        public async Task GetHazception()
        {
            var db = new HazceptionDataContext();
            var examCandidateResults = 
                await db
                .ExamCandidateResults
                .ExpandAll()
                .ToList();
            var options = new CSharpSerializeToClassParameters("MyClass");
            var serializer = new CSharpObjectSerializer(options);
            var code = serializer.Serialize(examCandidateResults);
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