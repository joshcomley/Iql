using System.Collections;
using Iql.Queryable.Operations;

namespace Iql.Queryable
{
    public interface IRelatedListChangedEvent
    {
        object Owner { get; }
        object Item { get; }
        RelatedListChangeKind Kind { get; }
        IList List { get; }
        CompositeKey ItemKey { get; }
    }
}