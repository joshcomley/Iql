using System;
using System.Collections.Generic;
using System.Linq;
using Iql.OData;
using Iql.OData.Extensions;
using Iql.Queryable.Data.Queryable;
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
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=($it/Name eq 'hello')",
                uri);

            query = query.OrderBy(c => c.Name).Expand(c => c.Type);
            uri = query.ResolveODataUri();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=($it/Name eq 'hello')&$orderby=$it/Name&$expand=Type",
                uri);
        }

        [TestMethod]
        public void TestFilteringOnFilteredNestedCollectionResultCount()
        {
            var query = Db.People.Where(c => c.Types.Count(t => t.TypeId == 2) > 2);
            var uri = Uri.UnescapeDataString(query.ResolveODataUri());
            Assert.AreEqual(
                @"http://localhost:28000/odata/People?$filter=($it/Types/$count($it/TypeId eq 2) gt 2)",
                uri);
        }

        [TestMethod]
        public void TestFilteringOnNestedCollectionCount()
        {
            var query = Db.People.Where(c => c.Types.Count > 2);
            var uri = Uri.UnescapeDataString(query.ResolveODataUri());
            Assert.AreEqual(
                @"http://localhost:28000/odata/People?$filter=($it/Types/$count gt 2)",
                uri);
        }

        [TestMethod]
        public void EnumFlagsExactCheck()
        {
            var query = Db.Users.Where(c => c.Permissions == (UserPermissions.Edit | UserPermissions.Create)
#if TypeScript
    , new EvaluateContext(code => Evaluator.Eval(code))
#endif
            );
            var uri = Uri.UnescapeDataString(query.ResolveODataUri());
            Assert.AreEqual(@"http://localhost:28000/odata/Users?$filter=($it/Permissions eq '10')",
                uri);
        }

        [TestMethod]
        public void EnumFlagsExactConstructedManuallyCheck()
        {
            var query = Db.Users.WhereEquals(new IqlIsEqualToExpression(
                new IqlPropertyExpression(
                    nameof(ApplicationUser.Permissions),
                    new IqlRootReferenceExpression()), 
                new IqlEnumLiteralExpression(null).AddValue(10, "")));
            var uri = Uri.UnescapeDataString(query.ResolveODataUri());
            Assert.AreEqual(@"http://localhost:28000/odata/Users?$filter=($it/Permissions eq '10')",
                uri);
        }

        [TestMethod]
        public void EnumFlagsContainsCheck()
        {
            var query = Db.Users.Where(c => (c.Permissions & (UserPermissions.Edit | UserPermissions.Create)) != 0
#if TypeScript
    , new EvaluateContext(code => Evaluator.Eval(code))
#endif
            );
            var uri = Uri.UnescapeDataString(query.ResolveODataUri());
            Assert.AreEqual(@"http://localhost:28000/odata/Users?$filter=($it/Permissions has 'Create,Edit')",
                uri);
        }

        [TestMethod]
        public void EnumFlagsContainsOrCheck()
        {
            var query = Db.Users.Where(c => (c.Permissions & (UserPermissions.Edit | UserPermissions.Create)) != 0 || (c.Permissions & (UserPermissions.Delete)) != 0
#if TypeScript
    , new EvaluateContext(code => Evaluator.Eval(code))
#endif
            );
            var uri = Uri.UnescapeDataString(query.ResolveODataUri());
            Assert.AreEqual(@"http://localhost:28000/odata/Users?$filter=(($it/Permissions has 'Create,Edit') or ($it/Permissions has 'Delete'))",
                uri);
        }

        [TestMethod]
        public void EnumFlagsContainsCheckConstructedManually()
        {
            var enumExpression = new IqlEnumLiteralExpression(typeof(UserPermissions));
            var expressionRoot = new IqlHasExpression(
                new IqlPropertyExpression(nameof(ApplicationUser.Permissions), new IqlRootReferenceExpression()),
                enumExpression);
            enumExpression.AddValue((long)UserPermissions.Edit, nameof(UserPermissions.Edit));
            enumExpression.AddValue((long)UserPermissions.Create, nameof(UserPermissions.Create));
            var query = Db.Users.WhereEquals(expressionRoot);
            var uri = Uri.UnescapeDataString(query.ResolveODataUri());
            Assert.AreEqual(@"http://localhost:28000/odata/Users?$filter=($it/Permissions has 'Create,Edit')",
                uri);
        }

        [TestMethod]
        public void EnumFlagsContainsOrCheckConstructedManually()
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
            var uri = Uri.UnescapeDataString(query.ResolveODataUri());
            Assert.AreEqual(@"http://localhost:28000/odata/Users?$filter=(($it/Permissions has 'Create,Edit') or ($it/Permissions has 'Delete'))",
                uri);
        }

        [TestMethod]
        public void TestResolveCountUri()
        {
            var query = Db.Clients.Where(c => c.Name == "hello").Expand(c => c.UsersCount);

            var uri = query.ResolveODataUri();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=($it/Name eq 'hello')&$expand=Users/$count",
                uri);
        }

        [TestMethod]
        public void TestResolveUriFromIQueryable()
        {
            IQueryableBase query = Db.Clients.Where(c => c.Name == "hello");

            var uri = query.ResolveODataUriFromQuery(Db);
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=($it/Name eq 'hello')",
                uri);

            query = query.OrderByProperty(nameof(Client.Name));
            uri = query.ResolveODataUriFromQuery(Db);
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=($it/Name eq 'hello')&$orderby=$it/Name",
                uri);
        }

        [TestMethod]
        public void TestResolveUriFromIQueryable2()
        {
            IQueryableBase query = Db.Clients.Where(c => c.Name == "hello2");

            var uri = query.ResolveODataUriFromQuery(Db);
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=($it/Name eq 'hello2')",
                uri);

            query = query.OrderByProperty(nameof(Client.Name));
            uri = query.ResolveODataUriFromQuery(Db);
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=($it/Name eq 'hello2')&$orderby=$it/Name",
                uri);
        }
    }
}