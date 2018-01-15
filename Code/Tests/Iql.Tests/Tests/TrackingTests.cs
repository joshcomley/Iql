using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class TrackingTests : TestsBase
    {
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