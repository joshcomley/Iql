using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Events;

namespace Iql.Queryable.Data.Lists
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