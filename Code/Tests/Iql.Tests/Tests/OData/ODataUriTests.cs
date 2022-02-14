using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Entities;
using Iql.Entities.Services;
using Iql.OData;
using Iql.OData.Extensions;
using Iql.Queryable;
#if TypeScript
using Iql.Parsing;
using Iql.Parsing.Expressions;
using Iql.Queryable.Expressions;
#else
using Brandless.ObjectSerializer;
using Iql.DotNet.Serialization;
#endif
using Iql.Tests.Context;
using Iql.Tests.Data.Services;
using Iql.Tests.Tests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;
using Iql.Parsing;
using Iql.Parsing.Expressions;

namespace Iql.Tests.Tests.OData
{
    [TestClass]
    public class ODataUriTests : TestsBase
    {
        public override void TestInitialize()
        {
            base.TestInitialize();
            Db.ServiceProvider.Unregister<TestNowService>();
        }

        public override void TestCleanUp()
        {
            base.TestCleanUp();
            Db.ServiceProvider.Register<TestNowService>();
        }

        [TestMethod]
        public async Task TestIntersectsWithEmptyPointsListShouldBeIgnored()
        {
            var polygon = new IqlPolygonExpression(
                new IqlRingExpression());
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
            Assert.AreEqual(@"http://localhost:28000/odata/Sites", uri);
        }

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
            Assert.AreEqual(
                @"http://localhost:28000/odata/Sites?$filter=geo.intersects($it/Location,geography'SRID=4326;POLYGON((2 1,4 5,3 7,2 1))')",
                uri);
        }

        [TestMethod]
        public async Task TestSearchPivot()
        {
            var query = Db.ClientCategoriesPivot.Search(
                "abc",
                IqlSearchKind.Primary | IqlSearchKind.Secondary | IqlSearchKind.Relationships);
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(
                @"http://localhost:28000/odata/ClientCategoriesPivot?$filter=(contains($it/Client/Name,'abc') or contains($it/Category/Name,'abc'))",
                uri);
        }

        [TestMethod]
        public async Task ExampleTest()
        {
            var query = Db
                .Clients
                .Where(client => client.Name.Contains("abc"))
                .Expand(client => client.People)
                .OrderBy(client => client.Name);
            var uri = await query.ResolveODataUriAsync();
            var someClient = Db.Clients.WithKey(123);
            uri = await someClient.ResolveODataUriAsync();
            var instance = new IqlDataSetQueryExpression
            {
                DataSet = new IqlDataSetReferenceExpression
                {
                    Name = "Clients",
                    Kind = IqlExpressionKind.DataSetReference,
                    ReturnType = IqlType.Collection
                },
                EntityTypeName = "Client",
                WithKey = new IqlWithKeyExpression
                {
                    KeyEqualToExpressions = new List<IqlIsEqualToExpression>
                    {
                        new IqlIsEqualToExpression
                        {
                            Left = new IqlPropertyExpression
                            {
                                PropertyName = "Id",
                                Kind = IqlExpressionKind.Property,
                                ReturnType = IqlType.Unknown,
                                Parent = new IqlRootReferenceExpression
                                {
                                    VariableName = "entity",
                                    Kind = IqlExpressionKind.RootReference
                                }
                            },
                            Right = new IqlLiteralExpression
                            {
                                Value = 123,
                                InferredReturnType = IqlType.Integer,
                                Kind = IqlExpressionKind.Literal
                            },
                            Kind = IqlExpressionKind.IsEqualTo,
                            ReturnType = IqlType.Boolean
                        }
                    },
                    Kind = IqlExpressionKind.WithKey,
                    ReturnType = IqlType.Class
                },
                Parameters = new List<IqlRootReferenceExpression>
                {
                    new IqlRootReferenceExpression
                    {
                        EntityTypeName = "Client",
                        InferredReturnType = IqlType.Unknown,
                        Kind = IqlExpressionKind.RootReference,
                        ReturnType = IqlType.Unknown
                    }
                },
                Kind = IqlExpressionKind.DataSetQuery,
                ReturnType = IqlType.Class
            };
            //var iql = new CSharpObjectSerializer().SerializeToString(await someClient.ToIqlAsync());
        }

        [TestMethod]
        public async Task TestSearchPivotWithExclude()
        {
            var query = Db.ClientCategoriesPivot.Search(
                "abc",
                IqlSearchKind.Primary | IqlSearchKind.Secondary | IqlSearchKind.Relationships,
                null,
                new[] { IqlPropertyPath.FromExpression<ClientCategoryPivot>(_ => _.Category, TypeResolver) });
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(
                @"http://localhost:28000/odata/ClientCategoriesPivot?$filter=contains($it/Client/Name,'abc')", uri);
        }

        [TestMethod]
        public async Task TestSearchPivotRemaining()
        {
            var key = CompositeKey.Ensure(7, Db.EntityConfigurationContext.EntityType<ClientCategory>());
            var query = Db.Clients.SearchRemaining(
                Db.EntityConfigurationContext.EntityType<ClientCategoryPivot>().FindRelationship(_ => _.Category),
                key,
                "abc"
            );
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(
                @"http://localhost:28000/odata/Clients?$filter=(contains($it/Name,'abc') and not(Categories/any(child:(child/CategoryId eq 7))))",
                uri);
        }

        [TestMethod]
        public async Task TestSearchPivotRemainingWithSpecificExcludes()
        {
            var clientCategoryPivot = new ClientCategoryPivot();
            clientCategoryPivot.CategoryId = 7;
            clientCategoryPivot.ClientId = 9;
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 15
            });
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 9
            });
            AppDbContext.InMemoryDb.ClientCategories.Add(new ClientCategory
            {
                Id = 7
            });
            var excludeClient = await Db.Clients.GetWithKeyAsync(15);
            var categoryWithImplicitExcludes = await Db.ClientCategories.GetWithKeyAsync(7);
            var excludeClientByKey = Db.EntityConfigurationContext.EntityType<Client>()
                .GetCompositeKey(new Client
                {
                    Id = 26
                });
            categoryWithImplicitExcludes.Clients.Add(new ClientCategoryPivot
            {
                CategoryId = 7,
                ClientId = 100
            });
            categoryWithImplicitExcludes.Clients.Add(new ClientCategoryPivot
            {
                CategoryId = 7,
                ClientId = 103
            });
            Db.ClientCategoriesPivot.Add(clientCategoryPivot);
            var query = Db.Clients.SearchRemaining(
                Db.EntityConfigurationContext.EntityType<ClientCategoryPivot>().FindRelationship(_ => _.Category),
                categoryWithImplicitExcludes,
                "abc",
                IqlSearchKind.Primary,
                null,
                new[] { IqlPropertyPath.FromExpression<ClientCategoryPivot>(_ => _.Category, TypeResolver) },
                new object[] { excludeClient, excludeClientByKey });
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(
                @"http://localhost:28000/odata/Clients?$filter=((contains($it/Name,'abc') and not(Categories/any(child:(child/CategoryId eq 7)))) and ((((not(($it/Id eq 100)) and not(($it/Id eq 103))) and not(($it/Id eq 9))) and not(($it/Id eq 15))) and not(($it/Id eq 26))))",
                uri);
        }

        [TestMethod]
        public async Task TestNonRingPolygonBecomesRing()
        {
            var polygon = new IqlPolygonExpression(
                new IqlRingExpression(new IqlPointExpression[]
                {
                    new IqlPointExpression(2, 1),
                    new IqlPointExpression(4, 5),
                    new IqlPointExpression(3, 7)
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
            Assert.AreEqual(
                @"http://localhost:28000/odata/Sites?$filter=geo.intersects($it/Location,geography'SRID=4326;POLYGON((2 1,4 5,3 7,2 1))')",
                uri);
        }

        [TestMethod]
        public async Task TestGuidInMappedType()
        {
            var query = Db.UsersManager.Set.WithKey(new Guid("3f32ef79-d56c-4932-a860-12b9cbf980c7"));
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Users('3f32ef79-d56c-4932-a860-12b9cbf980c7')", uri);
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
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(
                @"http://localhost:28000/odata/Sites?$filter=(geo.distance($it/Location,geography'SRID=4326;POINT(2 1)') lt 150)",
                uri);
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
            Assert.AreEqual(
                @"http://localhost:28000/odata/Sites?$filter=(geo.distance($it/Location,$it/Location) lt 150)", uri);
        }

        [TestMethod]
        public async Task TestWhereWithFinalExpression()
        {
            var query = Db.People.WherePropertyEquals(
                nameof(Person.Description),
                new IqlFinalExpression<string>("hello"));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(
                @"http://localhost:28000/odata/People?$filter=($it/Description eq hello)",
                uri);
        }
        
#if !TypeScript
        [TestMethod]
        public async Task TestWhereOnGuid()
        {
            var query = Db.People.WhereEquals(new IqlIsEqualToExpression(
                new IqlPropertyExpression(
                    nameof(Person.Guid),
                    new IqlRootReferenceExpression()),
                new IqlLiteralExpression(Guid.Empty)));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(
                @"http://localhost:28000/odata/People?$filter=($it/Guid eq 00000000-0000-0000-0000-000000000000)",
                uri);
        }

        [TestMethod]
        public async Task TestWhereOnGuidWithString()
        {
            var query = Db.People.WhereEquals(new IqlIsEqualToExpression(
                new IqlPropertyExpression(
                    nameof(Person.Guid),
                    new IqlRootReferenceExpression()),
                new IqlLiteralExpression(Guid.Empty.ToString())));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(
                @"http://localhost:28000/odata/People?$filter=($it/Guid eq 00000000-0000-0000-0000-000000000000)",
                uri);
        }
#endif

        [TestMethod]
        public async Task TestCurrentUserId()
        {
            Db.ServiceProvider.Unregister<IqlCurrentUserService>();
            var query = Db.People.WhereEquals(new IqlIsEqualToExpression(
                new IqlPropertyExpression(
                    nameof(Person.CreatedByUserId),
                    new IqlRootReferenceExpression()),
                new IqlCurrentUserIdExpression()));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(@"http://localhost:28000/odata/People?$filter=($it/CreatedByUserId eq null)",
                uri);
            Db.ServiceProvider.RegisterInstance<IqlCurrentUserService>(new TestCurrentUserResolver());
            uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(
                $@"http://localhost:28000/odata/People?$filter=($it/CreatedByUserId eq '{TestCurrentUserResolver.TestCurrentUserId}')",
                uri);
            Db.ServiceProvider.Unregister<IqlCurrentUserService>();
        }

        [TestMethod]
        public async Task TestCollectionCount()
        {
            var uri = await Db.People.ExpandCollectionCount(s => s.Types).ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/People?$expand=Types/$count", uri);
        }

        [TestMethod]
        public async Task TestCurrentUserGetId()
        {
            Db.ServiceProvider.Unregister<IqlCurrentUserService>();
            var query = Db.People.Where(_ => _.CreatedByUserId == IqlCurrentUser.Get<ApplicationUser>().Id
#if TypeScript
                ,
                                new EvaluateContext
                                {
                                    Context = this,
                                    Evaluate = n => (Func<object, object>)Evaluator.Eval(n)
                                }
#endif
            );
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(@"http://localhost:28000/odata/People?$filter=($it/CreatedByUserId eq null)",
                uri);
            Db.ServiceProvider.RegisterInstance<IqlCurrentUserService>(new TestCurrentUserResolver());
            uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(
                $@"http://localhost:28000/odata/People?$filter=($it/CreatedByUserId eq '{TestCurrentUserResolver.TestCurrentUserId}')",
                uri);
            Db.ServiceProvider.Unregister<IqlCurrentUserService>();
        }

        [TestMethod]
        public async Task TestCurrentLocation()
        {
            //            var point = new IqlPointExpression(2, 1);
            //            var query = Db.People.Where(site => site.Location.DistanceFrom(point) < 500
            //#if TypeScript
            //                    , 
            //                    new EvaluateContext
            //                    {
            //                        Context = this,
            //                        Evaluate = n => (Func<object, object>)Evaluator.Eval(n)
            //                    }
            //#endif
            //            );
            //            var iql = await query.ToIqlAsync();
            //            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            //            var xml = new CSharpObjectSerializer().Serialize(iql).Initialiser;
            //            var iql2 = new IqlDataSetQueryExpression
            //            {
            //                DataSet = new IqlDataSetReferenceExpression
            //                {
            //                    Name = "People",
            //                    Kind = IqlExpressionKind.DataSetReference,
            //                    ReturnType = IqlType.Collection
            //                },
            //                Filter = new IqlLambdaExpression
            //                {
            //                    Body = new IqlIsLessThanExpression
            //                    {
            //                        Left = new IqlDistanceExpression
            //                        {
            //                            Srid = 4326,
            //                            Left = new IqlPropertyExpression
            //                            {
            //                                PropertyName = "Location",
            //                                Kind = IqlExpressionKind.Property,
            //                                ReturnType = IqlType.Unknown,
            //                                Parent = new IqlRootReferenceExpression
            //                                {
            //                                    EntityTypeName = "Person",
            //                                    VariableName = "site",
            //                                    InferredReturnType = IqlType.Unknown,
            //                                    Kind = IqlExpressionKind.RootReference,
            //                                    ReturnType = IqlType.Unknown
            //                                }
            //                            },
            //                            Right = new IqlLiteralExpression
            //                            {
            //                                Value = new IqlPointExpression
            //                                {
            //                                    X = 2,
            //                                    Y = 1,
            //                                    Srid = 4326,
            //                                    Kind = IqlExpressionKind.GeoPoint,
            //                                    ReturnType = IqlType.GeographyPoint
            //                                },
            //                                InferredReturnType = IqlType.GeographyPoint,
            //                                Kind = IqlExpressionKind.Literal,
            //                                ReturnType = IqlType.GeographyPoint
            //                            },
            //                            Kind = IqlExpressionKind.Distance,
            //                            ReturnType = IqlType.Decimal
            //                        },
            //                        Right = new IqlLiteralExpression
            //                        {
            //                            Value = 500,
            //                            InferredReturnType = IqlType.Decimal,
            //                            Kind = IqlExpressionKind.Literal,
            //                            ReturnType = IqlType.Decimal
            //                        },
            //                        Kind = IqlExpressionKind.IsLessThan,
            //                        ReturnType = IqlType.Unknown
            //                    },
            //                    Parameters = new List<IqlRootReferenceExpression>
            //                    {
            //                        new IqlRootReferenceExpression
            //                        {
            //                            EntityTypeName = "Person",
            //                            VariableName = "site",
            //                            InferredReturnType = IqlType.Unknown,
            //                            Kind = IqlExpressionKind.RootReference,
            //                            ReturnType = IqlType.Unknown
            //                        }
            //                    },
            //                    Kind = IqlExpressionKind.Lambda,
            //                    ReturnType = IqlType.Unknown
            //                },
            //                Parameters = new List<IqlRootReferenceExpression>
            //                {
            //                    new IqlRootReferenceExpression
            //                    {
            //                        EntityTypeName = "Person",
            //                        InferredReturnType = IqlType.Unknown,
            //                        Kind = IqlExpressionKind.RootReference,
            //                        ReturnType = IqlType.Unknown
            //                    }
            //                },
            //                Kind = IqlExpressionKind.DataSetQuery,
            //                ReturnType = IqlType.Class
            //            };
            var query = Db.People.WhereEquals(new IqlIsLessThanExpression(
                new IqlDistanceExpression(
                    new IqlPropertyExpression(
                        nameof(Person.Location),
                        new IqlRootReferenceExpression()),
                    new IqlCurrentLocationExpression()),
                new IqlLiteralExpression(500)));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(@"http://localhost:28000/odata/People", uri);
            Db.ServiceProvider.RegisterInstance<IqlCurrentLocationService>(new TestCurrentLocationResolver());
            var currentLatitude = 51.5054597;
            TestCurrentLocationResolver.CurrentLatitude = currentLatitude;
            var currentLongitude = -0.0775452;
            TestCurrentLocationResolver.CurrentLongitude = currentLongitude;
            uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(
                @"http://localhost:28000/odata/People?$filter=(geo.distance($it/Location,geography'SRID=4326;POINT(-0.077545 51.50546)') lt 500)",
                uri);
            TestCurrentLocationResolver.CurrentLatitude = null;
            TestCurrentLocationResolver.CurrentLongitude = null;
            Db.ServiceProvider.Unregister<IqlCurrentLocationService>();
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
            var reducedValue = "463202.03500926046";
            Assert.AreEqual($@"http://localhost:28000/odata/Sites?$filter=(geo.length($it/Line) lt {reducedValue})",
                uri);
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
            Assert.AreEqual(
                @"http://localhost:28000/odata/Clients?$filter=(Sites/$count($filter=(AdditionalSendReportsTo/$count gt 22)) gt 3)",
                uri);
        }

#if !TypeScript
        // TypeScript translation converts this to ".indexOf(..)"
        [TestMethod]
        public async Task StringNotContainsShouldIncludeNullAndEmptyCheck()
        {
            var query = Db.Clients.Where(c => !c.Name.Contains("xyz"));
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(
                @"http://localhost:28000/odata/Clients?$filter=((not(contains($it/Name,'xyz')) or ($it/Name eq null)) or ($it/Name eq ''))",
                uri);
        }

#endif
        [TestMethod]
        public async Task StringNegatedIndexOfShouldIncludeNullAndEmptyCheck()
        {
            var query = Db.Clients.Where(c => c.Name.IndexOf("xyz") == -1);
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(
                @"http://localhost:28000/odata/Clients?$filter=(((indexof(tolower($it/Name),'xyz') eq -1) or ($it/Name eq null)) or ($it/Name eq ''))",
                uri);
        }

        [TestMethod]
        public async Task StringDoubleNegatedIndexOfShouldIncludeNullAndEmptyCheck()
        {
            var query = Db.Clients.Where(c => !!(c.Name.IndexOf("xyz") == -1));
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(
                @"http://localhost:28000/odata/Clients?$filter=not(not((((indexof(tolower($it/Name),'xyz') eq -1) or ($it/Name eq null)) or ($it/Name eq ''))))",
                uri);
        }

        [TestMethod]
        public async Task StringTripleNegatedIndexOfShouldNotIncludeNullAndEmptyCheck()
        {
            var query = Db.Clients.Where(c => !!!(c.Name.IndexOf("xyz") == -1));
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(
                @"http://localhost:28000/odata/Clients?$filter=not(not(not((indexof(tolower($it/Name),'xyz') eq -1))))",
                uri);
        }

        [TestMethod]
        public async Task StringTripleNegatedIndexOfShouldNotIncludeNullAndEmptyCheck2()
        {
            var query = Db.Clients.Where(c => (((c.Name.IndexOf("xyz") == -1) == false) == false) == false);
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(
                @"http://localhost:28000/odata/Clients?$filter=not(not(not((indexof(tolower($it/Name),'xyz') eq -1))))",
                uri);
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
        public async Task TestExpandDisplayFormatter()
        {
            var query = Db.SiteInspections.ExpandForDisplayFormatter();
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/SiteInspections?$expand=Site,CreatedByUser", uri);
        }

        [TestMethod]
        public async Task TestExpandDisplayFormatterWithOtherFilters()
        {
            var query = Db.SiteInspections.ExpandForDisplayFormatter()
                .SearchForDisplayFormatter("abc", null, true)
                .OrderByDefault(false)
                .Take(5);
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(
                @"http://localhost:28000/odata/SiteInspections?$filter=(contains($it/Site/Name,'abc') or contains($it/CreatedByUser/FullName,'abc'))&$expand=Site,CreatedByUser&$orderby=$it/CreatedDate&$top=5",
                uri);
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
            Assert.AreEqual(@"http://localhost:28000/odata/Users/IqlSampleApp.Me()", uri);
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
            Assert.AreEqual(
                @"http://localhost:28000/odata/Users('928B9116-B06C-49EF-98C9-52A776E03ECD')/IqlSampleApp.ReinstateUser",
                uri);
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
        public async Task TestFilterAndExpandUri()
        {
            var query = Db.Clients.Where(c => c.Name == "hello");

            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/Clients?$filter=($it/Name eq 'hello')",
                uri);

            query = query.OrderBy(c => c.Name).Expand(c => c.Type);
            uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(
                @"http://localhost:28000/odata/Clients?$filter=($it/Name eq 'hello')&$expand=Type&$orderby=$it/Name",
                uri);
        }

        [TestMethod]
        public async Task TestFilteringOnFilteredNestedCollectionResultCount()
        {
            var query = Db.People.ExpandCollection(c => c.Types,
                tq => tq.Where(c => c.Description == "a").Expand(c => c.Type));
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
            var query = Db.People.Where(c =>
                c.Types.Count(t => t.Description.IndexOf("TEST") != -1) > c.Types.Count * 0.5);
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(
                @"http://localhost:28000/odata/People?$filter=(Types/$count($filter=(indexof(tolower(Description),'test') ne -1)) gt (Types/$count mul 0.5))",
                uri);
        }

        [TestMethod]
        public async Task TestDivide()
        {
            var query = Db.People.Where(c =>
                c.Types.Count(t => t.Description.IndexOf("TEST") != -1) > c.Types.Count / 0.5);
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
        public async Task DateNowTicksString()
        {
            Db.ServiceProvider.Register<TestNowService>();
            var query = Db.Users.WhereEquals(new IqlIsEqualToExpression(
                new IqlPropertyExpression(
                    nameof(Client.Name),
                    new IqlRootReferenceExpression()),
                new IqlNowTicksStringExpression()));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Db.ServiceProvider.Unregister<TestNowService>();
#if TypeScript
            Assert.AreEqual(@"http://localhost:28000/odata/Users?$filter=($it/Name eq '1546398245000')",
                uri);
#else
            Assert.AreEqual(@"http://localhost:28000/odata/Users?$filter=($it/Name eq '636819950450000000')",
                uri);
#endif
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
            Assert.AreEqual(
                @"http://localhost:28000/odata/Users?$filter=($it/CreatedDate gt (now() sub duration'P7D'))",
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
            Assert.AreEqual(
                @"http://localhost:28000/odata/Users?$filter=not(($it/CreatedDate gt (now() sub duration'P7D')))",
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
            Assert.AreEqual(
                @"http://localhost:28000/odata/Users?$filter=($it/CreatedDate gt (now() sub duration'P3650DT7H15M33.014S'))",
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
            var rootVariableName = "_";
            var query = Db.Users.WhereEquals(new IqlAnyExpression(rootVariableName,
                new IqlPropertyExpression(
                    nameof(ApplicationUser.ClientsCreated),
                    new IqlRootReferenceExpression()),
                IqlLambdaExpression.Create(new IqlIsEqualToExpression(new IqlPropertyExpression(
                        nameof(Client.Name),
                        new IqlRootReferenceExpression(rootVariableName)),
                    new IqlLiteralExpression("jimbo", IqlType.String)), IqlType.Boolean, rootVariableName)));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(@"http://localhost:28000/odata/Users?$filter=ClientsCreated/any(_:(_/Name eq 'jimbo'))",
                uri);
        }

        [TestMethod]
        public async Task AnyCheck2()
        {
            var rootVariableName = "_";
            var query = Db.Users.WhereEquals(new IqlAnyExpression(rootVariableName,
                new IqlPropertyExpression(
                    nameof(ApplicationUser.ClientsCreated),
                    new IqlRootReferenceExpression()),
                IqlLambdaExpression.Create(new IqlStringIncludesExpression(new IqlPropertyExpression(
                        nameof(Client.Name),
                        new IqlRootReferenceExpression(rootVariableName)),
                    new IqlLiteralExpression("jimbo", IqlType.String)), IqlType.Boolean, rootVariableName)));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(
                @"http://localhost:28000/odata/Users?$filter=ClientsCreated/any(_:contains(_/Name,'jimbo'))",
                uri);
        }

        [TestMethod]
        public async Task AllCheck()
        {
            var rootVariableName = "_";
            var query = Db.Users.WhereEquals(new IqlAllExpression(rootVariableName,
                new IqlPropertyExpression(
                    nameof(ApplicationUser.ClientsCreated),
                    new IqlRootReferenceExpression()),
                IqlLambdaExpression.Create(new IqlIsEqualToExpression(new IqlPropertyExpression(
                        nameof(Client.Name),
                        new IqlRootReferenceExpression(rootVariableName)),
                    new IqlLiteralExpression("jimbo", IqlType.String)), IqlType.Boolean, rootVariableName)));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(@"http://localhost:28000/odata/Users?$filter=ClientsCreated/all(_:(_/Name eq 'jimbo'))",
                uri);
        }

        [TestMethod]
        public async Task AllCheck2()
        {
            var rootVariableName = "_";
            var query = Db.Users.WhereEquals(new IqlAllExpression(rootVariableName,
                new IqlPropertyExpression(
                    nameof(ApplicationUser.ClientsCreated),
                    new IqlRootReferenceExpression()),
                IqlLambdaExpression.Create(new IqlStringIncludesExpression(new IqlPropertyExpression(
                        nameof(Client.Name),
                        new IqlRootReferenceExpression(rootVariableName)),
                    new IqlLiteralExpression("jimbo", IqlType.String)), IqlType.Boolean, rootVariableName)));
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(
                @"http://localhost:28000/odata/Users?$filter=ClientsCreated/all(_:contains(_/Name,'jimbo'))",
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
            var query = Db.Users.Where(c =>
                    (c.Permissions & (UserPermissions.Edit | UserPermissions.Create)) != 0 ||
                    (c.Permissions & (UserPermissions.Delete)) != 0
#if TypeScript
    , new EvaluateContext(code => Evaluator.Eval(code))
#endif
            );
            var uri = Uri.UnescapeDataString(await query.ResolveODataUriAsync());
            Assert.AreEqual(
                @"http://localhost:28000/odata/Users?$filter=(($it/Permissions has 'Create,Edit') or ($it/Permissions has 'Delete'))",
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
            Assert.AreEqual(
                @"http://localhost:28000/odata/Users?$filter=(($it/Permissions has 'Create,Edit') or ($it/Permissions has 'Delete'))",
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
        public async Task TestRelationshipFilterRuleUri()
        {
            var person = new Person();
            person.SiteId = 12;
            var query = await Db.SiteAreas.ApplyRelationshipFiltersByExpressionAsync(
                p => p.SiteArea,
                person
            );
            var uri = await query.ResolveODataUriAsync();
            uri = Uri.UnescapeDataString(uri);
            Assert.AreEqual(@"http://localhost:28000/odata/SiteAreas?$filter=($it/SiteId eq 12)",
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


        [TestMethod]
        public async Task TestOrderByDistance()
        {
            var point = new IqlPointExpression(2, 1);
            var query = Db.Sites.OrderBy(site => site.Location.DistanceFrom(point) < 150
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
            Assert.AreEqual(
                @"http://localhost:28000/odata/Sites?$orderby=(geo.distance($it/Location,geography'SRID=4326;POINT(2 1)') lt 150)",
                uri);
        }
    }
}