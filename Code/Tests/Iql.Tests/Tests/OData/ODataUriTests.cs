using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.OData;
using Iql.OData.Extensions;
using Iql.Queryable;
#if TypeScript
using Iql.Parsing;
using Iql.Queryable.Expressions;
#endif
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests.OData
{
    [TestClass]
    public class ODataUriTests : TestsBase
    {
        [TestMethod]
        public async Task TestOrderByCount()
        {
            var query = Db.Clients.OrderBy(c => c.SitesCount);
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$orderby=$it/Sites/$count", uri);
        }

        [TestMethod]
        public async Task TestNestedOrderByCount()
        {
            var query = Db.Clients.ExpandCollection(c => c.Sites, sq => sq.OrderBy(c => c.ChildrenCount));
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$expand=Sites($orderby=Children/$count)", uri);
        }

        [TestMethod]
        public async Task TestExpandCount()
        {
            var query = Db.Clients.ExpandRelationship(nameof(Client.SitesCount));
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$expand=Sites/$count", uri);
        }

        [TestMethod]
        public async Task TestExpandCountWithMultipleCallsToGetODataUri()
        {
            var query = Db.Clients.ExpandRelationship(nameof(Client.SitesCount));
            var uri = await query.ResolveODataUriAsync();
            uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$expand=Sites/$count", uri);
            uri = await query.ResolveODataUriAsync();
            uri = await query.ResolveODataUriAsync();
            uri = await query.ResolveODataUriAsync();
            uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$expand=Sites/$count", uri);
        }

        [TestMethod]
        public async Task DuplicateExpandsShouldOnlyResultInOneExpandInstance()
        {
            var initialExpand = Db
                .Clients
                .ExpandRelationship(nameof(Client.SitesCount));
            var query = initialExpand
                .ExpandRelationship(nameof(Client.SitesCount))
                ;
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$expand=Sites/$count", uri);
        }

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
        public async Task TestResolveUri()
        {
            var query = Db.Clients.Where(c => c.Name == "hello");

            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=($it/Name eq 'hello')",
                uri);

            query = query.OrderBy(c => c.Name).Expand(c => c.Type);
            uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=($it/Name eq 'hello')&$expand=Type&$orderby=$it/Name",
                uri);
        }

        [TestMethod]
        public async Task TestFilteringOnFilteredNestedCollectionResultCount()
        {
            var query = Db.People.ExpandCollection(c => c.Types, tq => tq.Where(c => c.Description == "a").Expand(c => c.Type));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(
                @"http://localhost:28000/odata/People?$expand=Types($filter=(Description eq 'a');$expand=Type)",
                uri);
        }

        [TestMethod]
        public async Task TestSkip()
        {
            var query = Db.People.Skip(10);
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(
                @"http://localhost:28000/odata/People?$skip=10",
                uri);
        }

        [TestMethod]
        public async Task TestTake()
        {
            var query = Db.People.Take(10);
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(
                @"http://localhost:28000/odata/People?$top=10",
                uri);
        }

        [TestMethod]
        public async Task TestSkipAndTake()
        {
            var query = Db.People.Skip(7).Take(10);
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(
                @"http://localhost:28000/odata/People?$skip=7&$top=10",
                uri);
        }

        [TestMethod]
        public async Task TestFilteringOnFilteredNestedCollectionWithExpandUsesSemicolonS()
        {
            var query = Db.People.Where(c => c.Types.Count(t => t.TypeId == 2) > 2);
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(
                @"http://localhost:28000/odata/People?$filter=($it/Types/$count($filter=(TypeId eq 2)) gt 2)",
                uri);
        }

        [TestMethod]
        public async Task TestStringFilteringOnNestedCollectionResultCount()
        {
            var query = Db.People.Where(c => c.Types.Count(t => t.Description.Contains("test")) > 2);
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
#if !TypeScript
            Assert.AreEqual(
                @"http://localhost:28000/odata/People?$filter=($it/Types/$count($filter=contains(Description,'test')) gt 2)",
                uri);
#else
            Assert.AreEqual(
                @"http://localhost:28000/odata/People?$filter=($it/Types/$count($filter=(indexof(tolower(Description),'test') ne -1)) gt 2)",
                uri);
#endif
        }

        [TestMethod]
        public async Task TestStringFilteringOnNestedCollectionWithStringIndexOfResultCount()
        {
            var query = Db.People.Where(c => c.Types.Count(t => t.Description.IndexOf("TEST") != -1) > 2);
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(
                @"http://localhost:28000/odata/People?$filter=($it/Types/$count($filter=(indexof(tolower(Description),'test') ne -1)) gt 2)",
                uri);
        }

        [TestMethod]
        public async Task TestMultiply()
        {
            var query = Db.People.Where(c => c.Types.Count(t => t.Description.IndexOf("TEST") != -1) > c.Types.Count * 0.5);
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(
                @"http://localhost:28000/odata/People?$filter=($it/Types/$count($filter=(indexof(tolower(Description),'test') ne -1)) gt ($it/Types/$count mul 0.5))",
                uri);
        }

        [TestMethod]
        public async Task TestDivide()
        {
            var query = Db.People.Where(c => c.Types.Count(t => t.Description.IndexOf("TEST") != -1) > c.Types.Count / 0.5);
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(
                @"http://localhost:28000/odata/People?$filter=($it/Types/$count($filter=(indexof(tolower(Description),'test') ne -1)) gt ($it/Types/$count div 0.5))",
                uri);
        }

        [TestMethod]
        public async Task TestFilteringOnNestedCollectionCount()
        {
            var query = Db.People.Where(c => c.Types.Count > 2);
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(
                @"http://localhost:28000/odata/People?$filter=($it/Types/$count gt 2)",
                uri);
        }

        [TestMethod]
        public async Task EnumFlagsExactCheck()
        {
            var query = Db.Users.Where(c => c.Permissions == (UserPermissions.Edit | UserPermissions.Create)
#if TypeScript
    , new EvaluateContext(code => Evaluator.Eval(code))
#endif
            );
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(@"http://localhost:28000/odata/Users?$filter=($it/Permissions eq '10')",
                uri);
        }

        [TestMethod]
        public async Task DateNowExpression()
        {
            var query = Db.Users.WhereEquals(new IqlIsGreaterThanExpression(
                new IqlPropertyExpression(
                    nameof(Client.CreatedDate),
                    new IqlRootReferenceExpression()),
                new IqlNowExpression()));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(@"http://localhost:28000/odata/Users?$filter=($it/CreatedDate gt now())",
                uri);
        }

        [TestMethod]
        public async Task DateNowWithSimpleTimeSpanSubtractionExpression()
        {
            var query = Db.Users.WhereEquals(new IqlIsGreaterThanExpression(
                new IqlPropertyExpression(
                    nameof(Client.CreatedDate),
                    new IqlRootReferenceExpression()),
                new IqlSubtractExpression(new IqlNowExpression(),
                    new IqlTimeSpanExpression().Set(7))));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(@"http://localhost:28000/odata/Users?$filter=($it/CreatedDate gt (now() sub duration'P7D'))",
                uri);
        }

        [TestMethod]
        public async Task DateNowWithComplexTimeSpanSubtractionExpression()
        {
            var query = Db.Users.WhereEquals(new IqlIsGreaterThanExpression(
                new IqlPropertyExpression(
                    nameof(Client.CreatedDate),
                    new IqlRootReferenceExpression()),
                new IqlSubtractExpression(new IqlNowExpression(),
                    new IqlTimeSpanExpression().Set(365 * 10, 7, 15, 33, 14))));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(@"http://localhost:28000/odata/Users?$filter=($it/CreatedDate gt (now() sub duration'P3650DT7H15M33.014S'))",
                uri);
        }

        [TestMethod]
        public async Task EnumFlagsExactConstructedManuallyCheck()
        {
            var query = Db.Users.WhereEquals(new IqlIsEqualToExpression(
                new IqlPropertyExpression(
                    nameof(ApplicationUser.Permissions),
                    new IqlRootReferenceExpression()),
                new IqlEnumLiteralExpression(null).AddValue(10, "")));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(@"http://localhost:28000/odata/Users?$filter=($it/Permissions eq '10')",
                uri);
        }

        [TestMethod]
        public async Task EnumFlagsContainsCheck()
        {
            var query = Db.Users.Where(c => (c.Permissions & (UserPermissions.Edit | UserPermissions.Create)) != 0
#if TypeScript
    , new EvaluateContext(code => Evaluator.Eval(code))
#endif
            );
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(@"http://localhost:28000/odata/Users?$filter=($it/Permissions has 'Create,Edit')",
                uri);
        }

        [TestMethod]
        public async Task EnumFlagsContainsOrCheck()
        {
            var query = Db.Users.Where(c => (c.Permissions & (UserPermissions.Edit | UserPermissions.Create)) != 0 || (c.Permissions & (UserPermissions.Delete)) != 0
#if TypeScript
    , new EvaluateContext(code => Evaluator.Eval(code))
#endif
            );
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(@"http://localhost:28000/odata/Users?$filter=(($it/Permissions has 'Create,Edit') or ($it/Permissions has 'Delete'))",
                uri);
        }

        [TestMethod]
        public async Task EnumFlagsContainsCheckConstructedManually()
        {
            var enumExpression = new IqlEnumLiteralExpression(typeof(UserPermissions));
            var expressionRoot = new IqlHasExpression(
                new IqlPropertyExpression(nameof(ApplicationUser.Permissions), new IqlRootReferenceExpression()),
                enumExpression);
            enumExpression.AddValue((long)UserPermissions.Edit, nameof(UserPermissions.Edit));
            enumExpression.AddValue((long)UserPermissions.Create, nameof(UserPermissions.Create));
            var query = Db.Users.WhereEquals(expressionRoot);
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(@"http://localhost:28000/odata/Users?$filter=($it/Permissions has 'Create,Edit')",
                uri);
        }

        [TestMethod]
        public async Task EnumFlagsContainsOrCheckConstructedManually()
        {
            var has1 = new IqlHasExpression(
                new IqlPropertyExpression(nameof(ApplicationUser.Permissions), new IqlRootReferenceExpression()),
                new IqlEnumLiteralExpression(typeof(UserPermissions))
                    .AddValue((long)UserPermissions.Edit, nameof(UserPermissions.Edit))
                    .AddValue((long)UserPermissions.Create, nameof(UserPermissions.Create)));
            var has2 = new IqlHasExpression(
                new IqlPropertyExpression(nameof(ApplicationUser.Permissions), new IqlRootReferenceExpression()),
                new IqlEnumLiteralExpression(typeof(UserPermissions))
                    .AddValue((long)UserPermissions.Edit, nameof(UserPermissions.Delete)));
            var query = Db.Users.WhereEquals(new IqlOrExpression(
                has1,
                has2));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(@"http://localhost:28000/odata/Users?$filter=(($it/Permissions has 'Create,Edit') or ($it/Permissions has 'Delete'))",
                uri);
        }

        [TestMethod]
        public async Task TestResolveCountUri()
        {
            var query = Db.Clients.Where(c => c.Name == "hello").Expand(c => c.UsersCount);

            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=($it/Name eq 'hello')&$expand=Users/$count",
                uri);
        }

        [TestMethod]
        public async Task TestResolveUriFromIQueryable()
        {
            IQueryableBase query = Db.Clients.Where(c => c.Name == "hello");

            var uri = await query.ResolveODataUriFromQueryAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=($it/Name eq 'hello')",
                uri);

            query = query.OrderByProperty(nameof(Client.Name));
            uri = await query.ResolveODataUriFromQueryAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=($it/Name eq 'hello')&$orderby=$it/Name",
                uri);
        }

        [TestMethod]
        public async Task TestResolveUriFromIQueryable2()
        {
            IQueryableBase query = Db.Clients.Where(c => c.Name == "hello2");

            var uri = await query.ResolveODataUriFromQueryAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=($it/Name eq 'hello2')",
                uri);

            query = query.OrderByProperty(nameof(Client.Name));
            uri = await query.ResolveODataUriFromQueryAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=($it/Name eq 'hello2')&$orderby=$it/Name",
                uri);
        }
    }
}