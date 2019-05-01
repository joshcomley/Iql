namespace Iql.Data.Context
{
    public class SaveEventsBase : ISaveEventsInternal
    {
        private static int _activeCount = 0;
        public static int ActiveCount => _activeCount;

        public void Increment()
        {
            _activeCount++;
        }

        public void Decrement()
        {
            _activeCount--;
        }
    }
}