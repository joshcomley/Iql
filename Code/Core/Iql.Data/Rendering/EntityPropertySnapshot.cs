namespace Iql.Data.Rendering
{
    public class EntityPropertySnapshot
    {
        public PropertyDetail Detail { get; set; }
        public string Kind { get; set; }
        public bool CanShow { get; set; }
        public bool CanEdit { get; set; }
        public EntityPropertySnapshot[] ChildProperties { get; set; }

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