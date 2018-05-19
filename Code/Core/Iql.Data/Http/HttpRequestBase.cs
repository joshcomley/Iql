using System.Collections.Generic;

namespace Iql.Data.Http
{
    public abstract class HttpRequestBase : IHttpRequest
    {
        public List<HttpHeader> Headers { get; set; }
        public string Body { get; set; }
    }
}