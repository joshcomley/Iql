﻿using System;
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
            IqlQueryableAdapter.ExpressionConverter = () => new DotNetExpressionToIqlConverter();

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
            var javaScript = JavaScriptIqlParser.GetJavaScript(iql);
            var odata = ODataIqlParser.GetOData(iql, null);

            await Print("Expression resolved", iql.GetType().Name);
            await Print("JavaScript", javaScript.Expression);
            await Print("OData", odata);

            await Print("Data access", async () => await TestDb.Run());
        }

        static async Task Print(string title, string result)
        {
            await Print(title, async () => Console.WriteLine(result));
        }

        static async Task Print(string title, Func<Task> result)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(title + ":");
            await result();
        }
    }
}
