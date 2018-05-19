namespace Iql.Queryable.Data.Http
{
    public class HttpRequest : HttpRequestBase
    {
        public HttpRequest(string body = null)
        {
            Body = body;
        }
    }
}