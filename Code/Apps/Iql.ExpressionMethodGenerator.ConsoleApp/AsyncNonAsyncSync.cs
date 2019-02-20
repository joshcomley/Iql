using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Iql.ExpressionMethodGenerator.ConsoleApp
{
    public class AsyncNonAsyncSync : MethodGenerator
    {
        public static void Sync()
        {
            var file = "ActionParserContext.cs";
            var sourceFile = Directory.EnumerateFiles(ResolveIqlSolutionDirectory(), $"Async{file}", SearchOption.AllDirectories).Single();
            var targetFile = Path.Combine(Path.GetDirectoryName(sourceFile), file);
            var text = File.ReadAllText(sourceFile);
            text = Regex.Replace(text, @"Task\<(?<Return>.*?)\>\s", "${Return} ");
            text = text.Replace("async ", "");
            text = text.Replace("await ", "");
            text = text.Replace("Async", "");
            text = $@"// DO NOT EDIT - this code was auto generated from ""{Path.GetFileName(sourceFile)}""

{text}";
            File.WriteAllText(targetFile, text);
        }
    }
}