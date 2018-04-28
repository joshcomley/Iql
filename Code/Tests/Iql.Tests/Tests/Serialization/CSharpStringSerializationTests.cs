#if !TypeScript
using System;
using System.Linq.Expressions;
using Iql.DotNet;
using Iql.DotNet.Serialization;
using Iql.Queryable;
using Iql.Queryable.Data.DataStores.InMemory.QueryApplicator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests.Serialization
{
    [TestClass]
    public class CSharpStringSerializationTests
    {
        [TestMethod]
        public void TestConvertToCSharp()
        {
            AssertCode(
                s => s.Name != null && s.Name.Substring(2, 3) == "hi",
                @"entity => (((entity.Name == null ? null : entity.Name.ToUpper()) != null) && ((entity.Name.Substring(2, 3) == null ? null : entity.Name.Substring(2, 3).ToUpper()) == (""hi"" == null ? null : ""hi"".ToUpper())))");
        }

        private static void AssertCode(Expression<Func<Client, bool>> expression, string expected)
        {
            IqlQueryableAdapter.ExpressionConverter = () => new DotNetExpressionConverter();
            var xml = IqlSerializer.SerializeToXml(
                expression);
            var iqlExpression = IqlSerializer.DeserializeFromXml(xml);
            var code = IqlQueryableAdapter.ExpressionConverter().ConvertIqlToExpressionString<Client>(iqlExpression);
            Assert.AreEqual(
                expected,
                code
            );
        }
    }
}
#endif