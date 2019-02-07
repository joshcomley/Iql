using System.Threading.Tasks;
using Iql.OData;
using Iql.Tests.Context;
using IqlSampleApp.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests.OData
{
    [TestClass]
    public class ODataGetTests : TestsBase
    {
        [TestMethod]
        public async Task TestGetExpand()
        {
            var db = new HazceptionDataContext(new ODataDataStore(Db.EntityConfigurationContext));
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
            var db = new HazceptionDataContext(new ODataDataStore(Db.EntityConfigurationContext));
            var user = await
                db
                    .Users
                    .ExpandCollection(u => u.ExamResults)
                    .GetWithKeyAsync("this-will-return-null");
        }

        [TestMethod]
        public async Task TestGeographyGetFromCollection()
        {
            var db = new AppDbContext(new ODataDataStore(Db.EntityConfigurationContext));
            var sites = await db.Sites.ToListAsync();
            Assert.AreEqual(2, sites.Count);
            AssertGeographyProperties(sites[0]);
            var siteWithNullProperties = sites[1];
            Assert.IsNull(siteWithNullProperties.Location);
            Assert.IsNull(siteWithNullProperties.Area);
            Assert.IsNull(siteWithNullProperties.Line);
        }

        [TestMethod]
        public async Task TestGeographyGetFromKey()
        {
            var db = new AppDbContext(new ODataDataStore(Db.EntityConfigurationContext));
            var site = await db.Sites.GetWithKeyAsync(1);
            AssertGeographyProperties(site);
        }

        private static void AssertGeographyProperties(Site site)
        {
            Assert.IsNotNull(site);
            Assert.IsNotNull(site.Area);
            Assert.IsNotNull(site.Area.OuterRing);
            Assert.AreEqual(0, site.Area.InnerRings.Count);
            Assert.AreEqual(4, site.Area.OuterRing.Points.Count);
            Assert.IsTrue(site.Area is IqlPolygonExpression);
            Assert.IsTrue(site.Area.OuterRing is IqlRingExpression);
            AssertPoint(site.Area.OuterRing.Points[0], -80.19, 25.774);
            AssertPoint(site.Area.OuterRing.Points[1], -66.118, 18.466);
            AssertPoint(site.Area.OuterRing.Points[2], -64.757, 32.321);
            AssertPoint(site.Area.OuterRing.Points[3], -80.19, 25.774);
            Assert.IsNotNull(site.Line);
            Assert.IsTrue(site.Line is IqlLineExpression);
            Assert.AreEqual(3, site.Line.Points.Count);
            AssertPoint(site.Line.Points[0], -80.19, 25.774);
            AssertPoint(site.Line.Points[1], -66.118, 18.466);
            AssertPoint(site.Line.Points[2], -64.757, 32.321);
            Assert.IsNotNull(site.Location);
            AssertPoint(site.Location, 13.2846523, 52.5067614);
        }

        private static void AssertPoint(IqlPointExpression point, double x, double y)
        {
            Assert.IsTrue(point is IqlPointExpression);
            Assert.AreEqual(x, point.X);
            Assert.AreEqual(y, point.Y);
        }
    }
}