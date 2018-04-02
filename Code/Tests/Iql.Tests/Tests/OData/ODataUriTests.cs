using System.Web;
using Iql.OData;
using Iql.OData.Extensions;
using Iql.Parsing;
using Iql.Queryable;
using Iql.Queryable.Data.Queryable;
using Iql.Queryable.Expressions;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests.OData
{
    [TestClass]
    public class ODataUriTests : TestsBase
    {
        [TestMethod]
        public void TestResolveEntitySetMethodUriWithNoParameters()
        {
            var db = new AppDbContext(new ODataDataStore());
            var meRequest = db.Users.Me();
            var uri = meRequest.Uri;
            Assert.AreEqual(@"http://localhost:28000/odata/Users/Tunnel.Me", uri);
        }

        [TestMethod]
        public void TestResolveEntitySetMethodUriWithParameters()
        {
            var db = new AppDbContext(new ODataDataStore());
            var meRequest = db.Users.ForClient(7, 2);
            var uri = meRequest.Uri;
            Assert.AreEqual(@"http://localhost:28000/odata/Users/Tunnel.ForClient(id=7,type=2)", uri);
        }

        [TestMethod]
        public void TestResolveEntityMethodUriWithNoParameters()
        {
            var db = new AppDbContext(new ODataDataStore());
            var meRequest = db.Users.ReinstateUser(new ApplicationUser { Id = "928B9116-B06C-49EF-98C9-52A776E03ECD" });
            var uri = meRequest.Uri;
            Assert.AreEqual(@"http://localhost:28000/odata/Users('928B9116-B06C-49EF-98C9-52A776E03ECD')/Tunnel.ReinstateUser", uri);
        }

        [TestMethod]
        public void TestResolveEntityMethodUriWithParameters()
        {
            var db = new AppDbContext(new ODataDataStore());
            var meRequest = db.ClientTypes.SayHi(new ClientType { Id = 2 }, "bebo");
            var uri = meRequest.Uri;
            Assert.AreEqual(@"http://localhost:28000/odata/ClientTypes(2)/Tunnel.SayHi(name='bebo')", uri);
        }

        [TestMethod]
        public void TestResolveUri()
        {
            var query = Db.Clients.Where(c => c.Name == "hello");

            var uri = query.ResolveODataUri();
            uri = HttpUtility.UrlDecode(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=(Name eq 'hello')",
                uri);

            query = query.OrderBy(c => c.Name).Expand(c => c.Type);
            uri = query.ResolveODataUri();
            uri = HttpUtility.UrlDecode(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=(Name eq 'hello')&$orderby=Name&$expand=Type",
                uri);
        }

        [TestMethod]
        public void EnumFlagsCheck()
        {
            
            var query = Db.Users.Where(c => (c.Permissions & (UserPermissions.Edit | UserPermissions.Create)) != 0
#if TypeScript
    , new EvaluateContext(code => Evaluator.Eval(code))
#endif
            );
            var uri = HttpUtility.UrlDecode(query.ResolveODataUri());
            Assert.AreEqual(@"http://localhost:28000/odata/Users?$filter=(Permissions has 'Create,Edit')",
                uri);
        }

        [TestMethod]
        public void TestResolveCountUri()
        {
            var query = Db.Clients.Where(c => c.Name == "hello").Expand(c => c.UsersCount);

            var uri = query.ResolveODataUri();
            uri = HttpUtility.UrlDecode(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=(Name eq 'hello')&$expand=Users/$count",
                uri);
        }

        [TestMethod]
        public void TestResolveUriFromIQueryable()
        {
            IQueryableBase query = Db.Clients.Where(c => c.Name == "hello");

            var uri = query.ResolveODataUriFromQuery(Db);
            uri = HttpUtility.UrlDecode(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=(Name eq 'hello')",
                uri);

            query = query.OrderByProperty(nameof(Client.Name));
            uri = query.ResolveODataUriFromQuery(Db);
            uri = HttpUtility.UrlDecode(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=(Name eq 'hello')&$orderby=Name",
                uri);
        }

        [TestMethod]
        public void TestResolveUriFromIQueryable2()
        {
            IQueryableBase query = Db.Clients.Where(c => c.Name == "hello2");

            var uri = query.ResolveODataUriFromQuery(Db);
            uri = HttpUtility.UrlDecode(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=(Name eq 'hello2')",
                uri);

            query = query.OrderByProperty(nameof(Client.Name));
            uri = query.ResolveODataUriFromQuery(Db);
            uri = HttpUtility.UrlDecode(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=(Name eq 'hello2')&$orderby=Name",
                uri);
        }
    }
}