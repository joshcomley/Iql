using System;
using Iql.Queryable.Data.Lists;

namespace Iql.Queryable.Events
{
    public interface IObservableListChangeEvent
    {
        Type ItemType { get; }
        object Item { get; }
        ObservableListChangeKind Kind { get; }
        IObservableList List { get; }
    }
}