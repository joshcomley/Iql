using System;
using Iql.OData.IqlToODataExpression.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests.OData
{
    public class MyObjRecursive
    {
        public int Age { get; set; }
        public MyObjRecursive Deep { get; set; }
    }

    [TestClass]
    public class ODataLiteralParserTests
    {
        [TestMethod]
        public void ParseObject()
        {
            var o = new
            {
                Name = "Mr",
                Age = 99,
                Date = new DateTime(1999, 9, 9, 1, 2, 3)
            };
            var result = ODataLiteralParser.ODataEncode(o);
#if TypeScript
            Assert.AreEqual("{Name:'Mr',Age:99,Date:'1999-09-09T01:02:03.000Z'}", result);
#else
            Assert.AreEqual("{Name:'Mr',Age:99,Date:'1999-09-09T01:02:03Z'}", result);
#endif
        }

        [TestMethod]
        public void ParseEmptyObject()
        {
            var o = new
            {
            };
            var result = ODataLiteralParser.ODataEncode(o);
            Assert.AreEqual("{}", result);
        }

#if !TypeScript
        [TestMethod]
        public void ParseRecursiveObject()
        {
            var o = new MyObjRecursive
            {
                Age = 99,
            };
            o.Deep = o;
            var result = ODataLiteralParser.ODataEncode(o);
            Assert.AreEqual("{Age:99}", result);
        }
#endif
    }
}