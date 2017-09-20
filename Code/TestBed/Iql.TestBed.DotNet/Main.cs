using System;
using Iql.DotNet.Serialization;
using Iql.JavaScript;
using Iql.JavaScript.IqlToJavaScript.Parsers;
using Iql.OData.Parsers;

namespace Iql.TestBed.DotNet
{
    public class Main
    {
        public static void Run()
        {
            var body = JavaScriptCodeExtractor.ExtractBody("function(p) { return p.Id; }");
            body = JavaScriptCodeExtractor.ExtractBody("p => p.Id");
            body = JavaScriptCodeExtractor.ExtractBody("s => s.Name.includes(\"o\")");

            var xml = IqlSerializer.SerializeToXml<Person>(
                s => s.Title != "Josh" || s.Description == "Josh");
            //xml = IqlSerializer.SerializeToXml<Person>(
            //    p => p.Name == "Josh");
            //Console.WriteLine("XML:");
            //Console.WriteLine(xml);

            Console.WriteLine();
            Console.WriteLine();
            var iql = IqlSerializer.DeserializeFromXml(xml);
            Console.WriteLine("Expression resolved:");
            Console.WriteLine(iql.GetType().Name);

            Console.WriteLine();
            Console.WriteLine();
            var javaScript =
                JavaScriptIqlParser.GetJavaScript(iql, null);
            Console.WriteLine("JavaScript:");
            Console.WriteLine(javaScript.Expression);

            //var odata =
            //    ODataIqlParser.GetOData(iql, null);
        }
    }
}
