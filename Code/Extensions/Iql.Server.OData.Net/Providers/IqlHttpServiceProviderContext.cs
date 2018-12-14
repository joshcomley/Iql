using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Iql.Server.OData.Net
{
    public class IqlHttpServiceProviderContext
    {
        public HttpContext HttpContext { get; set; }
        public Func<string, Task<object>> ResolveUserIdByName { get; set; }

        public IqlHttpServiceProviderContext(HttpContext httpContext, Func<string, Task<object>> resolveUserIdByName)
        {
            HttpContext = httpContext;
            ResolveUserIdByName = resolveUserIdByName;
        }
    }
}