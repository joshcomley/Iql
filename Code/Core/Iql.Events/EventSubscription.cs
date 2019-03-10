namespace Iql.Events
{
    public class EventSubscription
    {
        public IEventUnsubcriber EventSubscriber { get; }
        public int Id { get; }

        public EventSubscription(IEventUnsubcriber eventSubscriber, int id)
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