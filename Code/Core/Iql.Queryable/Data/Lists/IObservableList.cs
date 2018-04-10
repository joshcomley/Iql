using System;
using System.Collections;
using Iql.Queryable.Events;

namespace Iql.Queryable.Data.Lists
{
    public interface IObservableList : IList
    {
        IEventSubscriber<IObservableListChangeEvent> Change { get; }
#if TypeScript
        void Clear();
        bool Contains(object value);
        int IndexOf(object value);
        void Insert(int index, object value);
        void RemoveAt(int index);
#endif
        object Add(object value);
        bool Remove(object item);
    }
}