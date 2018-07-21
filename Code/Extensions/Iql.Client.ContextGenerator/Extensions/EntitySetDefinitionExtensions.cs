using System;
using Iql.OData.TypeScript.Generator.Definitions;

namespace Iql.OData.TypeScript.Generator.Extensions
{
    public static class EntitySetDefinitionExtensions
    {
        public static string GetDbSetName(this EntitySetDefinition set, Func<string, string> nameMapper)
        {
            var setName = $"{nameMapper(set.Type.Name)}Set";
            return setName;
        }
    }
}