using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Brandless.ObjectSerializer;
using Iql.Data;
using Iql.Data.Context;
using Iql.Data.Crud.Operations.Results;
using Iql.Data.DataStores;
using Iql.Data.Events;
using Iql.Data.Http;
using Iql.Data.Lists;
using Iql.Data.Methods;
using Iql.Entities;
using Iql.Entities.Events;
using Iql.Entities.InferredValues;
using Iql.Entities.Metadata;
using Iql.Entities.PropertyChangers;
using Iql.Entities.Relationships;
using Iql.Entities.Rules.Display;
using Iql.Entities.SpecialTypes;
using Iql.Entities.Validation.Validation;
using Iql.OData.Json;
using Iql.OData.Methods;
using Iql.OData.TypeScript.Generator.ClassGenerators;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.Models;
using Iql.OData.TypeScript.Generator.Parsers;
using Iql.Parsing;
using Iql.Queryable.Operations;
using Newtonsoft.Json.Linq;
using TsBeautify;
using TypeSharp;
using TypeSharp.Conversion;

namespace Iql.OData.TypeScript.Generator.DataContext
{
    public class ODataDataContextGenerator
    {
        public ODataDataContextGenerator(
            string oDataSchemaUrl,
            string iqlSchemaUrl, 
            string applicationRoot,
            string applicationPath, 
            string outputSubFolder, 
            GeneratorSettings settings,
            params string[] modulesFolders)
        {
            ODataSchemaUrl = oDataSchemaUrl;
            IqlSchemaUrl = iqlSchemaUrl;
            ApplicationRoot = applicationRoot;
            if (!string.IsNullOrWhiteSpace(ODataSchemaUrl) && string.IsNullOrWhiteSpace(iqlSchemaUrl))
            {
                var uri = new Uri(oDataSchemaUrl);
                IqlSchemaUrl = $"{uri.Scheme}://{uri.Authority}/iql";
            }
            ApplicationPath = applicationPath;
            OutputSubFolder = outputSubFolder;
            Settings = settings;
            ModulesFolders = modulesFolders;
        }

        public string ODataSchemaUrl { get; set; }
        public string IqlSchemaUrl { get; set; }
        public string ApplicationRoot { get; }
        public string ApplicationPath { get; set; }
        public string OutputSubFolder { get; set; }
        public GeneratorSettings Settings { get; }
        public string[] ModulesFolders { get; }
        public Func<string, string> NameMapper { get; set; } = x => x;

        public async Task GenerateDataContextAsync(OutputType outputType)
        {
            string edmx = null;
            string iqlJson = null;
            using (var wc = new WebClient())
            {
                edmx = wc.DownloadString(ODataSchemaUrl);
                iqlJson = wc.DownloadString(IqlSchemaUrl);
            }

            //edmx = File.ReadAllText(@"D:\code\hazception.edmx");
            //File.WriteAllText(@"D:\code\hazception.edmx", edmx);
            //XDocument doc = XDocument.Parse(edmx);
            var odataParser = new ODataSchemaParser(edmx, iqlJson);
            var schema = odataParser.Parse();
            //schema.Dump();
            var typeScriptConverter = new ODataSchemaGenerator(schema, Settings);
            var files = typeScriptConverter.Generate(OutputType.CSharp);
            files.Reverse();

            if (!Directory.Exists(ApplicationPath))
            {
                Directory.CreateDirectory(ApplicationPath);
            }
            var outputFullPath = string.IsNullOrWhiteSpace(OutputSubFolder) ? ApplicationPath : Path.Combine(ApplicationPath, OutputSubFolder);
            if (!Directory.Exists(outputFullPath))
            {
                Directory.CreateDirectory(outputFullPath);
            }
            //var odataPath = Path.Combine(outputFullPath, "OData");
            //if (!Directory.Exists(odataPath))
            //{
            //    Directory.CreateDirectory(odataPath);
            //}
            //var odataConfigurationTypeScript = GenerateODataConfigurationClasses();
            //var odataConfigurationFilePath = Path.Combine(odataPath, nameof(ODataConfiguration) + ".ts");
            //File.WriteAllText(odataConfigurationFilePath, odataConfigurationTypeScript);
            var fileGroups = files.GroupBy(f => f.QualifiedName).ToList();
            var potentialImports = new[]
            {
                typeof(CompositeKey),
                typeof(ODataConfiguration),
                typeof(IHttpProvider),
                typeof(Data.Context.DataContext),
                typeof(IEntity),
                typeof(EntityValidationResult<>),
                typeof(RelatedList<,>),
                typeof(PropertyValidationResult<>),
                typeof(GetDataResult<object>),
                typeof(UsersDefinition),
                typeof(CustomReportsDefinition),
                typeof(UserSettingsDefinition),
                typeof(EntityConfiguration<>),
                typeof(EntityConfigurationBuilder),
                typeof(IDataStore),
                typeof(ODataDataStore),
                typeof(ODataResult<object>),
                typeof(EvaluateContext),
                typeof(Type),
                typeof(List<object>),
                typeof(DbSet<object, object>),
                typeof(Task),
                typeof(JObject),
                typeof(EventEmitter<>),
                typeof(PropertyChangeEvent<>),
                typeof(ODataMethodScope),
                typeof(ODataMethodType),
                typeof(MethodResult),
                typeof(DataMethodResult<>),
                typeof(ODataParameter),
                typeof(ExistsChangeEvent),
                typeof(ObservableCollection<>),
                typeof(ODataDataMethodRequest<>),
                typeof(ODataMethodRequest),
                typeof(IPropertyChangeEvent),
                typeof(PropertyKind),
                typeof(Expression),
                typeof(Func<>),
                typeof(Enum),
                typeof(IDataContext),
                typeof(IqlType),
                typeof(DisplayRuleKind),
                typeof(DisplayRuleAppliesToKind),
                typeof(EntityManageKind),
                typeof(HelpTextKind),
                typeof(IInferredValueConfiguration),
                typeof(InferredValueConfiguration),
                typeof(HelpText),
                typeof(IqlPointExpression),
                typeof(IqlMultiPointExpression),
                typeof(IqlPolygonExpression),
                typeof(IqlMultiPolygonExpression),
                typeof(ValueMapping),
                typeof(RelationshipMapping),
                typeof(IqlLineExpression),
                typeof(IqlMultiLineExpression),
                typeof(PrimitivePropertyChanger),
                typeof(PointPropertyChanger),
                typeof(PolygonPropertyChanger),
                typeof(EntityTypeService),
            };

            void WriteBaseClasses(StringBuilder builder, List<GeneratedFile> list)
            {
                if (list.Any())
                {
                    builder.AppendLine();
                    builder.AppendLine();
                    foreach (var emptyBaseClass in list)
                    {
                        var entityBaseClassGenerator = new EntityBaseClassGenerator(null, null, schema, OutputType.CSharp, Settings);
                        entityBaseClassGenerator.Generate(emptyBaseClass);
                        builder.AppendLine(entityBaseClassGenerator.ToCode());
                        builder.AppendLine();
                    }
                }
            }
            var root = Path.Combine(ApplicationRoot, outputFullPath);

            var outputFiles = new List<OutputFile>();
            foreach (var fileGroup in fileGroups)
            {
                var filePath = Path.Combine(root, $"{fileGroup.First().FileName}.cs");
                //(outputType == OutputType.TypeScript ? ".ts" : ".cs"));
                outputFiles.Add(GetCSharpOutputFile(filePath, fileGroup, potentialImports, WriteBaseClasses, fileGroups));
                //switch (outputType)
                //{
                //    case OutputType.TypeScript:
                //        outputFiles.Add();WriteTypeScriptFile(filePath, fileGroup, potentialImports, WriteBaseClasses);
                //        break;
                //    case OutputType.CSharp:
                //        break;
                //}
            }

            var newOutputFiles = new List<OutputFile>();
            if (outputType == OutputType.TypeScript)
            {
                var tempFolder = Path.Combine(Path.GetTempPath(), "TypeSharp", Guid.NewGuid().ToString());
                if (!Directory.Exists(tempFolder))
                {
                    Directory.CreateDirectory(tempFolder);
                }

                var tempOutputFiles = new List<string>();
                foreach (var file in outputFiles)
                {
                    var tempOutputFile = Path.Combine(tempFolder, Path.GetFileName(file.Path));
                    File.WriteAllText(tempOutputFile, file.Contents);
                    tempOutputFiles.Add(tempOutputFile);
                }

                var conversionCollections = new List<ConversionCollection>();
                var conversionCollection = new ConversionCollection(tempOutputFiles, ApplicationRoot, "datacontext");
                conversionCollections.Add(conversionCollection);
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
                defaultConversionSettings.MetadataReferences.AddReference<ODataConfiguration>("@brandless/iql.odata", true);
                defaultConversionSettings.MetadataReferences.AddReference<Data.Context.DataContext>("@brandless/iql.data", true);
                defaultConversionSettings.MetadataReferences.AddReference<QueryOperation>("@brandless/iql.queryable", true);
                defaultConversionSettings.MetadataReferences.AddReference<IqlExpression>("@brandless/iql", true);
                defaultConversionSettings.MetadataReferences.AddReference<MediaKeyBase>("@brandless/iql.entities", true);
                defaultConversionSettings.MetadataReferences.AddReference<IqlParserRegistry>("@brandless/iql.parsing", true);
                defaultConversionSettings.MetadataReferences.AddReference<EvaluateContext>("@brandless/iql.conversion", true);
                var result = await CSharpToTypescriptConverter.ConvertToTypeScriptAsync(
                    conversionCollections,
                    defaultConversionSettings
                );
                foreach (var convertedFile in result.OutputFiles)
                {
                    newOutputFiles.Add(new OutputFile(Path.Combine(outputFullPath, convertedFile.Path.Substring(ApplicationRoot.Length)), convertedFile.GetContents()));
                }
            }
            else
            {
                newOutputFiles = outputFiles;
            }

            foreach (var file in newOutputFiles)
            {
                var newFilePath = file.Path;
                var directoryName = Path.GetDirectoryName(newFilePath);
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
                File.WriteAllText(newFilePath, file.Contents);
            }

            //var odataConfigurationImports = BuildImports(
            //    ResolveImportsWithinTypeScriptFile(new[] { "Type", "Interface", nameof(GetDataResult<object>) }, File.ReadAllText(odataConfigurationFilePath), ApplicationPath,
            //    odataConfigurationFilePath));
            //if (!string.IsNullOrWhiteSpace(odataConfigurationImports))
            //{
            //    File.WriteAllText(odataConfigurationFilePath, odataConfigurationImports + "\r\n" + odataConfigurationTypeScript);
            //}
        }

        [DebuggerDisplay("{Path}")]
        private class OutputFile
        {
            public string Path { get; set; }
            public string Contents { get; set; }

            public OutputFile(string path, string contents)
            {
                Path = path;
                Contents = contents;
            }
        }
        private OutputFile GetCSharpOutputFile(string filePath, IGrouping<string, GeneratedFile> fileGroup, Type[] validationImports, Action<StringBuilder, List<GeneratedFile>> writeBaseClasses, IEnumerable<IGrouping<string, GeneratedFile>> fileGroups)
        {
            var fileContents = new StringBuilder();
            var usings = new List<string>();
            var dic = validationImports.ToDictionary(SimpleCSharpTypeName);
            var importsToLookFor = dic.Keys.ToList();
            var baseClassesToGenerate = new List<GeneratedFile>();
            foreach (var file in fileGroup)
            {
                var resolveImportsWithinCSharpFile = ResolveImportsWithinCSharpFile(
                    importsToLookFor,
                    file.Contents).ToList();
                if (file.IsEntity)
                {
                    baseClassesToGenerate.Add(file);
                }
                usings.AddRange(resolveImportsWithinCSharpFile);
            }

            var baseClasses = new StringBuilder();
            writeBaseClasses(baseClasses, baseClassesToGenerate);
            var baseClassesStr = baseClasses.ToString();
            var resolveImportsWithinBaseClassesFile = ResolveImportsWithinCSharpFile(
                importsToLookFor,
                baseClassesStr).ToList();
            usings.AddRange(resolveImportsWithinBaseClassesFile);

            var fullUsings = new List<string>();
            foreach (var import in usings)
            {
                fullUsings.Add(dic[import].Namespace);
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
            return new OutputFile(filePath, contents);
        }        

        private static string SimpleCSharpTypeName(Type i)
        {
            var name = i.Name;
            var import = name;
            var index = import.IndexOf("`");
            if (index != -1)
            {
                import = import.Substring(0, index);
            }
            return import;
        }

        private void WriteTypeScriptFile(
            string filePath,
            IGrouping<string, GeneratedFile> fileGroup,
            Type[] potenetialImports,
            Action<StringBuilder, List<GeneratedFile>> writeBaseClasses)
        {
            var emptyBaseClassesToGenerate = new List<GeneratedFile>();
            var otherImports = new List<TypeScriptImportReference>();
            var references = fileGroup.SelectMany(f => f.References).Distinct()
                .Where(r => r.Namespace != fileGroup.Key && !r.IsUnknown);
            var typeReferences = references as ODataTypeDefinition[] ?? references.ToArray();
            if (typeReferences.Any())
            {
                foreach (var reference in typeReferences)
                {
                    otherImports.Add(new TypeScriptImportReference($"{(reference.IsImport ? "" : "./")}{reference.Namespace}", reference.Name));
                    //import { UserType } from './ISite.App.Data.Models'
                    //importsBuilder.AppendLine(
                    //    $"import {{ {reference.Name} }} from \"\";");
                }
            }

            var fileContents = new StringBuilder();
            var importsToLookFor = potenetialImports.Select(SimpleCSharpTypeName).ToList();
            foreach (var file in fileGroup)
            {
                importsToLookFor.AddRange(file.References.Where(r => r.IsUnknown).Select(r => r.Name));
                var hasBaseClass = false;
                if (!string.IsNullOrWhiteSpace(file.BaseClassName))
                {
                    hasBaseClass = true;
                    importsToLookFor.Add(file.BaseClassName);
                }
                fileContents.AppendLine(file.Contents);
                var resolveImportsWithinTypeScriptFile = ResolveImportsWithinTypeScriptFile(
                    importsToLookFor,
                    file.Contents,
                    filePath,
                    ApplicationPath,
                    new[] { filePath },
                    ModulesFolders).ToList();
                if (file.IsEntity &&
                    hasBaseClass &&
                    !resolveImportsWithinTypeScriptFile.Any(r => r.Import.Equals(file.BaseClassName,
                        StringComparison.OrdinalIgnoreCase)))
                {
                    emptyBaseClassesToGenerate.Add(file);
                }
                otherImports.AddRange(resolveImportsWithinTypeScriptFile);
            }

            var contents = new StringBuilder();
            writeBaseClasses(contents, emptyBaseClassesToGenerate);
            var baseClassesContents = contents.ToString();
            Console.WriteLine($"Base classes for \"{filePath}\":");
            Console.WriteLine(string.IsNullOrWhiteSpace(baseClassesContents) ? "Empty" : baseClassesContents);
            var resolveImportsWithinBaseClasses = ResolveImportsWithinTypeScriptFile(
                importsToLookFor,
                baseClassesContents,
                filePath,
                ApplicationPath,
                new[] { filePath },
                ModulesFolders).ToList();
            otherImports.AddRange(resolveImportsWithinBaseClasses);
            contents.AppendLine(fileContents.ToString().Trim());
            var contentsStr = contents.ToString();
            var allImports = BuildImports(otherImports).Trim();
            if (!string.IsNullOrWhiteSpace(allImports))
            {
                contentsStr = allImports + "\r\n" + contentsStr;
            }

            contentsStr = contentsStr.Trim();
            contentsStr = new TsBeautifier().Configure(c => c.OpenBlockOnNewLine = true).Beautify(contentsStr);
            File.WriteAllText(filePath, contentsStr);
        }

        private static string BuildImports(IEnumerable<TypeScriptImportReference> imports)
        {
            var sb = new StringBuilder();
            foreach (var import in imports.GroupBy(i => i.RelativePath))
            {
                sb.AppendLine(
                    $"import {{ {string.Join(", ", import.Select(i => i.Import).Distinct())} }} from \"{import.Key}\";");
            }
            return sb.ToString();
        }

        //private string GenerateODataConfigurationClasses()
        //{
        //    var executingAssembly = Assembly.GetExecutingAssembly();
        //    var assemblyPath = Path.GetFullPath(Path.GetDirectoryName(executingAssembly.Location));
        //    var conversionSettings = new DefaultConversionSettings();
        //    conversionSettings.WriteToDisk = true;
        //    var source = Path.Combine(assemblyPath, "DataContext\\ODataConfiguration.cs");
        //    var collection = new ConversionCollection(
        //        new[] { source }, ApplicationPath);
        //    conversionSettings.GenerateImports = false;
        //    return TypeSharp.CSharpToTypescriptConverter.ConvertToTypeScript(
        //        File.ReadAllText(source), conversionSettings);
        //}

        private static IEnumerable<string> ResolveImportsWithinCSharpFile(
            IEnumerable<string> validationMatches, string contents)
        {
            var imports = new List<string>();
            foreach (var classToImport in validationMatches)
            {
                if (Regex.IsMatch(contents, "\\b" + classToImport + "\\b"))
                {
                    imports.Add(classToImport);
                }
            }
            return imports.Distinct();
        }

        class FileDetail
        {
            public FileDetail(string path, string contents)
            {
                Path = path;
                Contents = contents;
                Exports = new List<string>();
                ExportsSearched = new Dictionary<string, string>();
            }

            public List<string> Exports { get; }
            public Dictionary<string, string> ExportsSearched { get; }
            public string Path { get; }
            public string Contents { get; }
        }

        private static readonly Dictionary<string, Dictionary<string, FileDetail>> FolderFileContents = new Dictionary<string, Dictionary<string, FileDetail>>();
        private static readonly  Dictionary<string, List<FileDetail>> FolderFiles = new Dictionary<string, List<FileDetail>>();
        private static object _fileDetailsLocker = new object();
        private static Dictionary<string, FileDetail> GetTypeScriptFiles(string path, bool isModulesPath, IEnumerable<string> validationMatches)
        {
            lock (_fileDetailsLocker)
            {
                if (!FolderFiles.ContainsKey(path))
                {
                    FolderFiles.Add(path, new List<FileDetail>());
                    FolderFileContents.Add(path, new Dictionary<string, FileDetail>());
                    foreach (var subFile in Directory.GetFiles(path, "*.ts", SearchOption.AllDirectories))
                    {
                        if (isModulesPath)
                        {
                            var parts = subFile.Split(Path.DirectorySeparatorChar);
                            if (!parts.Contains("es5"))
                            {
                                continue;
                            }
                        }
                        var subFileContents = File.ReadAllText(subFile);
                        var fileDetail = new FileDetail(subFile, subFileContents);
                        FolderFiles[path].Add(fileDetail);
                    }
                }

                foreach (var fileDetail in FolderFiles[path])
                {
                    var exportsToLookFor = (validationMatches as string[] ?? validationMatches.ToArray()).Distinct().ToArray();
                    foreach (var import in exportsToLookFor)
                    {
                        if (!fileDetail.ExportsSearched.ContainsKey(import))
                        {
                            if (Regex.IsMatch(fileDetail.Contents, "(class|interface|type|enum)\\s+\\b" + import + "\\b"))
                            {
                                fileDetail.Exports.Add(import);
                                var folderFileContent = FolderFileContents[path];
                                if (!folderFileContent.ContainsKey(import))
                                {
                                    folderFileContent.Add(import, fileDetail);
                                }
                            }
                            fileDetail.ExportsSearched.Add(import, import);
                        }
                    }
                }
                return FolderFileContents[path];
            }
        }

        private static IEnumerable<TypeScriptImportReference> ResolveImportsWithinTypeScriptFile(
            IEnumerable<string> validationMatches,
            string contents,
            string filePath,
            string basePath,
            string[] ignorePaths,
            params string[] modulesFolders
            )
        {
            var searchPaths = new List<string>();
            searchPaths.Add(basePath);
            searchPaths.AddRange(modulesFolders);
            var imports = new List<TypeScriptImportReference>();
            foreach (var classToImport in validationMatches)
            {
                if (Regex.IsMatch(contents, "\\b" + classToImport + "\\b"))
                {
                    foreach (var searchPathRoot in searchPaths)
                    {
                        var searchPath = searchPathRoot;
                        var isModulesPath = false;
                        if (modulesFolders.Contains(searchPath))
                        {
                            isModulesPath = true;
                            searchPath = Path.Combine(searchPath, "@brandless");
                        }
                        var validationClassPath = "";
                        var files = GetTypeScriptFiles(searchPath, isModulesPath, validationMatches);
                        if (files.ContainsKey(classToImport))
                        {
                            validationClassPath = files[classToImport].Path;
                            var path = validationClassPath;
                            if (validationClassPath != null && ignorePaths.Any(
                                ip => ip.Equals(path, StringComparison.OrdinalIgnoreCase)))
                            {
                                validationClassPath = null;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(validationClassPath))
                        {
                            var relativePath = "";
                            if (isModulesPath)
                            {
                                relativePath =
                                    Path.GetFileName(searchPath) + Path.AltDirectorySeparatorChar + validationClassPath.Substring(searchPath.Length).Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar).First();
                            }
                            else
                            {
                                var path1 = new Uri(filePath);
                                var path2 = new Uri(validationClassPath);
                                var diff = path1.MakeRelativeUri(path2);
                                relativePath = diff.OriginalString;
                                if (!relativePath.StartsWith("."))
                                {
                                    relativePath = "./" + relativePath;
                                }
                                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(relativePath);
                                // Deal with .d.ts
                                if (fileNameWithoutExtension.EndsWith(".d", StringComparison.OrdinalIgnoreCase))
                                {
                                    fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileNameWithoutExtension);
                                }
                                relativePath = Path.Combine(Path.GetDirectoryName(relativePath),
                                    fileNameWithoutExtension).Replace('\\', '/');
                            }
                            imports.Add(new TypeScriptImportReference(relativePath, classToImport));
                        }
                    }
                }
            }
            return imports;
        }
    }

    public enum OutputType
    {
        TypeScript,
        CSharp
    }
}