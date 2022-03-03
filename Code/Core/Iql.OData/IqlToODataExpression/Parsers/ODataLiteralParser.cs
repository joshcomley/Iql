using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Parsing.Types;
using Newtonsoft.Json;

namespace Iql.OData.IqlToODataExpression.Parsers
{
    public class ODataLiteralParser : ODataActionParserBase<IqlLiteralExpression>
    {
        public override IqlExpression ToQueryString(IqlLiteralExpression action,
            ODataIqlParserContext parser)
        {
            var value = action.Value;
            return new IqlFinalExpression<string>(
                action.Kind == IqlExpressionKind.Final
                    ? (value ?? "").ToString()
                    : ODataEncode(value, null, action.ReturnType, parser.TypeResolver)
            );
        }

        public static string ODataEncode(object value,
            Type valueType = null,
            IqlType iqlType = IqlType.Unknown,
            ITypeResolver typeResolver = null)
        {
            return ODataEncodeInternal(
                0,
                new List<object>(),
                value,
                valueType,
                iqlType,
                typeResolver);
        }

        private static string ODataEncodeInternal(
            int depth,
            List<object> references,
            object value,
            Type valueType = null,
            IqlType iqlType = IqlType.Unknown,
            ITypeResolver typeResolver = null)
        {
            if (depth > 20)
            {
                return "";
            }

            references.Add(value);
            if (value == null)
            {
                return "null";
            }

            if (iqlType == IqlType.Guid)
            {
                return (value ?? "").ToString();
            }

            if (value is Int64)
            {
                return value.ToString();
            }

            if (value is string)
            {
                var str = value as string;
                str = Regex.Replace(str, "'", "''");
                return $"\'{str}\'";
            }

            if (value is DateTime || value is DateTimeOffset)
            {
                var v = JsonConvert.SerializeObject(value);
                if (v.StartsWith("'") || v.StartsWith("\""))
                {
                    v = v.Substring(1, v.Length - 2);
                }

                if (!v.EndsWith("Z"))
                {
                    v = $"{v}Z";
                }

                return $"'{v}'";
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

            var type = valueType ?? value.GetType();
            IIqlTypeMetadata entityType = null;
            if (typeResolver != null)
            {
                entityType = typeResolver.FindTypeByType(type);
            }

            var propertyValues = new List<string>();
            PropertyInfo[] properties;
            #if !TypeScript
            if (valueType?.IsValueType != true)
            #endif
            {
                if (entityType == null || !(entityType is EntityConfigurationTypeProvider))
                {
                    properties = value.GetType().GetRuntimeProperties().ToArray();
                }
                else
                {
                    properties = (entityType as EntityConfigurationTypeProvider).GetProperties();
                }

                if (properties != null && properties.Length > 0)
                {
                    foreach (var property in properties)
                    {
                        var childValue = property.GetValue(value);
                        if (!references.Contains(childValue))
                        {
                            var encodedValue = ODataEncodeInternal(
                                depth + 1,
                                references,
                                childValue,
                                property.PropertyType,
                                IqlType.Unknown,
                                typeResolver);
                            propertyValues.Add($"{property.Name}:{encodedValue}");
                        }
                    }

                    return $"{{{string.Join(",", propertyValues)}}}";
                }
            }

            if (JsonConvert.SerializeObject(value) == JsonConvert.SerializeObject(new { }))
            {
                return "{}";
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

            var serializedPoints =
                string.Join(",", pointsCopy.Select(_ => $"{RoundForOData(_.X)} {RoundForOData(_.Y)}"));
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