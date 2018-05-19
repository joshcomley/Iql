using Iql.Data.Events;
using Iql.Entities;

namespace Iql.Data.Lists
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