using System.Collections.Generic;
using Iql.OData.TypeScript.Generator.Models;

namespace Iql.OData.TypeScript.Generator.Definitions
{
    public class EntityFunctionDefinition
    {
        public EntityTypeReference EntityType { get; set; }
        public ODataMethodScope Scope { get; set; }
        public string Name { get; set; }
        public ITypeInfo ReturnType { get; set; }
        public EntityFunctionDefinitionType Type { get; set; }

        public List<EntityFunctionParameterDefinition> Parameters { get; set; } =
            new List<EntityFunctionParameterDefinition>();

        public string Namespace { get; set; }
    }
}