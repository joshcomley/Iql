using System.Collections.Generic;

namespace Iql.Queryable.Data.Http
{
    public abstract class HttpRequestBase : IHttpRequest
    {
        public List<HttpHeader> Headers { get; set; }
        public string Body { get; set; }
    }
}