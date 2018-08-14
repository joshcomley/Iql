using System.Collections.Generic;
using Iql.Entities;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.EntityConfiguration;
using Iql.Server.Serialization;

namespace Iql.OData.TypeScript.Generator.Parsers
{
    public class ODataSchema
    {
        public List<EntityTypeDefinition> EntityTypes { get; set; } = new List<EntityTypeDefinition>();
        public List<EntitySetDefinition> EntitySets { get; set; } = new List<EntitySetDefinition>();
        public List<EnumTypeDefinition> EnumTypes { get; set; } = new List<EnumTypeDefinition>();
        public List<EntityFunctionDefinition> Functions { get; set; } = new List<EntityFunctionDefinition>();
        public Dictionary<string, IEntityConfiguration>  EntityConfigurations { get; set; }
        public EntityConfigurationDocument EntityConfigurationDocument { get; set; }
    }
}