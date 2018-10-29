using System;

namespace Iql.Entities.PropertyChangers
{
    public class DatePropertyChanger : ComplexPropertyChanger<DateTimeOffset>
    {
        public static DatePropertyChanger Instance { get; } = new DatePropertyChanger();
        protected override bool CheckEquivalence(DateTimeOffset newValue, DateTimeOffset oldValue)
        {
            return oldValue.Ticks == newValue.Ticks;
        }
    }
}