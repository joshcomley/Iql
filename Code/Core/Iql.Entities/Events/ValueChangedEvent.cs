namespace Iql.Entities.Events
{
    public class ValueChangedEvent<TValue>
    {
        public TValue OldValue { get; }
        public TValue NewValue { get; }

        public ValueChangedEvent(TValue oldValue, TValue newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}