using System.Diagnostics;
using Iql.Entities;

namespace Iql.Data.Rendering
{
    [DebuggerDisplay("{Property.Name}, can edit: {CanEdit}")]
    public class EntityPropertySnapshot
    {
        public PropertyDetail Detail { get; set; }
        public string Kind { get; set; }
        public bool IsEnum => Kind == "enum";
        public bool IsString => Kind == "string";
        public bool IsDate => Kind == "date";
        public bool IsBoolean => Kind == "boolean";
        public bool IsNumber => Kind == "number";
        public bool IsGeoPolygon => Kind == "geopolygon";
        public bool IsGeoPoint => Kind == "geopoint";
        public bool IsKey => Kind == "key";
        public bool IsRelationshipKey => Kind == "relationship-key";
        public bool IsRelationship => Kind == "relationship";
        public bool IsRelationshipTarget => Kind == "relationship-target";
        public bool IsRelationshipSource => Kind == "relationship-source";
        public bool IsGroup => Kind == "group";
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