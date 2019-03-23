using System.Diagnostics;
using Iql.Entities;

namespace Iql.Data.Rendering
{
    [DebuggerDisplay("{Property.Name}, can edit: {CanEdit}, can show: {CanShow}")]
    public class EntityPropertySnapshot
    {
        public PropertyDetail Detail { get; set; }
        public string Kind { get; set; }
        public bool IsEnum => Kind == PropertyRenderingKind.Enum;
        public bool IsString => Kind == PropertyRenderingKind.String;
        public bool IsGuid => Kind == PropertyRenderingKind.Guid;
        public bool IsDate => Kind == PropertyRenderingKind.Date;
        public bool IsTimeSpan => Kind == PropertyRenderingKind.TimeSpan;
        public bool IsBoolean => Kind == PropertyRenderingKind.Boolean;
        public bool IsNumber => Kind == PropertyRenderingKind.Number;
        public bool IsGeoPolygon => Kind == PropertyRenderingKind.GeoPolygon;
        public bool IsGeoPoint => Kind == PropertyRenderingKind.GeoPoint;

        private bool _asPropertySet = false;
        private IProperty _asProperty;
        private IProperty AsProperty
        {
            get
            {
                if (!_asPropertySet)
                {
                    _asProperty = Property as IProperty;
                }

                return _asProperty;
            }
        }

        public bool IsKey
        {
            get
            {
                if (AsProperty != null)
                {
                    return AsProperty.Kind.HasFlag(PropertyKind.Key);
                }
                return false;
            }
        }

        public bool IsRelationshipKey
        {
            get
            {
                if (AsProperty != null)
                {
                    return AsProperty.Kind.HasFlag(PropertyKind.RelationshipKey);
                }
                return false;
            }
        }
        public bool IsRelationship
        {
            get
            {
                if (AsProperty != null)
                {
                    return AsProperty.Kind.HasFlag(PropertyKind.Relationship);
                }
                return false;
            }
        }
        public bool IsRelationshipTarget => Kind == PropertyRenderingKind.RelationshipTarget;
        public bool IsRelationshipSource => Kind == PropertyRenderingKind.RelationshipSource;
        public bool IsFile => Kind == PropertyRenderingKind.File;
        public bool IsGroup => Kind == PropertyRenderingKind.Group;
        public bool IsUnknown => Kind == PropertyRenderingKind.Unknown;
        public bool CanShow { get; set; }
        public bool CanEdit { get; set; }
        public EntityPropertySnapshot[] ChildProperties { get; set; }
        public string PropertyName => Property?.Name;
        public IPropertyContainer Property => Detail?.Property;

        public EntityPropertySnapshot(PropertyDetail detail, string kind, bool canShow, bool canEdit, EntityPropertySnapshot[] childProperties)
        {
            Detail = detail;
            Kind = kind;
            CanShow = canShow;
            CanEdit = canEdit;
            ChildProperties = childProperties;
        }
    }
}