using Iql.OData.TypeScript.Generator.Builders;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.Models;
using Iql.OData.TypeScript.Generator.Parsers;

namespace Iql.OData.TypeScript.Generator
{
    public abstract class TypeResolver
    {
        public GeneratorSettings Settings { get; }
        private readonly ODataSchema _schema;

        protected TypeResolver(ODataSchema schema, GeneratorSettings settings)
        {
            Settings = settings;
            _schema = schema;
        }

        public EntityTypeReference TryResolveType(ITypeInfo odataTypeName)
        {
            return new EntityTypeReferenceBuilder().Build(odataTypeName, _schema);
        }

        public GeneratorTypeDefinition ResolveTypeNameFromODataName(ITypeInfo type, bool resolveCollection = false,
            params string[] hints)
        {
            var typeDefinition = ResolveTypeNameFromODataNameInternal(type, resolveCollection, hints);
            if (Settings.NameMapper != null)
            {
                var originalName = typeDefinition.Name;
                typeDefinition.Name = Settings.NameMapper(originalName);
                typeDefinition.OriginalName = originalName;
            }
            return typeDefinition;
        }

        public abstract GeneratorTypeDefinition ResolveTypeNameFromODataNameInternal(ITypeInfo type,
            bool resolveCollection = false,
            params string[] hints);
    }
}