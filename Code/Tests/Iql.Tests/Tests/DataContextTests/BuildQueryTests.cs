using System;
using System.Threading.Tasks;
using Iql.Data.Queryable;
using Iql.OData.Extensions;
using Iql.Tests.Data.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Tests.DataContextTests
{
    [TestClass]
    public class BuildQueryTests : TestsBase
    {
        [TestMethod]
        public async Task BuildQueryFromPropertyCollectionWithNestedExpands()
        {
            var config = Db.EntityConfigurationContext.EntityType<Site>();
            var childrenRelationship = config.FindCollectionRelationship(p => p.Children);
            var collection = config.PropertyCollection(
                _ => childrenRelationship,
                _ => _.PropertyCollection(pc => pc.PropertyPath(p => p.CreatedByUser.Client.Type.Name))
            );
            var query = collection.BuildQueryFromPropertyGroup<Site>(Db);
            var uri = await query.ResolveODataUriAsync();
            Assert.AreEqual(@"http://localhost:28000/odata/Sites?$expand=Children/$count,CreatedByUser($expand=Client($expand=Type))", Uri.UnescapeDataString(uri));
        }
    }
}