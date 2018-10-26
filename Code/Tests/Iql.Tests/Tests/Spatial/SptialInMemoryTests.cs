using System.Threading.Tasks;
#if TypeScript
using System;
using Iql.Parsing;
using Iql.Parsing.Expressions;
#endif
using Iql.Tests.Context;
using IqlSampleApp.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class SptialInMemoryTests : TestsBase
    {
        [TestMethod]
        public async Task TestInMemoryDistance()
        {
            var site = new Site
            {
                Location = SptialFunctionsTests.WithinBermudaTrianglePoint,
                Id = 3
            };
            AppDbContext.InMemoryDb.Sites.Add(site);
            var sites = await Db.Sites.Where(s => s.Location.DistanceFrom(SptialFunctionsTests.BerlinPoint) < 2000
#if TypeScript
                    , 
                    new EvaluateContext
                    {
                        Context = this,
                        Evaluate = n => (Func<object, object>)Evaluator.Eval(n)
                    }
#endif
    ).ToListAsync();
            Assert.AreEqual(0, sites.Count);
            sites = await Db.Sites.Where(s => s.Location.DistanceFrom(SptialFunctionsTests.NotWithinBermudaTrianglePoint) < 2000).ToListAsync();
            Assert.AreEqual(1, sites.Count);
        }
    }
}