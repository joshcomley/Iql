using System.Threading.Tasks;

namespace Iql.Queryable.Data.Http
{
    public interface IHttpProvider
    {
        Task<IHttpResult<TResult>> Get<TResult>(string uri, IHttpRequest payload = null);
        Task<IHttpResult<TResult>> Post<TResult>(string uri, IHttpRequest payload = null);
        Task<IHttpResult<TResult>> Put<TResult>(string uri, IHttpRequest payload = null);
        Task<IHttpResult<TResult>> Delete<TResult>(string uri, IHttpRequest payload = null);
    }
}