using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Haz.App.Data.Entities;
using Iql.JavaScript.Extensions;
using Iql.Queryable.Data.Http;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Iql.Tests.Tests.OData
{
    [TestClass]
    public class ODataPostTests : ODataTestsBase
    {
        [TestMethod]
        public async Task AddingEntityFailureShouldLeaveEntityMarkedAsNotNew()
        {
            await RequestLog.LogSessionAsync(async log =>
            {
                await log.InterceptAsync(
                    (method, c, request) => HttpResult.FromString(
                        @"{""error"":{""code"":"""",""message"":"""",""details"":[{""code"":"""",""target"":""Name"",""message"":""Please enter a name""}]}}", success: false),
                    async () =>
                    {
                        var db = NewHazDb();
                        var client = EntityHelper.NewHazClient();
                        db.Clients.Add(client);
                        client.Name = "New client 123";
                        var result = await db.SaveChangesAsync();
                        var state = db.DataStore.Tracking.TrackingSet<HazClient>().GetEntityState(client);
                        Assert.AreEqual(false, result.Success);
                        Assert.AreEqual(true, state.IsNew);
                        Assert.AreEqual(1, result.Results.Count);
                        var crudResult = result.Results.First();
                        Assert.AreEqual(1, crudResult.EntityValidationResults.Count);
                        var entityValidationResult = crudResult.EntityValidationResults.First();
                        Assert.AreEqual(1, entityValidationResult.Value.PropertyValidationResults.Count());
                        var propertyValidationResult = entityValidationResult.Value.PropertyValidationResults.First();
                        Assert.AreEqual(1, propertyValidationResult.ValidationFailures.Count);
                        var propertyValidationError = propertyValidationResult.ValidationFailures.First();
                        Assert.AreEqual("Please enter a name", propertyValidationError.Message);
                        Assert.AreEqual("Name", propertyValidationResult.Property.Name);
                    });
            });
        }

        [TestMethod]
        public async Task TestPostSingleEntity()
        {
            await RequestLog.LogSessionAsync(async log =>
            {
                Assert.AreEqual(0, log.Posts.Count);
                var db = NewHazDb();
                var client = EntityHelper.NewHazClient();
                db.Clients.Add(client);
                client.Name = "New client 123";
                var result = await db.SaveChangesAsync();
                Assert.AreEqual(true, result.Success);
                var request = log.Posts.Pop().Single();
                var changes = db.DataStore.Tracking.GetUpdates();
                Assert.AreEqual(0, changes.Count);
                Assert.AreEqual("http://localhost:58000/odata/Clients", request.Uri);
                var body = request.Body.Body;
                var compressed = body.CompressJson();
                Assert.AreEqual(@"{
  ""Id"": 0,
  ""TypeId"": 7,
  ""Name"": ""New client 123"",
  ""Guid"": ""00000000-0000-0000-0000-000000000000"",
  ""CreatedDate"": ""2018-01-01T00:00:00.0+00:00"",
  ""Version"": 0,
  ""PersistenceKey"": ""e4a693fc-1041-4dd9-9f57-7097dd7053a3""
}".CompressJson(), compressed);
                await db.SaveChangesAsync();
                Assert.AreEqual(0, log.Posts.Count);
            });
        }
    }
}
