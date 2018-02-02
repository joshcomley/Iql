using Iql.Queryable.Data.EntityConfiguration.Relationships;

namespace Iql.Queryable.Data.Tracking
{
    public interface ITrackedRelationship
    {
        IRelationship Relationship { get;}
    }
}