using System;
using System.Collections.Generic;
using System.Linq;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.Parsers;

namespace Iql.OData.TypeScript.Generator.Extensions
{
    public static class EntityTypeDefinitionExtensions
    {
        public static List<ODataTypeDefinition> FindAllInternalTypeReferences(
            this EntityTypeDefinition entityTypeDefinition, 
            ODataSchema schema, GeneratorSettings settings)
        {
            var typeNames =
                entityTypeDefinition.Functions.SelectMany(m => m.Parameters.Select(p => p.TypeInfo))
                    .Concat(entityTypeDefinition.Properties.Select(p => p.TypeInfo));
            var converter = new TypeScriptTypeResolver(schema, settings);
            var types = typeNames.Select(n => converter.TryResolveType(n));
            return types.Where(t => t.Type != null).Select(type => type.Type).ToList();
        }
    }
}