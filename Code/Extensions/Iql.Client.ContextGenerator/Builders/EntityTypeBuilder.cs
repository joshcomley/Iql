using System.Xml.Linq;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.Extensions;
using Iql.OData.TypeScript.Generator.Parsers;

namespace Iql.OData.TypeScript.Generator.Builders
{
    public class EntityTypeBuilder
    {
        private readonly XElement _entityType;
        private readonly string _namespace;
        private ODataSchema _schema;

        public EntityTypeBuilder(XElement entityType, string @namespace, ODataSchema schema)
        {
            _entityType = entityType;
            _namespace = @namespace;
            _schema = schema;
        }

        public EntityTypeDefinition Build()
        {
            var ns = _entityType.ODataNamespace();
            var entityTypeDefinition = new EntityTypeDefinition();
            var name = _entityType.Attribute("Name").Value;
            var fullName = _namespace + "." + name;
            entityTypeDefinition.Name = name;
            entityTypeDefinition.Namespace = _namespace;
            //EntityTypeDefinition.FullName = fullName;
            //"Building".Dump(fullName);
            //entityType.Dump();
            return entityTypeDefinition;
        }
    }
}