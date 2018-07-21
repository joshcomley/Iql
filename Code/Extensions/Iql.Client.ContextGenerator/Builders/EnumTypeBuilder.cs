using System.Xml.Linq;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.Extensions;

namespace Iql.OData.TypeScript.Generator.Builders
{
    public class EnumTypeBuilder
    {
        public EnumTypeDefinition Build(XElement element)
        {
            var enumTypeDefinition = new EnumTypeDefinition();
            enumTypeDefinition.Name = element.Attribute("Name").Value;
            var isFlagsAttribute = element.Attribute("IsFlags");
            enumTypeDefinition.IsFlags = isFlagsAttribute?.Value == "true";
            enumTypeDefinition.Namespace = element.ODataNamespace();
            foreach (var value in element.Elements())
            {
                var enumValueDefinition = new EnumTypeValueDefinition();
                enumValueDefinition.Name = value.Attribute("Name").Value;
                enumValueDefinition.Value = value.Attribute("Value").Value;
                enumTypeDefinition.Values.Add(enumValueDefinition);
            }
            return enumTypeDefinition;
        }
    }
}