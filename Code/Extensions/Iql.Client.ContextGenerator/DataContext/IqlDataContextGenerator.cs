using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Iql.Entities;
using Iql.Events;
using Iql.Forms.Geography;
using Iql.Forms.Syncing;
using Iql.OData.TypeScript.Generator.ClassGenerators;
using Iql.OData.TypeScript.Generator.Models;
using Iql.OData.TypeScript.Generator.Parsers;
using Iql.Parsing;
using Iql.Queryable.Operations;
using TsBeautify;
using TypeSharp;
using TypeSharp.Conversion;

namespace Iql.OData.TypeScript.Generator.DataContext
{
    public class IqlDataContextGenerator
    {
        public IqlDataContextGenerator(
            string oDataSchemaUrl,
            string iqlSchemaUrl,
            string outputFolder,
            GeneratorSettings settings)
        {
            ODataSchemaUrl = oDataSchemaUrl;
            IqlSchemaUrl = iqlSchemaUrl;
            OutputFolder = outputFolder;
            if (!string.IsNullOrWhiteSpace(ODataSchemaUrl) && string.IsNullOrWhiteSpace(iqlSchemaUrl))
            {
                var uri = new Uri(oDataSchemaUrl);
                IqlSchemaUrl = $"{uri.Scheme}://{uri.Authority}/iql";
            }
            Settings = settings;
        }

        public string ODataSchemaUrl { get; set; }
        public string IqlSchemaUrl { get; set; }
        public string OutputFolder { get; }
        public GeneratorSettings Settings { get; }
        public Func<string, string> NameMapper { get; set; } = x => x;

        public async Task<GeneratedContexts> GenerateDataContextAsync(OutputKind outputKind)
        {
            string edmx = null;
            string iqlJson = null;
            using (var wc = new WebClient())
            {
                edmx = wc.DownloadString(ODataSchemaUrl);
                iqlJson = wc.DownloadString(IqlSchemaUrl);
            }
            var odataParser = new ODataSchemaParser(edmx, iqlJson);
            var schema = odataParser.Parse();
            var dataSchemaGenerator = new ODataSchemaGenerator(schema, Settings);
            var files = await dataSchemaGenerator.GenerateAsync(OutputKind.CSharp);
            files.Reverse();

            var fileGroups = files.GroupBy(f => f.QualifiedName).ToList();

            void WriteBaseClasses(StringBuilder builder, List<Models.GeneratedFile> list)
            {
                if (list.Any())
                {
                    builder.AppendLine();
                    builder.AppendLine();
                    foreach (var emptyBaseClass in list)
                    {
                        var entityBaseClassGenerator = new EntityBaseClassGenerator(null, null, schema, OutputKind.CSharp, Settings);
                        entityBaseClassGenerator.Generate(emptyBaseClass);
                        builder.AppendLine(entityBaseClassGenerator.ToCode());
                        builder.AppendLine();
                    }
                }
            }

            var outputFiles = new List<GeneratedFile>();
            foreach (var fileGroup in fileGroups)
            {
                var filePath = $"{fileGroup.First().FileName}.cs";
                if (!string.IsNullOrWhiteSpace(OutputFolder))
                {
                    filePath = Path.Combine(OutputFolder, filePath);
                }
                outputFiles.Add(GetCSharpOutputFile(filePath, fileGroup, WriteBaseClasses, fileGroups));
            }

            GeneratedContext typeScriptContext = null;
            GeneratedContext cSharpContext = new GeneratedContext(OutputKind.CSharp, outputFiles.ToArray());
            if (outputKind == OutputKind.TypeScript)
            {
                var conversionCollection = new ConversionCollection();
                conversionCollection.Name = "datacontext";
                conversionCollection.AddFiles(outputFiles.Select(_ => new ConversionFileEntry(_.Path, _.Contents)).ToArray());
                var defaultConversionSettings = new DefaultConversionSettings
                {
                    OutputClassFunctionsDeclared = false,
                    OutputClassInterfacesImplemented = false,
                    OutputClassNameStaticProperty = false,
                    OutputClassPropertiesDeclared = true,
                    OutputHardReferences = false,
                    OutputJsonClassConversion = false,
                    OutputTypeLoaded = false,
                    WriteToDisk = false,
                    NpmOutput = true,
                    WrapGettersAndSetters = false,
                    AddUtilityFiles = false,
                    SafeCasts = false
                };
                await defaultConversionSettings.MetadataReferences.AddReferenceAsync<IqlExpression>("@brandless/iql", true);
                await defaultConversionSettings.MetadataReferences.AddReferenceAsync<EventEmitterExceptions>("@brandless/iql.events", true);
                await defaultConversionSettings.MetadataReferences.AddReferenceAsync<EvaluateContext>("@brandless/iql.conversion", true);
                await defaultConversionSettings.MetadataReferences.AddReferenceAsync<IqlParserRegistry>("@brandless/iql.parsing", true);
                await defaultConversionSettings.MetadataReferences.AddReferenceAsync<QueryOperation>("@brandless/iql.queryable", true);
                await defaultConversionSettings.MetadataReferences.AddReferenceAsync<MediaKeyBase>("@brandless/iql.entities", true);
                await defaultConversionSettings.MetadataReferences.AddReferenceAsync<Data.Context.DataContext>("@brandless/iql.data", true);
                await defaultConversionSettings.MetadataReferences.AddReferenceAsync<ODataConfiguration>("@brandless/iql.odata", true);
                await defaultConversionSettings.MetadataReferences.AddReferenceAsync<IqlSyncResult>("@brandless/iql.forms", true);

                var result = await CSharpToTypescriptConverter.ConvertToTypeScriptAsync(
                    new[] { conversionCollection },
                    defaultConversionSettings
                );
                var newOutputFiles = new List<GeneratedFile>();
                foreach (var convertedFile in result.SourceOutputFiles)
                {
                    var newPath =
                        string.IsNullOrWhiteSpace(OutputFolder)
                            ? convertedFile.FileName
                            : Path.Combine(OutputFolder, Path.GetFileName(convertedFile.FileName));
                    newOutputFiles.Add(new GeneratedFile(OutputKind.TypeScript, newPath, convertedFile.GetContents()));
                }
                typeScriptContext = new GeneratedContext(OutputKind.TypeScript, newOutputFiles.ToArray());
            }

            if (!string.IsNullOrWhiteSpace(OutputFolder))
            {
                switch (outputKind)
                {
                    case OutputKind.TypeScript:
                        typeScriptContext.SaveTo(OutputFolder);
                        break;
                    case OutputKind.CSharp:
                        cSharpContext.SaveTo(OutputFolder);
                        break;
                }
            }

            return new GeneratedContexts(cSharpContext, typeScriptContext);
        }

        private GeneratedFile GetCSharpOutputFile(
            string filePath,
            IGrouping<string, Models.GeneratedFile> fileGroup,
            Action<StringBuilder, List<Models.GeneratedFile>> writeBaseClasses,
            IEnumerable<IGrouping<string, Models.GeneratedFile>> fileGroups)
        {
            var fileContents = new StringBuilder();
            var usings = new List<Type>();
            var baseClassesToGenerate = new List<Models.GeneratedFile>();
            foreach (var file in fileGroup)
            {
                var resolveImportsWithinCSharpFile = file.Contents.ResolveImportsWithinCSharpFile().ToList();
                if (file.IsEntity)
                {
                    baseClassesToGenerate.Add(file);
                }
                usings.AddRange(resolveImportsWithinCSharpFile);
            }

            var baseClasses = new StringBuilder();
            writeBaseClasses(baseClasses, baseClassesToGenerate);
            var baseClassesStr = baseClasses.ToString();
            var resolveImportsWithinBaseClassesFile = baseClassesStr.ResolveImportsWithinCSharpFile().ToList();
            usings.AddRange(resolveImportsWithinBaseClassesFile);

            var fullUsings = new List<string>();
            foreach (var import in usings)
            {
                fullUsings.Add(import.Namespace);
                foreach (var otherFileKey in fileGroups)
                {
                    if (otherFileKey != fileGroup)
                    {
                        foreach (var otherFile in otherFileKey)
                        {
                            if (!string.IsNullOrWhiteSpace(otherFile.Namespace))
                            {
                                fullUsings.Add(otherFile.Namespace.Trim());
                            }
                        }
                    }
                }
            }

            var final = fullUsings.Distinct().ToList();
            if (!final.Contains("System"))
            {
                final.Add("System");
            }
            foreach (var @using in final)
            {
                fileContents.AppendLine($"using {@using};");
            }

            fileContents.Append(baseClassesStr);
            foreach (var file in fileGroup)
            {
                fileContents.AppendLine(file.Contents);
            }

            var contents = fileContents.ToString();
            contents = new TsBeautifier().Configure(c => c.OpenBlockOnNewLine = true).Beautify(contents);
            return new GeneratedFile(OutputKind.CSharp, filePath, contents);
        }
    }
}