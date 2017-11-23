using Iql.Queryable.Data.EntityConfiguration.Relationships;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public class RelationshipMatch
    {
        public IRelationship Relationship { get; }
        public bool PartnerIsSource { get; }
        public IRelationshipDetail ThisEnd { get; }
        public IRelationshipDetail OtherEnd { get; }
        public RelationshipMatch(IRelationship relationship, bool partnerIsSource)
        {
            Relationship = relationship;
            PartnerIsSource = partnerIsSource;
            ThisEnd = partnerIsSource ? relationship.Source : relationship.Target;
            OtherEnd = partnerIsSource ? relationship.Target : relationship.Source;
        }
    }
}