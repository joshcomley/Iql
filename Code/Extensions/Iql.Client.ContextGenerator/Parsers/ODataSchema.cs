using System.Collections.Generic;
using Iql.Entities;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.EntityConfiguration;

namespace Iql.OData.TypeScript.Generator.Parsers
{
    public class ODataSchema
    {
        public List<EntityTypeDefinition> EntityTypes { get; set; } = new List<EntityTypeDefinition>();
        public List<EntitySetDefinition> EntitySets { get; set; } = new List<EntitySetDefinition>();
        public List<EnumTypeDefinition> EnumTypes { get; set; } = new List<EnumTypeDefinition>();
        public List<EntityFunctionDefinition> Functions { get; set; } = new List<EntityFunctionDefinition>();
        public Dictionary<string, IEntityMetadata>  EntityConfigurations { get; set; }
    }
}