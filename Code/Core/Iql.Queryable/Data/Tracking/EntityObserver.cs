using System;
using System.Collections.Generic;
using Iql.Queryable.Data.Crud.State;
using Iql.Queryable.Events;

namespace Iql.Queryable.Data.Tracking
{
    public class EntityObserver
    {
        private readonly Dictionary<string, KeyValuePair<IEventSubscriberBase, int>> _subscriptions = new Dictionary<string, KeyValuePair<IEventSubscriberBase, int>>();
        private readonly Dictionary<IRelatedList, int> _relatedListChangedSubscriptions =
            new Dictionary<IRelatedList, int>();

        public IEntityStateBase EntityState { get; }
        private IEntity Entity { get; }
        public EntityObserver(IEntityStateBase entityState)
        {
            EntityState = entityState;
            Entity = entityState.Entity as IEntity;
        }

        public void RegisterPropertyChanged(Action<IPropertyChangeEvent> action)
        {
            if (Entity.PropertyChanged == null)
            {
                Entity.PropertyChanged = new EventEmitter<IPropertyChangeEvent>();
            }
            RegisterEvent(action, nameof(IEntity.PropertyChanged), Entity.PropertyChanged);
        }

        public void RegisterExistsChanged(Action<ExistsChangeEvent> action)
        {
            if (Entity.ExistsChanged == null)
            {
                Entity.ExistsChanged = new EventEmitter<ExistsChangeEvent>();
            }
            RegisterEvent(action, nameof(IEntity.ExistsChanged), Entity.ExistsChanged);
        }

        public void RegisterPropertyChanging(Action<IPropertyChangeEvent> action)
        {
            if (Entity.PropertyChanging == null)
            {
                Entity.PropertyChanging = new EventEmitter<IPropertyChangeEvent>();
            }
            RegisterEvent(action, nameof(IEntity.PropertyChanging), Entity.PropertyChanging);
        }

        public void RegisterMarkForDeletionChanged(Action<MarkedForDeletionChangeEvent> action)
        {
            RegisterEvent(action, nameof(IEntityStateBase.MarkedForDeletionChanged), EntityState.MarkedForDeletionChanged);
        }

        private void RegisterEvent<TEvent>(Action<TEvent> action, string key, IEventSubscriber<TEvent> eventEmitter)
        {
            if(_subscriptions.ContainsKey(key))
            {
                throw new Exception("Attempting to register for entity event twice");
            }
            _subscriptions.Add(key, new KeyValuePair<IEventSubscriberBase, int>(eventEmitter, eventEmitter.Subscribe(action)));
        }

        public void RegisterRelatedListChanged(Action<IRelatedListChangeEvent> action)
        {
            var matches = EntityState.EntityConfiguration.AllRelationships();
            for (var j = 0; j < matches.Count; j++)
            {
                var relationship = matches[j];
                if (relationship.ThisEnd.IsCollection)
                {
                    var relatedList = (IRelatedList) Entity.GetPropertyValue(relationship.ThisEnd.Property);
                    _relatedListChangedSubscriptions.Add(relatedList,
                        relatedList.RelatedListChange.Subscribe(action));
                }
            }
        }

        public void Unobserve()
        {
            foreach (var key in _subscriptions)
            {
                key.Value.Key.Unsubscribe(key.Value.Value);
            }

            foreach (var key in _relatedListChangedSubscriptions)
            {
                key.Key.RelatedListChange.Unsubscribe(key.Value);
            }
        }
    }
}