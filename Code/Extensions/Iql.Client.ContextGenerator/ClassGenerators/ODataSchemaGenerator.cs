using System;
using System.Collections.Generic;
using Iql.OData.TypeScript.Generator.DataContext;
using Iql.OData.TypeScript.Generator.Extensions;
using Iql.OData.TypeScript.Generator.Models;
using Iql.OData.TypeScript.Generator.Parsers;

namespace Iql.OData.TypeScript.Generator.ClassGenerators
{
    public class ODataSchemaGenerator
    {
        public GeneratorSettings Settings { get; }
        private readonly ODataSchema _schema;

        public ODataSchemaGenerator(ODataSchema schema, GeneratorSettings settings)
        {
            Settings = settings;
            _schema = schema;
        }

        public List<GeneratedFile> Generate(OutputType outputType)
        {
            var files = new List<GeneratedFile>();
            if (Settings.GenerateEntities)
            {
                foreach (var type in _schema.AllTypes())
                {
                    files.Add(new EntityTypeToClassGenerator(_schema, type.Namespace, null, type, outputType, Settings).Generate());
                }
            }
            //		foreach (var entitySet in _schema.EntitySets)
            //		{
            //			files.Add(new EntitySetToTypeScriptClassGenerator(_schema, entitySet).Generate());
            //		}
            //files.Add(new EntitySetQueryableTypeScriptClassGenerator(_schema).Generate());
            //files.Add(new EntitySetTypeScriptBaseClassGenerator(_schema).Generate());
            files.AddRange(new EntitySetAccessorTypeScriptClassesGenerator(_schema, Settings).Generate(outputType));
            return files;
        }
    }
}