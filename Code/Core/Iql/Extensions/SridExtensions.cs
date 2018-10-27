namespace Iql.Extensions
{
    public static class SridExtensions
    {
        public static IqlType ResolveTypeFromSrid(this int? srid, IqlExpressionKind kind)
        {
            if (srid != null && srid != 0)
            {
                switch (kind)
                {
                    case IqlExpressionKind.GeoLine:
                        return IqlType.GeographyLine;
                    case IqlExpressionKind.GeoMultiLine:
                        return IqlType.GeographyMultiLine;
                    case IqlExpressionKind.GeoPoint:
                        return IqlType.GeographyPoint;
                    case IqlExpressionKind.GeoMultiPoint:
                        return IqlType.GeographyMultiPoint;
                    case IqlExpressionKind.GeoPolygon:
                        return IqlType.GeographyPolygon;
                    case IqlExpressionKind.GeoMultiPolygon:
                        return IqlType.GeographyMultiPolygon;
                    case IqlExpressionKind.GeoRing:
                        return IqlType.GeographyRing;
                }
            }
            switch (kind)
            {
                case IqlExpressionKind.GeoLine:
                    return IqlType.GeometryLine;
                case IqlExpressionKind.GeoMultiLine:
                    return IqlType.GeometryMultiLine;
                case IqlExpressionKind.GeoPoint:
                    return IqlType.GeometryPoint;
                case IqlExpressionKind.GeoMultiPoint:
                    return IqlType.GeometryMultiPoint;
                case IqlExpressionKind.GeoPolygon:
                    return IqlType.GeometryPolygon;
                case IqlExpressionKind.GeoMultiPolygon:
                    return IqlType.GeometryMultiPolygon;
                case IqlExpressionKind.GeoRing:
                    return IqlType.GeometryRing;
            }
            return IqlType.Unknown;
        }
    }
}