using System;
using System.Threading.Tasks;

namespace Iql.Tests.Context
{
    public class RequestLog
    {
        public RequestStack Posts { get; } = new RequestStack();
        public RequestStack Patches { get; } = new RequestStack();
        public RequestStack Deletes { get; } = new RequestStack();

        public static RequestLog Instance { get; private set; }

        public static async Task LogSessionAsync(Func<RequestLog, Task> action)
        {
            var old = Instance;
            Instance = new RequestLog();
            await action(Instance);
            Instance = old;
        }
    }
}