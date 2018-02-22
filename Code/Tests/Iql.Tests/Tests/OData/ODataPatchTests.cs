using System;
using System.Linq;
using System.Threading.Tasks;
using Haz.App.Data.Entities;
using Iql.Tests.Context;
using Iql.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests.OData
{
    [TestClass]
    public class ODataPatchTests : ODataTestsBase
    {
        [TestMethod]
        public async Task TestPatchSingleEntity()
        {
            await RequestLog.LogSessionAsync(async log =>
            {
                var db = NewDb();
                var client = new HazClient() { PersistenceKey = new Guid("e4a693fc-1041-4dd9-9f57-7097dd7053a3") };
                db.Clients.Add(client);
                client.Name = "New client 123";
                await db.SaveChanges();
                client.Name = "Some new name";
                await db.SaveChanges();
                var request = log.Patches.Pop().Single();
                Assert.AreEqual(@"{
  ""Name"": ""Some new name"",
  ""Id"": 0
}".CompressJson(), request.Body.Body.CompressJson());
                Assert.AreEqual(@"http://localhost:58000/odata/Clients(0)", request.Uri);
            });
        }
    }
}