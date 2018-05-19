using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Data.Queryable;

namespace Iql.Queryable.Operations
{
    public class ExpandDetail
    {
        public ExpandDetail(
            IQueryableBase sourceQueryable,
            IQueryableBase targetQueryable,
            IRelationship relationship,
            bool isTarget,
            bool isCount
        )
        {
            SourceQueryable = sourceQueryable;
            TargetQueryable = targetQueryable;
            Relationship = relationship;
            IsTarget = isTarget;
            IsCount = isCount;
            ExpandedQueryable = isTarget ? targetQueryable : sourceQueryable;
        }
        public IQueryableBase ExpandedQueryable { get; }
        public IQueryableBase SourceQueryable { get; set; }
        public IQueryableBase TargetQueryable { get; set; }
        public IRelationship Relationship { get; }
        public bool IsTarget { get; }
        public bool IsCount { get; }

        public IProperty GetExpandProperty()
        {
            return IsTarget ? Relationship.Target.Property : Relationship.Source.Property;
        }
    }
}