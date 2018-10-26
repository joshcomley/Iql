#if TypeScript
using System;
using Iql.JavaScript.JavaScriptExpressionToIql;
using Iql.Parsing;
#endif
using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Entities.Rules.Relationship;
using Iql.Entities.SpecialTypes;
using Iql.JavaScript.Extensions;
#if !TypeScript
#endif
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
        [TestMethod]
        public void TestConditionalExpression()
        {
            var converter = new JavaScriptExpressionConverter();
            var expression = converter.ConvertJavaScriptStringToIql<ApplicationUser>(@"function (c) { return 1 > 2 ? 3 : 4; }");
            var lambda = expression.Expression as IqlLambdaExpression;
            Assert.IsTrue(lambda.Body is IqlConditionExpression);
            var javascript =
                converter.ConvertIqlToExpressionStringByType(expression.Expression, typeof(ApplicationUser));
            Assert.AreEqual(@"function(entity) { return ((1 > 2)?3:4); }",
                javascript);
        }

        [TestMethod]
        public void TestLambdaGeneratingLambda()
        {
            var instance = RelationshipFilterContextExpressions.Get();
            var converter = new JavaScriptExpressionConverter();
            var expression = converter.ConvertIqlToExpressionString(instance);
            Assert.AreEqual(@"function(entity) { return function(entity2) { return (entity2.ClientId == entity.Owner.ClientId); }; }",
                expression);
        }

        [TestMethod]
        public void TestLambda()
        {
            var converter = new JavaScriptExpressionConverter();
            var expression = converter.ConvertJavaScriptStringToIql<ApplicationUser>(@"function(entity2) { return (entity2.ClientId == 1); }");
            var javascript =
                converter.ConvertIqlToExpressionStringByType(expression.Expression, typeof(ApplicationUser));
            Assert.AreEqual(@"function(entity) { return ((entity || {})[""ClientId""] == 1); }",
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
                converter.ConvertIqlToExpressionStringAs<MyCustomReport>(expression);
            Assert.AreEqual(@"function(context) { return context.Where(function(entity) { return ((entity || {})[""MyId""] == '9cac910f-6b7c-46b8-9de6-d4373a0063d8'); }); }",
                javascript);
        }

        [TestMethod]
        public async Task TestConvertPropertyExpression()
        {
            var javaScriptExpressionConverter = new JavaScriptExpressionConverter();
            var result = javaScriptExpressionConverter.ConvertJavaScriptStringToIql<ApplicationUser>(
                $"p => p.{nameof(ApplicationUser.Id)}");
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
                typeof(Client));

#if !TypeScript
            var expected = @"function(context) { return context.Where(function(entity) { return ((((entity || {})[""Name""] == null) ? null : ((entity || {})[""Name""] || """").toUpperCase()) == 'A'); }, JSON.parse('{""Body"":{""Left"":{""PropertyName"":""Name"",""IsIqlExpression"":true,""Key"":null,""Kind"":30,""ReturnType"":1,""Parent"":{""EntityTypeName"":""Client"",""VariableName"":""c"",""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Key"":null,""Kind"":28,""ReturnType"":1,""Parent"":null}},""Right"":{""Value"":""a"",""InferredReturnType"":4,""IsIqlExpression"":true,""Key"":null,""Kind"":26,""ReturnType"":4,""Parent"":null},""IsIqlExpression"":true,""Key"":null,""Kind"":10,""ReturnType"":1,""Parent"":null},""Parameters"":[{""EntityTypeName"":""Client"",""VariableName"":""c"",""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Key"":null,""Kind"":28,""ReturnType"":1,""Parent"":null}],""IsIqlExpression"":true,""Key"":null,""Kind"":55,""ReturnType"":1,""Parent"":null}'))
		.Expand(JSON.parse('{""NavigationProperty"":{""PropertyName"":""Type"",""IsIqlExpression"":true,""Key"":null,""Kind"":30,""ReturnType"":1,""Parent"":{""EntityTypeName"":""Client"",""VariableName"":""c"",""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Key"":null,""Kind"":28,""ReturnType"":1,""Parent"":null}},""Query"":{""DataSet"":{""Name"":""ClientTypes"",""IsIqlExpression"":true,""Key"":null,""Kind"":50,""ReturnType"":2,""Parent"":null},""OrderBys"":null,""IncludeCount"":null,""Skip"":null,""Take"":null,""Expands"":null,""Filter"":null,""WithKey"":null,""Parameters"":[{""EntityTypeName"":""ClientType"",""VariableName"":null,""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Key"":null,""Kind"":28,""ReturnType"":1,""Parent"":null}],""IsIqlExpression"":true,""Key"":null,""Kind"":49,""ReturnType"":3,""Parent"":null},""Count"":false,""IsIqlExpression"":true,""Key"":null,""Kind"":48,""ReturnType"":2,""Parent"":null}'))
		.Expand(JSON.parse('{""NavigationProperty"":{""PropertyName"":""Sites"",""IsIqlExpression"":true,""Key"":null,""Kind"":30,""ReturnType"":1,""Parent"":{""EntityTypeName"":""Client"",""VariableName"":""c"",""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Key"":null,""Kind"":28,""ReturnType"":1,""Parent"":null}},""Query"":{""DataSet"":{""Name"":""Sites"",""IsIqlExpression"":true,""Key"":null,""Kind"":50,""ReturnType"":2,""Parent"":null},""OrderBys"":null,""IncludeCount"":null,""Skip"":null,""Take"":null,""Expands"":[{""NavigationProperty"":{""PropertyName"":""CreatedByUser"",""IsIqlExpression"":true,""Key"":null,""Kind"":30,""ReturnType"":1,""Parent"":{""EntityTypeName"":""Site"",""VariableName"":""s"",""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Key"":null,""Kind"":28,""ReturnType"":1,""Parent"":null}},""Query"":{""DataSet"":{""Name"":""Users"",""IsIqlExpression"":true,""Key"":null,""Kind"":50,""ReturnType"":2,""Parent"":null},""OrderBys"":null,""IncludeCount"":null,""Skip"":null,""Take"":null,""Expands"":null,""Filter"":null,""WithKey"":null,""Parameters"":[{""EntityTypeName"":""ApplicationUser"",""VariableName"":null,""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Key"":null,""Kind"":28,""ReturnType"":1,""Parent"":null}],""IsIqlExpression"":true,""Key"":null,""Kind"":49,""ReturnType"":3,""Parent"":null},""Count"":false,""IsIqlExpression"":true,""Key"":null,""Kind"":48,""ReturnType"":2,""Parent"":null}],""Filter"":null,""WithKey"":null,""Parameters"":[{""EntityTypeName"":""Site"",""VariableName"":null,""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Key"":null,""Kind"":28,""ReturnType"":1,""Parent"":null}],""IsIqlExpression"":true,""Key"":null,""Kind"":49,""ReturnType"":3,""Parent"":null},""Count"":false,""IsIqlExpression"":true,""Key"":null,""Kind"":48,""ReturnType"":2,""Parent"":null}'))
		.OrderBy(function(entity) { return (entity || {})[""AverageSales""]; }, true); }";
#else
            var expected = @"function(context) { return context.Where(function(entity) { return ((((entity || {})[""Name""] == null) ? null : ((entity || {})[""Name""] || """").toUpperCase()) == 'A'); }, JSON.parse('{""Body"":{""Left"":{""PropertyName"":""Name"",""IsIqlExpression"":true,""Kind"":30,""ReturnType"":4,""Parent"":{""EntityTypeName"":""Client"",""VariableName"":""c"",""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}},""Right"":{""Value"":""a"",""InferredReturnType"":4,""IsIqlExpression"":true,""Kind"":26,""ReturnType"":4,""Parent"":null},""IsIqlExpression"":true,""Kind"":10,""ReturnType"":1,""Parent"":null},""Parameters"":[{""EntityTypeName"":null,""VariableName"":""c"",""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}],""IsIqlExpression"":true,""Kind"":55,""ReturnType"":1,""Parent"":null}'))
		.Expand(JSON.parse('{""NavigationProperty"":{""PropertyName"":""Type"",""IsIqlExpression"":true,""Kind"":30,""ReturnType"":1,""Parent"":{""EntityTypeName"":""Client"",""VariableName"":""c"",""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}},""Query"":{""DataSet"":{""Name"":""ClientTypes"",""IsIqlExpression"":true,""Kind"":50,""ReturnType"":2,""Parent"":null},""OrderBys"":null,""Expands"":null,""Filter"":null,""WithKey"":null,""Parameters"":[{""EntityTypeName"":""ClientType"",""VariableName"":null,""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}],""IsIqlExpression"":true,""Kind"":49,""ReturnType"":3,""Parent"":null},""Count"":false,""IsIqlExpression"":true,""Kind"":48,""ReturnType"":2,""Parent"":null}'))
		.Expand(JSON.parse('{""NavigationProperty"":{""PropertyName"":""Sites"",""IsIqlExpression"":true,""Kind"":30,""ReturnType"":1,""Parent"":{""EntityTypeName"":""Client"",""VariableName"":""c"",""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}},""Query"":{""DataSet"":{""Name"":""Sites"",""IsIqlExpression"":true,""Kind"":50,""ReturnType"":2,""Parent"":null},""OrderBys"":null,""Expands"":[{""NavigationProperty"":{""PropertyName"":""CreatedByUser"",""IsIqlExpression"":true,""Kind"":30,""ReturnType"":1,""Parent"":{""EntityTypeName"":""Site"",""VariableName"":""s"",""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}},""Query"":{""DataSet"":{""Name"":""Users"",""IsIqlExpression"":true,""Kind"":50,""ReturnType"":2,""Parent"":null},""OrderBys"":null,""Expands"":null,""Filter"":null,""WithKey"":null,""Parameters"":[{""EntityTypeName"":""ApplicationUser"",""VariableName"":null,""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}],""IsIqlExpression"":true,""Kind"":49,""ReturnType"":3,""Parent"":null},""Count"":false,""IsIqlExpression"":true,""Kind"":48,""ReturnType"":2,""Parent"":null}],""Filter"":null,""WithKey"":null,""Parameters"":[{""EntityTypeName"":""Site"",""VariableName"":null,""Value"":null,""InferredReturnType"":1,""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}],""IsIqlExpression"":true,""Kind"":49,""ReturnType"":3,""Parent"":null},""Count"":false,""IsIqlExpression"":true,""Kind"":48,""ReturnType"":2,""Parent"":null}'))
		.OrderBy(function(entity) { return (entity || {})[""AverageSales""]; }, true); }";
#endif
            var cleaned = expected.Clean();
            Assert.AreEqual(cleaned, javaScript);
            IqlJavaScriptIqlExpressionExtensions.NormalizeJson = false;
        }

        [TestMethod]
        public void TestFilterOnChildCollectionToOData()
        {
            var iql = new JavaScriptExpressionConverter().ConvertJavaScriptStringToIql<Client>(
                "function(t) { return t.Sites.filter(s => s.AdditionalSendReportsTo.length > 22).length > 3; }");
            var odataFilter = new ODataExpressionConverter().ConvertIqlToExpressionStringAs<Client>(iql.Expression);
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
                typeof(Client));
            Assert.AreEqual(
                @"function(context) { return context.Where(function(entity) { return ((entity || {})[""Id""] == 7); }); }",
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
                typeof(Client));
            Assert.AreEqual(
                @"function(context) { return context.OrderBy(function(entity) { return (entity || {})[""Name""]; }, false); }",
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
            var js = new JavaScriptExpressionConverter().ConvertIqlToTypeScriptExpressionString(iql);
            Assert.AreEqual(@"entity => ((entity || {})[""Piano""] || {})[""NumberOfKeys""]", js);
        }

        [TestMethod]
        public void TestMinifiedLambda()
        {
            var iql = new JavaScriptExpressionConverter().ConvertJavaScriptStringToIql<ApplicationUser>(
                "function(t) { return 1 == t.IsLockedOut }");
            var odataFilter = new ODataExpressionConverter().ConvertIqlToExpressionStringAs<ApplicationUser>(iql.Expression
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
                "function(t) { return t.IsLockedOut === true }");
            var odataFilter = new ODataExpressionConverter().ConvertIqlToExpressionStringAs<ApplicationUser>(iql.Expression);
            Assert.AreEqual(@"($it/IsLockedOut eq true)", odataFilter);
        }

        [TestMethod]
        public void TestUnaryNot()
        {
            var iql = new JavaScriptExpressionConverter().ConvertJavaScriptStringToIql<ApplicationUser>(
                "function(t) { return !t.IsLockedOut }");
            var odataFilter = new ODataExpressionConverter().ConvertIqlToExpressionStringAs<ApplicationUser>(iql.Expression);
            Assert.AreEqual(@"($it/IsLockedOut eq false)", odataFilter);
        }

        [TestMethod]
        public void TestUnarySequenceOfNots()
        {
            var iql = new JavaScriptExpressionConverter().ConvertJavaScriptStringToIql<ApplicationUser>(
                "function(t) { return !!!!!t.IsLockedOut }");
            var odataFilter = new ODataExpressionConverter().ConvertIqlToExpressionStringAs<ApplicationUser>(iql.Expression);
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
                "p => p.Title == `Test` || ((p.Types.filter(t => t.TypeId == 4 || t.Description == p.Title).length > 0))");
            Assert.AreEqual(
                @"function(entity) { return (((((entity || {})[""Title""] == null) ? null : ((entity || {})[""Title""] || """").toUpperCase()) == 'TEST') || (((entity || {})[""Types""].filter(function(entity2) { return (((entity2 || {})[""TypeId""] == 4) || ((((entity2 || {})[""Description""] == null) ? null : ((entity2 || {})[""Description""] || """").toUpperCase()) == (((entity || {})[""Title""] == null) ? null : ((entity || {})[""Title""] || """").toUpperCase()))); }).length) > 0)); }",
                js);
            //var iql3 = new DotNetExpressionConverter().ConvertLambdaToIql<Person>(p =>
            //    p.Title == "Test" || p.Types.Any(t => t.TypeId == 4 || t.Description == "Kettle"));
            // var xml = IqlSerializer.SerializeToXml(iql2.Expression);
        }

        [TestMethod]
        public void TestCompressedJavaScript()
        {
            var js = new JavaScriptExpressionConverter().ConvertJavaScriptStringToJavaScriptString<Client>(
                "function(n){return n.Sites.filter(function(n){return n.UsersCount>0}).length>0}");
            Assert.AreEqual(
                @"function(entity) { return (((entity || {})[""Sites""].filter(function(entity) { return ((entity || {})[""UsersCount""] > 0); }).length) > 0); }",
                js);
            //function(n){return n.CandidateResults.filter(function(n){return!n.Exam.IsTraining}).length>0}
        }

        [TestMethod]
        public void TestCompressedCompoundJavaScript()
        {
            var js = new JavaScriptExpressionConverter().ConvertJavaScriptStringToJavaScriptString<Client>(
                "function(n){return n.Sites.filter(function(n){return!n.CreatedByUser.EmailConfirmed}).length>0}");
            Assert.AreEqual(
                @"function(entity) { return (((entity || {})[""Sites""].filter(function(entity) { return (((entity || {})[""CreatedByUser""] || {})[""EmailConfirmed""] == false); }).length) > 0); }",
                js);
            //function(n){return n.CandidateResults.filter(function(n){return!n.Exam.IsTraining}).length>0}
        }

        [TestMethod]
        public void TestConvertingCompressedBooleanAsDigitExpression()
        {
            var iql = new JavaScriptExpressionConverter().ConvertJavaScriptStringToIql<Site>(
                "function(n){return 0==n.CreatedByUser.EmailConfirmed}");
            var js = new JavaScriptExpressionConverter().ConvertIqlToExpressionStringByType(iql.Expression, typeof(Client));
            Assert.AreEqual(@"function(entity) { return (false == ((entity || {})[""CreatedByUser""] || {})[""EmailConfirmed""]); }", js);
            var odataFilter = new ODataExpressionConverter().ConvertIqlToExpressionStringAs<Client>(iql.Expression);
            Assert.AreEqual(@"(false eq $it/CreatedByUser/EmailConfirmed)", odataFilter);
        }

        [TestMethod]
        public async Task FilterChildCollectionCount()
        {
            var db = Db;
            var js = @"c => c.Types.length > 2";
            var iql = new JavaScriptExpressionConverter().ConvertJavaScriptStringToIql<Person>(
                js).Expression;
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
            //var iqlJson = JsonConvert.SerializeObject(iql.Expression).CompressJson();
            //Assert.AreEqual(@"{""Left"":{""Left"":{""PropertyName"":""Title"",""Type"":29,""ReturnType"":4,""Parent"":{""Value"":null,""VariableName"":""p"",""Type"":27,""ReturnType"":1,""Parent"":null}},""Right"":{""Value"":""Test"",""InferredReturnType"":4,""Type"":25,""ReturnType"":4,""Parent"":null},""Type"":9,""ReturnType"":1,""Parent"":null},""Right"":{""Left"":{""RootVariableName"":""t"",""Value"":{""Left"":{""Left"":{""PropertyName"":""TypeId"",""Type"":29,""ReturnType"":5,""Parent"":{""Value"":null,""VariableName"":""t"",""Type"":27,""ReturnType"":1,""Parent"":{""PropertyName"":""Types"",""Type"":29,""ReturnType"":2,""Parent"":{""Value"":null,""VariableName"":""p"",""Type"":27,""ReturnType"":1,""Parent"":null}}}},""Right"":{""Value"":4.0,""InferredReturnType"":6,""Type"":25,""ReturnType"":6,""Parent"":null},""Type"":9,""ReturnType"":1,""Parent"":null},""Right"":{""Left"":{""PropertyName"":""Description"",""Type"":29,""ReturnType"":4,""Parent"":{""Value"":null,""VariableName"":""t"",""Type"":27,""ReturnType"":1,""Parent"":{""PropertyName"":""Types"",""Type"":29,""ReturnType"":2,""Parent"":{""Value"":null,""VariableName"":""p"",""Type"":27,""ReturnType"":1,""Parent"":null}}}},""Right"":{""Value"":""Kettle"",""InferredReturnType"":4,""Type"":25,""ReturnType"":4,""Parent"":null},""Type"":9,""ReturnType"":1,""Parent"":null},""Type"":3,""ReturnType"":1,""Parent"":null},""Type"":45,""ReturnType"":5,""Parent"":{""PropertyName"":""Types"",""Type"":29,""ReturnType"":2,""Parent"":{""Value"":null,""VariableName"":""p"",""Type"":27,""ReturnType"":1,""Parent"":null}}},""Right"":{""Value"":0.0,""InferredReturnType"":6,""Type"":25,""ReturnType"":6,""Parent"":null},""Type"":4,""ReturnType"":1,""Parent"":null},""Type"":3,""ReturnType"":1,""Parent"":null}",
            //    iqlJson);
            //var xml = IqlSerializer.SerializeToXml(iql.Expression);
            var js1 = new JavaScriptExpressionConverter().ConvertJavaScriptStringToJavaScriptString<Person>(
                jsString);

            //            var js = new JavaScriptExpressionConverter().ConvertLambdaToJavaScript<Person>(
            //                p => p.Title == "Test" || p.Types.Any(t => t.TypeId == 4 || t.Description == "Kettle")
            //#if TypeScript
            //            , null
            //#endif
            //                     );
            Assert.AreEqual(@"function(entity) { return (((((entity || {})[""Title""] == null) ? null : ((entity || {})[""Title""] || """").toUpperCase()) == 'TEST') || (((entity || {})[""Types""].filter(function(entity2) { return (((entity2 || {})[""TypeId""] == 4) || ((((entity2 || {})[""Description""] == null) ? null : ((entity2 || {})[""Description""] || """").toUpperCase()) == 'KETTLE')); }).length) > 0)); }",
                js1);
            //Assert.AreEqual(@"entity => (((((entity || {})[""Title""] == null) ? null : ((entity || {})[""Title""] || """").toUpperCase()) == 'TEST') || (((entity || {})[""Types""].filter(function(entity2) { return (((entity2 || {})[""TypeId""] == 4) || ((((entity2 || {})[""Description""] == null) ? null : ((entity2 || {})[""Description""] || """").toUpperCase()) == 'KETTLE')); }).length) > 0))",
            //    js);
        }

        [TestMethod]
        public void TestTypicalValidationExpression()
        {
            ConverterConfig.Init();
            var iql = IqlConverter.Instance
                .ConvertLambdaToIql<Person>(p => p.Type.CreatedByUser.Client.Name
#if TypeScript
            , null
#endif
                )
                .Expression;
            var js = new JavaScriptExpressionConverter().ConvertIqlToTypeScriptExpressionString(iql);
            Assert.AreEqual(@"entity => ((((entity || {})[""Type""] || {})[""CreatedByUser""] || {})[""Client""] || {})[""Name""]", js);
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
            var js = new JavaScriptExpressionConverter().ConvertIqlToExpressionStringByType(iqlExpression, typeof(ApplicationUser));
            Assert.AreEqual(@"function(entity) { return ((entity || {})[""Id""] == 'a'); }", js);
        }

#if !TypeScript
        // Only do these in .NET as we do not support 'string.IsNullOrWhitespace(..) in TypeScript'
        [TestMethod]
        public void TestModeratelyComplicatedValidationExpression()
        {
            ConverterConfig.Init();
            var iql = IqlConverter.Instance
               .ConvertLambdaToIql<Person>(p => string.IsNullOrWhiteSpace(p.CreatedByUserId) && p.CreatedByUser == null
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
            var js = new JavaScriptExpressionConverter().ConvertIqlToTypeScriptExpressionString(iql);
            Assert.AreEqual(@"entity => ((((entity || {})[""CreatedByUserId""] == null) || (((entity || {})[""CreatedByUserId""] || """").trim() == '')) && ((entity || {})[""CreatedByUser""] == null))", js);
        }

        [TestMethod]
        public void StringIsNullOrWhiteSpaceExpression()
        {
            ConverterConfig.Init();
            var iql = IqlConverter.Instance
                .ConvertLambdaToIql<Person>(p => string.IsNullOrWhiteSpace(p.CreatedByUserId)
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
            var js = new JavaScriptExpressionConverter().ConvertIqlToTypeScriptExpressionString(iql);
            Assert.AreEqual(@"entity => (((entity || {})[""CreatedByUserId""] == null) || (((entity || {})[""CreatedByUserId""] || """").trim() == ''))", js);
        }
#endif

        [TestMethod]
        public void SimpleNullCheckExpression()
        {
            ConverterConfig.Init();
            var iql = IqlConverter.Instance
                .ConvertLambdaToIql<Person>(p => p.CreatedByUser == null
#if TypeScript
                    , null
#endif
                                                 )
                .Expression;
            var js = new JavaScriptExpressionConverter().ConvertIqlToTypeScriptExpressionString(iql);
            Assert.AreEqual(@"entity => ((entity || {})[""CreatedByUser""] == null)", js);
        }
    }
}