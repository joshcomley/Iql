using System.Linq;
using System.Xml.Linq;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.Extensions;
using Iql.OData.TypeScript.Generator.Parsers;

namespace Iql.OData.TypeScript.Generator.Builders
{
    public class EntitySetBuilder
    {
        public EntitySetDefinition Build(XElement element, ODataSchema schema)
        {
            var ns = element.ODataNamespace();
            //element.Dump("Entity Set");
            var entityTypeName = element.Attribute("EntityType").Value;
            var entitySetName = element.Attribute("Name").Value;
            //entityTypeName.Dump();
            var entityTypeDefinition = schema.EntityTypes.Single(et => et.FullName == entityTypeName);
            var definition = new EntitySetDefinition(entityTypeDefinition);
            definition.Name = entitySetName;
            definition.Namespace = element.ODataNamespace();
            return definition;
        }
    }
}