using Iql.Entities.Relationships;

namespace Iql.Data.Relationships
{
    public class RelationshipChangedEvent
    {
        public object Source { get; }
        public IRelationship Relationship { get; }

        public RelationshipChangedEvent(object source, IRelationship relationship)
        {
            Source = source;
            Relationship = relationship;
        }
    }
}