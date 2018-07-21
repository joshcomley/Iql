using System.Collections.Generic;
using System.Linq;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.Parsers;

namespace Iql.OData.TypeScript.Generator.Extensions
{
    public static class ODataSchemaExtensions
    {
        public static IEnumerable<ODataTypeDefinition> AllTypes(this ODataSchema schema)
        {
            return schema.EntityTypes.Cast<ODataTypeDefinition>().Concat(schema.EnumTypes);
        }
    }
}