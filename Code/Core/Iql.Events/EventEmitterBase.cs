using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Iql.Events
{
    public class SubscriptionAction<TSubscriptionAction>
    {
        public EventSubscription Subscription { get; }
        public TSubscriptionAction Action { get; }

        public SubscriptionAction(EventSubscription subscription, TSubscriptionAction action)
        {
            Subscription = subscription;
            Action = action;
        }
    }
    public abstract class EventEmitterBase<TEvent, TSubscriptionAction> : EventEmitterRoot, IEventSubscriberRoot
    {
        protected EventEmitterBase(BackfireMode backfireMode = BackfireMode.None)
        {
            BackfireMode = backfireMode;
        }

        private BackfireMode _backfireMode = BackfireMode.None;
        private EventEmitter<EventSubscription> _onSubscribe;
        private EventEmitter<EventSubscription> _onUnsubscribe;
        private int _subscriptionId;

        protected List<SubscriptionAction<TSubscriptionAction>> ResolveSubscriptionActions(IEnumerable<EventSubscription> subscriptions)
        {
            List<EventSubscription> subscriptionActions;
            if (subscriptions == null)
            {
                subscriptionActions = Subscriptions;
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
            if(subscriptionActions == null)
            {
                return null;
            }
            return subscriptionActions
                .Select(_ => new SubscriptionAction<TSubscriptionAction>(_, (TSubscriptionAction)_.Action)).ToList();
        }

        protected EventSubscription FindSubscription(int id)
        {
            if (SubscriptionsById.ContainsKey(id) && SubscriptionsById.ContainsKey(id))
            {
                return SubscriptionsById[id];
            }
            return null;
        }

        [JsonIgnore]
        public EventEmitter<EventSubscription> OnSubscribe =>
            _onSubscribe = _onSubscribe ?? new EventEmitter<EventSubscription>();

        [JsonIgnore]
        public EventEmitter<EventSubscription> OnUnsubscribe =>
            _onUnsubscribe = _onUnsubscribe ?? new EventEmitter<EventSubscription>();

        private List<EventSubscription> _subscriptions = null;
        protected List<EventSubscription> Subscriptions =>
            _subscriptions = _subscriptions ?? new List<EventSubscription>();
        private Dictionary<int, EventSubscription> _subscriptionsById;

        protected Dictionary<int, EventSubscription> SubscriptionsById => _subscriptionsById = _subscriptionsById ?? new Dictionary<int, EventSubscription>();

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
        private List<TEvent> _backfires;

        public List<TEvent> Backfires => _backfires = _backfires ?? new List<TEvent>();

        public void Unsubscribe(int subscription)
        {
            if (SubscriptionsById == null)
            {
                return;
            }

            var sub = SubscriptionsById.ContainsKey(subscription) ? SubscriptionsById[subscription] : null;
            if (sub != null)
            {
                Subscriptions.Remove(sub);
                if (_onUnsubscribe != null)
                {
                    OnUnsubscribe.Emit(() => sub);
                }
            }

            SubscriptionsById.Remove(subscription);
        }

        public void UnsubscribeAll(string key = null)
        {
            if (_onUnsubscribe != null)
            {
                foreach (var sub in Subscriptions)
                {
                    if(key == null || sub.Key == key)
                    {
                        OnUnsubscribe.Emit(() => sub);
                        if (key != null)
                        {
                            SubscriptionsById.Remove(sub.Id);
;                        }
                    }
                }
            }

            if (key == null)
            {
                SubscriptionsById.Clear();
                Subscriptions.Clear();
            }
            else
            {

            }
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

        protected EventSubscription SubscribeInternal(TSubscriptionAction action, string key = null, int? allowedCount = null)
        {
            var id = ++_subscriptionId;
            var sub = new EventSubscription(this, id, action);
            sub.Key = key;
            sub.AllowedCallCount = allowedCount;
            SubscriptionsById.Add(id, sub);
            _subscriptions = SubscriptionsById.Values.ToList();
            if (_onSubscribe != null)
            {
                OnSubscribe.Emit(() => sub);
            }
            return sub;
        }
    }
}