using System;
using Iql.Data.Lists;
using Iql.Entities;
using Iql.Entities.Lists.Events;

namespace Iql.Data.Events
{
    public interface IRelatedListChangeEvent
    {
        IObservableListChangeEvent ObservableListChangeEvent { get; }
        Type OwnerType { get; }
        object Owner { get; }
        Type ItemType { get; }
        object Item { get; }
        RelatedListChangeKind Kind { get; }
        IRelatedList List { get; }
        CompositeKey ItemKey { get; }
    }
}