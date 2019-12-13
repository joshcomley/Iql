namespace Iql.Entities.Events
{
    public class ValueChangedEvent<TValue, TOwner>
    {
        public TValue OldValue { get; }
        public TValue NewValue { get; }
        public TOwner Owner { get; }

        public ValueChangedEvent(TValue oldValue, TValue newValue, TOwner owner)
        {
            OldValue = oldValue;
            NewValue = newValue;
            Owner = owner;
        }
    }
}