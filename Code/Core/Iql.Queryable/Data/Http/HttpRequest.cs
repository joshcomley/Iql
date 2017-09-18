namespace Iql.Queryable.Data.Http
{
    public class HttpRequest<T> : HttpRequestBase
    {
        public new T Payload { get; set; }

        public HttpRequest(T payload = default(T))
        {
            Payload = payload;
        }
    }
}