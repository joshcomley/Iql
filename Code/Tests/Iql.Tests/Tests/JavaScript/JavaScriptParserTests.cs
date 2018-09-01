﻿using System.Threading.Tasks;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests.JavaScript
{
    [TestClass]
    public class JavaScriptParserTests
    {
        [TestMethod]
        public async Task ConditionalExpression()
        {
            var parser = new JavaScriptExpressionStringToExpressionTreeParser(
                "function (c) { return 1 > 2 ? 3 : 4; }");
            var parsed = parser.Parse();
            Assert.IsTrue(parsed is ConditionalJavaScriptExpressionNode);
        }

        [TestMethod]
        public async Task ExpressionWithMultiplyEs6()
        {
            var parser = new JavaScriptExpressionStringToExpressionTreeParser(
                "function (c) { return (((c.Types).filter(t => (t.Description.indexOf((`TEST`))) != -1).length)) > c.Types.length * 0.5; }");
            var parsed = parser.Parse();
        }

        [TestMethod]
        public async Task ExpressionWithMultiplyEs5()
        {
            var parser = new JavaScriptExpressionStringToExpressionTreeParser(
                "function (c) { return (((c.Types).filter(function (t) { return (t.Description.indexOf((`TEST`))) != -1 }).length)) > c.Types.length * 0.5; }");
            var parsed = parser.Parse();
        }
    }
}