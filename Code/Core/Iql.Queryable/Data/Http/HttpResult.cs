namespace Iql.Queryable.Data.Http
{
    public class HttpResult<TResult>: IHttpResult<TResult>
    {
        public TResult ResponseData { get; set; }
        public bool Success { get; set; }

        public HttpResult(TResult responseData, bool success)
        {
            ResponseData = responseData;
            Success = success;
        }
    }
}