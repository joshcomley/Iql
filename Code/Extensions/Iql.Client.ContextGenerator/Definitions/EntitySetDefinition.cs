using System.Collections.Generic;

namespace Iql.OData.TypeScript.Generator.Definitions
{
    public class EntitySetDefinition : ODataTypeDefinition
    {
        public EntitySetDefinition(EntityTypeDefinition type)
        {
            Type = type;
        }

        public EntityTypeDefinition Type { get; set; }
        public List<EntityFunctionDefinition> Functions { get; set; } = new List<EntityFunctionDefinition>();
    }
}