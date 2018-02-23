using System;

namespace Iql.Queryable
{
    public interface IObservableListChangeEvent
    {
        Type ItemType { get; }
        object Item { get; }
        ObservableListChangeKind Kind { get; }
        IObservableList List { get; }
    }
}