using System;
using System.Collections;
using System.Collections.Generic;

namespace Iql.Queryable
{
    public class ObservableList<T> : IList<T>, IList
    {
        private readonly List<T> _rootList = new List<T>();

        public IEnumerator<T> GetEnumerator()
        {
            return _rootList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _rootList).GetEnumerator();
        }

        public virtual void Add(T item)
        {
            _rootList.Add(item);
        }

        public virtual void AddRange(IEnumerable<T> list)
        {
            foreach (var item in list)
            {
                _rootList.Add(item);
            }
        }

        public virtual int Add(object value)
        {
            var length = Count;
            Add((T)value);
            return Count - length;
        }

        public virtual void Clear()
        {
            _rootList.Clear();
        }

        public bool Contains(object value)
        {
            return ((IList) _rootList).Contains(value);
        }

        public int IndexOf(object value)
        {
            return ((IList) _rootList).IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            Insert(index, (T)value);
        }

        void IList.Remove(object value)
        {
            Remove((T) value);
        }

        public virtual bool Remove(T item)
        {
            return _rootList.Remove(item);
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
            ((ICollection) _rootList).CopyTo(array, index);
        }

        public int Count => _rootList.Count;

        public bool IsSynchronized => ((ICollection) _rootList).IsSynchronized;

        public object SyncRoot => ((ICollection) _rootList).SyncRoot;

        public bool IsReadOnly => false;

        object IList.this[int index]
        {
            get { return ((IList)_rootList)[index]; }
            set { ((IList) _rootList)[index] = value; }
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
            _rootList.RemoveAt(index);
        }

        public bool IsFixedSize => ((IList) _rootList).IsFixedSize;

        public T this[int index]
        {
            get {return  _rootList[index]; }
            set { _rootList[index] = value; }
        }
    }
}