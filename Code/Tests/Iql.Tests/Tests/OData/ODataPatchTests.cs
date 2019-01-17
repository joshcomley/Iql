using System;
using System.Linq;
using System.Threading.Tasks;
using Haz.App.Data.Entities;
using Iql.JavaScript.Extensions;
using Iql.Tests.Context;
using Iql.Tests.Data.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Tests.OData
{
    [TestClass]
    public class ODataPatchTests : ODataTestsBase
    {
        [TestMethod]
        public async Task TestPatchSelectedPropertiesOnly()
        {
            var db = NewHazDb();
            var nameProperty = db.EntityConfigurationContext.EntityType<HazClient>().FindProperty(nameof(HazClient.Name));
            var descriptionProperty = db.EntityConfigurationContext.EntityType<HazClient>().FindProperty(nameof(HazClient.Description));
            await RequestLog.LogSessionAsync(async log =>
            {
                var client = EntityHelper.NewHazClient();
                db.Clients.Add(client);
                client.Name = "New client 123";
                var result = await db.SaveChangesAsync();
                Assert.IsTrue(result.Success);
                client.Name = "Some new name";
                client.Description = "Some new description";
                result = await db.SaveChangesAsync(null, new[]{ nameProperty });
                Assert.IsTrue(result.Success);
                var request = log.Patches.Pop().Single();
                Assert.AreEqual(@"{
  ""Name"": ""Some new name"",
  ""Id"": 0
}".CompressJson(), request.Body.Body.CompressJson());
                Assert.AreEqual(@"http://localhost:58000/odata/Clients(0)", request.Uri);
                client.Name = "Some new name 2";
                client.Description = "Some new description 2";
                result = await db.SaveChangesAsync(null, new[] { descriptionProperty });
                Assert.IsTrue(result.Success);
                request = log.Patches.Pop().Single();
                Assert.AreEqual(@"{
  ""Description"": ""Some new description 2"",
  ""Id"": 0
}".CompressJson(), request.Body.Body.CompressJson());
                Assert.AreEqual(@"http://localhost:58000/odata/Clients(0)", request.Uri);
            });
        }

        [TestMethod]
        public async Task TestPatchSingleEntity()
        {
            await RequestLog.LogSessionAsync(async log =>
            {
                var db = NewHazDb();
                var client = EntityHelper.NewHazClient();
                db.Clients.Add(client);
                client.Name = "New client 123";
                var result = await db.SaveChangesAsync();
                Assert.IsTrue(result.Success);
                client.Name = "Some new name";
                result = await db.SaveChangesAsync();
                var request = log.Patches.Pop().Single();
                Assert.AreEqual(@"{
  ""Name"": ""Some new name"",
  ""Id"": 0
}".CompressJson(), request.Body.Body.CompressJson());
                Assert.AreEqual(@"http://localhost:58000/odata/Clients(0)", request.Uri);
            });
        }

        [TestMethod]
        public async Task TestUpdatePivotEntityWithNewRelationshipKey()
        {

            await RequestLog.LogSessionAsync(async log =>
            {
                var db = NewDb();
                var map = new PersonTypeMap
                {
                    CreatedDate = DateTime.Now,
                    PersonId = 1,
                    TypeId = 1,
                    Description = "Abc",
                    Notes = "Def"
                };
                db.PersonTypesMap.Add(map);
                var result = await db.SaveChangesAsync();
                Assert.IsTrue(result.Success);
                map.PersonId = 2;
                result = await db.SaveChangesAsync();
                var request = log.Patches.Pop().Single();
                Assert.AreEqual(@"http://localhost:28000/odata/PersonTypesMap(PersonId=1,TypeId=1)", request.Uri);
                Assert.AreEqual(@"{
  ""PersonId"": 2,
  ""TypeId"": 1
}".CompressJson(), request.Body.Body.CompressJson());
            });
        }
    }
}