using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Hazception.ApiContext.Base;
using Iql.OData.Data;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.Http;

namespace Iql.Tests.Context
{
    public class HazceptionDataContext : HazceptionDataContextBase
    {
        public HazceptionDataContext() : base(
            new ODataDataStore())
        {
            ODataConfiguration.ApiUriBase = @"http://localhost:58000/odata";
            this.ODataConfiguration.HttpProvider = new ODataHttpProvider();
        }
    }

    public class ODataHttpProvider : IHttpProvider
    {
        public async Task<IHttpResult> Get(string uri, IHttpRequest payload = null)
        {
            var result= await new HttpClient().GetAsync(uri);
            return new HttpResult(await result.Content.ReadAsStringAsync(), result.IsSuccessStatusCode);
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
