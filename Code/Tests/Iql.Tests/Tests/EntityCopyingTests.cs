using System;
using System.Threading.Tasks;
using Iql.Data;
using IqlSampleApp.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class EntityCopyingTests : TestsBase
    {
        [TestMethod]
        public async Task TestCopyEntity()
        {
            var site = new Site
            {
                Id = 1235,
                Guid = new Guid("41536523-c009-43ba-941c-7c98e747fd66"),
                Location = new IqlPointExpression(10, 20),
                Name = "My Site"
            };
            var copy = await site.CopyAsAsync(Db, typeof(Site));
            Assert.AreNotEqual(site.Id, copy.Id);
            Assert.AreNotEqual(site.Guid, copy.Guid);
            Assert.AreEqual(site.Name, copy.Name);
            Assert.IsNotNull(copy.Location);
            Assert.AreNotEqual(copy.Location, site.Location);
            Assert.AreEqual(copy.Location.X, site.Location.X);
            Assert.AreEqual(copy.Location.Y, site.Location.Y);
        }
    }
}