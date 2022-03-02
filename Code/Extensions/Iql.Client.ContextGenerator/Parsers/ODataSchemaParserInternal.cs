using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Iql.OData.TypeScript.Generator.Builders;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.EntityConfiguration;
using Iql.OData.TypeScript.Generator.Extensions;
using EntityConfigurationParser = Iql.Server.Serialization.Deserialization.EntityConfigurationParser;

namespace Iql.OData.TypeScript.Generator.Parsers
{
    internal class ODataSchemaParserInternal
    {
        private XDocument _doc;
        private ODataSchema _schema;

        public ODataSchema Parse(string odataXml, string iqlJson)
        {
            _schema = new ODataSchema();
            _doc = XDocument.Parse(odataXml);
            var entityConfigurationDocument = EntityConfigurationParser.FromJson(iqlJson);
            _schema.EntityConfigurations = entityConfigurationDocument.EntityTypes.ToDictionary(t => t.Name);
            _schema.EntityConfigurationDocument = entityConfigurationDocument;
            ParseEntityTypes(_doc.ByTagNames("ComplexType"));
            ParseEntityTypeProperties(_doc.ByTagNames("ComplexType"));
            ParseEntityTypes(_doc.ByTagNames("EntityType"));
            ParseEntityTypeProperties(_doc.ByTagNames("EntityType"));
            ParseEnumTypes(_doc.ByTagNames("EnumType"));
            ParseEntitySets(_doc.ByTagNames("EntitySet"));
            ParseFunctions(_doc.ByTagNames("Function", "Action"));
            //ParseNavigationProperties(ByTagNames("NavigationProperty"));
            // Get Entity Types
            // Get Entity Sets
            // Get Entity 
            return _schema;
        }

        private void ParseEntityTypeProperties(IEnumerable<XElement> elements)
        {
            foreach (var entityType in elements)
            {
                var entityTypeDefinition =
                    _schema.EntityTypes.SingleOrDefault(
                        et => et.FullName == entityType.ODataNamespace() + "." + entityType.Attribute("Name").Value);
                if (entityTypeDefinition == null)
                {
                    //entityType.Attribute("Name").Value.Dump();
                    throw new Exception();
                }
                var children = entityType.Elements();

                // Do the properties first as we will need them for the
                // key definition afterwards
                foreach (var child in children)
                {
                    switch (child.Name.LocalName)
                    {
                        case "Property":
                        case "NavigationProperty":
                            entityTypeDefinition.Properties.Add(new EntityTypePropertyBuilder().Build(child, _schema));
                            break;
                    }
                    //child.Name.LocalName.Dump();
                }
                foreach (var child in children)
                {
                    switch (child.Name.LocalName)
                    {
                        case "Key":
                            entityTypeDefinition.Key =
                                new EntityTypeKeyBuilder().Build(entityTypeDefinition, child, _schema);
                            break;
                    }
                    //child.Name.LocalName.Dump();
                }
            }
        }
        //	private void ParseNavigationProperties(IEnumerable<XElement> elements)
        //	{
        //		foreach (var navigationProperty in elements)
        //		{
        //			new NavigationPropertyBuilder().Build(navigationProperty, _schema);
        //		}
        //	}

        private void ParseEnumTypes(IEnumerable<XElement> elements)
        {
            foreach (var enumType in elements)
            {
                _schema.EnumTypes.Add(new EnumTypeBuilder().Build(enumType));
            }
        }

        private void ParseFunctions(IEnumerable<XElement> elements)
        {
            foreach (var functionOrAction in elements)
            {
                var functionDefinition = new EntityFunctionBuilder().Build(functionOrAction, _schema);
                switch (functionDefinition.Scope)
                {
                    case ODataMethodScopeKind.Global:
                        _schema.Functions.Add(functionDefinition);
                        break;
                    case ODataMethodScopeKind.Collection:
                    case ODataMethodScopeKind.Entity:
                        var collection = _schema.EntitySets.Single(ec => ec.Type == functionDefinition.EntityType.Type);
                        if (functionDefinition.Scope == ODataMethodScopeKind.Collection)
                        {
                            collection.Functions.Add(functionDefinition);
                        }
                        else
                        {
                            collection.Type.Functions.Add(functionDefinition);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void ParseEntityTypes(IEnumerable<XElement> elements)
        {
            foreach (var entityType in elements)
            {
                _schema.EntityTypes.Add(new EntityTypeBuilder(entityType, entityType.ODataNamespace(), _schema)
                    .Build());
            }
        }

        private void ParseEntitySets(IEnumerable<XElement> elements)
        {
            foreach (var entitySet in elements)
            {
                _schema.EntitySets.Add(new EntitySetBuilder().Build(entitySet, _schema));
            }
        }
    }
}