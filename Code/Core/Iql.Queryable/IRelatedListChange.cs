using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Events;
using Iql.Queryable.Operations;

namespace Iql.Queryable
{
    public interface IRelatedListChange
    {
        RelationshipMatch Relationship { get; }
        object Owner { get; }
        IRelatedList List { get; }
        CompositeKey ItemKey {get;}
        object Item { get; }
        RelatedListChangeKind Kind { get; }
    }
}