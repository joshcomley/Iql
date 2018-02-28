using System.Web;
using Iql.OData.Data;
using Iql.Queryable;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests.OData
{
    [TestClass]
    public class ODataUriTests : TestsBase
    {
        [TestMethod]
        public void TestResolveUri()
        {
            var query = Db.Clients.Where(c => c.Name == "hello");

            var uri = query.ResolveODataQueryUri();
            uri = HttpUtility.UrlDecode(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=(Name eq 'hello')",
                uri);

            query = query.OrderBy(c => c.Name).Expand(c => c.Type);
            uri = query.ResolveODataQueryUri();
            uri = HttpUtility.UrlDecode(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=(Name eq 'hello')&$orderby=Name&$expand=Type",
                uri);
        }

        [TestMethod]
        public void TestResolveUriFromIQueryable()
        {
            IQueryableBase query = Db.Clients.Where(c => c.Name == "hello");

            var uri = query.ResolveODataQueryUriFromQuery(Db);
            uri = HttpUtility.UrlDecode(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=(Name eq 'hello')",
                uri);

            query = query.OrderByProperty(nameof(Client.Name));
            uri = query.ResolveODataQueryUriFromQuery(Db);
            uri = HttpUtility.UrlDecode(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=(Name eq 'hello')&$orderby=Name",
                uri);
        }
    }
}