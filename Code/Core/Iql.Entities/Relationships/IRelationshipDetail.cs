namespace Iql.Entities.Relationships
{
    public interface ITargetRelationshipSourceDetail : IRelationshipDetail
    {
        bool SupportsCascadeDelete { get; set; }
    }
    public interface ISourceRelationshipDetail : IRelationshipDetail
    {

    }

    public interface IRelationshipDetail : IRelationshipDetailMetadata, IConfigurable<IRelationshipDetail>
    {
        IRelationshipDetail OtherSide { get; }
        IRelationship Relationship { get; }
        CompositeKey GetCompositeKey(object entityOrCompositeKey, bool inverse = false);
        void MarkDirty(object entity);
        IProperty[] Constraints { get; }
        IProperty[] AllProperties { get; }
    }
}