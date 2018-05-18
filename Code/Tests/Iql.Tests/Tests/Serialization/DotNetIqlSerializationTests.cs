#if !TypeScript
using System;
using System.Linq;
using System.Linq.Expressions;
using Iql.DotNet;
using Iql.DotNet.Extensions;
using Iql.DotNet.Serialization;
using Iql.Queryable;
using Iql.Queryable.Data.EntityConfiguration.Rules.Relationship;
using Iql.Queryable.Expressions;
using Iql.Queryable.Extensions;
using Iql.Serialization;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests.Serialization
{
    [TestClass]
    public class DotNetIqlSerializationTests
    {
        [TestMethod]
        public void TestNestedLambda()
        {
            var db = new AppDbContext();
            Expression<Func<RelationshipFilterContext<Person>, Expression<Func<PersonLoading, bool>>>> filterExpression
                = context => loading => loading.Name == context.Owner.Title;
            var iqlXml = IqlXmlSerializer.SerializeToXml(filterExpression);
            var iql = IqlXmlSerializer.DeserializeFromXml(iqlXml);
            var exp = new DotNetExpressionConverter().ConvertIqlToExpression<RelationshipFilterContext<Person>>(iql);
            Assert.AreEqual(@"<?xml version=""1.0"" encoding=""utf-16""?>
<IqlLambdaExpression xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Kind>Lambda</Kind>
  <ReturnType>Unknown</ReturnType>
  <Parameters>
    <IqlRootReferenceExpression>
      <Kind>RootReference</Kind>
      <ReturnType>Unknown</ReturnType>
      <VariableName>context</VariableName>
    </IqlRootReferenceExpression>
  </Parameters>
  <Body xsi:type=""IqlLambdaExpression"">
    <Kind>Lambda</Kind>
    <ReturnType>Unknown</ReturnType>
    <Parameters>
      <IqlRootReferenceExpression>
        <Kind>RootReference</Kind>
        <ReturnType>Unknown</ReturnType>
        <VariableName>loading</VariableName>
      </IqlRootReferenceExpression>
    </Parameters>
    <Body xsi:type=""IqlIsEqualToExpression"">
      <Kind>IsEqualTo</Kind>
      <ReturnType>Unknown</ReturnType>
      <Left xsi:type=""IqlPropertyExpression"">
        <Kind>Property</Kind>
        <ReturnType>Unknown</ReturnType>
        <Parent xsi:type=""IqlRootReferenceExpression"">
          <Kind>RootReference</Kind>
          <ReturnType>Unknown</ReturnType>
          <VariableName>loading</VariableName>
        </Parent>
        <PropertyName>Name</PropertyName>
      </Left>
      <Right xsi:type=""IqlPropertyExpression"">
        <Kind>Property</Kind>
        <ReturnType>Unknown</ReturnType>
        <Parent xsi:type=""IqlPropertyExpression"">
          <Kind>Property</Kind>
          <ReturnType>Unknown</ReturnType>
          <Parent xsi:type=""IqlVariableExpression"">
            <Kind>Variable</Kind>
            <ReturnType>Unknown</ReturnType>
            <VariableName>context</VariableName>
          </Parent>
          <PropertyName>Owner</PropertyName>
        </Parent>
        <PropertyName>Title</PropertyName>
      </Right>
    </Body>
  </Body>
</IqlLambdaExpression>", iqlXml);
        }

        [TestMethod]
        public void TestSerializeSinglePropertyToXml()
        {
            var xml = IqlXmlSerializer.SerializeToXml<Client, string>(client => client.Name);
            Assert.AreEqual(@"<?xml version=""1.0"" encoding=""utf-16""?>
<IqlLambdaExpression xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Kind>Lambda</Kind>
  <ReturnType>Unknown</ReturnType>
  <Parameters>
    <IqlRootReferenceExpression>
      <Kind>RootReference</Kind>
      <ReturnType>Unknown</ReturnType>
      <VariableName>client</VariableName>
    </IqlRootReferenceExpression>
  </Parameters>
  <Body xsi:type=""IqlPropertyExpression"">
    <Kind>Property</Kind>
    <ReturnType>Unknown</ReturnType>
    <Parent xsi:type=""IqlRootReferenceExpression"">
      <Kind>RootReference</Kind>
      <ReturnType>Unknown</ReturnType>
      <VariableName>client</VariableName>
    </Parent>
    <PropertyName>Name</PropertyName>
  </Body>
</IqlLambdaExpression>", xml);
        }

        [TestMethod]
        public void TestSerializeSinglePropertyToXmlFromLambdaWithoutKnownType()
        {
            Expression<Func<Client, string>> exp = client => client.Name;
            LambdaExpression lambda = exp;
            var xml = IqlXmlSerializer.SerializeToXml(lambda);
            Assert.AreEqual(@"<?xml version=""1.0"" encoding=""utf-16""?>
<IqlLambdaExpression xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Kind>Lambda</Kind>
  <ReturnType>Unknown</ReturnType>
  <Parameters>
    <IqlRootReferenceExpression>
      <Kind>RootReference</Kind>
      <ReturnType>Unknown</ReturnType>
      <VariableName>client</VariableName>
    </IqlRootReferenceExpression>
  </Parameters>
  <Body xsi:type=""IqlPropertyExpression"">
    <Kind>Property</Kind>
    <ReturnType>Unknown</ReturnType>
    <Parent xsi:type=""IqlRootReferenceExpression"">
      <Kind>RootReference</Kind>
      <ReturnType>Unknown</ReturnType>
      <VariableName>client</VariableName>
    </Parent>
    <PropertyName>Name</PropertyName>
  </Body>
</IqlLambdaExpression>", xml);
        }

        [TestMethod]
        public void TestSerializePropertyStringConcatenationToXml()
        {
            var xml = IqlXmlSerializer.SerializeToXml<Client, string>(client => client.Name + " (" + client.Description + ")");
            Assert.AreEqual(@"<?xml version=""1.0"" encoding=""utf-16""?>
<IqlLambdaExpression xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Kind>Lambda</Kind>
  <ReturnType>Unknown</ReturnType>
  <Parameters>
    <IqlRootReferenceExpression>
      <Kind>RootReference</Kind>
      <ReturnType>Unknown</ReturnType>
      <VariableName>client</VariableName>
    </IqlRootReferenceExpression>
  </Parameters>
  <Body xsi:type=""IqlAddExpression"">
    <Kind>Add</Kind>
    <ReturnType>Unknown</ReturnType>
    <Left xsi:type=""IqlAddExpression"">
      <Kind>Add</Kind>
      <ReturnType>Unknown</ReturnType>
      <Left xsi:type=""IqlAddExpression"">
        <Kind>Add</Kind>
        <ReturnType>Unknown</ReturnType>
        <Left xsi:type=""IqlPropertyExpression"">
          <Kind>Property</Kind>
          <ReturnType>Unknown</ReturnType>
          <Parent xsi:type=""IqlRootReferenceExpression"">
            <Kind>RootReference</Kind>
            <ReturnType>Unknown</ReturnType>
            <VariableName>client</VariableName>
          </Parent>
          <PropertyName>Name</PropertyName>
        </Left>
        <Right xsi:type=""IqlLiteralExpression"">
          <Kind>Literal</Kind>
          <ReturnType>String</ReturnType>
          <Value xsi:type=""xsd:string""> (</Value>
          <InferredReturnType>String</InferredReturnType>
        </Right>
      </Left>
      <Right xsi:type=""IqlPropertyExpression"">
        <Kind>Property</Kind>
        <ReturnType>Unknown</ReturnType>
        <Parent xsi:type=""IqlRootReferenceExpression"">
          <Kind>RootReference</Kind>
          <ReturnType>Unknown</ReturnType>
          <VariableName>client</VariableName>
        </Parent>
        <PropertyName>Description</PropertyName>
      </Right>
    </Left>
    <Right xsi:type=""IqlLiteralExpression"">
      <Kind>Literal</Kind>
      <ReturnType>String</ReturnType>
      <Value xsi:type=""xsd:string"">)</Value>
      <InferredReturnType>String</InferredReturnType>
    </Right>
  </Body>
</IqlLambdaExpression>", xml);
        }

        [TestMethod]
        public void TestDeserializeStringConcatenationFromXmlAndApply()
        {
            IqlExpressionConversion.DefaultExpressionConverter = () => new DotNetExpressionConverter();
            var xml = IqlXmlSerializer.SerializeToXml<Client, string>(c => c.Name + " (" + c.Description + ")" + " - " + c.Id);
            var expression = IqlXmlSerializer.DeserializeFromXml(xml);
            var query = IqlConverter.Instance.ConvertIqlToFunction<Client, string>(expression);

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
        public void TestDeserializeCollectionFilterExpression()
        {
            AssertCode(
                c => c.Sites.Any(s => s.Address == "some address"),
                @"entity => (entity.Sites.LongCount(entity1 => (((entity1.Address == null) ? entity1.Address : entity1.Address.ToUpper()) == ""SOME ADDRESS"")) > 0)");
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
            IqlExpressionConversion.DefaultExpressionConverter = () => new DotNetExpressionConverter();
            var xml = IqlXmlSerializer.SerializeToXml(
                expression);
            var iqlExpression = IqlXmlSerializer.DeserializeFromXml(xml);
            var query = IqlConverter.Instance.ConvertIqlToFunction<Client, bool>(iqlExpression);
            var code = query.ToCSharpString();
            Assert.AreEqual(
                expected,
                code);
        }
    }
}
#endif
