using Iql.Data.Rendering;
using Iql.Entities;
using Iql.Entities.NestedSets;
using Iql.Entities.PropertyGroups.Files;
using Iql.Entities.Relationships;

namespace Iql.Data.Extensions
{
    public static class IqlDataPropertyExtensions
    {
        public static string ResolveKind(this IPropertyContainer property)
        {
            var kind = IqlPropertyRenderingKind.Unknown;
            var prop = property as PropertyBase;
            var rel = property as RelationshipDetailBase;
            if (prop != null)
            {
                switch (prop.TypeDefinition.Kind)
                {
                    case IqlType.Enum:
                        kind = IqlPropertyRenderingKind.Enum;
                        break;
                    case IqlType.Guid:
                        kind = IqlPropertyRenderingKind.Guid;
                        break;
                    case IqlType.String:
                        kind = IqlPropertyRenderingKind.String;
                        break;
                    case IqlType.TimeSpan:
                        kind = IqlPropertyRenderingKind.TimeSpan;
                        break;
                    case IqlType.Date:
                        kind = IqlPropertyRenderingKind.Date;
                        break;
                    case IqlType.Boolean:
                        kind = IqlPropertyRenderingKind.Boolean;
                        break;
                    case IqlType.Integer:
                    case IqlType.Decimal:
                        kind = IqlPropertyRenderingKind.Number;
                        break;
                    case IqlType.GeometryPolygon:
                    case IqlType.GeographyPolygon:
                        kind = IqlPropertyRenderingKind.GeoPolygon;
                        break;
                    case IqlType.GeometryPoint:
                    case IqlType.GeographyPoint:
                        kind = IqlPropertyRenderingKind.GeoPoint;
                        break;
                }
                //if (prop.Kind.HasFlag(IqlPropertyKind.Key))
                //{
                //    kind = IqlPropertyRenderingKind.Key;
                //}
                //else if (prop.Kind.HasFlag(IqlPropertyKind.RelationshipKey))
                //{
                //    kind = IqlPropertyRenderingKind.RelationshipKey;
                //}
                //else if (prop.Kind == IqlPropertyKind.Relationship)
                //{
                //    kind = IqlPropertyRenderingKind.Relationship;
                //}
            }
            else if (rel != null && rel.RelationshipSide == RelationshipSide.Target)
            {
                kind = IqlPropertyRenderingKind.RelationshipTarget;
            }
            else if (rel != null && rel.RelationshipSide == RelationshipSide.Source)
            {
                kind = IqlPropertyRenderingKind.RelationshipSource;
            }
            else if (property is IFile)
            {
                kind = IqlPropertyRenderingKind.File;
            }
            else if (property is INestedSet)
            {
                kind = IqlPropertyRenderingKind.Tree;
            }
            else
            {
                kind = IqlPropertyRenderingKind.Group;
            }

            return kind;
        }
    }
}