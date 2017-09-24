using System;
using System.Threading.Tasks;
using Iql.DotNet;
using Iql.DotNet.Serialization;
using Iql.JavaScript;
using Iql.JavaScript.IqlToJavaScript.Parsers;
using Iql.OData.Parsers;
using Iql.Queryable;

namespace Iql.TestBed.DotNet
{
    public class Main
    {
        public static async Task Run()
        {
            IqlQueryableAdapter.ExpressionConverter = () => new ExpressionToIqlConverter();

            var body = JavaScriptCodeExtractor.ExtractBody("function(p) { return p.Id; }");
            body = JavaScriptCodeExtractor.ExtractBody("p => p.Id");
            body = JavaScriptCodeExtractor.ExtractBody("s => s.Name.includes(\"o\")");

            var xml = IqlSerializer.SerializeToXml<Person>(
                s => s.Title != "Josh" || s.Description == "Josh");
            //xml = IqlSerializer.SerializeToXml<Person>(
            //    p => p.Name == "Josh");
            //Console.WriteLine("XML:");
            //Console.WriteLine(xml);

            var iql = IqlSerializer.DeserializeFromXml(xml);
            var javaScript = JavaScriptIqlParser.GetJavaScript(iql, null);
            var odata = ODataIqlParser.GetOData(iql, null);

            Print("Expression resolved", iql.GetType().Name);
            Print("JavaScript", javaScript.Expression);
            Print("OData", odata);

            Print("Data access", async () => await TestDb.Run());
        }

        static void Print(string title, string result)
        {
            Print(title, () => Console.WriteLine(result));
        }

        static void Print(string title, Action result)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(title + ":");
            result();
        }
    }
}
