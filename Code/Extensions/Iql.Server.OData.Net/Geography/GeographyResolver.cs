using System;
using Microsoft.Spatial;
using NetTopologySuite.Geometries;

namespace Iql.Server.OData.Net.Geography
{
    public abstract class GeographyResolver<T>
    {
        public T Resolve(Type type)
        {
            // Assume Geography
            if (type == typeof(GeographyPoint) || type == typeof(Point))
            {
                return ResolveFromGeographyPoint();
            }
            if (type == typeof(GeographyMultiPoint) || type == typeof(MultiPoint))
            {
                return ResolveFromGeographyMultiPoint();
            }
            if (type == typeof(GeographyLineString) || type == typeof(LineString))
            {
                return ResolveFromGeographyLine();
            }
            if (type == typeof(GeographyMultiLineString) || type == typeof(MultiLineString))
            {
                return ResolveFromGeographyMultiLine();
            }
            if (type == typeof(GeographyPolygon) || type == typeof(Polygon))
            {
                return ResolveFromGeographyPolygon();
            }
            if (type == typeof(GeographyMultiPolygon) || type == typeof(MultiPolygon))
            {
                return ResolveFromGeographyMultiPolygon();
            }
            if (type == typeof(GeometryPoint))
            {
                return ResolveFromGeometryPoint();
            }
            if (type == typeof(GeometryMultiPoint))
            {
                return ResolveFromGeometryMultiPoint();
            }
            if (type == typeof(GeometryLineString))
            {
                return ResolveFromGeometryLine();
            }
            if (type == typeof(GeometryMultiLineString))
            {
                return ResolveFromGeometryMultiLine();
            }
            if (type == typeof(GeometryPolygon))
            {
                return ResolveFromGeometryPolygon();
            }
            if (type == typeof(GeometryMultiPolygon))
            {
                return ResolveFromGeometryMultiPolygon();
            }

            return default(T);
        }

        public abstract T ResolveFromGeographyPoint();
        public abstract T ResolveFromGeographyMultiPoint();
        public abstract T ResolveFromGeographyLine();
        public abstract T ResolveFromGeographyMultiLine();
        public abstract T ResolveFromGeographyPolygon();
        public abstract T ResolveFromGeographyMultiPolygon();
        public abstract T ResolveFromGeometryPoint();
        public abstract T ResolveFromGeometryMultiPoint();
        public abstract T ResolveFromGeometryLine();
        public abstract T ResolveFromGeometryMultiLine();
        public abstract T ResolveFromGeometryPolygon();
        public abstract T ResolveFromGeometryMultiPolygon();
    }
}