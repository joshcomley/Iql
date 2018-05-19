using Iql.Entities.Relationships;

namespace Iql.Entities
{
    public class RelationshipMatch
    {
        public IRelationship Relationship { get; }
        public bool ThisIsTarget { get; }
        public IRelationshipDetail ThisEnd { get; }
        public IRelationshipDetail OtherEnd { get; }
        public RelationshipMatch(IRelationship relationship, bool thisIsTarget)
        {
            Relationship = relationship;
            ThisIsTarget = thisIsTarget;
            ThisEnd = thisIsTarget ? relationship.Target : relationship.Source;
            OtherEnd = thisIsTarget ? relationship.Source : relationship.Target;
        }
    }
}