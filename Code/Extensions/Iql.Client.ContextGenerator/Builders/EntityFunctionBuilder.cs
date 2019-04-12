using System.Linq;
using System.Xml.Linq;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.Models;
using Iql.OData.TypeScript.Generator.Parsers;

namespace Iql.OData.TypeScript.Generator.Builders
{
    public class EntityFunctionBuilder
    {
        public EntityFunctionDefinition Build(XElement element, ODataSchema schema)
        {
            var functionDefinition = new EntityFunctionDefinition();
            functionDefinition.Type = EntityFunctionDefinitionType.Action;
            functionDefinition.Namespace = element.Parent.Attribute("Namespace").Value;
            functionDefinition.Name = element.Attribute("Name").Value;
            if (element.Name.LocalName == "Function")
            {
                functionDefinition.Type = EntityFunctionDefinitionType.Function;
            }
            var parameters = element.Elements()
                .Where(e => e.Name.LocalName == "Parameter" && e.Name.LocalName != "bindingParameter");
            var returnType = element.Elements().SingleOrDefault(e => e.Name.LocalName == "ReturnType");
            if (returnType != null)
            {
                functionDefinition.ReturnType = new TypeInfo(returnType.Attribute("Type").Value);
            }
            foreach (var parameter in parameters)
            {
                if (parameter.Attribute("Name").Value == "bindingParameter")
                {
                    continue;
                }
                functionDefinition.Parameters.Add(new EntityFunctionParameterDefinition
                {
                    Name = parameter.Attribute("Name").Value,
                    TypeInfo = new TypeInfo(parameter.Attribute("Type").Value)
                });
            }

            var functionElementType =
                element.Elements().SingleOrDefault(e =>
                    e.Name.LocalName == "Parameter" &&
                    e.Attribute("Name") != null &&
                    e.Attribute("Name").Value == "bindingParameter");
            if (functionElementType != null)
            {
                var functionEntityType =
                    new EntityTypeReferenceBuilder().Build(new TypeInfo(functionElementType.Attribute("Type").Value), schema);
                var forEntitySet = functionEntityType.IsCollection;
                functionDefinition.Scope = forEntitySet 
                    ? ODataMethodScopeKind.Collection 
                    : ODataMethodScopeKind.Entity;
                functionDefinition.EntityType = functionEntityType;
            }
            else
            {
                functionDefinition.Scope = ODataMethodScopeKind.Global;
            }
            return functionDefinition;
        }
    }
}