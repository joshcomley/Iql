using System;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.Models;

namespace Iql.OData.TypeScript.Generator
{
    public interface ISchemaTypeResolver
    {
        GeneratorTypeDefinition ResolveTypeNameFromODataName(ITypeInfo type, bool resolveCollection = false, params string[] hints);
        EntityTypeReference TryResolveType(ITypeInfo type);
        ITypeInfo TranslateType(Type type, params string[] hints);
        string ResolveName(Type type, params string[] hints);
    }
}