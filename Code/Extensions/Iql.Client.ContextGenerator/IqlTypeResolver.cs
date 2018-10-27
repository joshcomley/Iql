using Iql.OData.TypeScript.Generator.Definitions;

namespace Iql.OData.TypeScript.Generator
{
    public static class IqlTypeResolver
    {
        public static IqlType ResolveIqlType(
            this ITypeInfo type)

        {
            if (type.EdmType.StartsWith("Collection("))
            {
                return IqlType.Collection;
            }

            switch (type.EdmType)
            {
                case "String":
                case "Edm.String":
                case "Guid":
                case "Edm.Guid":
                    return IqlType.String;
                case "Int32":
                case "Edm.Int32":
                case "Int64":
                case "Edm.Int64":
                    return IqlType.Integer;
                case "Double":
                case "Edm.Double":
                case "Single":
                case "Edm.Single":
                case "Decimal":
                case "Edm.Decimal":
                    return IqlType.Decimal;
                case "Boolean":
                case "Edm.Boolean":
                    return IqlType.Boolean;
                case "DateTimeOffset":
                case "Edm.DateTimeOffset":
                    return IqlType.Date;
                case "GeometryPoint":
                case "Edm.GeometryPoint":
                    return IqlType.GeometryPoint;
                case "GeometryMultiPoint":
                case "Edm.GeometryMultiPoint":
                    return IqlType.GeometryMultiPoint;
                case "GeometryPolygon":
                case "Edm.GeometryPolygon":
                    return IqlType.GeometryPolygon;
                case "GeometryMultiPolygon":
                case "Edm.GeometryMultiPolygon":
                    return IqlType.GeometryMultiPolygon;
                case "GeometryLineString":
                case "Edm.GeometryLineString":
                    return IqlType.GeometryLine;
                case "GeometryMultiLineString":
                case "Edm.GeometryMultiLineString":
                    return IqlType.GeometryMultiLine;
                case "GeographyPoint":
                case "Edm.GeographyPoint":
                    return IqlType.GeographyPoint;
                case "GeographyMultiPoint":
                case "Edm.GeographyMultiPoint":
                    return IqlType.GeographyMultiPoint;
                case "GeographyPolygon":
                case "Edm.GeographyPolygon":
                    return IqlType.GeographyPolygon;
                case "GeographyMultiPolygon":
                case "Edm.GeographyMultiPolygon":
                    return IqlType.GeographyMultiPolygon;
                case "GeographyLineString":
                case "Edm.GeographyLineString":
                    return IqlType.GeographyLine;
                case "GeographyMultiLineString":
                case "Edm.GeographyMultiLineString":
                    return IqlType.GeographyMultiLine;
            }

            if (type.IsEnum)
            {
                return IqlType.Enum;
            }
            return IqlType.Unknown;
        }
    }
}