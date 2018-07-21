using System.Collections.Generic;

namespace Iql.OData.TypeScript.Generator.Definitions
{
    public class EntityTypeDefinition : ODataTypeDefinition
    {
        public List<EntityFunctionDefinition> Functions { get; set; } = new List<EntityFunctionDefinition>();
        public List<PropertyDefinition> Properties { get; set; } = new List<PropertyDefinition>();
        public KeyDefinition Key { get; set; }
    }
}