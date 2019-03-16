using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Entities;
using Iql.OData.TypeScript.Generator;
using Iql.OData.TypeScript.Generator.ClassGenerators;
using Iql.OData.TypeScript.Generator.DataContext;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.Models;
using Iql.OData.TypeScript.Generator.Parsers;
using Iql.Server.Serialization.Deserialization.EntityConfiguration;
using GeneratedFile = Iql.OData.TypeScript.Generator.Models.GeneratedFile;

namespace Iql.Client.ContextGenerator.ClassGenerators
{
    public class EntityTypeServiceGenerator : ClassGenerator
    {
        private readonly string _className;
        private readonly IEnumerable<EntitySetDefinition> _entitySetDefinitions;

        public EntityTypeServiceGenerator(
            ODataSchema schema,
            string @namespace,
            string fileName,
            string className,
            IEnumerable<EntitySetDefinition> entitySetDefinitions,
            OutputKind outputKind,
            GeneratorSettings settings) : base(fileName, @namespace, schema, outputKind, settings)
        {
            _entitySetDefinitions = entitySetDefinitions;
            _className = className;
        }

        public GeneratedFile Generate()
        {
            Class(
                _className,
                Namespace,
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
                        GenerateType(entitySetDefinition.Type.Name);
                        GenerateConfiguration(entitySetDefinition.Type.Name);
                        foreach (var property in entitySetDefinition.Type.Properties)
                        {
                            GenerateProperty(entitySetDefinition.Type.Name, property.Name);
                        }
                    }
                }, nameof(EntityTypeService));
            File.Contents = Contents();
            return File;
        }

        private void GenerateConfiguration(string type)
        {
            var privateName = $@"_configurationFor_{type}";
            AppendLine($"private {nameof(EntityConfiguration<object>)}<{type}> {privateName} = null;");
            AppendLine($"public {nameof(EntityConfiguration<object>)}<{type}> ConfigurationFor_{type} => {privateName} = {privateName} ?? {nameof(EntityTypeService.Builder)}.{nameof(IEntityConfigurationBuilder.EntityType)}<{type}>();");
        }

        private void GenerateType(string type)
        {
            AppendLine($"public {nameof(Type)} TypeOf_{type} => typeof({type});");
        }

        private void GenerateProperty(string type, string property)
        {
            var publicName = $"{type}_{property}";
            var privateName = $"_{publicName}";
            AppendLine($"private {nameof(IProperty)} {privateName} = null;");
            AppendLine(
                $"public {nameof(IProperty)} {publicName} {{ get {{ return {privateName} = {privateName} ?? {nameof(EntityTypeService.FindPropertyByName)}<{type}>({String(property)}); }} }}");
        }
    }
}