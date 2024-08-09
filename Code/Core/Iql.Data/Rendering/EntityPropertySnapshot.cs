using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Iql.Conversion;
using Iql.Entities;

namespace Iql.Data.Rendering
{
    [DebuggerDisplay("{Property.Name}, can edit: {CanEdit}, can show: {CanShow}")]
    public class EntityPropertySnapshot
    {
        public EntityPropertySnapshot Parent { get; private set; }
        public EntityPropertySnapshot Root => Parent == null ? this : Parent.Root;
        public PropertyDetail Detail { get; set; }
        public string Kind { get; set; }
        public bool IsEnum => Kind == IqlPropertyRenderingKind.Enum;
        public bool IsString => Kind == IqlPropertyRenderingKind.String;
        public bool IsGuid => Kind == IqlPropertyRenderingKind.Guid;
        public bool IsDate => Kind == IqlPropertyRenderingKind.Date;
        public bool IsTimeSpan => Kind == IqlPropertyRenderingKind.TimeSpan;
        public bool IsBoolean => Kind == IqlPropertyRenderingKind.Boolean;
        public bool IsNumber => Kind == IqlPropertyRenderingKind.Number;
        public bool IsGeoPolygon => Kind == IqlPropertyRenderingKind.GeoPolygon;
        public bool IsGeoPoint => Kind == IqlPropertyRenderingKind.GeoPoint;

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
                    return AsProperty.Kind.HasFlag(IqlPropertyKind.Key);
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
                    return AsProperty.Kind.HasFlag(IqlPropertyKind.RelationshipKey);
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
                    return AsProperty.Kind.HasFlag(IqlPropertyKind.Relationship);
                }
                return false;
            }
        }
        public bool IsRelationshipTarget => Kind == IqlPropertyRenderingKind.RelationshipTarget;
        public bool IsRelationshipSource => Kind == IqlPropertyRenderingKind.RelationshipSource;
        public bool IsFile => Kind == IqlPropertyRenderingKind.File;
        public bool IsGroup => Kind == IqlPropertyRenderingKind.Group;
        public bool IsUnknown => Kind == IqlPropertyRenderingKind.Unknown;
        public bool CanShow { get; set; }
        public SnapshotReasonKind CanShowReason { get; }
        public bool CanEdit { get; set; }
        public SnapshotReasonKind CanEditReason { get; }
        public EntityPropertySnapshot[] ChildProperties { get; }
        public string PropertyName => Property?.Name;
        public IPropertyContainer Property => Detail?.Property;
        public bool EditPermissionDenied => CanEditReason == SnapshotReasonKind.Permissions && !CanEdit;
        public bool ReadPermissionDenied => CanShowReason == SnapshotReasonKind.Permissions && !CanShow;
        public IEntityConfigurationBuilder Builder => EntityConfiguration?.Builder;
        public IEntityConfiguration EntityConfiguration => Detail == null ? null : Detail.Property.EntityConfiguration;
        public EntityPropertySnapshot(PropertyDetail detail, string kind, bool canShow, SnapshotReasonKind canShowReason, bool canEdit,
            SnapshotReasonKind canEditReason, EntityPropertySnapshot[] childProperties)
        {
            Detail = detail;
            Kind = kind;
            CanShow = canShow;
            CanShowReason = canShowReason;
            CanEdit = canEdit;
            CanEditReason = canEditReason;
            ChildProperties = childProperties;
            if(ChildProperties != null)
            {
                foreach (var child in ChildProperties)
                {
                    child.Parent = this;
                }
            }
        }

        public EntityPropertySnapshot FindDescendentPropertyByName(string name)
        {
            return Flattened().Where(_ => _.PropertyName == name).SingleOrDefault();
        }
        
        public EntityPropertySnapshot FindDescendentProperty(IProperty property)
        {
            return Flattened().Where(_ => _.Property == property).SingleOrDefault();
        }

        public EntityPropertySnapshot FindDescendentPropertyByExpression<T>(Expression<Func<T, object>> expression)
        {
            var name = expression.ToIqlPropertyExpression(Root.EntityConfiguration)?.PropertyName;
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            return Flattened().Where(_ => _.PropertyName == name).SingleOrDefault();
        }

        public EntityPropertySnapshot[] Flattened()
        {
            var list = new List<EntityPropertySnapshot>();
            if (IsGroup)
            {
                foreach (var child in ChildProperties)
                {
                    list.AddRange(child.Flattened());
                }
            }
            else
            {
                list.Add(this);
            }
            return list.Distinct().ToArray();
        }
    }

    public enum SnapshotReasonKind
    {
        Configuration = 1,
        Permissions = 2
    }
}