using System.Collections.Generic;
using Iql.Data.Events;
using Iql.Entities.Events;

namespace Iql.Data.Lists
{
    public class EntityList<T> : ObservableList<T>
    {
        private readonly Dictionary<IEntity, EventSubscription> _entitySubscriptions = new Dictionary<IEntity, EventSubscription>();

        protected override T AddItem(T item)
        {
            if (!Contains(item))
            {
                var iEntity = item as IEntity;
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
            var item = this[index] as IEntity;
            base.RemoveAt(index);
            if (item != null && _entitySubscriptions.ContainsKey(item))
            {
                _entitySubscriptions[item].Unsubscribe();
                _entitySubscriptions.Remove(item);
            }
        }
    }
}