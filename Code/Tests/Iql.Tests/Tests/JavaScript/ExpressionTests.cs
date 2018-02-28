using Iql.JavaScript.JavaScriptExpressionToIql.Expressions.JavaScript;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests.JavaScript
{
    [TestClass]
    public class ExpressionTests
    {
        [TestMethod]
        public void TestCompileDotNetExpressionToJavaScript()
        {
            var iql = new IqlPropertyExpression
                {
                    PropertyName = "Title",
                    Type = IqlExpressionType.Property,
                    ReturnType = IqlType.Unknown,
                    Parent = new IqlRootReferenceExpression
                    {
                        VariableName = "p",
                        Type = IqlExpressionType.RootReference,
                        ReturnType = IqlType.Unknown
                    }
                }
                ;
            var js = new JavaScriptExpressionConverter().ConvertIqlToTypeScriptExpressionString(iql);
            Assert.AreEqual(@"entity => (function() { return entity === null || entity === undefined ? null : entity.Title;})()", js);
        }
    }
}