using System.Threading.Tasks;
using Iql.OData;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests.OData
{
    [TestClass]
    public class ODataGetTests : TestsBase
    {
        [TestMethod]
        public async Task TestGetExpand()
        {
            var db = new HazceptionDataContext(new ODataDataStore());
            var user = await
                db
                    .Users
                    .ExpandCollection(u => u.ExamResults, examResults =>
                        examResults
                            .Expand(results => results.Exam)
                            .Expand(results => results.Video)
                            .ExpandCollection(results => results.Results, results =>
                                results.Expand(examResult => examResult.Hazard))
                    )
                    .GetWithKeyAsync("2b2b0e44-4579-4965-8e3a-097e6684b767");
            Assert.AreEqual(1, user.ExamResults.Count);
        }

        [TestMethod]
        public async Task TestFailedGet()
        {
            var db = new HazceptionDataContext(new ODataDataStore());
            var user = await
                db
                    .Users
                    .ExpandCollection(u => u.ExamResults)
                    .GetWithKeyAsync("this-will-return-null");
        }

    }
}