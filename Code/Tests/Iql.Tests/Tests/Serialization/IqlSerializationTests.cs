#if !TypeScript
using System;
using System.Linq.Expressions;
using Iql.DotNet;
using Iql.DotNet.Extensions;
using Iql.DotNet.Serialization;
using Iql.Queryable;
using Iql.Queryable.Data.DataStores.InMemory.QueryApplicator;
using Iql.Queryable.Expressions;
using Iql.Queryable.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests.Serialization
{
    [TestClass]
    public class IqlSerializationTests
    {
        [TestMethod]
        public void TestSerializeSinglePropertyToXml()
        {
            var xml = IqlSerializer.SerializeToXml<Client, string>(client => client.Name);
            Assert.AreEqual(@"<?xml version=""1.0"" encoding=""utf-16""?>
<IqlPropertyExpression xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Type>Property</Type>
  <ReturnType>Unknown</ReturnType>
  <Parent xsi:type=""IqlRootReferenceExpression"">
    <Type>RootReference</Type>
    <ReturnType>Unknown</ReturnType>
    <VariableName>client</VariableName>
  </Parent>
  <PropertyName>Name</PropertyName>
</IqlPropertyExpression>", xml);
        }

        [TestMethod]
        public void TestSerializePropertyStringConcatenationToXml()
        {
            var xml = IqlSerializer.SerializeToXml<Client, string>(client => client.Name + " (" + client.Description + ")");
            Assert.AreEqual(@"<?xml version=""1.0"" encoding=""utf-16""?>
<IqlAddExpression xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Type>Add</Type>
  <ReturnType>Unknown</ReturnType>
  <Left xsi:type=""IqlAddExpression"">
    <Type>Add</Type>
    <ReturnType>Unknown</ReturnType>
    <Left xsi:type=""IqlAddExpression"">
      <Type>Add</Type>
      <ReturnType>Unknown</ReturnType>
      <Left xsi:type=""IqlPropertyExpression"">
        <Type>Property</Type>
        <ReturnType>Unknown</ReturnType>
        <Parent xsi:type=""IqlRootReferenceExpression"">
          <Type>RootReference</Type>
          <ReturnType>Unknown</ReturnType>
          <VariableName>client</VariableName>
        </Parent>
        <PropertyName>Name</PropertyName>
      </Left>
      <Right xsi:type=""IqlLiteralExpression"">
        <Type>Literal</Type>
        <ReturnType>String</ReturnType>
        <Value xsi:type=""xsd:string""> (</Value>
        <InferredReturnType>String</InferredReturnType>
      </Right>
    </Left>
    <Right xsi:type=""IqlPropertyExpression"">
      <Type>Property</Type>
      <ReturnType>Unknown</ReturnType>
      <Parent xsi:type=""IqlRootReferenceExpression"">
        <Type>RootReference</Type>
        <ReturnType>Unknown</ReturnType>
        <VariableName>client</VariableName>
      </Parent>
      <PropertyName>Description</PropertyName>
    </Right>
  </Left>
  <Right xsi:type=""IqlLiteralExpression"">
    <Type>Literal</Type>
    <ReturnType>String</ReturnType>
    <Value xsi:type=""xsd:string"">)</Value>
    <InferredReturnType>String</InferredReturnType>
  </Right>
</IqlAddExpression>", xml);
        }

        [TestMethod]
        public void TestDeserializeStringConcatenationFromXmlAndApply()
        {
            IqlQueryableAdapter.ExpressionConverter = () => new DotNetExpressionConverter();
            var xml = IqlSerializer.SerializeToXml<Client, string>(c => c.Name + " (" + c.Description + ")" + " - " + c.Id);
            var expression = IqlSerializer.DeserializeFromXml(xml);
            var query = IqlQueryableAdapter.ExpressionConverter().ConvertIqlToFunction<Client, string>(expression);

            var client = new Client();
            client.Name = "Brandless";
            client.Description = "The best company";
            client.Id = 12;
            var compiledQuery = query.Compile();
            var result = compiledQuery.Invoke(client);
            Assert.AreEqual($"{client.Name} ({client.Description}) - {client.Id}", result);
        }

        [TestMethod]
        public void TestDeserializeComplexExpression()
        {
            AssertCode(
                c => !(c.Name != "Josh"),
                @"entity => !((((entity.Name == null) ? entity.Name : entity.Name.ToUpper()) != ""JOSH""))");
        }

        [TestMethod]
        public void TestDeserializeStringTrimExpression()
        {
            AssertCode(
                c => !string.IsNullOrWhiteSpace(c.Name) || !string.IsNullOrWhiteSpace(c.Description),
                @"entity => (!(((((entity.Name == null) ? entity.Name : entity.Name.ToUpper()) == null) || (((entity.Name.Trim() == null) ? entity.Name.Trim() : entity.Name.Trim().ToUpper()) == """"))) || !(((((entity.Description == null) ? entity.Description : entity.Description.ToUpper()) == null) || (((entity.Description.Trim() == null) ? entity.Description.Trim() : entity.Description.Trim().ToUpper()) == """"))))");
        }

        [TestMethod]
        public void TestDeserializeStringLengthExpression()
        {
            AssertCode(
                s => s.Name != null && s.Name.Length > 50,
                @"entity => ((((entity.Name == null) ? entity.Name : entity.Name.ToUpper()) != null) && (entity.Name.Length > 50))");
        }

        [TestMethod]
        public void TestDeserializeParenthesisExpression()
        {
            AssertCode(
                s => (s.Name == null || s.Name.Length > 50) && (s.Name == "Jim") ,
                @"entity => (((((entity.Name == null) ? entity.Name : entity.Name.ToUpper()) == null) || (entity.Name.Length > 50)) && (((entity.Name == null) ? entity.Name : entity.Name.ToUpper()) == ""JIM""))");
        }


        [TestMethod]
        public void TestDeserializeStringSubStringWithoutTakeExpression()
        {
            //
            AssertCode(
                s => s.Name != null && s.Name.Substring(2) == "hi",
                @"entity => ((((entity.Name == null) ? entity.Name : entity.Name.ToUpper()) != null) && (((entity.Name.Substring(2) == null) ? entity.Name.Substring(2) : entity.Name.Substring(2).ToUpper()) == ""HI""))"
            );
        }

        [TestMethod]
        public void TestDeserializeStringSubStringWithTakeExpression()
        {
            //
            AssertCode(
                s => s.Name != null && s.Name.Substring(2, 3) == "hi",
                @"entity => ((((entity.Name == null) ? entity.Name : entity.Name.ToUpper()) != null) && (((entity.Name.Substring(2, 3) == null) ? entity.Name.Substring(2, 3) : entity.Name.Substring(2, 3).ToUpper()) == ""HI""))"
            );
        }

        private static void AssertCode(Expression<Func<Client, bool>> expression, string expected)
        {
            IqlQueryableAdapter.ExpressionConverter = () => new DotNetExpressionConverter();
            var xml = IqlSerializer.SerializeToXml(
                expression);
            var iqlExpression = IqlSerializer.DeserializeFromXml(xml);
            var query = IqlQueryableAdapter.ExpressionConverter().ConvertIqlToFunction<Client, bool>(iqlExpression);
            var code = query.ToCSharpString();
            Assert.AreEqual(
                expected,
                code);
        }
    }
}
#endif
