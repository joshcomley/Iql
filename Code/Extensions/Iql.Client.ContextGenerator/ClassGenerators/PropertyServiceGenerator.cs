using Brandless.ObjectSerializer;
using Iql.Entities;
using Iql.OData.TypeScript.Generator.DataContext;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.Models;
using Iql.OData.TypeScript.Generator.Parsers;
using System.Collections.Generic;
using System.Linq;
using Iql.Data.DataStores;
using Iql.Parsing;

namespace Iql.OData.TypeScript.Generator.ClassGenerators
{
    public class PropertyServiceGenerator : ClassGenerator
    {
        private CSharpObjectSerializer CSharpObjectSerializer { get; }
        private readonly string _className;
        private readonly IEnumerable<EntitySetDefinition> _entitySetDefinitions;
        private readonly string _namespace;

        public PropertyServiceGenerator(ODataSchema schema,
            string @namespace,
            string className,
            IEnumerable<EntitySetDefinition> entitySetDefinitions,
            OutputType outputType,
            GeneratorSettings settings) : base(schema, outputType, settings)
        {
            _entitySetDefinitions = entitySetDefinitions;
            _namespace = @namespace;
            _className = className;
            CSharpObjectSerializer = new CSharpObjectSerializer();
        }

        public GeneratedFile Generate()
        {
            File.FileName = _namespace;
            File.Namespace = _namespace;
            Class(
                _className,
                _namespace,
                "",
                () =>
                {
                    var ctorParams = new IVariable[]
                    {
                        new EntityFunctionParameterDefinition("builder", TypeResolver.TranslateType(typeof(IEntityConfigurationBuilder))),
                    }.ToList();
                    Constructor(ctorParams, () =>
                        {
                        },
                        ctorParams);
                    foreach (var entitySetDefinition in _entitySetDefinitions)
                    {
                        foreach (var property in entitySetDefinition.Type.Properties)
                        {
                            GenerateProperty(entitySetDefinition.Type.Name, property.Name);
                        }
                    }
                }, nameof(PropertyService));
            File.Contents = Contents();
            return File;
        }

        private void GenerateProperty(string type, string property)
        {
            var publicName = $"{type}_{property}";
            var privateName = $"_{publicName}";
            AppendLine($"private {nameof(IProperty)} {privateName} = null;");
            AppendLine(
                $"public {nameof(IProperty)} {publicName} {{ get {{ return {privateName} = {privateName} ?? {nameof(PropertyService.FindPropertyByName)}<{type}>({String(property)}); }} }}");
        }
    }
}