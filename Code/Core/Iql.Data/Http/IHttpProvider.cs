using System.Threading.Tasks;

namespace Iql.Queryable.Data.Http
{
    public interface IHttpProvider
    {
        Task<IHttpResult> Get(string uri, IHttpRequest payload = null);
        Task<IHttpResult> Post(string uri, IHttpRequest payload = null);
        Task<IHttpResult> Put(string uri, IHttpRequest payload = null);
        Task<IHttpResult> Delete(string uri, IHttpRequest payload = null);
    }
}