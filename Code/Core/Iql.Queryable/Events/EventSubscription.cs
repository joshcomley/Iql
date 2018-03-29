namespace Iql.Queryable.Events
{
    public class EventSubscription
    {
        public IEventSubscriberBase EventSubscriber { get; }
        public int Id { get; }

        public EventSubscription(IEventSubscriberBase eventSubscriber, int id)
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