using Iql.OData.TypeScript.Generator.Models;

namespace Iql.OData.TypeScript.Generator.Definitions
{
    public class NavigationPropertyDefinition : PropertyDefinition
    {
        public EntityTypeReference EntityType { get; set; }
        public string Partner { get; set; }
        public ReferentialConstraint Constraint { get; set; }
    }
}