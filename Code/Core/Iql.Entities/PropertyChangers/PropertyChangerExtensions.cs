using System.Collections.Generic;
using Iql.Entities.Extensions;

namespace Iql.Entities.PropertyChangers
{
    public static class PropertyChangerExtensions
    {
        private static readonly Dictionary<ITypeDefinition, PropertyChanger> PropertyChangerCache = new Dictionary<ITypeDefinition, PropertyChanger>();
        public static PropertyChanger ResolveChanger(this ITypeDefinition typeDefinition)
        {
            if (!PropertyChangerCache.ContainsKey(typeDefinition))
            {
                PropertyChangerCache.Add(typeDefinition, PropertyChangerInternal(typeDefinition));
            }
            return PropertyChangerCache[typeDefinition];
        }

        private static PropertyChanger PropertyChangerInternal(ITypeDefinition typeDefinition)
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