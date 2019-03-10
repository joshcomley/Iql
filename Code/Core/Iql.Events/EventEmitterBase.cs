using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql.Events
{
    public class EventEmitterBase<TEvent, TSubscription> : IEventUnsubcriber
    {
        public EventEmitterBase(BackfireMode backfireMode = BackfireMode.None)
        {
            BackfireMode = backfireMode;
        }

        private BackfireMode _backfireMode = BackfireMode.None;
        private EventEmitter<EventSubscription> _onSubscribe;
        private EventEmitter<EventSubscription> _onUnsubscribe;
        private int _subscriptionId;
        private Dictionary<int, TSubscription> _subscriptions;
        protected List<TSubscription> SubscriptionActions { get; set; }

        public EventEmitter<EventSubscription> OnSubscribe =>
            _onSubscribe = _onSubscribe ?? new EventEmitter<EventSubscription>();

        public EventEmitter<EventSubscription> OnUnsubscribe =>
            _onUnsubscribe = _onUnsubscribe ?? new EventEmitter<EventSubscription>();

        protected Dictionary<EventSubscription, EventSubscription> Subscriptions { get; } =
            new Dictionary<EventSubscription, EventSubscription>();

        protected Dictionary<int, EventSubscription> SubscriptionsLookup { get; } =
            new Dictionary<int, EventSubscription>();

        public BackfireMode BackfireMode
        {
            get => _backfireMode;
            set
            {
                var old = _backfireMode;
                if (old != BackfireMode.None && value == BackfireMode.None)
                {
                    EventHistory.Clear();
                }

                _backfireMode = value;
            }
        }

        protected List<TEvent> EventHistory { get; } = new List<TEvent>();

        public void Unsubscribe(int subscription)
        {
            if (_subscriptions == null)
            {
                return;
            }

            var sub = SubscriptionsLookup.ContainsKey(subscription) ? SubscriptionsLookup[subscription] : null;
            if (sub != null)
            {
                Subscriptions.Remove(sub);
                OnUnsubscribe.Emit(() => sub);
            }

            SubscriptionsLookup.Remove(subscription);
            _subscriptions.Remove(subscription);
        }

        public void UnsubscribeAll()
        {
            foreach (var sub in Subscriptions)
            {
                OnUnsubscribe.Emit(() => sub.Key);
            }

            SubscriptionsLookup.Clear();
            Subscriptions.Clear();
            _subscriptions = new Dictionary<int, TSubscription>();
            SubscriptionActions = new List<TSubscription>();
        }

        public void Dispose()
        {
            UnsubscribeAll();
        }

        public Func<TEvent> BuildEventObjectFactory(Func<TEvent> eventObjectFactory)
        {
            TEvent ev = default(TEvent);
            var alreadyCreated = false;

            TEvent CreateEvent()
            {
                if (!alreadyCreated)
                {
                    ev = eventObjectFactory == null ? (TEvent) (object) null : eventObjectFactory();
                }

                alreadyCreated = true;
                return ev;
            }

            switch (BackfireMode)
            {
                case BackfireMode.All:
                    EventHistory.Add(CreateEvent());
                    break;
                case BackfireMode.Last:
                    EventHistory.Clear();
                    EventHistory.Add(CreateEvent());
                    break;
                case BackfireMode.None:
                    EventHistory.Clear();
                    break;
            }

            return () => { return CreateEvent(); };
        }

        protected EventSubscription SubscribeInternal(TSubscription action)
        {
            if (_subscriptions == null)
            {
                _subscriptions = new Dictionary<int, TSubscription>();
            }

            var id = ++_subscriptionId;
            _subscriptions.Add(id, action);
            SubscriptionActions = _subscriptions.Values.ToList();
            var sub = new EventSubscription(this, id);
            OnSubscribe.Emit(() => sub);
            return sub;
        }
    }
}