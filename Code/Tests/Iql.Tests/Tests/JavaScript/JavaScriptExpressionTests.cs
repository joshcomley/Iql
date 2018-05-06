#if TypeScript
using System;
using Iql.JavaScript.JavaScriptExpressionToIql;
using Iql.Parsing;
#endif
using System.Threading.Tasks;
using Iql.JavaScript.Extensions;
#if !TypeScript
#endif
using Iql.JavaScript.JavaScriptExpressionToIql;
using Iql.OData;
using Iql.OData.Extensions;
using Iql.Queryable.Expressions;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests.JavaScript
{
    [TestClass]
    public class JavaScriptExpressionTests : TestsBase
    {
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
            var expected = @"function(context) { return context.Where(function(entity) { return ((((entity || {})[""Name""] == null) ? null : ((entity || {})[""Name""] || """").toUpperCase()) == 'A'); }, JSON.parse('{""Left"":{""PropertyName"":""Name"",""IsIqlExpression"":true,""Kind"":30,""ReturnType"":1,""Parent"":{""Value"":null,""VariableName"":""c"",""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}},""Right"":{""Value"":""a"",""InferredReturnType"":4,""IsIqlExpression"":true,""Kind"":26,""ReturnType"":4,""Parent"":null},""IsIqlExpression"":true,""Kind"":10,""ReturnType"":1,""Parent"":null}'))
		.Expand(JSON.parse('{""NavigationProperty"":{""PropertyName"":""Type"",""IsIqlExpression"":true,""Kind"":30,""ReturnType"":1,""Parent"":{""Value"":null,""VariableName"":""c"",""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}},""Query"":{""DataSet"":{""Name"":""ClientTypes"",""IsIqlExpression"":true,""Kind"":50,""ReturnType"":2,""Parent"":null},""OrderBys"":null,""IncludeCount"":null,""Skip"":null,""Take"":null,""Expands"":null,""Filter"":null,""WithKey"":null,""IsIqlExpression"":true,""Kind"":49,""ReturnType"":3,""Parent"":null},""Count"":false,""IsIqlExpression"":true,""Kind"":48,""ReturnType"":2,""Parent"":null}'))
		.Expand(JSON.parse('{""NavigationProperty"":{""PropertyName"":""Sites"",""IsIqlExpression"":true,""Kind"":30,""ReturnType"":1,""Parent"":{""Value"":null,""VariableName"":""c"",""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}},""Query"":{""DataSet"":{""Name"":""Sites"",""IsIqlExpression"":true,""Kind"":50,""ReturnType"":2,""Parent"":null},""OrderBys"":null,""IncludeCount"":null,""Skip"":null,""Take"":null,""Expands"":[{""NavigationProperty"":{""PropertyName"":""CreatedByUser"",""IsIqlExpression"":true,""Kind"":30,""ReturnType"":1,""Parent"":{""Value"":null,""VariableName"":""s"",""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}},""Query"":{""DataSet"":{""Name"":""Users"",""IsIqlExpression"":true,""Kind"":50,""ReturnType"":2,""Parent"":null},""OrderBys"":null,""IncludeCount"":null,""Skip"":null,""Take"":null,""Expands"":null,""Filter"":null,""WithKey"":null,""IsIqlExpression"":true,""Kind"":49,""ReturnType"":3,""Parent"":null},""Count"":false,""IsIqlExpression"":true,""Kind"":48,""ReturnType"":2,""Parent"":null}],""Filter"":null,""WithKey"":null,""IsIqlExpression"":true,""Kind"":49,""ReturnType"":3,""Parent"":null},""Count"":false,""IsIqlExpression"":true,""Kind"":48,""ReturnType"":2,""Parent"":null}'))
		.OrderBy(function(entity) { return (entity || {})[""AverageSales""]; }, true); }";
#else
            var expected = @"function(context) { return context.Where(function(entity) { return ((((entity || {})[""Name""] == null) ? null : ((entity || {})[""Name""] || """").toUpperCase()) == 'A'); }, JSON.parse('{""Left"":{""PropertyName"":""Name"",""IsIqlExpression"":true,""Kind"":30,""ReturnType"":4,""Parent"":{""Value"":null,""VariableName"":""c"",""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}},""Right"":{""Value"":""a"",""InferredReturnType"":4,""IsIqlExpression"":true,""Kind"":26,""ReturnType"":4,""Parent"":null},""IsIqlExpression"":true,""Kind"":10,""ReturnType"":1,""Parent"":null}'))
		.Expand(JSON.parse('{""NavigationProperty"":{""PropertyName"":""Type"",""IsIqlExpression"":true,""Kind"":30,""ReturnType"":1,""Parent"":{""Value"":null,""VariableName"":""c"",""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}},""Query"":{""DataSet"":{""Name"":""ClientTypes"",""IsIqlExpression"":true,""Kind"":50,""ReturnType"":2,""Parent"":null},""Filter"":null,""WithKey"":null,""IsIqlExpression"":true,""Kind"":49,""ReturnType"":3,""Parent"":null},""Count"":false,""IsIqlExpression"":true,""Kind"":48,""ReturnType"":2,""Parent"":null}'))
		.Expand(JSON.parse('{""NavigationProperty"":{""PropertyName"":""Sites"",""IsIqlExpression"":true,""Kind"":30,""ReturnType"":1,""Parent"":{""Value"":null,""VariableName"":""c"",""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}},""Query"":{""DataSet"":{""Name"":""Sites"",""IsIqlExpression"":true,""Kind"":50,""ReturnType"":2,""Parent"":null},""Expands"":[{""NavigationProperty"":{""PropertyName"":""CreatedByUser"",""IsIqlExpression"":true,""Kind"":30,""ReturnType"":1,""Parent"":{""Value"":null,""VariableName"":""s"",""IsIqlExpression"":true,""Kind"":28,""ReturnType"":1,""Parent"":null}},""Query"":{""DataSet"":{""Name"":""Users"",""IsIqlExpression"":true,""Kind"":50,""ReturnType"":2,""Parent"":null},""Filter"":null,""WithKey"":null,""IsIqlExpression"":true,""Kind"":49,""ReturnType"":3,""Parent"":null},""Count"":false,""IsIqlExpression"":true,""Kind"":48,""ReturnType"":2,""Parent"":null}],""Filter"":null,""WithKey"":null,""IsIqlExpression"":true,""Kind"":49,""ReturnType"":3,""Parent"":null},""Count"":false,""IsIqlExpression"":true,""Kind"":48,""ReturnType"":2,""Parent"":null}'))
		.OrderBy(function(entity) { return (entity || {})[""AverageSales""]; }, true); }";
#endif
            Assert.AreEqual(expected.Clean(), javaScript);
            IqlJavaScriptIqlExpressionExtensions.NormalizeJson = false;
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
            var odataFilter = new ODataExpressionConverter().ConvertIqlToExpressionString<ApplicationUser>(iql.Expression
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
            var odataFilter = new ODataExpressionConverter().ConvertIqlToExpressionString<ApplicationUser>(iql.Expression);
            Assert.AreEqual(@"($it/IsLockedOut eq true)", odataFilter);
        }

        [TestMethod]
        public void TestUnaryNot()
        {
            var iql = new JavaScriptExpressionConverter().ConvertJavaScriptStringToIql<ApplicationUser>(
                "function(t) { return !t.IsLockedOut }");
            var odataFilter = new ODataExpressionConverter().ConvertIqlToExpressionString<ApplicationUser>(iql.Expression);
            Assert.AreEqual(@"($it/IsLockedOut eq false)", odataFilter);
        }

        [TestMethod]
        public void TestUnarySequenceOfNots()
        {
            var iql = new JavaScriptExpressionConverter().ConvertJavaScriptStringToIql<ApplicationUser>(
                "function(t) { return !!!!!t.IsLockedOut }");
            var odataFilter = new ODataExpressionConverter().ConvertIqlToExpressionString<ApplicationUser>(iql.Expression);
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
        public async Task FilterChildCollectionCount()
        {
            var db = Db;
            var js = @"c => c.Types.length > 2";
            var iql = new JavaScriptExpressionConverter().ConvertJavaScriptStringToIql<Person>(
                js).Expression;
            Assert.IsTrue(iql is IqlIsGreaterThanExpression);
            Assert.IsTrue((iql as IqlIsGreaterThanExpression).Left is IqlCountExpression);
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