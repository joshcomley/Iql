using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Iql.OData.IqlToODataExpression.Parsers
{
    public class ODataLiteralParser : ODataActionParserBase<IqlLiteralExpression>
    {
        public override IqlExpression ToQueryString(IqlLiteralExpression action,
            ODataIqlParserInstance parser)
        {
            var value = action.Value;
            return new IqlFinalExpression<string>(ODataEncode(value, action.ReturnType));
        }

        public static string ODataEncode(object value, IqlType type = IqlType.Unknown)
        {
            if (type == IqlType.Guid)
            {
                return (value ?? "").ToString();
            }

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

            if (value is IqlGeographyPointExpression)
            {
                var point = value as IqlGeographyPointExpression;
                return
                    $@"geography'SRID={point.Srid};POINT({point.Y} {point.X})'";
            }

            if (value is IqlGeographyPolygonExpression)
            {
                var polygon = value as IqlGeographyPolygonExpression;
                return
                    $@"geography'SRID={polygon.Srid};POLYGON(({string.Join(",", polygon.Points.Select(_ => $"{_.Y} {_.X}"))}))'";
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