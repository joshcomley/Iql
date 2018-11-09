namespace Iql.Entities.PropertyChangers
{
    public abstract class ComplexPropertyChanger<TValueType> : PropertyChanger
    {
        public override bool AreEquivalent<TValue>(TValue newValue, TValue oldValue)
        {
            if (newValue == null && oldValue != null)
            {
                return false;
            }

            if (oldValue == null && newValue != null)
            {
                return false;
            }

            if (oldValue == null)
            {
                return true;
            }

            return CheckEquivalence((TValueType)(object)newValue, (TValueType)(object)oldValue);
        }

        protected abstract bool CheckEquivalence(TValueType newValue, TValueType oldValue);
        public override TValue CloneValue<TValue>(TValue value)
        {
            if (value == null)
            {
                return default(TValue);
            }
            return (TValue)(object)CloneValueInternal((TValueType)(object)value);
        }

        //public override void ApplyTo<TValue>(TValue source, TValue applyTo)
        //{
        //    ApplyToInternal((TValueType)(object)source, (TValueType)(object)applyTo);
        //}

        protected abstract TValueType CloneValueInternal(TValueType value);
        //protected abstract void ApplyToInternal(TValueType value, TValueType applyTo);
    }
}