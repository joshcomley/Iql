using Iql.Queryable.Data.EntityConfiguration.Relationships;

namespace Iql.Queryable.Data.Tracking
{
    public interface ITrackedRelationship
    {
        object Owner { get; }
        object Entity { get;}
        IRelationshipDetail OwnerDetail { get; }
        IRelationshipDetail EntityDetail { get; }
        IRelationship Relationship { get;}
    }
}