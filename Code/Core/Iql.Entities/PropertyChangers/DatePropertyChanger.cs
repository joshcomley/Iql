using System;

namespace Iql.Entities.PropertyChangers
{
    public class DatePropertyChanger : ComplexPropertyChanger<DateTimeOffset>
    {
        private static DatePropertyChanger _instance = null;
        public static DatePropertyChanger Instance => _instance = _instance ?? new DatePropertyChanger();
        protected override bool CheckEquivalence(DateTimeOffset newValue, DateTimeOffset oldValue)
        {
            return oldValue.Ticks == newValue.Ticks;
        }

        protected override DateTimeOffset CloneValueInternal(DateTimeOffset value)
        {
            return value;
        }

        //protected override void ApplyToInternal(DateTimeOffset value, DateTimeOffset applyTo)
        //{
        //}
    }
}