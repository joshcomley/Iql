using System;
using Iql.Queryable.Data.Lists;

namespace Iql.Queryable.Events
{
    public class ObservableListChangeEvent<T> : IObservableListChangeEvent
    {
        public Type ItemType => typeof(T);
        public object Item { get; }
        public ObservableListChangeKind Kind { get; }
        public IObservableList List { get; }

        public ObservableListChangeEvent(
            object item, 
            ObservableListChangeKind kind, 
            IObservableList list)
        {
            Item = item;
            Kind = kind;
            List = list;
        }
    }
}