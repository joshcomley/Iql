using System;
using System.Threading.Tasks;
using Iql.Tests.Context;
using Iql.Tests.Tests.OData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests.SpecialTypes
{
    [TestClass]
    public class SpecialTypesTests : TestsBase
    {
        [TestMethod]
        public async Task TestSaveCustomReport()
        {
            var cloudReport = new MyCustomReport
            {
                MyId = new Guid("b3c1d398-a921-4b9d-844b-fd891d5cc37e"),
                MyName = "Report Name",
                MyIql = "Some IQL"
            };
            AppDbContext.InMemoryDb.MyCustomReports.Add(cloudReport);
            var report =
                await Db.CustomReportsManager.Set.GetWithKeyAsync(new Guid("b3c1d398-a921-4b9d-844b-fd891d5cc37e"));
            Assert.IsNotNull(report);
            Assert.AreEqual("Report Name", report.Name);
            Assert.AreEqual("Some IQL", report.Iql);
            report.Iql = "New IQL";
            report.Name = "New Report Name";
            var result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            Assert.AreEqual(cloudReport.MyName, "New Report Name");
            Assert.AreEqual(cloudReport.MyIql, "New IQL");
        }
    }
}
