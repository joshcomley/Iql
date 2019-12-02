using System;
using System.Collections.Generic;

namespace Iql.Events
{
    public class EventEmitterExceptions
    {
        private static bool ThrowAlwaysDelayedInitialized;
        private static Dictionary<Type, Type> ThrowAlwaysDelayed;
        private static Dictionary<Type, Type> ThrowAlways { get { if(!ThrowAlwaysDelayedInitialized) { ThrowAlwaysDelayedInitialized = true; ThrowAlwaysDelayed = new Dictionary<Type, Type>(); } return ThrowAlwaysDelayed; } set { ThrowAlwaysDelayedInitialized = true; ThrowAlwaysDelayed = value; } }
        public static void EnsureIsThrown<T>()
            where T : Exception
        {
            var type = typeof(T);
            if (!ThrowAlways.ContainsKey(type))
            {
                ThrowAlways.Add(type, type);
            }
        }

        public static bool ShouldBeThrown(Exception exception)
        {
            return ThrowAlways.ContainsKey(exception.GetType());
        }
    }
}