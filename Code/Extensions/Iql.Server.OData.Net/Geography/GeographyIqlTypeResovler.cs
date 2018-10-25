namespace Iql.Server.OData.Net.Geography
{
    public class GeographyIqlTypeResovler : GeographyResolver<IqlType>
    {
        public override IqlType ResolveFromGeographyPoint()
        {
            return IqlType.GeographyPoint;
        }

        public override IqlType ResolveFromGeographyLine()
        {
            return IqlType.GeographyLine;
        }

        public override IqlType ResolveFromGeographyMultiLine()
        {
            return IqlType.GeographyMultiLine;
        }

        public override IqlType ResolveFromGeographyPolygon()
        {
            return IqlType.GeographyPolygon;
        }

        public override IqlType ResolveFromGeographyMultiPolygon()
        {
            return IqlType.GeographyMultiPolygon;
        }

        public override IqlType ResolveFromGeometryPoint()
        {
            return IqlType.GeometryPoint;
        }

        public override IqlType ResolveFromGeometryLine()
        {
            return IqlType.GeometryLine;
        }

        public override IqlType ResolveFromGeometryMultiLine()
        {
            return IqlType.GeometryMultiLine;
        }

        public override IqlType ResolveFromGeometryPolygon()
        {
            return IqlType.GeometryPolygon;
        }

        public override IqlType ResolveFromGeometryMultiPolygon()
        {
            return IqlType.GeometryMultiPolygon;
        }
    }
}