using System.Text.RegularExpressions;
using Iql.Parsing;

namespace Iql.OData.Parsers
{
    public class ODataLiteralParser : ODataActionParserBase<IqlLiteralExpression>
    {
        public override IqlExpression ToQueryString(IqlLiteralExpression action,
            ODataIqlParserInstance parser)
        {
            if (action.Value is string)
            {
                var str = action.Value as string;
                str = Regex.Replace(str, "'", "''");
                return new IqlAggregateExpression(
                    new IqlFinalExpression("'"), new IqlFinalExpression(str), new IqlFinalExpression("'"));
            }
            return new IqlFinalExpression(action.Value.ToString());
        }
    }
}