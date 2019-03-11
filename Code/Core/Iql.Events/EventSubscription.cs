namespace Iql.Events
{
    public class EventSubscription
    {
        public IEventUnsubscriber EventSubscriber { get; }
        public int Id { get; }

        public EventSubscription(IEventUnsubscriber eventSubscriber, int id)
        {
            EventSubscriber = eventSubscriber;
            Id = id;
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