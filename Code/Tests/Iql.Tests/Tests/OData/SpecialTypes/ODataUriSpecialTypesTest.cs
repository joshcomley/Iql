using System;
using System.Threading.Tasks;
using Iql.Data.SpecialTypes;
using Iql.Entities.SpecialTypes;
using Iql.OData.Extensions;
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
    }
}