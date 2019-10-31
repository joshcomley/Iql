using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Iql.Events
{
    public class IqlEventManager : IDisposable
    {
        private readonly List<EventSubscription> _subscriptions = new List<EventSubscription>();

        public void Pause()
        {
            for (var i = 0; i < _subscriptions.Count; i++)
            {
                _subscriptions[i].Pause();
            }
        }

        public void Resume()
        {
            for (var i = 0; i < _subscriptions.Count; i++)
            {
                _subscriptions[i].Resume();
            }
        }

        public void Dispose()
        {
            UnsubscribeAll();
        }

        public void UnsubscribeAll()
        {
            for (var i = 0; i < _subscriptions.Count; i++)
            {
                _subscriptions[i].Unsubscribe();
            }
        }

        public EventSubscription Subscribe<T>(IEventSubscriber<T> subscriber, Action<T> action)
        {
            var sub = subscriber.Subscribe(action);
            _subscriptions.Add(sub);
            return sub;
        }

        public EventSubscription SubscribeAsync<T>(IAsyncEventSubscriber<T> subscriber, Func<T, Task> action)
        {
            var sub = subscriber.SubscribeAsync(action);
            _subscriptions.Add(sub);
            return sub;
        }

        //public class IqlEventService
        //{
        //    public entitySelected = new EventEmitter<IqlEntitySelectedEvent<any>>();
        //    public entityHighlight = new EventEmitter<IqlEntityHighlightedEvent<any>>();

        //    public highlight(entity: any, on: boolean)
        //    {
        //        this.entityHighlight.Emit(() => new IqlEntityHighlightedEvent(on, entity));
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