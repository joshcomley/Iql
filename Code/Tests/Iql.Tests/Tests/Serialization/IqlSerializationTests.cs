using Iql.DotNet;
using Iql.DotNet.IqlToDotNet;
using Iql.DotNet.Serialization;
using Iql.Queryable;
using Iql.Queryable.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests.Serialization
{
#if !TypeScript
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
  <ReturnType>Void</ReturnType>
  <Left xsi:type=""IqlAddExpression"">
    <Type>Add</Type>
    <ReturnType>Void</ReturnType>
    <Left xsi:type=""IqlAddExpression"">
      <Type>Add</Type>
      <ReturnType>Void</ReturnType>
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
  </Right>
</IqlAddExpression>", xml);
        }

        [TestMethod]
        public void TestDeserializeStringConcatenationFromXmlAndApply()
        {
            IqlQueryableAdapter.ExpressionConverter = () => new DotNetExpressionConverter();
            var xml = IqlSerializer.SerializeToXml<Client, string>(c => c.Name + " (" + c.Description + ")");
            var expression = IqlSerializer.DeserializeFromXml(xml);
            var query = IqlQueryableAdapter.ExpressionConverter().ConvertIqlToFunction<Client, string>(expression);

            var client = new Client();
            client.Name = "Brandless";
            client.Description = "The best company";
            var compiledQuery = query.Compile();
            var result = compiledQuery.Invoke(client);
            Assert.AreEqual($"{client.Name} ({client.Description})", result);
        }
    }
#endif
}