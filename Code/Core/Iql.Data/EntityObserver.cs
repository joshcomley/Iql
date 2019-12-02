using System;
using System.Collections.Generic;
using Iql.Data.Events;
using Iql.Data.Lists;
using Iql.Data.Tracking.State;
using Iql.Entities.Events;
using Iql.Entities.Extensions;
using Iql.Events;

namespace Iql.Data
{
    public class EntityObserver
    {
        private bool _subscriptionsDelayedInitialized;
        private Dictionary<string, KeyValuePair<IEventSubscriberBase, EventSubscription>> _subscriptionsDelayed;
        private Dictionary<string, KeyValuePair<IEventSubscriberBase, EventSubscription>> _subscriptions { get { if(!_subscriptionsDelayedInitialized) { _subscriptionsDelayedInitialized = true; _subscriptionsDelayed = new Dictionary<string, KeyValuePair<IEventSubscriberBase, EventSubscription>>(); } return _subscriptionsDelayed; } set { _subscriptionsDelayedInitialized = true; _subscriptionsDelayed = value; } }
        private bool _relatedListChangedSubscriptionsDelayedInitialized;
        private Dictionary<IRelatedList, EventSubscription> _relatedListChangedSubscriptionsDelayed;
        private Dictionary<IRelatedList, EventSubscription> _relatedListChangedSubscriptions { get { if(!_relatedListChangedSubscriptionsDelayedInitialized) { _relatedListChangedSubscriptionsDelayedInitialized = true; _relatedListChangedSubscriptionsDelayed =             new Dictionary<IRelatedList, EventSubscription>(); } return _relatedListChangedSubscriptionsDelayed; } set { _relatedListChangedSubscriptionsDelayedInitialized = true; _relatedListChangedSubscriptionsDelayed = value; } }

        public IEntityStateBase EntityState { get; }
        private IEntity Entity { get; }
        public EntityObserver(IEntityStateBase entityState)
        {
            EntityState = entityState;
            Entity = (IEntity)entityState.Entity;
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
            _subscriptions.Add(key, new KeyValuePair<IEventSubscriberBase, EventSubscription>(eventEmitter, eventEmitter.Subscribe(action)));
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
                key.Value.Value.Unsubscribe();
            }

            foreach (var key in _relatedListChangedSubscriptions)
            {
                key.Value.Unsubscribe();
            }
        }
    }
}