using System.Collections.Generic;
using Iql.Queryable.Data.Crud.State;
using Iql.Queryable.Events;

namespace Iql.Queryable
{
    public class EntityList<T> : ObservableList<T>
    {
        private readonly Dictionary<IEntity, int> _entitySubscriptions = new Dictionary<IEntity, int>();

        public override void Add(T item)
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
                base.Add(item);
            }
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
                item.ExistsChanged.Unsubscribe(_entitySubscriptions[item]);
                _entitySubscriptions.Remove(item);
            }
        }
    }
}