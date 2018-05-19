using System;
using Iql.Queryable.Data.Lists;

namespace Iql.Queryable.Events
{
    public interface IObservableListChangeEvent
    {
        bool Disallow { get; set; }
        Type ItemType { get; }
        bool ItemHasChanged { get; }
        object Item { get; set; }
        ObservableListChangeKind Kind { get; }
        IObservableList List { get; }
    }
}