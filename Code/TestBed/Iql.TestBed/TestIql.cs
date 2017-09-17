using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.DotNet;
using Iql.JavaScript.IqlToJavaScript.Parsers;
using Iql.OData.Parsers;
using Iql.Parsing;
using Iql.Queryable;

namespace Iql.OData.TypeScript.Generator.ConsoleApp.Library
{
    public class TestIql
    {
        public async Task Run()
        {
            IqlQueryableAdapter.ExpressionConverter = () => new ExpressionToIqlConverter();
            //TestExpressionToQueryString();
            await TestDb.Run();
        }

        public static void TestExpressionToQueryString()
        {
            var x = "Blah";
            var y = 3;
            Expression<Func<Person, object>> propertyExpression =
                person =>
                    //person.Name.Length > 5
                        person.Name;
            var propertyIql = ExpressionToIqlExpressionParser<Person>.Parse(propertyExpression, null);
            Expression<Func<Person, bool>> validationExpression =
                    person =>
                        //person.Name.Length > 5
                            person.Name == x && person.Age > 7 // ||
                //person.Age > (5 * y)
                ;
            var iql = ExpressionToIqlExpressionParser<Person>.Parse(validationExpression, null);
            var parser =
                new ActionParserInstance<ODataIqlData, ODataIqlExpressionAdapter>(new ODataIqlExpressionAdapter());
            var odata = parser.Parse(iql, null);
            Console.WriteLine("OData:");
            Console.WriteLine(odata);
            Console.WriteLine();
            //var iql = ExpressionToIqlExpressionParser<Person>.Parse(validationExpression);
            //var iql = ExpressionToIqlExpressionParser<Person>.ParseToXml(validationExpression);
            //var iqlExp = Iql.Serialization.IqlSerializer.DeserializeFromXml(iql);
            var exp = JavaScriptIqlParser.GetJavaScript(iql, null);
            Console.WriteLine("JavaScript:");
            Console.WriteLine(exp.Expression);
            Console.WriteLine();
            var validation = ExpressionToIqlExpressionParser<Person>.ParseToXml(validationExpression, null);
            Console.WriteLine("IQL:");
            Console.WriteLine(validation.ToCharArray());
        }
    }
}