namespace Iql.Queryable.Data.Http
{
    public class HttpRequest<T> : HttpRequestBase
    {
        public T Payload
        {
            get => (T)((IHttpRequest)this).Payload;
            set => ((IHttpRequest) this).Payload = value;
        }

        public HttpRequest(T payload = default(T))
        {
            Payload = payload;
        }
    }
}