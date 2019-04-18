using System;
using System.Threading.Tasks;
using Iql.Data.Http;
using Iql.Data.SpecialTypes;
using Iql.Entities.SpecialTypes;
using Iql.JavaScript.Extensions;
using Iql.OData;
using Iql.OData.Extensions;
using Iql.Tests.Context;
using Iql.Tests.Data.Context;
using IqlSampleApp.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests.OData
{
    [TestClass]
    public class ODataUriSpecialTypesTest : TestsBase
    {
        [TestMethod]
        public async Task LoadingCustomReports()
        {
            var query = Db.CustomReportsManager.Set.Where(_ => _.Name == "Hello");
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(@"http://localhost:28000/odata/MyCustomReports?$filter=($it/MyName eq 'Hello')",
                uri);
        }

        [TestMethod]
        public async Task AddingACustomReport()
        {
            var db = new AppDbContext(new ODataDataStore());
            var customReport = new IqlCustomReport() { Search = "abc", Name = "Report Name", UserId = "MyUser" };
            var query = db.CustomReportsManager.Set.Add(customReport);
            await RequestLog.LogSessionAsync(async log =>
            {
                Assert.AreEqual(0, log.Posts.Count);
                var result = await db.SaveChangesAsync();
                Assert.IsTrue(result.Success);
                Assert.AreEqual(1, log.Posts.Count);
                var request = log.Posts.Pop();
                var json = request.Body.Body;
                Assert.AreEqual(@"{
  ""MyUserId"": ""MyUser"",
  ""MyName"": ""Report Name"",
  ""MySortDescending"": false,
  ""MySearch"": ""abc""
}".NormalizeJson(), json.NormalizeJson());
            });
        }

        [TestMethod]
        public async Task ModifyingACustomReport()
        {
            var db = new AppDbContext(new ODataDataStore());
            await RequestLog.LogSessionAsync(async log =>
            {
                await log.InterceptAsync(
                    (method, url, request) =>
                    {
                        switch (method)
                        {
                            case HttpMethod.Get:
                                Assert.AreEqual(url, "http://localhost:28000/odata/MyCustomReports(3d054172-0b50-41a5-1450-08d6afabd0ad)");
                                return HttpResult.FromString(
                                    @"{
      ""MyId"": ""3d054172-0b50-41a5-1450-08d6afabd0ad"",
      ""MyUserId"": ""cd403222-0c78-4adc-bd53-0fec97841f96"",
      ""MyName"": ""Completed"",
      ""MyEntityType"": ""Todo"",
      ""MyIql"": ""{\""Left\"":{\""Left\"":{\""PropertyName\"":\""Status\"",\""IsIqlExpression\"":true,\""Kind\"":30,\""ReturnType\"":1,\""Parent\"":{\""EntityTypeName\"":null,\""VariableName\"":\""_0\"",\""Value\"":\""\"",\""InferredReturnType\"":4,\""IsIqlExpression\"":true,\""Kind\"":28,\""ReturnType\"":1,\""Parent\"":null}},\""Right\"":{\""Namespace\"":null,\""Value\"":[{\""Name\"":\""Complete\"",\""Value\"":3,\""InferredReturnType\"":5,\""IsIqlExpression\"":true,\""Kind\"":54,\""ReturnType\"":11,\""Parent\"":null}],\""InferredReturnType\"":2,\""IsIqlExpression\"":true,\""Kind\"":53,\""ReturnType\"":9,\""Parent\"":null},\""IsIqlExpression\"":true,\""Kind\"":10,\""ReturnType\"":1,\""Parent\"":null},\""Right\"":{\""Left\"":{\""PropertyName\"":\""Status\"",\""IsIqlExpression\"":true,\""Kind\"":30,\""ReturnType\"":1,\""Parent\"":{\""EntityTypeName\"":null,\""VariableName\"":\""_0\"",\""Value\"":\""\"",\""InferredReturnType\"":4,\""IsIqlExpression\"":true,\""Kind\"":28,\""ReturnType\"":1,\""Parent\"":null}},\""Right\"":{\""Namespace\"":null,\""Value\"":[{\""Name\"":\""NotStarted\"",\""Value\"":1,\""InferredReturnType\"":5,\""IsIqlExpression\"":true,\""Kind\"":54,\""ReturnType\"":11,\""Parent\"":null}],\""InferredReturnType\"":2,\""IsIqlExpression\"":true,\""Kind\"":53,\""ReturnType\"":9,\""Parent\"":null},\""IsIqlExpression\"":true,\""Kind\"":10,\""ReturnType\"":1,\""Parent\"":null},\""IsIqlExpression\"":true,\""Key\"":\""Enum\"",\""Kind\"":4,\""ReturnType\"":1,\""Parent\"":null}"",
      ""MySort"": ""Status"",
      ""MySortDescending"": false,
      ""MySearch"": null,
      ""MyFields"": null
}", success: true);
                            case HttpMethod.Patch:
                                Assert.AreEqual(url, "http://localhost:28000/odata/MyCustomReports(3d054172-0b50-41a5-1450-08d6afabd0ad)");
                                return HttpResult.FromString(
                                    @"{
      ""MyId"": ""3d054172-0b50-41a5-1450-08d6afabd0ad"",
      ""MyUserId"": ""cd403222-0c78-4adc-bd53-0fec97841f96"",
      ""MyName"": ""Changed"",
      ""MyEntityType"": ""Todo"",
      ""MyIql"": ""{\""Left\"":{\""Left\"":{\""PropertyName\"":\""Status\"",\""IsIqlExpression\"":true,\""Kind\"":30,\""ReturnType\"":1,\""Parent\"":{\""EntityTypeName\"":null,\""VariableName\"":\""_0\"",\""Value\"":\""\"",\""InferredReturnType\"":4,\""IsIqlExpression\"":true,\""Kind\"":28,\""ReturnType\"":1,\""Parent\"":null}},\""Right\"":{\""Namespace\"":null,\""Value\"":[{\""Name\"":\""Complete\"",\""Value\"":3,\""InferredReturnType\"":5,\""IsIqlExpression\"":true,\""Kind\"":54,\""ReturnType\"":11,\""Parent\"":null}],\""InferredReturnType\"":2,\""IsIqlExpression\"":true,\""Kind\"":53,\""ReturnType\"":9,\""Parent\"":null},\""IsIqlExpression\"":true,\""Kind\"":10,\""ReturnType\"":1,\""Parent\"":null},\""Right\"":{\""Left\"":{\""PropertyName\"":\""Status\"",\""IsIqlExpression\"":true,\""Kind\"":30,\""ReturnType\"":1,\""Parent\"":{\""EntityTypeName\"":null,\""VariableName\"":\""_0\"",\""Value\"":\""\"",\""InferredReturnType\"":4,\""IsIqlExpression\"":true,\""Kind\"":28,\""ReturnType\"":1,\""Parent\"":null}},\""Right\"":{\""Namespace\"":null,\""Value\"":[{\""Name\"":\""NotStarted\"",\""Value\"":1,\""InferredReturnType\"":5,\""IsIqlExpression\"":true,\""Kind\"":54,\""ReturnType\"":11,\""Parent\"":null}],\""InferredReturnType\"":2,\""IsIqlExpression\"":true,\""Kind\"":53,\""ReturnType\"":9,\""Parent\"":null},\""IsIqlExpression\"":true,\""Kind\"":10,\""ReturnType\"":1,\""Parent\"":null},\""IsIqlExpression\"":true,\""Key\"":\""Enum\"",\""Kind\"":4,\""ReturnType\"":1,\""Parent\"":null}"",
      ""MySort"": ""Status"",
      ""MySortDescending"": false,
      ""MySearch"": null,
      ""MyFields"": null
}", success: true);
                        }
                        Assert.IsNotNull(null);
                        return null;
                    },
                    async () =>
                    {
                        var result = await db.CustomReportsManager.Set.GetWithKeyAsync(new Guid("3d054172-0b50-41a5-1450-08d6afabd0ad"));
                        Assert.AreEqual(result.Id, new Guid("3d054172-0b50-41a5-1450-08d6afabd0ad"));
                        result.Name = "Changed";
                        var saveResult = await db.SaveChangesAsync();
                        Assert.AreEqual(true, saveResult.Success);
                    });
            });
        }
    }
}