﻿using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class SptialFunctionsTests : TestsBase
    {
        private static IqlGeographyPolygonExpression BermudaTriangle = IqlGeographyPolygonExpression.From(
            new[]
            {
                new[] {25.774, -80.190}, new[] {18.466, -66.118}, new[] {32.321, -64.757}, new[] {25.774, -80.190}
            });

        [TestMethod]
        public async Task TestDistance()
        {
            var distance = IqlPointExpression.DistanceBetween(
                52.2670518, -2.1379667,
                51.4466596, -0.2050156,
                IqlDistanceKind.Kilometers);
            Assert.IsTrue(distance - 161.055460155299 < 0.000001);
        }

        [TestMethod]
        public async Task TestSuccessfulIntersect()
        {
            Assert.IsTrue(new IqlGeographyPointExpression(25.4691308, -76.6887611).Intersects(BermudaTriangle));
        }

        [TestMethod]
        public async Task TestFailedIntersect()
        {
            Assert.IsFalse(new IqlGeographyPointExpression(21.687572, -78.2929751).Intersects(BermudaTriangle));
        }
    }
}