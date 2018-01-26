using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests {
    [TestClass]
    public class IqlExpressionTests : TestsBase
    {
        [TestMethod]
        public async Task ExpandShouldIncludeExpandedEntities()
        {
            var dates = new DateTimeOffset[] {DateTimeOffset.Now, DateTimeOffset.Now,};
            var root = new IqlRootReferenceExpression("root", "", typeof(RiskAssessment));
            var property = new IqlPropertyExpression(nameof(RiskAssessment.CreatedDate), null, IqlType.Unknown);
            property.Parent = root;
            var and = new IqlAndExpression(
                new IqlIsGreaterThanExpression(property, new IqlLiteralExpression(dates[0], IqlType.Unknown)),
                new IqlIsLessThanExpression(property, new IqlLiteralExpression(dates[1], IqlType.Unknown))
            );
            await Db.RiskAssessments.WhereEquals(and).ToList();
        }
    }
}