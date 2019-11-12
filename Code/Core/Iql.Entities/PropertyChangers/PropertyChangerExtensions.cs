using Iql.Entities.Extensions;

namespace Iql.Entities.PropertyChangers
{
    public static class PropertyChangerExtensions
    {
        public static PropertyChanger ResolveChanger(this ITypeDefinition typeDefinition)
        {
            if (typeDefinition != null)
            {
                var iqlType = typeDefinition.ToIqlType();
                switch (iqlType)
                {
                    case IqlType.Date:
                        return DatePropertyChanger.Instance;
                    case IqlType.GeographyPolygon:
                    case IqlType.GeometryPolygon:
                        return PolygonPropertyChanger.Instance;
                    case IqlType.GeographyPoint:
                    case IqlType.GeometryPoint:
                        return PointPropertyChanger.Instance;
                    case IqlType.Collection:
                        return CollectionPropertyChanger.Instance;
                }
            }
            return PrimitivePropertyChanger.Instance;
        }
    }
}