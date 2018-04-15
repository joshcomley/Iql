#if TypeScript
using System;
using Iql.JavaScript.JavaScriptExpressionToIql;
using Iql.Parsing;
#endif
using System.Linq;
using System.Threading.Tasks;
#if !TypeScript
using Iql.DotNet.Serialization;
#endif
using Iql.JavaScript.JavaScriptExpressionToIql;
using Iql.JavaScript.QueryableApplicator;
using Iql.OData.Json;
using Iql.Queryable.Data.DataStores.InMemory.QueryApplicator;
using Iql.Tests.Context;
using Iql.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests.JavaScript
{
    [TestClass]
    public class JavaScriptExpressionTests : TestsBase
    {
        [TestMethod]
        public void TestCompileExpressionToJavaScript()
        {
            var iql =
                    new IqlPropertyExpression
                    {
                        PropertyName = "NumberOfKeys",
                        Type = IqlExpressionType.Property,
                        ReturnType = IqlType.Unknown,
                        Parent = new IqlPropertyExpression
                        {
                            PropertyName = "Piano",
                            Type = IqlExpressionType.Property,
                            ReturnType = IqlType.Unknown,
                            Parent = new IqlRootReferenceExpression
                            {
                                VariableName = "p",
                                Type = IqlExpressionType.RootReference,
                                ReturnType = IqlType.Unknown
                            }
                        }
                    }
                ;
            var js = new JavaScriptExpressionConverter().ConvertIqlToTypeScriptExpressionString(iql);
            Assert.AreEqual(@"entity => ((entity || {})[""Piano""] || {})[""NumberOfKeys""]", js);
        }

        [TestMethod]
        public void TestNestedLambdas()
        {
            //var iql1 = new JavaScriptExpressionConverter().ConvertJavaScriptStringToIql<Person>(
            //    "p => p.Title == `Test` || p.Title == `Test2`");
            //var iql2 = new JavaScriptExpressionConverter().ConvertJavaScriptStringToIql<Person>(
            //    "p => p.Title == `Test` || ((p.Types.filter(t => t.TypeId == 4 || t.Description == `Kettle`).length > 0))");
            var configurationContext = Db.EntityConfigurationContext;
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
            var iql = IqlQueryableAdapter.ExpressionConverter()
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
            ConverterConfig.Init();
            var db = new AppDbContext();
            var js = db.Users.Where(u => u.Id == "a").ToJavaScriptQuery();
        }

        [TestMethod]
        public void TestIsEqualToLiteralExpression()
        {
            ConverterConfig.Init();
            var db = new AppDbContext();
            var iqlExpression = new IqlIsEqualToExpression(
                IqlExpression.GetPropertyExpression(nameof(ApplicationUser.Id)), 
                new IqlLiteralExpression("a"));
            var js = db.GetJavaScriptWhereQuery(iqlExpression);
            Assert.AreEqual(js.Expression, @"((entity || {})[""Id""] == 'a')");
        }

#if !TypeScript
        // Only do these in .NET as we do not support 'string.IsNullOrWhitespace(..) in TypeScript'
        [TestMethod]
        public void TestModeratelyComplicatedValidationExpression()
        {
            ConverterConfig.Init();
            var iql = IqlQueryableAdapter.ExpressionConverter()
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
            var iql = IqlQueryableAdapter.ExpressionConverter()
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
            var iql = IqlQueryableAdapter.ExpressionConverter()
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