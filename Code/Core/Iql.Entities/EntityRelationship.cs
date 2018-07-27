using Iql.Entities.Relationships;

namespace Iql.Entities
{
    public class EntityRelationship
    {
        public IRelationship Relationship { get; }
        public bool ThisIsTarget { get; }
        public IRelationshipDetail ThisEnd { get; }
        public IRelationshipDetail OtherEnd { get; }
        public EntityRelationship(IRelationship relationship, bool thisIsTarget)
        {
            Relationship = relationship;
            ThisIsTarget = thisIsTarget;
            ThisEnd = thisIsTarget ? relationship.Target : relationship.Source;
            OtherEnd = thisIsTarget ? relationship.Source : relationship.Target;
        }
    }
}