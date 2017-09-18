namespace Iql.Queryable.Data.Http
{
    public interface IHttpResult<TResult>
    {
        TResult ResponseData { get; set; }
        bool Success { get; set; }
    }
}