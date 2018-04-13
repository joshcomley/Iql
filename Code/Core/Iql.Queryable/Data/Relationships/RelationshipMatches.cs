using System.Collections;
using Iql.Queryable.Data.EntityConfiguration.Relationships;

namespace Iql.Queryable.Data.Relationships
{
    public class RelationshipMatches
    {
        public IRelationship Relationship { get; }
        public IList SourceMatches { get; set; }
        public IList TargetMatches { get; set; }

        public RelationshipMatches(
            IRelationship relationship,
            IList sourceMatches, 
            IList targetMatches)
        {
            Relationship = relationship;
            SourceMatches = sourceMatches;
            TargetMatches = targetMatches;
        }

        public void UnassignRelationships()
        {

        }
    }
}