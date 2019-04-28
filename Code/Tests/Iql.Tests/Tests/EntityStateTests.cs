using System;
using System.Threading.Tasks;
using Iql.Tests.Context;
using IqlSampleApp.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class EntityStateTests : TestsBase
    {
        [TestMethod]
        public async Task TestGetEntityStateByCompositeKey()
        {
            var site = new Site
            {
                Id = 1235,
                Guid = new Guid("41536523-c009-43ba-941c-7c98e747fd66"),
                Location = new IqlPointExpression(10, 20),
                Name = "My Site"
            };
            AppDbContext.InMemoryDb.Sites.Add(site);
            var localSite = await Db.Sites.GetWithKeyAsync(1235);
            Assert.IsNotNull(localSite);
            var key = Db.EntityConfigurationContext.EntityType<Site>().GetCompositeKey(localSite);
            var state = Db.GetEntityState(key);
            Assert.IsNotNull(state);
            Assert.AreEqual(localSite, state.Entity);
        }
    }
}