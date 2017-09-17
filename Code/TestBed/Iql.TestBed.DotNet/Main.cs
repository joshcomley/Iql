using Iql.DotNet;
using System;

namespace Iql.TestBed.DotNet
{
    public class Main
    {
        public static void Run()
        {
            var xml = ExpressionToIqlExpressionParser<Person>.ParseToXml(
                p => p.Name == "Josh" || p.Age > 20);
            Console.WriteLine(xml);
        }
    }
}
