using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Iql.Events
{
    public static class EventEmitterExtensions
    {
        public static TEvent EmitIfExists<TEvent>(this IEventEmitter<TEvent> emitter, Func<TEvent> eventObjectFactory = null,
            Action<TEvent> afterEvent = null, IEnumerable<EventSubscription> subscriptions = null)
        {
            if (emitter == null)
            {
                return default(TEvent);
            }
            return emitter.Emit(eventObjectFactory, afterEvent, subscriptions);
        }

        public static async Task<TEvent> EmitIfExistsAsync<TEvent>(this IAsyncEventEmitter<TEvent> emitter, Func<TEvent> eventObjectFactory = null,
            Func<TEvent, Task> afterEvent = null, IEnumerable<EventSubscription> subscriptions = null)
        {
            if (emitter == null)
            {
                return default(TEvent);
            }
            return await emitter.EmitAsync(eventObjectFactory, afterEvent, subscriptions);
        }
    }
}
