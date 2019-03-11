namespace Iql.Events
{
    public class SubscriptionMatch<TSubscriptionAction>
    {
        public TSubscriptionAction Action { get; set; }
        public EventSubscription Subscription { get; set; }

        public SubscriptionMatch(EventSubscription subscription, TSubscriptionAction action)
        {
            Subscription = subscription;
            Action = action;
        }
    }
}