namespace Iql.Queryable.Data.Http
{
    public class HttpResult : IHttpResult
    {
        public string ResponseData { get; set; }
        public bool Success { get; set; }

        public HttpResult(string responseData, bool success)
        {
            ResponseData = responseData;
            Success = success;
        }
    }
}