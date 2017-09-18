using System.Collections.Generic;

namespace Iql.Queryable.Data.Http
{
    public class HttpRequestBase : IHttpRequest
    {
        public List<HttpHeader> Headers { get; set; }
        public object Payload { get; set; }
    }
}