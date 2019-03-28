#if !TypeScript
using System;
using System.Linq.Expressions;
using Iql.Conversion;
using Iql.DotNet;
using Iql.DotNet.Serialization;
using Iql.Queryable;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;

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
            var typeResolver = new AppDbContext().EntityConfigurationContext;
            IqlExpressionConversion.DefaultExpressionConverter = () => new DotNetExpressionConverter();
            var xml = IqlXmlSerializer.SerializeToXml(expression, typeResolver);
            var iqlExpression = IqlXmlSerializer.DeserializeFromXml(xml);
            var code = IqlConverter.Instance.ConvertIqlToExpressionStringAs<Client>(iqlExpression, typeResolver);
            Assert.AreEqual(
                expected,
                code
            );
        }
    }
}
#endif