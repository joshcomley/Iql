using Iql.DotNet;
using System;
using Iql.DotNet.Serialization;
using Iql.JavaScript.IqlToJavaScript.Parsers;

namespace Iql.TestBed.DotNet
{
    public class Main
    {
        public static void Run()
        {
            var xml = IqlSerializer.SerializeToXml<Person>(
                p => p.Name == "Josh" || p.Age > 20);
            Console.WriteLine("XML:");
            Console.WriteLine(xml);

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
        }
    }
}
