using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql.Events
{
    public class EventEmitterBase<TEvent, TSubscriptionAction> : IEventUnsubscriber
    {
        public EventEmitterBase(BackfireMode backfireMode = BackfireMode.None)
        {
            BackfireMode = backfireMode;
        }

        private BackfireMode _backfireMode = BackfireMode.None;
        private EventEmitter<EventSubscription> _onSubscribe;
        private EventEmitter<EventSubscription> _onUnsubscribe;
        private int _subscriptionId;
        private Dictionary<int, TSubscriptionAction> _subscriptionActions;

        protected List<TSubscriptionAction> ResolveSubscriptionActions(IEnumerable<EventSubscription> subscriptions)
        {
            List<TSubscriptionAction> subscriptionActions;
            if (subscriptions == null)
            {
                subscriptionActions = SubscriptionActions;
            }
            else
            {
                subscriptionActions = new List<TSubscriptionAction>();
                foreach (var eventSubscription in subscriptions)
                {
                    var match = FindSubscription(eventSubscription.Id);
                    if (match != null && match.Subscription == eventSubscription && eventSubscription.EventSubscriber == this)
                    {
                        subscriptionActions.Add(match.Action);
                    }
                }
            }
            return subscriptionActions;
        }

        protected SubscriptionMatch<TSubscriptionAction> FindSubscription(int id)
        {
            if (_subscriptionActions.ContainsKey(id) && SubscriptionsLookup.ContainsKey(id))
            {
                return new SubscriptionMatch<TSubscriptionAction>(SubscriptionsLookup[id], _subscriptionActions[id]);
            }
            return null;
        }

        protected List<TSubscriptionAction> SubscriptionActions { get; set; }

        public EventEmitter<EventSubscription> OnSubscribe =>
            _onSubscribe = _onSubscribe ?? new EventEmitter<EventSubscription>();

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
            if (_subscriptionActions == null)
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
            _subscriptionActions.Remove(subscription);
        }

        public void UnsubscribeAll()
        {
            foreach (var sub in Subscriptions)
            {
                OnUnsubscribe.Emit(() => sub.Key);
            }

            SubscriptionsLookup.Clear();
            Subscriptions.Clear();
            _subscriptionActions = new Dictionary<int, TSubscriptionAction>();
            SubscriptionActions = new List<TSubscriptionAction>();
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
            if (_subscriptionActions == null)
            {
                _subscriptionActions = new Dictionary<int, TSubscriptionAction>();
            }

            var id = ++_subscriptionId;
            _subscriptionActions.Add(id, action);
            SubscriptionActions = _subscriptionActions.Values.ToList();
            var sub = new EventSubscription(this, id);
            SubscriptionsLookup.Add(id, sub);
            OnSubscribe.Emit(() => sub);
            return sub;
        }
    }
}