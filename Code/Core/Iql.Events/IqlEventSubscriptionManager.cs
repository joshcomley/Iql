using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Iql.Events
{
    public class IqlEventSubscriberManager : IDisposable
    {
        private bool _subscriptionsDelayedInitialized;
        private List<EventSubscription> _subscriptionsDelayed;
        private List<EventSubscription> _subscriptions { get { if(!_subscriptionsDelayedInitialized) { _subscriptionsDelayedInitialized = true; _subscriptionsDelayed = new List<EventSubscription>(); } return _subscriptionsDelayed; } set { _subscriptionsDelayedInitialized = true; _subscriptionsDelayed = value; } }

        public void Pause(string key = null)
        {
            for (var i = 0; i < _subscriptions.Count; i++)
            {
                if (key == null || _subscriptions[i].Key == key)
                {
                    _subscriptions[i].Pause();
                }
            }
        }

        public void PauseWhile(Action action, string key = null)
        {
            Pause(key);
            action();
            Resume(key);
        }

        public async Task PauseWhileAsync(Func<Task> action, string key = null)
        {
            Pause(key);
            await action();
            Resume(key);
        }

        public void Resume(string key = null)
        {
            for (var i = 0; i < _subscriptions.Count; i++)
            {
                if (key == null || _subscriptions[i].Key == key)
                {
                    _subscriptions[i].Resume();
                }
            }
        }

        public void Dispose()
        {
            UnsubscribeAll();
        }

        public void UnsubscribeAll(string key = null)
        {
            for (var i = 0; i < _subscriptions.Count; i++)
            {
                if(key == null || _subscriptions[i].Key == key)
                {
                    _subscriptions[i].Unsubscribe();
                }
            }
        }

        public EventSubscription[] SubscribeAll(IEnumerable<IEventSubscriberBase> subscribers, Action<IEventSubscriberBase, object> action, string key = null,
            int? allowedCount = null)
        {
            var subscriptions = new List<EventSubscription>();
            foreach (var subscriber in subscribers)
            {
                var sub = subscriber.Subscribe(_ => { action(subscriber, _); },
                    key, allowedCount);
                _subscriptions.Add(sub);
                subscriptions.Add(sub);
            }
            return subscriptions.ToArray();
        }

        public EventSubscription[] SubscribeAllAsync(IEnumerable<IAsyncEventSubscriberBase> subscribers, Func<IAsyncEventSubscriberBase, object, Task> action, string key = null,
            int? allowedCount = null)
        {
            var subscriptions = new List<EventSubscription>();
            foreach (var subscriber in subscribers)
            {
                var sub = subscriber.SubscribeAsync(async _ => { await action(subscriber, _); },
                    key, allowedCount);
                _subscriptions.Add(sub);
                subscriptions.Add(sub);
            }
            return subscriptions.ToArray();
        }

        public EventSubscription Subscribe<T>(IEventSubscriber<T> subscriber, Action<T> action, string key = null, int? allowedCount = null)
        {
            var sub = subscriber.Subscribe(action, key, allowedCount);
            _subscriptions.Add(sub);
            return sub;
        }

        public EventSubscription SubscribeOnce<T>(IEventSubscriber<T> subscriber, Action<T> action, string key = null)
        {
            var sub = subscriber.SubscribeOnce(action, key);
            _subscriptions.Add(sub);
            return sub;
        }

        public EventSubscription SubscribeAsync<T>(IAsyncEventSubscriber<T> subscriber, Func<T, Task> action, string key = null, int? allowedCount = null)
        {
            var sub = subscriber.SubscribeAsync(action, key, allowedCount);
            _subscriptions.Add(sub);
            return sub;
        }

        public EventSubscription SubscribeOnceAsync<T>(IAsyncEventSubscriber<T> subscriber, Func<T, Task> action, string key = null)
        {
            var sub = subscriber.SubscribeOnceAsync(action, key);
            _subscriptions.Add(sub);
            return sub;
        }

        //public class IqlEventService
        //{
        //    public entitySelected = new EventEmitter<IqlEntitySelectedEvent<any>>();
        //    public entityHighlight = new EventEmitter<IqlEntityHighlightedEvent<any>>();

        //    public highlight(entity: any, on: boolean)
        //    {
        //        this._entityHighlight.EmitIfExists(() => new IqlEntityHighlightedEvent(on, entity));
        //    }

        //    private dic = new Dictionary<any, IqlEventSubscriber>();
        //    public getSubscriber(key: any) : IqlEventSubscriber {
        //        if (!this.dic.ContainsKey(key)) {
        //            this.dic.Set(key, new IqlEventSubscriber(this));
        //        }
        //        return this.dic.Get(key);
        //    }
        //}
    }
}
