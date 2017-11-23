using System.Text.RegularExpressions;

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
                    new IqlFinalExpression<string>("'"), new IqlFinalExpression<string>(str), new IqlFinalExpression<string>("'"));
            }
            return new IqlFinalExpression<string>(action.Value.ToString());
        }
    }
}