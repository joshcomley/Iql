using System;
using NetTopologySuite.Geometries;

namespace Iql.Server.OData.Net.Geography
{
    public class GeographyTypeResovler : GeographyResolver<Type>
    {
        public override Type ResolveFromGeographyPoint()
        {
            return typeof(Point);
        }

        public override Type ResolveFromGeographyMultiPoint()
        {
            return typeof(MultiPoint);
        }

        public override Type ResolveFromGeographyLine()
        {
            return typeof(LineString);
        }

        public override Type ResolveFromGeographyMultiLine()
        {
            return typeof(MultiLineString);
        }

        public override Type ResolveFromGeographyPolygon()
        {
            return typeof(Polygon);
        }

        public override Type ResolveFromGeographyMultiPolygon()
        {
            return typeof(MultiPolygon);
        }

        public override Type ResolveFromGeometryPoint()
        {
            return typeof(Point);
        }

        public override Type ResolveFromGeometryMultiPoint()
        {
            return typeof(MultiPoint);
        }

        public override Type ResolveFromGeometryLine()
        {
            return typeof(LineString);
        }

        public override Type ResolveFromGeometryMultiLine()
        {
            return typeof(MultiLineString);
        }

        public override Type ResolveFromGeometryPolygon()
        {
            return typeof(Polygon);
        }

        public override Type ResolveFromGeometryMultiPolygon()
        {
            return typeof(MultiPolygon);
        }
    }
}