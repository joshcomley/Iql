using System.Collections;
using Iql.Entities.Lists.Events;
using Iql.Events;

namespace Iql.Entities.Lists
{
    public interface IObservableList : IList
    {
        bool EnsureUnique { get; set; }
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