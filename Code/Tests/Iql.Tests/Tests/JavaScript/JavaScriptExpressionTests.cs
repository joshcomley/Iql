using Iql.Parsing;
#if TypeScript || CustomEvaluate || true
using System;
using Iql.JavaScript.JavaScriptExpressionToIql;
#else
// using Brandless.ObjectSerializer;
#endif
using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Entities.Rules.Relationship;
using Iql.Entities.SpecialTypes;
using Iql.JavaScript.Extensions;
using Iql.JavaScript.JavaScriptExpressionToIql;
using Iql.OData;
using Iql.OData.Extensions;
using Iql.Tests.Context;
using Iql.Tests.Tests.OData;
using Iql.Tests.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Tests.JavaScript
{
    [TestClass]
    public class JavaScriptExpressionTests : TestsBase
    {
#if TypeScript || CustomEvaluate || true
        [TestMethod]
        public void TestWebPackedJavaScript()
        {
            var anonVariable = new
            {
                t = "123"
            };
            var converter = new JavaScriptExpressionConverter();
            var expression = converter.ConvertJavaScriptStringToIql<ApplicationUser>(@"function(_) { return _.Email == someVariable[/* IqlCurrentUser */ 't']; }", TypeResolver, 
                new EvaluateContext(_ =>
                {
                    if(_ == "t")
                    {
                        return anonVariable.t;
                    }
                    return anonVariable;
                }));
            //var expression = converter.ConvertJavaScriptStringToIql<ApplicationUser>(@"function(_) { return _.IsClosed == false && _.CreatedByUserId == _brandless_iql__WEBPACK_IMPORTED_MODULE_10__[/* IqlCurrentUser */ ""t""].Get().Id && _.CreatedDate > today; }", TypeResolver);
            //function(_) { return _.IsClosed == false && _.CreatedByUserId == _brandless_iql__WEBPACK_IMPORTED_MODULE_10__[/* IqlCurrentUser */ ""t""].Get().Id && _.CreatedDate > today; }
        }
#endif
        public string EntityType = "abc";

        [TestMethod]
        public void TestThis()
        {
            //_ => _.EntityType == this.parameters.EntityType
            var converter = new JavaScriptExpressionConverter();
            var expression = converter.ConvertJavaScriptStringToIql<ApplicationUser>(@"function (c) { return c.FullName == this.EntityType; }", TypeResolver,
                new EvaluateContext((name) => { return this; }));
            var lambda = expression.Expression as IqlLambdaExpression;
            Assert.AreEqual(IqlExpressionKind.IsEqualTo, lambda.Body.Kind);
            var javascript =
                converter.ConvertIqlToExpressionStringByType(expression.Expression, TypeResolver, typeof(ApplicationUser));
            Assert.AreEqual(@"function(entity, context) { return ((((entity || {}).FullName == null) ? null : ((entity || {}).FullName || '').toUpperCase()) === 'ABC'); }",
                javascript);
        }

        [TestMethod]
        public void TestConditionalExpression()
        {
            var converter = new JavaScriptExpressionConverter();
            var expression = converter.ConvertJavaScriptStringToIql<ApplicationUser>(@"function (c) { return 1 > 2 ? 3 : 4; }", TypeResolver);
            var lambda = expression.Expression as IqlLambdaExpression;
            Assert.IsTrue(lambda.Body is IqlConditionExpression);
            var javascript =
                converter.ConvertIqlToExpressionStringByType(expression.Expression, TypeResolver, typeof(ApplicationUser));
            Assert.AreEqual(@"function(entity, context) { return ((1 > 2)?3:4); }",
                javascript);
        }

        [TestMethod]
        public void TestLambdaGeneratingLambda()
        {
            var instance = RelationshipFilterContextExpressions.Get();
            var converter = new JavaScriptExpressionConverter();
            var expression = converter.ConvertIqlToExpressionString(instance, TypeResolver);
            Assert.AreEqual(@"function(entity, context) { return function(entity2, context) { return (entity2.ClientId === entity.Owner.ClientId); }; }",
                expression);
        }

        [TestMethod]
        public void TestLambda()
        {
            var converter = new JavaScriptExpressionConverter();
            var expression = converter.ConvertJavaScriptStringToIql<ApplicationUser>(@"function(entity2) { return (entity2.ClientId === 1); }", TypeResolver);
            var javascript =
                converter.ConvertIqlToExpressionStringByType(expression.Expression, TypeResolver, typeof(ApplicationUser));
            Assert.AreEqual(@"function(entity, context) { return ((entity || {}).ClientId === 1); }",
                javascript);
        }

        [TestMethod]
        public void TestGuid()
        {
            var converter = new JavaScriptExpressionConverter();
            var expression = new IqlDataSetQueryExpression
            {
                DataSet = new IqlDataSetReferenceExpression
                {
                    Name = "MyCustomReports",
                    Kind = IqlExpressionKind.DataSetReference,
                    ReturnType = IqlType.Collection
                },
                IncludeCount = true,
                WithKey = new IqlWithKeyExpression
                {
                    KeyEqualToExpressions = new List<IqlIsEqualToExpression>
                {
                    new IqlIsEqualToExpression
                    {
                        Left = new IqlPropertyExpression
                        {
                            PropertyName = "MyId",
                            Kind = IqlExpressionKind.Property,
                            ReturnType = IqlType.Guid,
                            Parent = new IqlRootReferenceExpression
                            {
                                Value = "",
                                VariableName = "entity",
                                Kind = IqlExpressionKind.RootReference,
                                ReturnType = IqlType.Unknown
                            }
                        },
                        Right = new IqlLiteralExpression
                        {
                            Value = "9cac910f-6b7c-46b8-9de6-d4373a0063d8",
                            InferredReturnType = IqlType.String,
                            Kind = IqlExpressionKind.Literal,
                            ReturnType = IqlType.Guid
                        },
                        Kind = IqlExpressionKind.IsEqualTo,
                        ReturnType = IqlType.Unknown
                    }
                },
                    Kind = IqlExpressionKind.WithKey,
                    ReturnType = IqlType.Class
                },
                Parameters = new List<IqlRootReferenceExpression>
            {
                new IqlRootReferenceExpression
                {
                    EntityTypeName = "MyCustomReport",
                    Kind = IqlExpressionKind.RootReference,
                    ReturnType = IqlType.Unknown
                }
            },
                Kind = IqlExpressionKind.DataSetQuery,
                ReturnType = IqlType.Class
            };
            var javascript =
                converter.ConvertIqlToExpressionStringAs<MyCustomReport>(expression, TypeResolver);
            Assert.AreEqual(@"function(contextWrapper) { return contextWrapper.Where(function(entity) { return ((entity || {}).MyId === '9cac910f-6b7c-46b8-9de6-d4373a0063d8'); }); }",
                javascript);
        }

        [TestMethod]
        public async Task TestConvertPropertyExpression()
        {
            var javaScriptExpressionConverter = new JavaScriptExpressionConverter();
            var result = javaScriptExpressionConverter.ConvertJavaScriptStringToIql<ApplicationUser>(
                $"p => p.{nameof(ApplicationUser.Id)}", TypeResolver);
            Assert.AreEqual(typeof(IqlLambdaExpression), result.Expression.GetType());
        }

        //[TestMethod]
        //public async Task TestFilterExpression()
        //{
        //    var javaScriptExpressionConverter = new JavaScriptExpressionConverter();
        //    var result = javaScriptExpressionConverter.ConvertJavaScriptStringToIql<ApplicationUser>(
        //        $"p => p.{nameof(ApplicationUser.ClientsCreated)}.filter(c => c.{nameof(Client.Name)} == 'abc')");
        //    Assert.AreEqual(typeof(IqlLambdaExpression), result.Expression.GetType());
        //}

        [TestMethod]
        public async Task TestStringIncludes()
        {
            IqlJavaScriptIqlExpressionExtensions.NormalizeJson = true;
            //var query = Db.Clients
            //        .Where(_ => _.Name.Contains("a"))
            //        .OrderByProperty(nameof(Client.Name))
            //    ;
            //var iqlTemp = await query.ToIqlAsync();
            //var iqlStr = new CSharpObjectSerializer().SerializeToString(iqlTemp);
            var iql = new IqlDataSetQueryExpression
            {
                DataSet = new IqlDataSetReferenceExpression
                {
                    Name = "Clients",
                    Kind = IqlExpressionKind.DataSetReference,
                    ReturnType = IqlType.Collection
                },
                OrderBys = new List<IqlOrderByExpression>
            {
                new IqlOrderByExpression
                {
                    OrderExpression = new IqlLambdaExpression
                    {
                        Body = new IqlPropertyExpression
                        {
                            PropertyName = "Name",
                            Kind = IqlExpressionKind.Property,
                            ReturnType = IqlType.Unknown,
                            Parent = new IqlRootReferenceExpression
                            {
                                VariableName = "entity",
                                Value = "",
                                InferredReturnType = IqlType.String,
                                Kind = IqlExpressionKind.RootReference,
                                ReturnType = IqlType.Unknown
                            }
                        },
                        Parameters = new List<IqlRootReferenceExpression>
                        {
                            new IqlRootReferenceExpression
                            {
                                VariableName = "entity",
                                Value = "",
                                InferredReturnType = IqlType.String,
                                Kind = IqlExpressionKind.RootReference,
                                ReturnType = IqlType.Unknown
                            }
                        },
                        Kind = IqlExpressionKind.Lambda,
                        ReturnType = IqlType.Unknown
                    },
                    Descending = false,
                    Kind = IqlExpressionKind.OrderBy,
                    ReturnType = IqlType.Collection
                }
            },
                Filter = new IqlLambdaExpression
                {
                    Body = new IqlStringIncludesExpression
                    {
                        Value = new IqlLiteralExpression
                        {
                            Value = "a",
                            InferredReturnType = IqlType.String,
                            Kind = IqlExpressionKind.Literal,
                            ReturnType = IqlType.String
                        },
                        Kind = IqlExpressionKind.StringIncludes,
                        ReturnType = IqlType.Boolean,
                        Parent = new IqlPropertyExpression
                        {
                            PropertyName = "Name",
                            Kind = IqlExpressionKind.Property,
                            ReturnType = IqlType.Unknown,
                            Parent = new IqlRootReferenceExpression
                            {
                                EntityTypeName = "Client",
                                VariableName = "_",
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.RootReference,
                                ReturnType = IqlType.Unknown
                            }
                        }
                    },
                    Parameters = new List<IqlRootReferenceExpression>
                {
                    new IqlRootReferenceExpression
                    {
                        EntityTypeName = "Client",
                        VariableName = "_",
                        InferredReturnType = IqlType.Unknown,
                        Kind = IqlExpressionKind.RootReference,
                        ReturnType = IqlType.Unknown
                    }
                },
                    Kind = IqlExpressionKind.Lambda,
                    ReturnType = IqlType.Unknown
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
            var javaScript = new JavaScriptExpressionConverter().ConvertIqlToExpressionStringByType(
                iql,
                TypeResolver,
                typeof(Client));

#if !TypeScript
            var expected = @"function(contextWrapper) { return contextWrapper.Where(function(entity, context) { return (((entity || {}).Name || '').toUpperCase() || '').includes(('a' || '').toUpperCase()); }, JSON.parse('{""Body"":{""Value"":{""Value"":""a"",""InferredReturnType"":4,""IsIqlExpression"":true,""Kind"":26,""ReturnType"":4,""Parent"":null},""IsIqlExpression"":true,""Kind"":31,""ReturnType"":7,""Parent"":{""PropertyName"":""Name"",""IsIqlExpression"":true,""Kind"":30,""ReturnType"":1,""Parent"":{""EntityTypeName"":""Client"",""VariableName"":""_"",""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}}},""Parameters"":[{""EntityTypeName"":""Client"",""VariableName"":""_"",""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}],""IsIqlExpression"":true,""Kind"":55,""ReturnType"":1,""Parent"":null}'))
		.OrderBy(function(entity2, context) { return (entity2 || {}).Name; }, false); }";
#else
            var expected = @"function(contextWrapper) { return contextWrapper.Where(function(entity, context) { return (((entity || {}).Name || '').toUpperCase() || '').includes(('a' || '').toUpperCase()); }, JSON.parse('{""Body"":{""Value"":{""Value"":""a"",""InferredReturnType"":4,""IsIqlExpression"":true,""Kind"":26,""ReturnType"":4,""Parent"":null},""IsIqlExpression"":true,""Kind"":31,""ReturnType"":7,""Parent"":{""PropertyName"":""Name"",""IsIqlExpression"":true,""Kind"":30,""ReturnType"":1,""Parent"":{""EntityTypeName"":""Client"",""VariableName"":""_"",""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}}},""Parameters"":[{""EntityTypeName"":""Client"",""VariableName"":""_"",""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}],""IsIqlExpression"":true,""Kind"":55,""ReturnType"":1,""Parent"":null}'))
		.OrderBy(function(entity2, context) { return (entity2 || {}).Name; }, false); }";
#endif
            var cleaned = expected.Clean();
            Assert.AreEqual(cleaned, javaScript.Clean());
            IqlJavaScriptIqlExpressionExtensions.NormalizeJson = false;
        }

        [TestMethod]
        public async Task TestFullQuery()
        {
            IqlJavaScriptIqlExpressionExtensions.NormalizeJson = true;
            var query = Db
                .Clients
                .Where(c => c.Name == "a")
                .OrderByDescending(c => c.AverageSales)
                .Expand(c => c.Type)
                .ExpandCollection(c => c.Sites, sites => sites.Expand(s => s.CreatedByUser))
                ;

            var iql = await query.ToIqlAsync();
            var javaScript = new JavaScriptExpressionConverter().ConvertIqlToExpressionStringByType(
                iql,
                TypeResolver,
                typeof(Client));

#if !TypeScript
            var expected = @"function(contextWrapper) { return contextWrapper.Where(function(entity, context) { return ((((entity || {}).Name == null) ? null : ((entity || {}).Name || '').toUpperCase()) === 'A'); }, JSON.parse('{""Body"":{""Left"":{""PropertyName"":""Name"",""IsIqlExpression"":true,""Kind"":30,""ReturnType"":1,""Parent"":{""EntityTypeName"":""Client"",""VariableName"":""c"",""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}},""Right"":{""Value"":""a"",""InferredReturnType"":4,""IsIqlExpression"":true,""Kind"":26,""ReturnType"":4,""Parent"":null},""IsIqlExpression"":true,""Kind"":10,""ReturnType"":7,""Parent"":null},""Parameters"":[{""EntityTypeName"":""Client"",""VariableName"":""c"",""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}],""IsIqlExpression"":true,""Kind"":55,""ReturnType"":7,""Parent"":null}'))
		.Expand(JSON.parse('{""NavigationProperty"":{""PropertyName"":""Type"",""IsIqlExpression"":true,""Kind"":30,""ReturnType"":1,""Parent"":{""EntityTypeName"":""Client"",""VariableName"":""c"",""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}},""Query"":{""DataSet"":{""Name"":""ClientTypes"",""IsIqlExpression"":true,""Kind"":50,""ReturnType"":2,""Parent"":null},""OrderBys"":null,""IncludeCount"":null,""Skip"":null,""Take"":null,""EntityTypeName"":""ClientType"",""Expands"":null,""Filter"":null,""WithKey"":null,""Parameters"":[{""EntityTypeName"":""ClientType"",""VariableName"":null,""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}],""IsIqlExpression"":true,""Kind"":49,""ReturnType"":3,""Parent"":null},""Count"":false,""IsIqlExpression"":true,""Kind"":48,""ReturnType"":2,""Parent"":null}'))
		.Expand(JSON.parse('{""NavigationProperty"":{""PropertyName"":""Sites"",""IsIqlExpression"":true,""Kind"":30,""ReturnType"":1,""Parent"":{""EntityTypeName"":""Client"",""VariableName"":""c"",""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}},""Query"":{""DataSet"":{""Name"":""Sites"",""IsIqlExpression"":true,""Kind"":50,""ReturnType"":2,""Parent"":null},""OrderBys"":null,""IncludeCount"":null,""Skip"":null,""Take"":null,""EntityTypeName"":""Site"",""Expands"":[{""NavigationProperty"":{""PropertyName"":""CreatedByUser"",""IsIqlExpression"":true,""Kind"":30,""ReturnType"":1,""Parent"":{""EntityTypeName"":""Site"",""VariableName"":""s"",""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}},""Query"":{""DataSet"":{""Name"":""Users"",""IsIqlExpression"":true,""Kind"":50,""ReturnType"":2,""Parent"":null},""OrderBys"":null,""IncludeCount"":null,""Skip"":null,""Take"":null,""EntityTypeName"":""ApplicationUser"",""Expands"":null,""Filter"":null,""WithKey"":null,""Parameters"":[{""EntityTypeName"":""ApplicationUser"",""VariableName"":null,""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}],""IsIqlExpression"":true,""Kind"":49,""ReturnType"":3,""Parent"":null},""Count"":false,""IsIqlExpression"":true,""Kind"":48,""ReturnType"":2,""Parent"":null}],""Filter"":null,""WithKey"":null,""Parameters"":[{""EntityTypeName"":""Site"",""VariableName"":null,""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}],""IsIqlExpression"":true,""Kind"":49,""ReturnType"":3,""Parent"":null},""Count"":false,""IsIqlExpression"":true,""Kind"":48,""ReturnType"":2,""Parent"":null}'))
		.OrderBy(function(entity, context) { return (entity || {}).AverageSales; }, true); }";
#else
            var expected = @"function(contextWrapper) { return contextWrapper.Where(function(entity, context) { return ((((entity || {}).Name == null) ? null : ((entity || {}).Name || '').toUpperCase()) === 'A'); }, JSON.parse('{""Body"":{""Left"":{""PropertyName"":""Name"",""IsIqlExpression"":true,""Kind"":30,""ReturnType"":4,""Parent"":{""EntityTypeName"":""Client"",""VariableName"":""c"",""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}},""Right"":{""Value"":""a"",""InferredReturnType"":4,""IsIqlExpression"":true,""Kind"":26,""ReturnType"":4,""Parent"":null},""IsIqlExpression"":true,""Kind"":10,""ReturnType"":7,""Parent"":null},""Parameters"":[{""EntityTypeName"":null,""VariableName"":""c"",""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}],""IsIqlExpression"":true,""Kind"":55,""ReturnType"":7,""Parent"":null}'))
		.Expand(JSON.parse('{""NavigationProperty"":{""PropertyName"":""Type"",""IsIqlExpression"":true,""Kind"":30,""ReturnType"":1,""Parent"":{""EntityTypeName"":""Client"",""VariableName"":""c"",""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}},""Query"":{""DataSet"":{""Name"":""ClientTypes"",""IsIqlExpression"":true,""Kind"":50,""ReturnType"":2,""Parent"":null},""OrderBys"":null,""EntityTypeName"":""ClientType"",""Expands"":null,""Filter"":null,""WithKey"":null,""Parameters"":[{""EntityTypeName"":""ClientType"",""VariableName"":null,""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}],""IsIqlExpression"":true,""Kind"":49,""ReturnType"":3,""Parent"":null},""Count"":false,""IsIqlExpression"":true,""Kind"":48,""ReturnType"":2,""Parent"":null}'))
		.Expand(JSON.parse('{""NavigationProperty"":{""PropertyName"":""Sites"",""IsIqlExpression"":true,""Kind"":30,""ReturnType"":1,""Parent"":{""EntityTypeName"":""Client"",""VariableName"":""c"",""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}},""Query"":{""DataSet"":{""Name"":""Sites"",""IsIqlExpression"":true,""Kind"":50,""ReturnType"":2,""Parent"":null},""OrderBys"":null,""EntityTypeName"":""Site"",""Expands"":[{""NavigationProperty"":{""PropertyName"":""CreatedByUser"",""IsIqlExpression"":true,""Kind"":30,""ReturnType"":1,""Parent"":{""EntityTypeName"":""Site"",""VariableName"":""s"",""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}},""Query"":{""DataSet"":{""Name"":""Users"",""IsIqlExpression"":true,""Kind"":50,""ReturnType"":2,""Parent"":null},""OrderBys"":null,""EntityTypeName"":""ApplicationUser"",""Expands"":null,""Filter"":null,""WithKey"":null,""Parameters"":[{""EntityTypeName"":""ApplicationUser"",""VariableName"":null,""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}],""IsIqlExpression"":true,""Kind"":49,""ReturnType"":3,""Parent"":null},""Count"":false,""IsIqlExpression"":true,""Kind"":48,""ReturnType"":2,""Parent"":null}],""Filter"":null,""WithKey"":null,""Parameters"":[{""EntityTypeName"":""Site"",""VariableName"":null,""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}],""IsIqlExpression"":true,""Kind"":49,""ReturnType"":3,""Parent"":null},""Count"":false,""IsIqlExpression"":true,""Kind"":48,""ReturnType"":2,""Parent"":null}'))
		.OrderBy(function(entity, context) { return (entity || {}).AverageSales; }, true); }";
#endif
            var cleaned = expected.Clean();
            var actual = javaScript.Clean();
            Assert.AreEqual(cleaned, actual);
            IqlJavaScriptIqlExpressionExtensions.NormalizeJson = false;
        }

        [TestMethod]
        public void TestFilterOnChildCollectionToOData()
        {
            var iql = new JavaScriptExpressionConverter().ConvertJavaScriptStringToIql<Client>(
                "function(t) { return t.Sites.filter(s => s.AdditionalSendReportsTo.length > 22).length > 3; }", TypeResolver);
            var odataFilter = new ODataExpressionConverter().ConvertIqlToExpressionStringAs<Client>(iql.Expression, TypeResolver);
            Assert.AreEqual(@"(Sites/$count($filter=(AdditionalSendReportsTo/$count gt 22)) gt 3)", odataFilter);
        }

        [TestMethod]
        public async Task TestWithKey()
        {
            var query = Db
                .Clients
                .WithKey(7);
            var iql = await query.ToIqlAsync();
            var javaScript = new JavaScriptExpressionConverter().ConvertIqlToExpressionStringByType(
                iql,
                TypeResolver,
                typeof(Client));
            Assert.AreEqual(
                @"function(contextWrapper) { return contextWrapper.Where(function(entity) { return ((entity || {}).Id === 7); }); }",
                javaScript);
        }

        [TestMethod]
        public async Task TestOrderBy()
        {
            var query = Db
                .Clients
                .OrderBy(c => c.Name);
            var iql = await query.ToIqlAsync();
            var javaScript = new JavaScriptExpressionConverter().ConvertIqlToExpressionStringByType(
                iql,
                TypeResolver,
                typeof(Client));
            Assert.AreEqual(
                @"function(contextWrapper) { return contextWrapper.OrderBy(function(entity, context) { return (entity || {}).Name; }, false); }",
                javaScript);
        }

        [TestMethod]
        public void TestCompileExpressionToJavaScript()
        {
            var iql =
                    new IqlPropertyExpression
                    {
                        PropertyName = "NumberOfKeys",
                        Kind = IqlExpressionKind.Property,
                        ReturnType = IqlType.Unknown,
                        Parent = new IqlPropertyExpression
                        {
                            PropertyName = "Piano",
                            Kind = IqlExpressionKind.Property,
                            ReturnType = IqlType.Unknown,
                            Parent = new IqlRootReferenceExpression
                            {
                                VariableName = "p",
                                Kind = IqlExpressionKind.RootReference,
                                ReturnType = IqlType.Unknown
                            }
                        }
                    }
                ;
            var js = new JavaScriptExpressionConverter().ConvertIqlToTypeScriptExpressionString(iql, TypeResolver);
            Assert.AreEqual(@"entity => ((entity || {}).Piano || {}).NumberOfKeys", js);
        }

        [TestMethod]
        public void TestMinifiedLambda()
        {
            var iql = new JavaScriptExpressionConverter().ConvertJavaScriptStringToIql<ApplicationUser>(
                "function(t) { return 1 == t.IsLockedOut }", TypeResolver);
            var odataFilter = new ODataExpressionConverter().ConvertIqlToExpressionStringAs<ApplicationUser>(iql.Expression, TypeResolver
#if TypeScript
            , null
#endif
            );
            Assert.AreEqual(@"(true eq $it/IsLockedOut)", odataFilter);
        }

        [TestMethod]
        public void TestEqualsEqualsEquals()
        {
            var iql = new JavaScriptExpressionConverter().ConvertJavaScriptStringToIql<ApplicationUser>(
                "function(t) { return t.IsLockedOut === true }", TypeResolver);
            var odataFilter = new ODataExpressionConverter().ConvertIqlToExpressionStringAs<ApplicationUser>(iql.Expression, TypeResolver);
            Assert.AreEqual(@"($it/IsLockedOut eq true)", odataFilter);
        }

        [TestMethod]
        public void TestUnaryNot()
        {
            var iql = new JavaScriptExpressionConverter().ConvertJavaScriptStringToIql<ApplicationUser>(
                "function(t) { return !t.IsLockedOut }", TypeResolver);
            var odataFilter = new ODataExpressionConverter().ConvertIqlToExpressionStringAs<ApplicationUser>(iql.Expression, TypeResolver);
            Assert.AreEqual(@"($it/IsLockedOut eq false)", odataFilter);
        }

        [TestMethod]
        public void TestUnarySequenceOfNots()
        {
            var iql = new JavaScriptExpressionConverter().ConvertJavaScriptStringToIql<ApplicationUser>(
                "function(t) { return !!!!!t.IsLockedOut }", TypeResolver);
            var odataFilter = new ODataExpressionConverter().ConvertIqlToExpressionStringAs<ApplicationUser>(iql.Expression, TypeResolver);
            Assert.AreEqual(@"((((($it/IsLockedOut eq false) eq false) eq false) eq false) eq false)", odataFilter);
        }

        [TestMethod]
        public void TestNestedLambdas()
        {
            //var iql1 = new JavaScriptExpressionConverter().ConvertJavaScriptStringToIql<Person>(
            //    "p => p.Title == `Test` || p.Title == `Test2`");
            //var iql2 = new JavaScriptExpressionConverter().ConvertJavaScriptStringToIql<Person>(
            //    "p => p.Title == `Test` || ((p.Types.filter(t => t.TypeId == 4 || t.Description == `Kettle`).length > 0))");
            var js = new JavaScriptExpressionConverter().ConvertJavaScriptStringToJavaScriptString<Person>(
                "p => p.Title == `Test` || ((p.Types.filter(t => t.TypeId == 4 || t.Description == p.Title).length > 0))", TypeResolver);
            Assert.AreEqual(
                @"function(entity, context) { return (((((entity || {}).Title == null) ? null : ((entity || {}).Title || '').toUpperCase()) === 'TEST') || ((((entity || {}).Types || []).filter(function(entity2) { return (((entity2 || {}).TypeId === 4) || ((((entity2 || {}).Description == null) ? null : ((entity2 || {}).Description || '').toUpperCase()) === (((entity || {}).Title == null) ? null : ((entity || {}).Title || '').toUpperCase()))); }).length) > 0)); }",
                js);
            //var iql3 = new DotNetExpressionConverter().ConvertLambdaToIql<Person>(p =>
            //    p.Title == "Test" || p.Types.Any(t => t.TypeId == 4 || t.Description == "Kettle"));
            // var xml = IqlSerializer.SerializeToXml(iql2.Expression);
        }

        [TestMethod]
        public void TestCompressedJavaScript()
        {
            var js = new JavaScriptExpressionConverter().ConvertJavaScriptStringToJavaScriptString<Client>(
                "function(n){return n.Sites.filter(function(n){return n.UsersCount>0}).length>0}", TypeResolver);
            Assert.AreEqual(
                @"function(entity, context) { return ((((entity || {}).Sites || []).filter(function(entity) { return ((entity || {}).UsersCount > 0); }).length) > 0); }",
                js);
            //function(n){return n.CandidateResults.filter(function(n){return!n.Exam.IsTraining}).length>0}
        }

        [TestMethod]
        public void TestCompressedCompoundJavaScript()
        {
            var js = new JavaScriptExpressionConverter().ConvertJavaScriptStringToJavaScriptString<Client>(
                "function(n){return n.Sites.filter(function(n){return!n.CreatedByUser.EmailConfirmed}).length>0}", TypeResolver);
            Assert.AreEqual(
                @"function(entity, context) { return ((((entity || {}).Sites || []).filter(function(entity) { return (((entity || {}).CreatedByUser || {}).EmailConfirmed === false); }).length) > 0); }",
                js);
            //function(n){return n.CandidateResults.filter(function(n){return!n.Exam.IsTraining}).length>0}
        }

        [TestMethod]
        public void TestConvertingCompressedBooleanAsDigitExpression()
        {
            var iql = new JavaScriptExpressionConverter().ConvertJavaScriptStringToIql<Site>(
                "function(n){return 0==n.CreatedByUser.EmailConfirmed}", TypeResolver);
            var js = new JavaScriptExpressionConverter().ConvertIqlToExpressionStringByType(iql.Expression, TypeResolver, typeof(Client));
            Assert.AreEqual(@"function(entity, context) { return (false === ((entity || {}).CreatedByUser || {}).EmailConfirmed); }", js);
            var odataFilter = new ODataExpressionConverter().ConvertIqlToExpressionStringAs<Client>(iql.Expression, TypeResolver);
            Assert.AreEqual(@"(false eq $it/CreatedByUser/EmailConfirmed)", odataFilter);
        }

        [TestMethod]
        public async Task FilterChildCollectionCount()
        {
            var db = Db;
            var js = @"c => c.Types.length > 2";
            var iql = new JavaScriptExpressionConverter().ConvertJavaScriptStringToIql<Person>(js, TypeResolver).Expression;
            Assert.IsTrue(iql is IqlLambdaExpression);
            var body = (iql as IqlLambdaExpression).Body;
            Assert.IsTrue(body is IqlIsGreaterThanExpression);
            var gtExpression = body as IqlIsGreaterThanExpression;
            Assert.IsTrue(gtExpression.Left is IqlCountExpression);
        }

        [TestMethod]
        public async Task FilterCollectionJavaScriptString()
        {
            ConverterConfig.Init();
            var db = Db;
            var jsString =
                @"p => p.Title == `Test` || ((p.Types.filter(t => t.TypeId == 4 || t.Description == `Kettle`).length > 0))";
            //var iql = new JavaScriptExpressionConverter().ConvertJavaScriptStringToIql<Person>(
            //    jsString);
            //var iqlJson = IqlJsonSerializer.Serialize(iql.Expression).CompressJson();
            //Assert.AreEqual(@"{""Left"":{""Left"":{""PropertyName"":""Title"",""Type"":29,""ReturnType"":4,""Parent"":{""Value"":null,""VariableName"":""p"",""Type"":27,""ReturnType"":1,""Parent"":null}},""Right"":{""Value"":""Test"",""InferredReturnType"":4,""Type"":25,""ReturnType"":4,""Parent"":null},""Type"":9,""ReturnType"":1,""Parent"":null},""Right"":{""Left"":{""RootVariableName"":""t"",""Value"":{""Left"":{""Left"":{""PropertyName"":""TypeId"",""Type"":29,""ReturnType"":5,""Parent"":{""Value"":null,""VariableName"":""t"",""Type"":27,""ReturnType"":1,""Parent"":{""PropertyName"":""Types"",""Type"":29,""ReturnType"":2,""Parent"":{""Value"":null,""VariableName"":""p"",""Type"":27,""ReturnType"":1,""Parent"":null}}}},""Right"":{""Value"":4.0,""InferredReturnType"":6,""Type"":25,""ReturnType"":6,""Parent"":null},""Type"":9,""ReturnType"":1,""Parent"":null},""Right"":{""Left"":{""PropertyName"":""Description"",""Type"":29,""ReturnType"":4,""Parent"":{""Value"":null,""VariableName"":""t"",""Type"":27,""ReturnType"":1,""Parent"":{""PropertyName"":""Types"",""Type"":29,""ReturnType"":2,""Parent"":{""Value"":null,""VariableName"":""p"",""Type"":27,""ReturnType"":1,""Parent"":null}}}},""Right"":{""Value"":""Kettle"",""InferredReturnType"":4,""Type"":25,""ReturnType"":4,""Parent"":null},""Type"":9,""ReturnType"":1,""Parent"":null},""Type"":3,""ReturnType"":1,""Parent"":null},""Type"":45,""ReturnType"":5,""Parent"":{""PropertyName"":""Types"",""Type"":29,""ReturnType"":2,""Parent"":{""Value"":null,""VariableName"":""p"",""Type"":27,""ReturnType"":1,""Parent"":null}}},""Right"":{""Value"":0.0,""InferredReturnType"":6,""Type"":25,""ReturnType"":6,""Parent"":null},""Type"":4,""ReturnType"":1,""Parent"":null},""Type"":3,""ReturnType"":1,""Parent"":null}",
            //    iqlJson);
            //var xml = IqlSerializer.SerializeToXml(iql.Expression);
            var js1 = new JavaScriptExpressionConverter().ConvertJavaScriptStringToJavaScriptString<Person>(jsString, TypeResolver);

            //            var js = new JavaScriptExpressionConverter().ConvertLambdaToJavaScript<Person>(
            //                p => p.Title == "Test" || p.Types.Any(t => t.TypeId == 4 || t.Description == "Kettle")
            //#if TypeScript
            //            , null
            //#endif
            //                     );
            Assert.AreEqual(@"function(entity, context) { return (((((entity || {}).Title == null) ? null : ((entity || {}).Title || '').toUpperCase()) === 'TEST') || ((((entity || {}).Types || []).filter(function(entity2) { return (((entity2 || {}).TypeId === 4) || ((((entity2 || {}).Description == null) ? null : ((entity2 || {}).Description || '').toUpperCase()) === 'KETTLE')); }).length) > 0)); }",
                js1);
            //Assert.AreEqual(@"entity => (((((entity || {})[""Title""] == null) ? null : ((entity || {})[""Title""] || """").toUpperCase()) == 'TEST') || (((entity || {})[""Types""].filter(function(entity2) { return (((entity2 || {})[""TypeId""] == 4) || ((((entity2 || {})[""Description""] == null) ? null : ((entity2 || {})[""Description""] || """").toUpperCase()) == 'KETTLE')); }).length) > 0))",
            //    js);
        }

        [TestMethod]
        public void TestTypicalValidationExpression()
        {
            ConverterConfig.Init();
            var iql = IqlConverter.Instance
                .ConvertLambdaToIql<Person>(p => p.Type.CreatedByUser.Client.Name,
                    TypeResolver
#if TypeScript
            , null
#endif
                )
                .Expression;
            var js = new JavaScriptExpressionConverter().ConvertIqlToTypeScriptExpressionString(iql, TypeResolver);
            Assert.AreEqual(@"(entity, context) => ((((entity || {}).Type || {}).CreatedByUser || {}).Client || {}).Name", js);
        }

        [TestMethod]
        public void TestCompareStringExpression()
        {
            //ConverterConfig.Init();
            //var db = new AppDbContext();
            //var js = db.Users.Where(u => u.Id == "a").ToJavaScriptQuery();
        }

        [TestMethod]
        public void TestIsEqualToLiteralExpression()
        {
            ConverterConfig.Init();
            var db = new AppDbContext();
            var iqlExpression = new IqlIsEqualToExpression(
                IqlExpression.GetPropertyExpression(nameof(ApplicationUser.Id)),
                new IqlLiteralExpression("a"));
            var js = new JavaScriptExpressionConverter().ConvertIqlToExpressionStringByType(iqlExpression, TypeResolver, typeof(ApplicationUser));
            Assert.AreEqual(@"function(entity) { return ((entity || {}).Id === 'a'); }", js);
        }

#if !TypeScript
        // Only do these in .NET as we do not support 'string.IsNullOrWhitespace(..) in TypeScript'
        [TestMethod]
        public void TestModeratelyComplicatedValidationExpression()
        {
            ConverterConfig.Init();
            var iql = IqlConverter.Instance
               .ConvertLambdaToIql<Person>(p => string.IsNullOrWhiteSpace(p.CreatedByUserId) && p.CreatedByUser == null,
                   TypeResolver
#if TypeScript
                    , 
                    new EvaluateContext
                    {
                        Context = this,
                        Evaluate = n => (Func<object, object>)Evaluator.Eval(n)
                    }
#endif
                                                 )
               .Expression;
            var js = new JavaScriptExpressionConverter().ConvertIqlToTypeScriptExpressionString(iql, TypeResolver);
            Assert.AreEqual(@"(entity, context) => ((((entity || {}).CreatedByUserId == null) || (((entity || {}).CreatedByUserId || '').trim() === '')) && ((entity || {}).CreatedByUser == null))", js);
        }

        [TestMethod]
        public void StringIsNullOrWhiteSpaceExpression()
        {
            ConverterConfig.Init();
            var iql = IqlConverter.Instance
                .ConvertLambdaToIql<Person>(p => string.IsNullOrWhiteSpace(p.CreatedByUserId),
                    TypeResolver
#if TypeScript
                    , 
                    new EvaluateContext
                    {
                        Context = this,
                        Evaluate = n => (Func<object, object>)Evaluator.Eval(n)
                    }
#endif
                )
                .Expression;
            var js = new JavaScriptExpressionConverter().ConvertIqlToTypeScriptExpressionString(iql, TypeResolver);
            Assert.AreEqual(@"(entity, context) => (((entity || {}).CreatedByUserId == null) || (((entity || {}).CreatedByUserId || '').trim() === ''))", js);
        }
#endif

        [TestMethod]
        public void SimpleNullCheckExpression()
        {
            ConverterConfig.Init();
            var iql = IqlConverter.Instance
                .ConvertLambdaToIql<Person>(p => p.CreatedByUser == null,
                    TypeResolver
#if TypeScript
                    , null
#endif
                                                 )
                .Expression;
            var js = new JavaScriptExpressionConverter().ConvertIqlToTypeScriptExpressionString(iql, TypeResolver);
            Assert.AreEqual(@"(entity, context) => ((entity || {}).CreatedByUser == null)", js);
        }
    }
}