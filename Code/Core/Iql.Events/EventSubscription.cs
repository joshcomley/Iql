using System;
using System.Threading.Tasks;

namespace Iql.Events
{
    public class EventSubscription
    {
        public int Priority { get; set; }
        public IEventUnsubscriber EventSubscriber { get; }
        public object Action { get; }
        public int Id { get; }
        public string Key { get; set; }
        public bool KeepSubscriptionAfterCallCount { get; set; }
        public bool IsWithinAllowedCallCount => AllowedCallCount == null || AllowedCallCount.Value > CallCount;
        public int? AllowedCallCount { get; set; }
        private int _callCount = 0;
        public int CallCount => _callCount;

        /// <summary>
        /// For internal use only
        /// </summary>
        public void RegisterCalled()
        {
            _callCount++;
        }

        public void Pause()
        {
            Paused = true;
        }

        public void PauseWhile(Action action)
        {
            Pause();
            action();
            Resume();
        }

        public async Task PauseWhileAsync(Func<Task> action)
        {
            Pause();
            await action();
            Resume();
        }

        public void Resume()
        {
            Paused = false;
        }

        public bool Paused { get; set; }

        public EventSubscription(IEventUnsubscriber eventSubscriber, int id, object action, int priority = 0)
        {
            EventSubscriber = eventSubscriber;
            Id = id;
            Action = action;
            Priority = priority;
        }

        private bool _unsubscribed;
        public void Unsubscribe()
        {
            if (!_unsubscribed)
            {
                _unsubscribed = true;
                EventSubscriber.Unsubscribe(Id);
            }
        }
    }
}