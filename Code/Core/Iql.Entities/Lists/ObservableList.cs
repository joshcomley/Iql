using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iql.Entities.Lists.Events;
using Iql.Events;

namespace Iql.Entities.Lists
{
    public class ObservableList<T> : IList<T>, IObservableList
    {
        private bool _rootLookupDelayedInitialized;
        private Dictionary<T, bool> _rootLookupDelayed;
        private Dictionary<T, bool> _rootLookup { get { if (!_rootLookupDelayedInitialized) { _rootLookupDelayedInitialized = true; _rootLookupDelayed = new Dictionary<T, bool>(); } return _rootLookupDelayed; } set { _rootLookupDelayedInitialized = true; _rootLookupDelayed = value; } }

        private bool _rootListDelayedInitialized;
        private List<T> _rootListDelayed;
        private List<T> _rootList { get { if (!_rootListDelayedInitialized) { _rootListDelayedInitialized = true; _rootListDelayed = new List<T>(); } return _rootListDelayed; } set { _rootListDelayedInitialized = true; _rootListDelayed = value; } }
        private EventEmitter<ObservableListChangeEvent<T>> _change;
        private bool _ensureUnique;
        private bool _noEvents;

        public virtual bool EnsureUnique
        {
            get => _ensureUnique;
            set
            {
                var old = _ensureUnique;
                _ensureUnique = value;
                if (old != value)
                {
                    if (value)
                    {
                        if (_rootLookupDelayed != null && _rootListDelayed != null)
                        {
                            _rootLookup.Clear();
                            for (var i = 0; i < _rootListDelayed.Count; i++)
                            {
                                var item = _rootListDelayed[i];
                                _rootLookup.Add(item, true);
                            }
                        }
                    }
                    else
                    {
                        if (_rootLookupDelayed != null)
                        {
                            _rootLookup.Clear();
                        }
                    }
                }
            }
        }

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
            if (EnsureUnique && _rootLookup.ContainsKey(item))
            {
                return default(T);
            }
            var eventResult = Emit((T)item, ObservableListChangeKind.Adding);
            if (eventResult == null || !eventResult.Disallow)
            {
                item = (T)(eventResult?.Item ?? item);
                if (EnsureUnique && _rootLookup.ContainsKey(item))
                {
                    return default(T);
                }
                _noEvents = true;
                _rootList.Add((T)item);
                _noEvents = false;
                if (EnsureUnique)
                {
                    _rootLookup.Add((T)item, true);
                }
                var addedEventResult = Emit((T)item, ObservableListChangeKind.Added);
                if (addedEventResult != null && addedEventResult.Disallow)
                {
                    _rootList.Remove((T)item);
                    if (EnsureUnique)
                    {
                        _rootLookup.Remove((T)item);
                    }
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
            var i = 0;
            var count = Count;
            while (Count > i)
            {
                RemoveAt(i);
                if (Count == count)
                {
                    i++;
                }
                else
                {
                    count = Count;
                }
            }
            //var copy = _rootList.ToList();
            //for (var i = 0; i < copy.Count; i++)
            //{
            //    Remove(copy[i]);
            //}
        }

        public bool Contains(object value)
        {
            return ((IList)_rootList).Contains(value);
        }

        public int IndexOf(object value)
        {
            return ((IList)_rootList).IndexOf(value);
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
            return EnsureUnique
                ? _rootLookup.ContainsKey(item)
                : _rootList.Contains(item);
        }

        public int IndexOf(T item)
        {
            return _rootList.IndexOf(item);
        }

        public virtual void Insert(int index, T item)
        {
            _rootList.Insert(index, item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _rootList.CopyTo(array, arrayIndex);
        }

        public void CopyTo(Array array, int index)
        {
            ((ICollection)_rootList).CopyTo(array, index);
        }

        public virtual void RemoveAt(int index)
        {
            var item = this[index];
            var ev = Emit(item, ObservableListChangeKind.Removing);
            if (ev != null && ev.Disallow)
            {
                return;
            }

            _noEvents = true;
            _rootList.RemoveAt(index);
            _noEvents = false;
            if (EnsureUnique)
            {
                _rootLookup.Remove(item);
            }
            Emit(item, ObservableListChangeKind.Removed);
        }

        public void Insert(int index, object value)
        {
            Insert(index, (T)value);
        }

        public T this[int index]
        {
            get
            {
                return _rootList[index];
            }
            set
            {
                if (!_noEvents && EnsureUnique && _rootLookup.ContainsKey((T)value))
                {
                    return;
                }
                var old = _rootList[index];
                if (Equals(old, value))
                {
                    return;
                }

                if (!_noEvents)
                {
                    var ev = Emit(old, ObservableListChangeKind.Removing);
                    if (ev != null && ev.Disallow)
                    {
                        return;
                    }
                }

                _rootLookup.Remove(old);
                _rootList[index] = default(T);
                if (!_noEvents)
                {
                    Emit(old, ObservableListChangeKind.Removed);
                    var ev = Emit(value, ObservableListChangeKind.Adding);
                    if (ev != null && ev.Disallow)
                    {
                        RemoveAt(index);
                        return;
                    }
                }
                _rootList[index] = value;
                if (!_noEvents)
                {
                    Emit(value, ObservableListChangeKind.Added);
                }
                _rootLookup.Add(value, true);
            }
        }

        object IList.this[int index]
        {
            get => this[index];
            set => this[index] = (T)value;
        }

        public int Count => _rootList.Count;

        public bool IsSynchronized => ((ICollection)_rootList).IsSynchronized;

        public object SyncRoot => ((ICollection)_rootList).SyncRoot;

        public bool IsReadOnly => false; public bool IsFixedSize => ((IList)_rootList).IsFixedSize;

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
