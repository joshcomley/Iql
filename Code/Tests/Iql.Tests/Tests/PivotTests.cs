using System.Threading.Tasks;
using Iql.Entities;
using Iql.Tests.Context;
using IqlSampleApp.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class PivotTests : TestsBase
    {
        [TestMethod]
        public async Task DeletePivotEntity()
        {
            var cloudClient = new Client
            {
                Id = 7
            };
            var cloudClientCategory = new ClientCategory
            {
                Id = 9
            };
            var cloudPivot = new ClientCategoryPivot
            {
                ClientId = 7,
                CategoryId = 9
            };
            AppDbContext.InMemoryDb.Clients.Add(cloudClient);
            AppDbContext.InMemoryDb.ClientCategories.Add(cloudClientCategory);
            AppDbContext.InMemoryDb.ClientCategoriesPivot.Add(cloudPivot);
            var pivot = await Db.ClientCategoriesPivot.ExpandAll().GetWithCompositeKeyAsync(CompositeKey.Ensure(cloudPivot,
                Db.EntityConfigurationContext.EntityType<ClientCategoryPivot>()));
            Assert.IsNotNull(pivot);
            Assert.IsNotNull(pivot.Category);
            Assert.IsNotNull(pivot.Client);
            Assert.AreEqual(1, pivot.Client.Categories.Count);
            Assert.AreEqual(1, pivot.Category.Clients.Count);
            Assert.AreNotEqual(pivot, cloudPivot);
            Assert.AreEqual(pivot.ClientId, 7);
            Assert.AreEqual(pivot.CategoryId, 9);
            var state = Db.DeleteEntity(pivot);
            Assert.AreEqual(pivot.ClientId, 7);
            Assert.AreEqual(pivot.CategoryId, 9);
            Assert.IsTrue(state.MarkedForDeletion);
            var pivot2 = await Db.ClientCategoriesPivot.GetWithCompositeKeyAsync(CompositeKey.Ensure(cloudPivot,
                Db.EntityConfigurationContext.EntityType<ClientCategoryPivot>()));
            Assert.AreEqual(pivot, pivot2);
            Assert.IsTrue(state.MarkedForDeletion);
            Assert.AreEqual(pivot.ClientId, 7);
            Assert.AreEqual(pivot.CategoryId, 9);
            var result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            Assert.IsFalse(Db.IsTracked(pivot));
        }
    }
}