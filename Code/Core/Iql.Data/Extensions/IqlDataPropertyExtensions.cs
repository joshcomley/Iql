using Iql.Data.Rendering;
using Iql.Entities;
using Iql.Entities.PropertyGroups.Files;
using Iql.Entities.Relationships;

namespace Iql.Data.Extensions
{
    public static class IqlDataPropertyExtensions
    {
        public static string ResolveKind(this IPropertyContainer property)
        {
            var kind = PropertyRenderingKind.Unknown;
            var prop = property as PropertyBase;
            var rel = property as RelationshipDetailBase;
            if (prop != null)
            {
                switch (prop.TypeDefinition.Kind)
                {
                    case IqlType.Enum:
                        kind = PropertyRenderingKind.Enum;
                        break;
                    case IqlType.Guid:
                        kind = PropertyRenderingKind.Guid;
                        break;
                    case IqlType.String:
                        kind = PropertyRenderingKind.String;
                        break;
                    case IqlType.TimeSpan:
                        kind = PropertyRenderingKind.TimeSpan;
                        break;
                    case IqlType.Date:
                        kind = PropertyRenderingKind.Date;
                        break;
                    case IqlType.Boolean:
                        kind = PropertyRenderingKind.Boolean;
                        break;
                    case IqlType.Integer:
                    case IqlType.Decimal:
                        kind = PropertyRenderingKind.Number;
                        break;
                    case IqlType.GeometryPolygon:
                    case IqlType.GeographyPolygon:
                        kind = PropertyRenderingKind.GeoPolygon;
                        break;
                    case IqlType.GeometryPoint:
                    case IqlType.GeographyPoint:
                        kind = PropertyRenderingKind.GeoPoint;
                        break;
                }
                //if (prop.Kind.HasFlag(PropertyKind.Key))
                //{
                //    kind = PropertyRenderingKind.Key;
                //}
                //else if (prop.Kind.HasFlag(PropertyKind.RelationshipKey))
                //{
                //    kind = PropertyRenderingKind.RelationshipKey;
                //}
                //else if (prop.Kind == PropertyKind.Relationship)
                //{
                //    kind = PropertyRenderingKind.Relationship;
                //}
            }
            else if (rel != null && rel.RelationshipSide == RelationshipSide.Target)
            {
                kind = PropertyRenderingKind.RelationshipTarget;
            }
            else if (rel != null && rel.RelationshipSide == RelationshipSide.Source)
            {
                kind = PropertyRenderingKind.RelationshipSource;
            }
            else if (property is IFile)
            {
                kind = PropertyRenderingKind.File;
            }
            else
            {
                kind = PropertyRenderingKind.Group;
            }

            return kind;
        }
    }
}