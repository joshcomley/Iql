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
        public EventEmitter<IEventSubscriberRoot> HasSubscriptionChanged => _hasSubscriptionChanged = _hasSubscriptionChanged ?? new EventEmitter<IEventSubscriberRoot>();

        public bool HasSubscription => _hasSubscription;
        public Action<IEventSubscriberRoot> OnHasSubscription { get; set; }
        public Action<IEventSubscriberRoot> OnHasNoSubscription { get; set; }

        public EventEmitter<IEventSubscriberRoot> SubscriptionCountChanged => _subscriptionCountChanged = _subscriptionCountChanged ?? new EventEmitter<IEventSubscriberRoot>();

        private int _subscriptionCount = 0;
        public int SubscriptionCount => _subscriptionCount;

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
            if (subscriptionActions == null)
            {
                return null;
            }
            return subscriptionActions
                .OrderBy(_ => _.Priority)
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
        private EventEmitter<IEventSubscriberRoot> _subscriptionCountChanged;
        private EventEmitter<IEventSubscriberRoot> _hasSubscriptionChanged;
        private bool _hasSubscription;

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
                    _onUnsubscribe.EmitIfExists(() => sub);
                }
            }

            SubscriptionsById.Remove(subscription);
            UpdateSubscriptionCount();
        }

        public void UnsubscribeAll(string key = null)
        {
            if (_onUnsubscribe != null)
            {
                foreach (var sub in Subscriptions)
                {
                    if (key == null || sub.Key == key)
                    {
                        _onUnsubscribe.EmitIfExists(() => sub);
                        if (key != null)
                        {
                            SubscriptionsById.Remove(sub.Id);
                            ;
                        }
                    }
                }
            }

            if (key == null)
            {
                SubscriptionsById.Clear();
                Subscriptions.Clear();
            }
            UpdateSubscriptionCount();
        }

        public override void Dispose()
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
                    ev = eventObjectFactory == null ? (TEvent)(object)null : eventObjectFactory();
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

        protected EventSubscription SubscribeInternal(TSubscriptionAction action, string key = null, int? allowedCount = null, int? priority = null)
        {
            var id = ++_subscriptionId;
            var sub = new EventSubscription(this, id, action);
            sub.Key = key;
            sub.Priority = priority ?? 0;
            sub.AllowedCallCount = allowedCount;
            SubscriptionsById.Add(id, sub);
            _subscriptions = SubscriptionsById.Values.ToList();
            _onSubscribe.EmitIfExists(() => sub);
            UpdateSubscriptionCount();
            return sub;
        }

        private void UpdateSubscriptionCount()
        {
            var old = _subscriptionCount;
            _subscriptionCount = SubscriptionsById.Count;
            if (old != _subscriptionCount)
            {
                _subscriptionCountChanged.EmitIfExists(() => this);
            }

            UpdateHasSubscription(_subscriptionCount > 0);
        }

        private void UpdateHasSubscription(bool hasSubscription)
        {
            var old = _hasSubscription;
            _hasSubscription = hasSubscription;
            if (old != hasSubscription)
            {
                _hasSubscriptionChanged.EmitIfExists(() => this);
                if (hasSubscription)
                {
                    if (OnHasSubscription != null)
                    {
                        OnHasSubscription(this);
                    }
                }
                else
                {
                    if (OnHasNoSubscription != null)
                    {
                        OnHasNoSubscription(this);
                    }
                }
            }
        }

        protected EventEmitterBase<TEvent, TSubscriptionAction> ConfigureBase(Action<IEventSubscriberRoot> onHasSubscription,
            Action<IEventSubscriberRoot> onHasNoSubscription = null)
        {
            OnHasSubscription = onHasSubscription;
            OnHasNoSubscription = onHasNoSubscription;
            return this;
        }

        IEventSubscriberRoot IEventSubscriberRoot.Configure(Action<IEventSubscriberRoot> onHasSubscription,
            Action<IEventSubscriberRoot> onHasNoSubscription = null)
        {
            return ConfigureBase(onHasSubscription, onHasNoSubscription);
        }
    }
}
