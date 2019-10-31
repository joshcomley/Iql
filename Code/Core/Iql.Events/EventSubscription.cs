namespace Iql.Events
{
    public class EventSubscription
    {
        public IEventUnsubscriber EventSubscriber { get; }
        public object Action { get; }
        public int Id { get; }
        
        public void Pause()
        {
            Paused = true;
        }

        public void Resume()
        {
            Paused = false;
        }

        public bool Paused { get; set; }

        public EventSubscription(IEventUnsubscriber eventSubscriber, int id, object action)
        {
            EventSubscriber = eventSubscriber;
            Id = id;
            Action = action;
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