using System.Collections.Generic;

namespace Iql.Data.Http
{
    public interface IHttpRequest
    {
        List<HttpHeader> Headers { get; set; }
        string Body { get; set; }
    }
}