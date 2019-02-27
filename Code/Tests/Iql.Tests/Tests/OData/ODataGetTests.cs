using System.Linq;
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
        public async Task TestGetExpandSingle()
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

            var result = user.ExamResults[0];
            Assert.IsNotNull(result.Exam);
            Assert.IsNotNull(result.Video);
        }

        [TestMethod]
        public async Task TestGetExpandCollection()
        {
            var db = new HazceptionDataContext(new ODataDataStore());
            var clients = await db.Clients.Expand(_ => _.CreatedByUser).Expand(_ => _.Type).ToListAsync();
            var client1 = clients.First(_ => _.Id == 1);
            Assert.IsNotNull(client1.CreatedByUser);
            Assert.IsNotNull(client1.Type);
            Assert.AreEqual(client1.TypeId, client1.Type.Id);
            Assert.AreEqual(client1.CreatedByUserId, client1.CreatedByUser.Id);
            Assert.AreEqual(client1.TypeId, 11);
            Assert.AreEqual(client1.CreatedByUserId, "6a01c713-f19e-4631-98e9-086ba24b8bec");

            var client2 = clients.First(_ => _.Id == 2);
            Assert.IsNotNull(client2.CreatedByUser);
            Assert.IsNotNull(client2.Type);
            Assert.AreEqual(client2.TypeId, client2.Type.Id);
            Assert.AreEqual(client2.CreatedByUserId, client2.CreatedByUser.Id);
            Assert.AreEqual(client2.TypeId, 12);
            Assert.AreEqual(client2.CreatedByUserId, "156d0004-f7ea-427d-b31a-816745363ded");

            var client3 = clients.First(_ => _.Id == 3);
            Assert.IsNotNull(client3.CreatedByUser);
            Assert.IsNotNull(client3.Type);
            Assert.AreEqual(client3.TypeId, client3.Type.Id);
            Assert.AreEqual(client3.CreatedByUserId, client3.CreatedByUser.Id);
            Assert.AreEqual(client3.TypeId, 13);
            Assert.AreEqual(client3.CreatedByUserId, "8ce9d27d-b018-47ba-8316-fe9a1baf1c61");
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

        [TestMethod]
        public async Task TestGeographyGetFromCollection()
        {
            var db = new AppDbContext(new ODataDataStore());
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
            var db = new AppDbContext(new ODataDataStore());
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