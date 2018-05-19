using System;
using Iql.Data.Lists;

namespace Iql.Data.Events
{
    public class ObservableListChangeEvent<T> : IObservableListChangeEvent
    {
        private object _item;
        private object _originalItem;
        public bool Disallow { get; set; }
        public Type ItemType => typeof(T);
        public bool ItemHasChanged { get; protected set; }

        public object Item
        {
            get => _item;
            set
            {
                if (_item == null)
                {
                    _originalItem = value;
                }
                else
                {
                    ItemHasChanged = value != _originalItem;
                }
                _item = value;
            }
        }

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