using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.OData;
using Iql.OData.Extensions;
using Iql.Queryable;
#if TypeScript
using Iql.Parsing;
using Iql.Parsing.Expressions;
using Iql.Queryable.Expressions;
#endif
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Tests.OData
{
    [TestClass]
    public class ODataUriTests : TestsBase
    {
        [TestMethod]
        public async Task TestIntersectsLiteral()
        {
            var polygon = new IqlPolygonExpression(
                new IqlRingExpression(new IqlPointExpression[]
                {
                    new IqlPointExpression(2, 1),
                    new IqlPointExpression(4, 5),
                    new IqlPointExpression(3, 7),
                    new IqlPointExpression(2, 1),
                }));
            var query = Db.Sites.Where(site => site.Location.Intersects(polygon)
#if TypeScript
                    , 
                    new EvaluateContext
                    {
                        Context = this,
                        Evaluate = n => (Func<object, object>)Evaluator.Eval(n)
                    }
#endif
            );

            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Sites?$filter=geo.intersects($it/Location,geography'SRID=4326;POLYGON((2 1,4 5,3 7,2 1))')", uri);
        }

        [TestMethod]
        public async Task TestIntersectsReference()
        {
            var query = Db.Sites.Where(site => site.Location.Intersects(site.Area)
#if TypeScript
                    , 
                    new EvaluateContext
                    {
                        Context = this,
                        Evaluate = n => (Func<object, object>)Evaluator.Eval(n)
                    }
#endif
            );
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Sites?$filter=geo.intersects($it/Location,$it/Area)", uri);
        }

        [TestMethod]
        public async Task TestDistanceLiteral()
        {
            var point = new IqlPointExpression(2, 1);
            var query = Db.Sites.Where(site => site.Location.DistanceFrom(point) < 150
#if TypeScript
                    , 
                    new EvaluateContext
                    {
                        Context = this,
                        Evaluate = n => (Func<object, object>)Evaluator.Eval(n)
                    }
#endif
                                               );
            var iql = await query.ToIqlAsync();
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Sites?$filter=(geo.distance($it/Location,geography'SRID=4326;POINT(2 1)') lt 150)", uri);
        }

        [TestMethod]
        public async Task TestDistanceReference()
        {
            var query = Db.Sites.Where(site => site.Location.DistanceFrom(site.Location) < 150
#if TypeScript
                    , 
                    new EvaluateContext
                    {
                        Context = this,
                        Evaluate = n => (Func<object, object>)Evaluator.Eval(n)
                    }
#endif
            );

            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Sites?$filter=(geo.distance($it/Location,$it/Location) lt 150)", uri);
        }

        [TestMethod]
        public async Task TestLengthLiteral()
        {
            var line = new IqlLineExpression(
                new IqlPointExpression[]
                {
                    new IqlPointExpression(1, 2),
                    new IqlPointExpression(5, 4),
                    new IqlPointExpression(7, 3),
                });
            var query = Db.Sites.Where(site => site.Line.Length() < line.Length()
#if TypeScript
                    , 
                    new EvaluateContext
                    {
                        Context = this,
                        Evaluate = n => (Func<object, object>)Evaluator.Eval(n)
                    }
#endif
            );

            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            var reducedValue = "745.451415829943";
#if TypeScript
            reducedValue = "745.4514158299432";
#endif
            Assert.AreEqual($@"http://localhost:28000/odata/Sites?$filter=(geo.length($it/Line) lt {reducedValue})", uri);
        }

        [TestMethod]
        public async Task TestLengthWithDirectValue()
        {
            var query = Db.Sites.Where(site => site.Line.Length() < 150
#if TypeScript
                    , 
                    new EvaluateContext
                    {
                        Context = this,
                        Evaluate = n => (Func<object, object>)Evaluator.Eval(n)
                    }
#endif
            );

            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Sites?$filter=(geo.length($it/Line) lt 150)", uri);
        }

        //[TestMethod]
        //public async Task TestLength()
        //{
        //    var point = new IqlPointExpression(1, 2);
        //    var query = Db.Sites.Where(site => site.Location.DistanceFrom(point, IqlDistanceKind.Kilometers) < 150);
        //    var uri = await query.ResolveODataUriAsync();
        //    uri = Uri.UnescapeDataString(uri);
        //    Assert.AreEqual(@"http://localhost:28000/odata/Sites?$filter=(geo.distance($it/Location,geography'SRID=4326;POINT(2 1)') lt 150)", uri);
        //}

        [TestMethod]
        public async Task TestCountNotSubmittedOnSingleEntity()
        {
            var query = Db.ApplicationLogs.IncludeCount().WithKey(new Guid("4e9dcf61-4abf-458e-abfc-07c20d5ef248"));
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/ApplicationLogs(4e9dcf61-4abf-458e-abfc-07c20d5ef248)", uri);
        }

        [TestMethod]
        public async Task TestGetGuidKey()
        {
            var query = Db.ApplicationLogs.WithKey(new Guid("4e9dcf61-4abf-458e-abfc-07c20d5ef248"));
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/ApplicationLogs(4e9dcf61-4abf-458e-abfc-07c20d5ef248)", uri);
        }

        [TestMethod]
        public async Task TestFilterOnChildCollection()
        {
            var query = Db.Clients.Where(c => c.Sites.Where(s => s.AdditionalSendReportsTo.Count > 22).Count() > 3);
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=(Sites/$count($filter=(AdditionalSendReportsTo/$count gt 22)) gt 3)", uri);
        }

#if !TypeScript
        // TypeScript translation converts this to ".indexOf(..)"
        [TestMethod]
        public async Task StringNotContainsShouldIncludeNullAndEmptyCheck()
        {
            var query = Db.Clients.Where(c => !c.Name.Contains("xyz"));
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=((not(contains($it/Name,'xyz')) or ($it/Name eq null)) or ($it/Name eq ''))", uri);
        }

#endif
        [TestMethod]
        public async Task StringNegatedIndexOfShouldIncludeNullAndEmptyCheck()
        {
            var query = Db.Clients.Where(c => c.Name.IndexOf("xyz") == -1);
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=(((indexof(tolower($it/Name),'xyz') eq -1) or ($it/Name eq null)) or ($it/Name eq ''))", uri);
        }

        [TestMethod]
        public async Task StringDoubleNegatedIndexOfShouldIncludeNullAndEmptyCheck()
        {
            var query = Db.Clients.Where(c => !!(c.Name.IndexOf("xyz") == -1));
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=not(not((((indexof(tolower($it/Name),'xyz') eq -1) or ($it/Name eq null)) or ($it/Name eq ''))))", uri);
        }

        [TestMethod]
        public async Task StringTripleNegatedIndexOfShouldNotIncludeNullAndEmptyCheck()
        {
            var query = Db.Clients.Where(c => !!!(c.Name.IndexOf("xyz") == -1));
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=not(not(not((indexof(tolower($it/Name),'xyz') eq -1))))", uri);
        }

        [TestMethod]
        public async Task StringTripleNegatedIndexOfShouldNotIncludeNullAndEmptyCheck2()
        {
            var query = Db.Clients.Where(c => (((c.Name.IndexOf("xyz") == -1) == false) == false) == false);
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=not(not(not((indexof(tolower($it/Name),'xyz') eq -1))))", uri);
        }

        [TestMethod]
        public async Task TestWithKey()
        {
            var query = Db.Clients.WithKey(7);
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients(7)", uri);
        }

        [TestMethod]
        public async Task TestOrderByCount()
        {
            var query = Db.Clients.OrderBy(c => c.SitesCount);
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$orderby=Sites/$count", uri);
        }

        [TestMethod]
        public async Task TestOrderByNestedProperty()
        {
            var query = Db.Clients.OrderBy(c => c.Type.Name);
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$orderby=$it/Type/Name", uri);
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
            Assert.AreEqual(@"http://localhost:28000/odata/Users/IqlSampleApp.Me", uri);
        }

        [TestMethod]
        public void TestResolveEntitySetMethodUriWithParameters()
        {
            var db = new AppDbContext(new ODataDataStore());
            var meRequest = db.Users.ForClient(7, 2);
            var uri = meRequest.Uri;
            Assert.AreEqual(@"http://localhost:28000/odata/Users/IqlSampleApp.ForClient(id=7,type=2)", uri);
        }

        [TestMethod]
        public void TestResolveEntityMethodUriWithNoParameters()
        {
            var db = new AppDbContext(new ODataDataStore());
            var meRequest = db.Users.ReinstateUser(new ApplicationUser { Id = "928B9116-B06C-49EF-98C9-52A776E03ECD" });
            var uri = meRequest.Uri;
            Assert.AreEqual(@"http://localhost:28000/odata/Users('928B9116-B06C-49EF-98C9-52A776E03ECD')/IqlSampleApp.ReinstateUser", uri);
        }

        [TestMethod]
        public void TestResolveEntityMethodUriWithParameters()
        {
            var db = new AppDbContext(new ODataDataStore());
            var meRequest = db.ClientTypes.SayHi(new ClientType { Id = 2 }, "bebo");
            var uri = meRequest.Uri;
            Assert.AreEqual(@"http://localhost:28000/odata/ClientTypes(2)/IqlSampleApp.SayHi(name='bebo')", uri);
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
                @"http://localhost:28000/odata/People?$filter=(Types/$count($filter=(TypeId eq 2)) gt 2)",
                uri);
        }

        [TestMethod]
        public async Task TestStringFilteringOnNestedCollectionResultCount()
        {
            var query = Db.People.Where(c => c.Types.Count(t => t.Description.Contains("test")) > 2);
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
#if !TypeScript
            Assert.AreEqual(
                @"http://localhost:28000/odata/People?$filter=(Types/$count($filter=contains(Description,'test')) gt 2)",
                uri);
#else
            Assert.AreEqual(
                @"http://localhost:28000/odata/People?$filter=(Types/$count($filter=(indexof(tolower(Description),'test') ne -1)) gt 2)",
                uri);
#endif
        }

        [TestMethod]
        public async Task TestStringFilteringOnNestedCollectionWithStringIndexOfResultCount()
        {
            var query = Db.People.Where(c => c.Types.Count(t => t.Description.IndexOf("TEST") != -1) > 2);
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(
                @"http://localhost:28000/odata/People?$filter=(Types/$count($filter=(indexof(tolower(Description),'test') ne -1)) gt 2)",
                uri);
        }

        [TestMethod]
        public async Task TestMultiply()
        {
            var query = Db.People.Where(c => c.Types.Count(t => t.Description.IndexOf("TEST") != -1) > c.Types.Count * 0.5);
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(
                @"http://localhost:28000/odata/People?$filter=(Types/$count($filter=(indexof(tolower(Description),'test') ne -1)) gt (Types/$count mul 0.5))",
                uri);
        }

        [TestMethod]
        public async Task TestDivide()
        {
            var query = Db.People.Where(c => c.Types.Count(t => t.Description.IndexOf("TEST") != -1) > c.Types.Count / 0.5);
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(
                @"http://localhost:28000/odata/People?$filter=(Types/$count($filter=(indexof(tolower(Description),'test') ne -1)) gt (Types/$count div 0.5))",
                uri);
        }

        [TestMethod]
        public async Task TestFilteringOnNestedCollectionCount()
        {
            var query = Db.People.Where(c => c.Types.Count > 2);
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(
                @"http://localhost:28000/odata/People?$filter=(Types/$count gt 2)",
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
        public async Task NegationExpression()
        {
            var query = Db.Users.WhereEquals(new IqlNotExpression(new IqlIsGreaterThanExpression(
                new IqlPropertyExpression(
                    nameof(Client.CreatedDate),
                    new IqlRootReferenceExpression()),
                new IqlSubtractExpression(new IqlNowExpression(),
                    new IqlTimeSpanExpression().Set(7)))));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(@"http://localhost:28000/odata/Users?$filter=not(($it/CreatedDate gt (now() sub duration'P7D')))",
                uri);
        }

        [TestMethod]
        public async Task StringIncludesExpression()
        {
            var query = Db.Users.WhereEquals(new IqlStringIncludesExpression(
                new IqlPropertyExpression(
                    nameof(Client.Name),
                    new IqlRootReferenceExpression()),
                new IqlLiteralExpression("abc", IqlType.String)));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(@"http://localhost:28000/odata/Users?$filter=contains($it/Name,'abc')",
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
        public async Task EnumEmptyCheck()
        {
            var query = Db.Users.WhereEquals(new IqlIsEqualToExpression(
                new IqlPropertyExpression(
                    nameof(ApplicationUser.Permissions),
                    new IqlRootReferenceExpression()),
                new IqlEnumLiteralExpression(null)));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(@"http://localhost:28000/odata/Users",
                uri);
        }

        [TestMethod]
        public async Task EmptyParenthesisCheck()
        {
            var query = Db.Users.WhereEquals(
                new IqlOrExpression(new IqlParenthesisExpression(null),
                    new IqlIsEqualToExpression(
                        new IqlPropertyExpression(
                            nameof(ApplicationUser.FullName),
                            new IqlRootReferenceExpression()),
                        new IqlLiteralExpression("abc"))));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(@"http://localhost:28000/odata/Users?$filter=($it/FullName eq 'abc')",
                uri);
        }


        [TestMethod]
        public async Task EnumEmptyReversedCheck()
        {
            var query = Db.Users.WhereEquals(new IqlIsEqualToExpression(
                new IqlEnumLiteralExpression(null),
                new IqlPropertyExpression(
                    nameof(ApplicationUser.Permissions),
                    new IqlRootReferenceExpression())));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(@"http://localhost:28000/odata/Users",
                uri);
        }

        [TestMethod]
        public async Task EnumEmptyHasCheck()
        {
            var query = Db.Users.WhereEquals(new IqlHasExpression(
                new IqlPropertyExpression(
                    nameof(ApplicationUser.Permissions),
                    new IqlRootReferenceExpression()),
                new IqlEnumLiteralExpression(null)));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(@"http://localhost:28000/odata/Users",
                uri);
        }

        [TestMethod]
        public async Task EnumEmptyOrCheck()
        {
            var query = Db.Users.WhereEquals(new IqlOrExpression(new IqlIsEqualToExpression(
                    new IqlPropertyExpression(
                        nameof(ApplicationUser.Permissions),
                        new IqlRootReferenceExpression()),
                    new IqlEnumLiteralExpression(null)),
                new IqlIsEqualToExpression(
                    new IqlPropertyExpression(
                        nameof(ApplicationUser.Permissions),
                        new IqlRootReferenceExpression()),
                    new IqlEnumLiteralExpression(null))
            ));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(@"http://localhost:28000/odata/Users",
                uri);
        }

        [TestMethod]
        public async Task EnumEmptyHasOrCheck()
        {
            var query = Db.Users.WhereEquals(new IqlOrExpression(new IqlHasExpression(
                    new IqlPropertyExpression(
                        nameof(ApplicationUser.Permissions),
                        new IqlRootReferenceExpression()),
                    new IqlEnumLiteralExpression(null)),
                new IqlHasExpression(
                    new IqlPropertyExpression(
                        nameof(ApplicationUser.Permissions),
                        new IqlRootReferenceExpression()),
                    new IqlEnumLiteralExpression(null))
            ));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(@"http://localhost:28000/odata/Users",
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
        public async Task AnyCheck()
        {
            var query = Db.Users.WhereEquals(new IqlAnyExpression("_",
                new IqlPropertyExpression(
                    nameof(ApplicationUser.ClientsCreated),
                    new IqlRootReferenceExpression()),
                new IqlIsEqualToExpression(new IqlPropertyExpression(
                        nameof(Client.Name),
                        new IqlRootReferenceExpression("_")),
                    new IqlLiteralExpression("jimbo", IqlType.String))));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(@"http://localhost:28000/odata/Users?$filter=ClientsCreated/any(_:(_/Name eq 'jimbo'))",
                uri);
        }

        [TestMethod]
        public async Task AllCheck()
        {
            var query = Db.Users.WhereEquals(new IqlAllExpression("_",
                new IqlPropertyExpression(
                    nameof(ApplicationUser.ClientsCreated),
                    new IqlRootReferenceExpression()),
                new IqlIsEqualToExpression(new IqlPropertyExpression(
                        nameof(Client.Name),
                        new IqlRootReferenceExpression("_")),
                    new IqlLiteralExpression("jimbo", IqlType.String))));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(@"http://localhost:28000/odata/Users?$filter=ClientsCreated/all(_:(_/Name eq 'jimbo'))",
                uri);
        }

        [TestMethod]
        public async Task AllCheck2()
        {
            var query = Db.Users.WhereEquals(new IqlAllExpression("_",
                new IqlPropertyExpression(
                    nameof(ApplicationUser.ClientsCreated),
                    new IqlRootReferenceExpression()),
                new IqlStringIncludesExpression(new IqlPropertyExpression(
                        nameof(Client.Name),
                        new IqlRootReferenceExpression("_")),
                    new IqlLiteralExpression("jimbo", IqlType.String))));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(@"http://localhost:28000/odata/Users?$filter=ClientsCreated/all(_:contains(_/Name,'jimbo'))",
                uri);
        }

        [TestMethod]
        public async Task EmptyAny()
        {
            var query = Db.Users.WhereEquals(new IqlAnyExpression("_",
                new IqlPropertyExpression(
                    nameof(ApplicationUser.ClientsCreated),
                    new IqlRootReferenceExpression()), null));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(@"http://localhost:28000/odata/Users?$filter=ClientsCreated/any()",
                uri);
        }

        [TestMethod]
        public async Task EmptyAll()
        {
            var query = Db.Users.WhereEquals(new IqlAllExpression("_",
                new IqlPropertyExpression(
                    nameof(ApplicationUser.ClientsCreated),
                    new IqlRootReferenceExpression()), null));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(@"http://localhost:28000/odata/Users",
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