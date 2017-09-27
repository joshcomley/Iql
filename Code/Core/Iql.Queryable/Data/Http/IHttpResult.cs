namespace Iql.Queryable.Data.Http
{
    public interface IHttpResult
    {
        string ResponseData { get; set; }
        bool Success { get; set; }
    }
}