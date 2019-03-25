using System;
using System.Linq;
using System.Threading.Tasks;
using Haz.App.Data.Entities;
using Iql.Data.Http;
using Iql.Data.Tracking;
using Iql.JavaScript.Extensions;
using Iql.Tests.Data.Context;
using IqlSampleApp.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
                        var state = db.TemporalDataTracker.TrackingSet<HazClient>().FindMatchingEntityState(client);
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
                var request = log.Posts.Pop();
                var changes = db.TemporalDataTracker.GetUpdates();
                Assert.AreEqual(0, changes.Count);
                Assert.AreEqual("http://localhost:58000/odata/Clients", request.Uri);
                var body = request.Body.Body;
                var compressed = body.NormalizeJson();
                Assert.AreEqual(@"{
  ""TypeId"": 7,
  ""Name"": ""New client 123"",
  ""Guid"": ""00000000-0000-0000-0000-000000000000"",
  ""CreatedDate"": ""2018-01-01T00:00:00.0+00:00"",
  ""Version"": 0,
  ""PersistenceKey"": ""e4a693fc-1041-4dd9-9f57-7097dd7053a3""
}".NormalizeJson(), compressed);
                await db.SaveChangesAsync();
                Assert.AreEqual(0, log.Posts.Count);
            });
        }

        [TestMethod]
        public async Task TestPostEnum()
        {
            PersistenceKeyGenerator.New = () => new Guid("eb6f72b5-ba5c-49c5-b34b-832bb172d353");
            IqlNewGuidExpression.NewGuid = () => new Guid("32cbb41e-3fb9-4375-86d7-9babefe021b3");
            await RequestLog.LogSessionAsync(async log =>
            {
                Assert.AreEqual(0, log.Posts.Count);
                var db = NewDb();
                var person = new Person
                {
                    Title = "Dummy",
                    Category = PersonCategory.AutoDescription,
                    Skills = PersonSkills.Chef | PersonSkills.Ninja
                };
                db.People.Add(person);
                var result = await db.SaveChangesAsync();
                Assert.AreEqual(true, result.Success);
                var request = log.Posts.Pop();
                var changes = db.TemporalDataTracker.GetUpdates();
                Assert.AreEqual(0, changes.Count);
                Assert.AreEqual("http://localhost:28000/odata/People", request.Uri);
                var body = request.Body.Body;
                var compressed = body.NormalizeJson();
                Assert.AreEqual(@"{
  ""Birthday"": ""0001-01-01T00:00:00.0+00:00"",
  ""IsComplete"": false,
  ""Title"": ""Dummy"",
  ""Description"":""I'm \\ \""auto\"""",
  ""Skills"": ""5"",
  ""Category"": ""2"",
  ""Guid"": ""32cbb41e-3fb9-4375-86d7-9babefe021b3"",
  ""CreatedDate"": ""0001-01-01T00:00:00.0+00:00"",
  ""PersistenceKey"": ""eb6f72b5-ba5c-49c5-b34b-832bb172d353""
}".NormalizeJson(), compressed);
                await db.SaveChangesAsync();
                Assert.AreEqual(0, log.Posts.Count);
            });
            IqlNewGuidExpression.NewGuid = () => Guid.NewGuid();
            PersistenceKeyGenerator.New = () => Guid.NewGuid();
        }

        [TestMethod]
        public async Task TestPostGeographyTypes()
        {
            await RequestLog.LogSessionAsync(async log =>
            {
                Assert.AreEqual(0, log.Posts.Count);
                var db = NewDb();
                var site = new Site
                {
                    FullAddress = "abc",
                    Area = SptialFunctionsTests.BermudaTrianglePolygon,
                    Line = SptialFunctionsTests.BermudaTriangleLine,
                    Location = SptialFunctionsTests.BerlinPoint,
                    Guid = new Guid("f3a3a088-0740-4904-9588-6b4c4ba37656"),
                    PersistenceKey = new Guid("90a702bd-9d2b-444e-ad3e-2ef15c31e016")
                };
                db.Sites.Add(site);
                var result = await db.SaveChangesAsync();
                Assert.AreEqual(true, result.Success);
                var request = log.Posts.Pop();
                var changes = db.TemporalDataTracker.GetUpdates();
                Assert.AreEqual(0, changes.Count);
                Assert.AreEqual("http://localhost:28000/odata/Sites", request.Uri);
                var body = request.Body.Body;
                var compressed = body.NormalizeJson();
                Assert.AreEqual(@"{
  ""Location"": {
    ""type"": ""Point"",
    ""coordinates"": [
      13.2846523,
      52.5067614
    ]
  },
  ""Area"": {
    ""type"": ""Polygon"",
    ""coordinates"": [
      [
        [
          -80.19,
          25.774
        ],
        [
          -66.118,
          18.466
        ],
        [
          -64.757,
          32.321
        ],
        [
          -80.19,
          25.774
        ]
      ]
    ]
  },
  ""Line"": {
    ""type"": ""LineString"",
    ""coordinates"": [
      [
        -80.19,
        25.774
      ],
      [
        -66.118,
        18.466
      ],
      [
        -64.757,
        32.321
      ]
    ]
  },
  ""FullAddress"": ""\n"",
  ""Left"": 0,
  ""Right"": 0,
  ""Guid"": ""f3a3a088-0740-4904-9588-6b4c4ba37656"",
  ""CreatedDate"": ""0001-01-01T00:00:00.0+00:00"",
  ""PersistenceKey"": ""90a702bd-9d2b-444e-ad3e-2ef15c31e016""
}".NormalizeJson(), compressed);
                await db.SaveChangesAsync();
                Assert.AreEqual(0, log.Posts.Count);
            });
        }
    }
}
