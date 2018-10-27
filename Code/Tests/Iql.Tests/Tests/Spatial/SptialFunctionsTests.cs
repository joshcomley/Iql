using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class SptialFunctionsTests : TestsBase
    {
        internal static IqlLineExpression BermudaTriangleLine = 
            new IqlLineExpression(
                new IqlPointExpression[]
                {
                    new IqlPointExpression(-80.190, 25.774), 
                    new IqlPointExpression(-66.118, 18.466), 
                    new IqlPointExpression(-64.757, 32.321), 
                });
        internal static IqlPolygonExpression BermudaTrianglePolygon = IqlPolygonExpression.From(
            new[]
            {
                new[] { -80.190, 25.774 }, new[] {-66.118, 18.466}, new[] {-64.757, 32.321}, new[] { -80.190, 25.774 }
            });

        internal static IqlPointExpression WithinBermudaTrianglePoint =
            new IqlPointExpression(-76.6887611, 25.4691308);

        internal static IqlPointExpression NotWithinBermudaTrianglePoint =
            new IqlPointExpression(-78.2929751, 21.687572);

        internal static IqlPointExpression BerlinPoint =
            new IqlPointExpression(13.2846523, 52.5067614);

        [TestMethod]
        public async Task TestDistance()
        {
            var distance = IqlPointExpression.DistanceBetween(-2.1379667,
                52.2670518, -0.2050156,
                51.4466596, IqlDistanceKind.Kilometers);
            Assert.IsTrue(distance - 161.055460155299 < 0.000001);
        }

        [TestMethod]
        public async Task TestSuccessfulIntersect()
        {
            Assert.IsTrue(WithinBermudaTrianglePoint.Intersects(BermudaTrianglePolygon));
        }

        [TestMethod]
        public async Task TestFailedIntersect()
        {
            Assert.IsFalse(NotWithinBermudaTrianglePoint.Intersects(BermudaTrianglePolygon));
        }
    }
}