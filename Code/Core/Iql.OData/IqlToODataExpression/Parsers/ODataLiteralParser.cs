using System;
using System.Text.RegularExpressions;

namespace Iql.OData.IqlToODataExpression.Parsers
{
    public class ODataLiteralParser : ODataActionParserBase<IqlLiteralExpression>
    {
        public override IqlExpression ToQueryString(IqlLiteralExpression action,
            ODataIqlParserInstance parser)
        {
            var value = action.Value;
            return new IqlFinalExpression<string>(ODataEncode(value));
        }

        public static string ODataEncode(object value)
        {
            if (value is string)
            {
                var str = value as string;
                str = Regex.Replace(str, "'", "''");
                return $"\'{str}\'";
            }

            if (value is DateTime)
            {
                var dateTime = (DateTime)value;
                return dateTime.ToString("o");
            }

            if (value is DateTimeOffset)
            {
                var dateTimeOffset = (DateTimeOffset)value;
                return dateTimeOffset.ToString("o");
            }

            if (value is bool)
            {
                var b = (bool)value;
                return b ? "true" : "false";
            }

            return value == null ? "null" : value.ToString();
        }
    }
}