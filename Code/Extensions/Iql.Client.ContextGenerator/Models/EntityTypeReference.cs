using Iql.OData.TypeScript.Generator.Definitions;

namespace Iql.OData.TypeScript.Generator.Models
{
    public class EntityTypeReference
    {
        public ODataTypeDefinition Type { get; set; }
        public bool IsCollection { get; set; }
        public bool IsNullable { get; set; }
    }
}