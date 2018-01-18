using System;
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
                    new IqlFinalExpression<string>("'"),
                    new IqlFinalExpression<string>(str),
                    new IqlFinalExpression<string>("'"));
            }
            else if (action.Value is DateTime)
            {
                var dateTime = (DateTime)action.Value;
                return new IqlFinalExpression<string>(dateTime.ToString("o"));
            }
            else if (action.Value is DateTimeOffset)
            {
                var dateTimeOffset = (DateTimeOffset)action.Value;
                return new IqlFinalExpression<string>(dateTimeOffset.ToString("o"));
            }
            return new IqlFinalExpression<string>(action.Value.ToString());
        }
    }
}