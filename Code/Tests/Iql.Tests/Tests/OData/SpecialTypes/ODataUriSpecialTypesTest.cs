using System;
using System.Threading.Tasks;
using Iql.Data.SpecialTypes;
using Iql.Entities.SpecialTypes;
using Iql.JavaScript.Extensions;
using Iql.OData;
using Iql.OData.Extensions;
using Iql.Tests.Context;
using Iql.Tests.Data.Context;
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
    }
}