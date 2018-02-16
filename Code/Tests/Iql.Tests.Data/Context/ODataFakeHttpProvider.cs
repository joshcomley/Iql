using System;
using System.Threading.Tasks;
using Iql.Queryable.Data.Http;

namespace Iql.Tests.Context
{
    public class ODataFakeHttpProvider : IHttpProvider
    {
        public async Task<IHttpResult> Get(string uri, IHttpRequest payload = null)
        {
            return new HttpResult(ODataFakeRequestResults.Get(uri), true);
        }

        public Task<IHttpResult> Post(string uri, IHttpRequest payload = null)
        {
            throw new NotImplementedException();
        }

        public Task<IHttpResult> Put(string uri, IHttpRequest payload = null)
        {
            throw new NotImplementedException();
        }

        public Task<IHttpResult> Delete(string uri, IHttpRequest payload = null)
        {
            throw new NotImplementedException();
        }
    }
}