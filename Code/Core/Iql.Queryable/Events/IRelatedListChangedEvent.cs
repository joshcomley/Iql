using System;
using System.Collections;
using Iql.Queryable.Operations;

namespace Iql.Queryable
{
    public interface IRelatedListChangedEvent
    {
        Type OwnerType { get; }
        object Owner { get; }
        Type ItemType { get; }
        object Item { get; }
        RelatedListChangeKind Kind { get; }
        IRelatedList List { get; }
        CompositeKey ItemKey { get; }
    }
}