using System.Collections;

namespace Iql.Queryable.Data.Relationships
{
    public class RelationshipMatches
    {
        public IList SourceMatches { get; set; }
        public IList TargetMatches { get; set; }

        public RelationshipMatches(IList sourceMatches, IList targetMatches)
        {
            SourceMatches = sourceMatches;
            TargetMatches = targetMatches;
        }
    }
}