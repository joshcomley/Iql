using System.Collections.Generic;

namespace Iql.Queryable.Data.Http
{
    public interface IHttpRequest
    {
        List<HttpHeader> Headers { get; set; }
        object Payload { get; set; }
    }
}