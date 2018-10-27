using System;

namespace Iql
{
    public abstract class IqlSridExpression : IqlReferenceExpression, ISrid
    {
        private int? _srid;

        private bool _sridHasBeenSet = false;
        public int? Srid
        {
            get
            {
                if (!_sridHasBeenSet && _srid == null)
                {
                    switch (ReturnType)
                    {
                        case IqlType.GeographyLine:
                        case IqlType.GeographyMultiLine:
                        case IqlType.GeographyPoint:
                        case IqlType.GeographyMultiPoint:
                        case IqlType.GeographyPolygon:
                        case IqlType.GeographyMultiPolygon:
                            return IqlConstants.DefaultGeographicSrid;
                    }
                }
                return _srid;
            }
            set
            {
                _sridHasBeenSet = true;
                _srid = value;
            }
        }

        protected IqlSridExpression(
            int? srid,
            IqlType returnType,
            IqlReferenceExpression parent = null,
            IqlExpressionKind? kind = null) : base(kind ?? ResolveKind(returnType), returnType, parent)
        {
            if (srid != null)
            {
                Srid = srid;
                if (srid != 0)
                {
                    switch (returnType)
                    {
                        case IqlType.GeometryLine:
                            ReturnType = IqlType.GeographyLine;
                            break;
                        case IqlType.GeometryMultiLine:
                            ReturnType = IqlType.GeographyMultiLine;
                            break;
                        case IqlType.GeometryPoint:
                            ReturnType = IqlType.GeographyPoint;
                            break;
                        case IqlType.GeometryMultiPoint:
                            ReturnType = IqlType.GeographyMultiPoint;
                            break;
                        case IqlType.GeometryPolygon:
                            ReturnType = IqlType.GeographyPolygon;
                            break;
                        case IqlType.GeometryMultiPolygon:
                            ReturnType = IqlType.GeographyMultiPolygon;
                            break;
                        case IqlType.GeometryRing:
                            ReturnType = IqlType.GeographyRing;
                            break;
                    }
                }
            }
        }

        private static IqlExpressionKind ResolveKind(IqlType returnType)
        {
            switch (returnType)
            {
                case IqlType.GeographyPoint:
                case IqlType.GeometryPoint:
                    return IqlExpressionKind.GeoPoint;
                case IqlType.GeographyMultiPoint:
                case IqlType.GeometryMultiPoint:
                    return IqlExpressionKind.GeoMultiPoint;
                case IqlType.GeographyPolygon:
                case IqlType.GeometryPolygon:
                    return IqlExpressionKind.GeoPolygon;
                case IqlType.GeographyMultiPolygon:
                case IqlType.GeometryMultiPolygon:
                    return IqlExpressionKind.GeoMultiPolygon;
                case IqlType.GeographyLine:
                case IqlType.GeometryLine:
                    return IqlExpressionKind.GeoLine;
                case IqlType.GeographyMultiLine:
                case IqlType.GeometryMultiLine:
                    return IqlExpressionKind.GeoMultiLine;
                case IqlType.GeographyRing:
                case IqlType.GeometryRing:
                    return IqlExpressionKind.GeoRing;
            }
            throw new NotImplementedException();
        }
    }
}