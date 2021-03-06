using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Iql.OData.IqlToODataExpression.Parsers
{
    public class ODataLiteralParser : ODataActionParserBase<IqlLiteralExpression>
    {
        public override IqlExpression ToQueryString(IqlLiteralExpression action,
            ODataIqlParserContext parser)
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

            if (value is IqlPointExpression)
            {
                var point = value as IqlPointExpression;
                return
                    $@"geography'SRID={TryGetSrid(value)};POINT({RoundForOData(point.X)} {RoundForOData(point.Y)})'";
            }

            if (value is IqlPolygonExpression)
            {
                var polygon = value as IqlPolygonExpression;
                var pointsExpressions = new List<IPointsExpression>();
                pointsExpressions.Add(polygon.OuterRing);
                if (polygon.InnerRings != null)
                {
                    pointsExpressions.AddRange(polygon.InnerRings);
                }
                return
                    $@"geography'SRID={TryGetSrid(value)};POLYGON({string.Join(",", pointsExpressions.Select(_ => SerializePoints(_, true, true)))})'";
            }

            if (value is IqlLineExpression)
            {
                var line = value as IqlLineExpression;
                return
                    $@"geography'SRID={TryGetSrid(value)};LINESTRING({SerializePoints(line)})'";
            }

            if (value is bool)
            {
                var b = (bool)value;
                return b ? "true" : "false";
            }

            return value == null ? "null" : value.ToString();
        }

        private static int TryGetSrid(object value)
        {
            if (value is ISrid)
            {
                return (value as ISrid).Srid ?? 0;
            }

            return 0;
        }

        private static string SerializePoints(IPointsExpression line, bool isRing = false, bool wrapInBrackets = false)
        {
            var pointsCopy = line.Points.ToList();
            if (isRing && pointsCopy.Count > 0)
            {
                var first = pointsCopy[0];
                var last = pointsCopy[pointsCopy.Count - 1];
                if (pointsCopy.Count == 1 || (first.X != last.X || first.Y != last.Y))
                {
                    pointsCopy.Add(new IqlPointExpression(first.X, first.Y));
                }
            }
            var serializedPoints = string.Join(",", pointsCopy.Select(_ => $"{RoundForOData(_.X)} {RoundForOData(_.Y)}"));
            return
                wrapInBrackets
                ? $"({serializedPoints})"
                : serializedPoints;
        }

        private static double RoundForOData(double num)
        {
            return Math.Round(num, 6);
        }
    }
}