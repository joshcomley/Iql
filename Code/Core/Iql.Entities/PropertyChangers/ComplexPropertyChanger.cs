namespace Iql.Entities.PropertyChangers
{
    public abstract class ComplexPropertyChanger<TValueType> : PropertyChanger
    {
        protected override bool AreEquivalent<TValue>(TValue newValue, TValue oldValue)
        {
            if (newValue == null && oldValue != null)
            {
                return false;
            }

            if (oldValue == null && newValue != null)
            {
                return false;
            }

            return CheckEquivalence((TValueType)(object)newValue, (TValueType)(object)oldValue);
        }

        protected abstract bool CheckEquivalence(TValueType newValue, TValueType oldValue);
    }
}