using Iql.Queryable.Data.EntityConfiguration.Relationships;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public class RelationshipMatch
    {
        public IRelationship Relationship { get; }
        public bool PartnerIsSource { get; }

        public RelationshipMatch(IRelationship relationship, bool partnerIsSource)
        {
            Relationship = relationship;
            PartnerIsSource = partnerIsSource;
        }
    }
}