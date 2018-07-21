using System.Collections.Generic;

namespace Iql.OData.TypeScript.Generator.Definitions
{
    public class EnumTypeDefinition : ODataTypeDefinition
    {
        public List<EnumTypeValueDefinition> Values { get; set; } = new List<EnumTypeValueDefinition>();
    }
}