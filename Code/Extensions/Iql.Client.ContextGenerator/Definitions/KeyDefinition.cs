using System.Collections.Generic;

namespace Iql.OData.TypeScript.Generator.Definitions
{
    public class KeyDefinition
    {
        public KeyDefinition()
        {
            Properties = new List<PropertyDefinition>();
        }

        public List<PropertyDefinition> Properties { get; set; }
    }
}