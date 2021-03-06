using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.DotNet;
using Iql.DotNet.Serialization;
using Iql.JavaScript.IqlToJavaScript.Parsers;
using Iql.OData.Parsers;
using Iql.Parsing;
using Iql.Queryable;

namespace Iql.TestBed
{
    public class TestIql
    {
        public async Task Run()
        {
            IqlQueryableAdapter.ExpressionConverter = () => new DotNetExpressionToIqlConverter();
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
                        person.Title;
            var propertyIql = ExpressionToIqlExpressionParser<Person>.Parse(
                propertyExpression
#if TypeScript
                , null
#endif
                );
            Expression<Func<Person, bool>> validationExpression =
                    person =>
                        //person.Name.Length > 5
                            person.Title == x && person.TypeId > 7 // ||
                //person.Age > (5 * y)
                ;
            var iql = ExpressionToIqlExpressionParser<Person>.Parse(
                validationExpression
#if TypeScript
                , null
#endif
                );
            var parser =
                new ODataIqlParserInstance(new ODataIqlExpressionAdapter());
            var odata = parser.Parse(iql);
            Console.WriteLine("OData:");
            Console.WriteLine(odata);
            Console.WriteLine();
            //var iql = ExpressionToIqlExpressionParser<Person>.Parse(validationExpression);
            //var iql = ExpressionToIqlExpressionParser<Person>.ParseToXml(validationExpression);
            //var iqlExp = Iql.Serialization.IqlSerializer.DeserializeFromXml(iql);
            var exp = JavaScriptIqlParser.GetJavaScript(iql);
            Console.WriteLine("JavaScript:");
            Console.WriteLine(exp.Expression);
            Console.WriteLine();
            var validation = IqlSerializer.SerializeToXml(validationExpression);
            Console.WriteLine("IQL:");
            Console.WriteLine(validation.ToCharArray());
        }
    }
}