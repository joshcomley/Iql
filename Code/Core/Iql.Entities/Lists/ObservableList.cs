using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Entities.Lists.Events;
using Iql.Events;

namespace Iql.Entities.Lists
{
    public class ObservableList<T> : IList<T>, IObservableList
    {
        private bool _rootListDelayedInitialized;
        private List<T> _rootListDelayed;
        private List<T> _rootList { get { if (!_rootListDelayedInitialized) { _rootListDelayedInitialized = true; _rootListDelayed = new List<T>(); } return _rootListDelayed; } set { _rootListDelayedInitialized = true; _rootListDelayed = value; } }
        private EventEmitter<ObservableListChangeEvent<T>> _change;

        public EventEmitter<ObservableListChangeEvent<T>> Change => _change = _change ?? new EventEmitter<ObservableListChangeEvent<T>>();

        IEventSubscriber<IObservableListChangeEvent> IObservableList.Change => Change;

        public IEnumerator<T> GetEnumerator()
        {
            return _rootList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_rootList).GetEnumerator();
        }

        public new T Add(T item)
        {
            return (T)AddItem(item);
        }

        void ICollection<T>.Add(T item)
        {
            AddItem(item);
        }

        protected virtual T AddItem(T item)
        {
            var eventResult = Emit((T)item, ObservableListChangeKind.Adding);
            if (eventResult == null || !eventResult.Disallow)
            {
                item = (T)(eventResult?.Item ?? item);
                _rootList.Add((T)item);
                var addedEventResult = Emit((T)item, ObservableListChangeKind.Added);
                if (addedEventResult != null && addedEventResult.Disallow)
                {
                    _rootList.Remove((T)item);
                    return default(T);
                }
                return item;
            }
            return default(T);
        }

        public void AddRange(IEnumerable<T> list)
        {
            foreach (var item in list)
            {
                AddItem(item);
            }
        }

        public int Add(object value)
        {
            var length = Count;
            AddItem((T)value);
            return Count - length;
        }

        public virtual void Clear()
        {
            _rootList.Clear();
        }

        public bool Contains(object value)
        {
            return ((IList)_rootList).Contains(value);
        }

        public int IndexOf(object value)
        {
            return ((IList)_rootList).IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            Insert(index, (T)value);
        }

        void IList.Remove(object value)
        {
            Remove((T)value);
        }

        bool IObservableList.Remove(object value)
        {
            return Remove((T)value);
        }

        object IObservableList.Add(object value)
        {
            return AddItem((T)value);
        }

        public virtual bool Remove(T item)
        {
            var success = false;
            for (var i = 0; i < Count; i++)
            {
                if (Equals(this[i], item))
                {
                    RemoveAt(i);
                    success = true;
                    break;
                }
            }
            return success;
        }

        public bool Contains(T item)
        {
            return _rootList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _rootList.CopyTo(array, arrayIndex);
        }

        public void CopyTo(Array array, int index)
        {
            ((ICollection)_rootList).CopyTo(array, index);
        }

        public int Count => _rootList.Count;

        public bool IsSynchronized => ((ICollection)_rootList).IsSynchronized;

        public object SyncRoot => ((ICollection)_rootList).SyncRoot;

        public bool IsReadOnly => false;

        object IList.this[int index]
        {
            get { return ((IList)_rootList)[index]; }
            set { ((IList)_rootList)[index] = value; }
        }

        public int IndexOf(T item)
        {
            return _rootList.IndexOf(item);
        }

        public virtual void Insert(int index, T item)
        {
            _rootList.Insert(index, item);
        }

        public virtual void RemoveAt(int index)
        {
            var item = this[index];
            Emit(item, ObservableListChangeKind.Removing);
            _rootList.RemoveAt(index);
            Emit(item, ObservableListChangeKind.Removed);
        }

        public bool IsFixedSize => ((IList)_rootList).IsFixedSize;

        public T this[int index]
        {
            get { return _rootList[index]; }
            set { _rootList[index] = value; }
        }

        protected ObservableListChangeEvent<T> Emit(T item, ObservableListChangeKind kind)
        {
            return EmitEvent(item, kind, EventObjectFactory(item, kind));
        }

        protected virtual ObservableListChangeEvent<T> EmitEvent(T item, ObservableListChangeKind kind, Func<ObservableListChangeEvent<T>> eventObjectFactory)
        {
            if (_change == null)
            {
                return null;
            }
            return _change.EmitIfExists(eventObjectFactory);
        }

        protected Func<ObservableListChangeEvent<T>> EventObjectFactory(T item, ObservableListChangeKind kind)
        {
            ObservableListChangeEvent<T> ev = null;
            return () => ev = ev ?? new ObservableListChangeEvent<T>(item, kind, this);
        }
    }
}
