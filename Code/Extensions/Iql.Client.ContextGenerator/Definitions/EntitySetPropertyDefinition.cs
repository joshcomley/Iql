namespace Iql.OData.TypeScript.Generator.Definitions
{
    public class EntitySetPropertyDefinition : PropertyDefinition
    {
        public EntitySetDefinition EntitySet { get; set; }
        public string EntityType { get; set; }
        public string KeyType { get; set; }
    }
}