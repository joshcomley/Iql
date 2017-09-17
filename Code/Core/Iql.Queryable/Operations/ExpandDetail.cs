using Iql.Queryable.Data.EntityConfiguration.Relationships;

namespace Iql.Queryable.Operations
{
    public class ExpandDetail
    {
        public ExpandDetail(
            IQueryableBase sourceQueryable,
            IQueryableBase targetQueryable,
            IRelationship relationship
        )
        {
            SourceQueryable = sourceQueryable;
            TargetQueryable = targetQueryable;
            Relationship = relationship;
        }

        public IQueryableBase SourceQueryable { get; set; }
        public IQueryableBase TargetQueryable { get; set; }
        public IRelationship Relationship { get; }
    }
}