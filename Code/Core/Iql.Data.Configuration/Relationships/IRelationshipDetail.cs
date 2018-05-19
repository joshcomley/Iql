using System;

namespace Iql.Data.Configuration.Relationships
{
    public interface IRelationshipDetail
    {
        IRelationshipDetail OtherSide { get; }
        RelationshipSide RelationshipSide { get; }
        IRelationship Relationship { get; }
        Type Type { get; }
        bool IsCollection { get; }
        IProperty Property { get; set; }
        IProperty CountProperty { get; }
        IEntityConfiguration Configuration { get; set; }
        CompositeKey GetCompositeKey(object entityOrCompositeKey, bool inverse = false);
        void MarkDirty(object entity);
        IProperty[] Constraints();
    }
}