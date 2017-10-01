using System.Collections.Generic;

namespace Iql.Queryable.Data.Http
{
    public abstract class HttpRequestBase : IHttpRequest
    {
        public List<HttpHeader> Headers { get; set; }
        object IHttpRequest.Payload { get; set; }
    }
}