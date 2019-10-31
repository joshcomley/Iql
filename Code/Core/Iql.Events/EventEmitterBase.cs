using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Iql.Events
{
    public class EventEmitterBase<TEvent, TSubscriptionAction> : IEventSubscriberRoot
    {
        public EventEmitterBase(BackfireMode backfireMode = BackfireMode.None)
        {
            BackfireMode = backfireMode;
        }

        private BackfireMode _backfireMode = BackfireMode.None;
        private EventEmitter<EventSubscription> _onSubscribe;
        private EventEmitter<EventSubscription> _onUnsubscribe;
        private int _subscriptionId;

        protected List<TSubscriptionAction> ResolveSubscriptionActions(IEnumerable<EventSubscription> subscriptions)
        {
            List<EventSubscription> subscriptionActions;
            if (subscriptions == null)
            {
                subscriptionActions = SubscriptionActions;
            }
            else
            {
                subscriptionActions = new List<EventSubscription>();
                foreach (var eventSubscription in subscriptions)
                {
                    var match = FindSubscription(eventSubscription.Id);
                    if (match != null && match == eventSubscription && eventSubscription.EventSubscriber == this)
                    {
                        subscriptionActions.Add(match);
                    }
                }
            }
            return subscriptionActions == null ? null : subscriptionActions.Where(_ => !_.Paused).Select(_ => (TSubscriptionAction)_.Action).ToList();
        }

        protected EventSubscription FindSubscription(int id)
        {
            if (SubscriptionsLookup.ContainsKey(id) && SubscriptionsLookup.ContainsKey(id))
            {
                return SubscriptionsLookup[id];
            }
            return null;
        }

        protected List<EventSubscription> SubscriptionActions { get; set; }

        [JsonIgnore]
        public EventEmitter<EventSubscription> OnSubscribe =>
            _onSubscribe = _onSubscribe ?? new EventEmitter<EventSubscription>();

        [JsonIgnore]
        public EventEmitter<EventSubscription> OnUnsubscribe =>
            _onUnsubscribe = _onUnsubscribe ?? new EventEmitter<EventSubscription>();

        protected Dictionary<EventSubscription, EventSubscription> Subscriptions { get; } =
            new Dictionary<EventSubscription, EventSubscription>();

        protected Dictionary<int, EventSubscription> SubscriptionsLookup { get; } =
            new Dictionary<int, EventSubscription>();

        public void ClearBackfires()
        {
            Backfires.Clear();
        }

        public bool HasBackfires => Backfires.Any();
        public int BackfireCount => Backfires.Count;

        public BackfireMode BackfireMode
        {
            get => _backfireMode;
            set
            {
                var old = _backfireMode;
                if (old != BackfireMode.None && value == BackfireMode.None)
                {
                    Backfires.Clear();
                }

                _backfireMode = value;
            }
        }

        public List<TEvent> Backfires { get; } = new List<TEvent>();

        public void Unsubscribe(int subscription)
        {
            if (SubscriptionsLookup == null)
            {
                return;
            }

            var sub = SubscriptionsLookup.ContainsKey(subscription) ? SubscriptionsLookup[subscription] : null;
            if (sub != null)
            {
                Subscriptions.Remove(sub);
                if (_onUnsubscribe != null)
                {
                    OnUnsubscribe.Emit(() => sub);
                }
            }

            SubscriptionsLookup.Remove(subscription);
        }

        public void UnsubscribeAll()
        {
            if (_onUnsubscribe != null)
            {
                foreach (var sub in Subscriptions)
                {
                    OnUnsubscribe.Emit(() => sub.Key);
                }
            }

            SubscriptionsLookup.Clear();
            Subscriptions.Clear();
            SubscriptionActions = new List<EventSubscription>();
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
                    Backfires.Add(CreateEvent());
                    break;
                case BackfireMode.Last:
                    Backfires.Clear();
                    Backfires.Add(CreateEvent());
                    break;
                case BackfireMode.None:
                    Backfires.Clear();
                    break;
            }

            return () => { return CreateEvent(); };
        }

        protected EventSubscription SubscribeInternal(TSubscriptionAction action)
        {
            var id = ++_subscriptionId;
            var sub = new EventSubscription(this, id, action);
            SubscriptionsLookup.Add(id, sub);
            SubscriptionActions = SubscriptionsLookup.Values.ToList();
            if (_onSubscribe != null)
            {
                OnSubscribe.Emit(() => sub);
            }
            return sub;
        }
    }
}