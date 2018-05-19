using System;
using System.Threading.Tasks;
using Iql.Data.Http;

namespace Iql.Tests.Context
{
    public class RequestLog
    {
        public RequestStack Posts { get; } = new RequestStack();
        public RequestStack Patches { get; } = new RequestStack();
        public RequestStack Deletes { get; } = new RequestStack();

        public async Task InterceptAsync(Func<HttpMethod, string, IHttpRequest, HttpResult> interceptor, Func<Task> action)
        {
            var oldInterceptor = Interceptor;
            Interceptor = interceptor;
            await action();
            Interceptor = oldInterceptor;
        }

        public Func<HttpMethod, string, IHttpRequest, HttpResult> Interceptor { get; set; }

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