using System;
using System.Collections;
using Iql.Queryable.Events;

namespace Iql.Queryable.Data.Lists
{
    public interface IObservableList : IList
    {
        IEventSubscriber<IObservableListChangeEvent> Change { get; }
#if TypeScript
        void Add(object value);
        void Clear();
        bool Contains(object value);
        int IndexOf(object value);
        void Insert(int index, object value);
        void RemoveAt(int index);
        bool Remove(object item);
#endif
    }
}