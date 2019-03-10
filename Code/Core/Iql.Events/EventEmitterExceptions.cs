using System;
using System.Collections.Generic;

namespace Iql.Events
{
    public class EventEmitterExceptions
    {
        private static readonly Dictionary<Type, Type> ThrowAlways = new Dictionary<Type, Type>();
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