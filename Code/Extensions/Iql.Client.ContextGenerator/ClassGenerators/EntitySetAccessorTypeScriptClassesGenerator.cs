using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Client.ContextGenerator.ClassGenerators;
using Iql.Entities;
using Iql.OData.TypeScript.Generator.DataContext;
using Iql.OData.TypeScript.Generator.Models;
using Iql.OData.TypeScript.Generator.Parsers;
using GeneratedFile = Iql.OData.TypeScript.Generator.Models.GeneratedFile;

namespace Iql.OData.TypeScript.Generator.ClassGenerators
{
    public class EntitySetAccessorTypeScriptClassesGenerator
    {
        public GeneratorSettings Settings { get; }
        private readonly ODataSchema _schema;

        public EntitySetAccessorTypeScriptClassesGenerator(ODataSchema schema, GeneratorSettings settings)
        {
            Settings = settings;
            _schema = schema;
        }

        public async Task<List<GeneratedFile>> GenerateAsync(OutputKind outputKind)
        {
            var files = new List<GeneratedFile>();
            var setGroups = _schema.EntitySets.GroupBy(es => es.Namespace);
            foreach (var setGroup in setGroups)
            {
                var dbSetsPath = $"{setGroup.Key}.Sets";
                if (Settings.GenerateDataContext)
                {
                    var dataContextGenerator = new DataContextGenerator(
                        _schema,
                        $"{setGroup.Key}.ApiContext.Base",
                        $"{setGroup.Key}.{nameof(DataContext)}Base",
                        $"{setGroup.Key}{nameof(DataContext)}Base",
                        dbSetsPath,
                        @"",
                        setGroup.Select(s => s),
                        outputKind,
                        Settings);
                    files.Add(await dataContextGenerator.GenerateAsync());
                }

                var propertyServiceGenerator = new EntityTypeServiceGenerator(
                    _schema,
                    $"{setGroup.Key}.ApiContext.Base",
                    $"{setGroup.Key}.{nameof(EntityTypeService)}Base",
                    $"{setGroup.Key}{nameof(EntityTypeService)}Base",
                    setGroup.Select(s => s),
                    outputKind,
                    Settings);
                files.Add(propertyServiceGenerator.Generate());

                if (Settings.GenerateEntitySets)
                {
                    var dbSetsGenerator = new DbSetsGenerator(
                        _schema,
                        dbSetsPath,
                        dbSetsPath,
                        setGroup.Select(s => s),
                        outputKind,
                        Settings);
                    files.Add(dbSetsGenerator.Generate());
                }
            }
            return files;
        }
    }
}