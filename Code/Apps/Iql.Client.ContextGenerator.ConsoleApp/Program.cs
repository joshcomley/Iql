using System;
using System.IO;
using System.Threading.Tasks;
using Iql.OData.TypeScript.Generator;
using Iql.OData.TypeScript.Generator.DataContext;
using TypeSharp.Conversion;

namespace Iql.Client.ContextGenerator.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var applicationPath = @"D:\Code\OData\OnlineOffline\Web\Angular\src\app\isite";
            //applicationPath = @"D:\Code\Iql\Code\SampleApps\Iql-TypeScript.Compiled\src\app";
            //applicationPath = @"D:\code\i-site\Code\Web\i-site-web\src\app";
            //applicationPath = @"C:\Users\joshc\Source\Repos\Angular5Hackathon\JStarter\ClientApp\app";
            //var outputSubFolder = @"External\DataContext";
            //var outputType = OutputType.TypeScript;

            //applicationPath = @"D:\Code\Iql\Code\TestBed\Iql.TestBed\Context";
            //outputSubFolder = "";
            //outputType = OutputType.CSharp;
            //url = "http://localhost:28000/odata/$metadata";

            var iqlSampleUrl = "http://localhost:64000/odata/$metadata";
            var scopeUrl = "http://localhost:46000/odata/$metadata";
            var isiteUrl = "http://localhost:48000/odata/$metadata";
            var iqlUrl = "http://localhost:28000/odata/$metadata";
            var hazceptionUrl = "http://localhost:58000/odata/$metadata";
            switch ("todoot")
            {
                case "todoot":
                    await GenerateWebAndMobile(
                        "http://localhost:47000/odata/$metadata",
                        @"D:\Code\Apps\todoot\Code\Web\ClientApp\app\generated\DataContext",
                        @"D:\Code\Apps\todoot\Code\Mobile\todoot\src\app\generated\DataContext");
                    //var isiteSettings = new GeneratorSettings("ISite.App.Data.Entities", null);
                    //isiteSettings.GenerateCountProperties = false;
                    //isiteSettings.GenerateEntities = false;
                    //isiteSettings.GenerateEntitySets = false;
                    //isiteSettings.GenerateDataContext = true;
                    //isiteSettings.ConfigureOData = false;
                    //GenerateWithSettings(isiteUrl, OutputType.CSharp, @"D:\Code\i-site\Code\Api\src\ISite.App.Data\", @"IqlContext", isiteSettings);
                    break;
                case "isite":
                    // D:\Code\Playgrounds\lazyNinjas
                    //await GenerateAsync(isiteUrl, OutputType.TypeScript, @"D:\Code\i-site\Code\Mobile\i-site\", @"D:\Code\i-site\Code\Mobile\i-site\src\", @"app\generated\DataContext");
                    await GenerateWebAndMobile(isiteUrl, @"D:\Code\i-site\Code\Web", @"D:\Code\i-site\Code\Mobile\i-site");
                    //var isiteSettings = new GeneratorSettings("ISite.App.Data.Entities", null);
                    //isiteSettings.GenerateCountProperties = false;
                    //isiteSettings.GenerateEntities = false;
                    //isiteSettings.GenerateEntitySets = false;
                    //isiteSettings.GenerateDataContext = true;
                    //isiteSettings.ConfigureOData = false;
                    //GenerateWithSettings(isiteUrl, OutputType.CSharp, @"D:\Code\i-site\Code\Api\src\ISite.App.Data\", @"IqlContext", isiteSettings);
                    break;
                //case "isite-old":
                //    await GenerateAsync(isiteUrl, OutputType.TypeScript, @"D:\Code\i-site-old\Code\Web\ClientApp\", @"app\generated\DataContext");
                //    var isiteOldSettings = new GeneratorSettings("ISite.App.Data.Entities", null);
                //    isiteOldSettings.GenerateCountProperties = false;
                //    isiteOldSettings.GenerateEntities = false;
                //    isiteOldSettings.GenerateEntitySets = false;
                //    isiteOldSettings.GenerateDataContext = true;
                //    isiteOldSettings.ConfigureOData = false;
                //    await GenerateWithSettingsAsync(isiteUrl, OutputType.CSharp, @"D:\Code\i-site-old\Code\Api\src\ISite.App.Data\", @"IqlContext", isiteOldSettings);
                //    break;
                //case "iqltests":
                //    await GenerateAsync(isiteUrl, OutputType.CSharp, @"D:\Code\Iql\Code\Tests\Iql.Tests\", @"Context");
                //    break;
                //case "tunneliqltests":
                //    await GenerateAsync(tunnelUrl, OutputType.CSharp, new[] { @"D:\Code\Iql\Code\Tests\Iql.Tests.Data\", @"D:\Code\Brandless\Iql\Code\Tests\Iql.Tests.Data\" }.First(Directory.Exists), @"Context");
                //    break;
                case "iql":
                    await GenerateAsync(
                        iqlUrl, 
                        OutputKind.CSharp,
                        @"D:\Code\Brandless\Iql\Code\Tests\Iql.Tests.Data\Context");
                    break;
                    //case "hazception-mobile":
                    //    await GenerateAsync(hazceptionUrl, OutputType.TypeScript, @"D:\Code\Hazception.App\Code\Mobile\Hazception\app\", @"DataContext\Generated");
                    //    break;
                    //case "isite-mobile":
                    //    await GenerateAsync(isiteUrl, OutputType.TypeScript, @"D:\Code\i-site\Code\Mobile\i-site3\app\", @"DataContext\Generated");
                    //    break;
                    //case "scope":
                    //    await GenerateAsync(scopeUrl, OutputType.TypeScript, @"D:\Code\Scope.App\Code\Web\ClientApp\", @"app\generated\DataContext");
                    //    break;
                    //case "iqlsample":
                    //    await GenerateAsync(iqlSampleUrl, OutputType.TypeScript, @"D:\Code\Brandless\IqlSample\Code\Web\ClientApp\", @"app\generated\DataContext");
                    //    break;
                    //case "hazception":
                    //    await GenerateAsync(hazceptionUrl, OutputType.TypeScript, @"D:\Code\Hazception.App\Code\Web3\ClientApp\", @"app\External\DataContext");
                    //    break;
                    //case "hazception-cs":
                    //    await GenerateAsync(hazceptionUrl, OutputType.CSharp, @"D:\Code\Iql\Code\Tests\Iql.Tests.Data\", @"Context", name =>
                    //    {
                    //        switch (name)
                    //        {
                    //            case "Client":
                    //            case "ClientType":
                    //            case "UserType":
                    //            case "ApplicationUser":
                    //                return "Haz" + name;
                    //        }
                    //        return name;
                    //    });
                    //    break;
            }
        }

        private static async Task GenerateWebAndMobile(string url, string webOutputPath, string mobileOutputPath)
        {
            var generatedContexts = await GenerateAsync(url, OutputKind.TypeScript);
            generatedContexts.TypeScriptContext.SaveTo(webOutputPath, true);
            generatedContexts.TypeScriptContext.SaveTo(mobileOutputPath, true);
            //foreach (var path in new[] {webOutputPath, mobileOutputPath})
            //{
            //}
            //var copyTo = Path.Combine(mobileOutputPath);
            //if (!Directory.Exists(copyTo))
            //{
            //    Directory.CreateDirectory(copyTo);
            //}

            //foreach (var file in Directory.EnumerateFiles(
            //    Path.Combine(webOutputPath, outputSubFolder)))
            //{
            //    Console.WriteLine("Copying:");
            //    Console.WriteLine(file);
            //    var destFileName = Path.Combine(
            //        copyTo,
            //        Path.GetFileName(file));
            //    Console.WriteLine("To:");
            //    Console.WriteLine(destFileName);
            //    File.Copy(file, destFileName, true);
            //}
        }

        private static Task<GeneratedContexts> GenerateAsync(string odataSchemaUrl, OutputKind outputKind, string outputFolder = null, Func<string, string> nameMapper = null, string @namespace = null, string iqlSchemaUrl = null)
        {
            return GenerateWithSettingsAsync(odataSchemaUrl, outputKind, outputFolder, new GeneratorSettings(@namespace, nameMapper), iqlSchemaUrl);
        }

        private static Task<GeneratedContexts> GenerateWithSettingsAsync(
            string odataSchemaUrl, 
            OutputKind outputKind, 
            string outputFolder, 
            GeneratorSettings settings = null, 
            string iqlSchemaUrl = null)
        {
            settings = settings ?? new GeneratorSettings(null, null);
            var generator = new IqlDataContextGenerator(
                odataSchemaUrl,
                iqlSchemaUrl,
                outputFolder,
                settings);
            generator.NameMapper = settings.NameMapper;
            return generator
                .GenerateDataContextAsync(outputKind);
        }
    }
}
