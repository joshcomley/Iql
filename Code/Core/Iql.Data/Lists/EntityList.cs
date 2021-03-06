using System.Collections.Generic;
using Iql.Data.Events;
using Iql.Entities.Lists;
using Iql.Events;

namespace Iql.Data.Lists
{
    public class EntityList<T> : ObservableList<T>
    {
        private bool _entitySubscriptionsDelayedInitialized;
        private Dictionary<IEntity, EventSubscription> _entitySubscriptionsDelayed;
        private Dictionary<IEntity, EventSubscription> _entitySubscriptions { get { if(!_entitySubscriptionsDelayedInitialized) { _entitySubscriptionsDelayedInitialized = true; _entitySubscriptionsDelayed = new Dictionary<IEntity, EventSubscription>(); } return _entitySubscriptionsDelayed; } set { _entitySubscriptionsDelayedInitialized = true; _entitySubscriptionsDelayed = value; } }

        protected override T AddItem(T item)
        {
            if (!Contains(item))
            {
                var iEntity = (IEntity)item;
                if (iEntity != null && !_entitySubscriptions.ContainsKey(iEntity))
                {
                    if (iEntity.ExistsChanged == null)
                    {
                        iEntity.ExistsChanged = new EventEmitter<ExistsChangeEvent>();
                    }
                    _entitySubscriptions.Add(iEntity, iEntity.ExistsChanged.Subscribe(PropertyChangeEvent));
                }
                return base.AddItem(item);
            }

            return default(T);
        }

        private void PropertyChangeEvent(ExistsChangeEvent existsChangeEvent)
        {
            if (!existsChangeEvent.NewValue)
            {
                this.Remove((T) existsChangeEvent.EntityState.Entity);
            }
        }

        public override void RemoveAt(int index)
        {
            var item = (IEntity)this[index];
            base.RemoveAt(index);
            if (item != null && _entitySubscriptions.ContainsKey(item))
            {
                _entitySubscriptions[item].Unsubscribe();
                _entitySubscriptions.Remove(item);
            }
        }
    }
}