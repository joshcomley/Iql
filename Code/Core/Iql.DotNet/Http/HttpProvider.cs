using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Iql.Queryable.Data.Http;
using Newtonsoft.Json;

namespace Iql.DotNet.Http
{
    public class DotNetHttpProvider : IHttpProvider
    {
        public async Task<IHttpResult> Get(string uri, IHttpRequest payload = null)
        {
            var httpClient = new HttpClient();
            if (payload != null)
            {
                foreach (var header in payload.Headers)
                {
                    httpClient.DefaultRequestHeaders.Add(header.Name, header.Value);
                }
                uri = AppedQueryStringValues(uri, payload.Payload);
            }
            var result = await httpClient.GetAsync(uri);
            var dataString = await result.Content.ReadAsStringAsync();
            var httpResult = new HttpResult(dataString, result.IsSuccessStatusCode);
            return httpResult;
        }

        public string AppedQueryStringValues(string uri ,object obj)
        {
            var uriBuilder = new UriBuilder(uri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            foreach (var property in obj.GetType().GetProperties())
            {
                var value = property.GetValue(obj, null);
                    query[property.Name] = value?.ToString();
            }
            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();
        }

        public Task<IHttpResult> Post(string uri, IHttpRequest payload = null)
        {
            throw new System.NotImplementedException();
        }

        public Task<IHttpResult> Put(string uri, IHttpRequest payload = null)
        {
            throw new System.NotImplementedException();
        }

        public Task<IHttpResult> Delete(string uri, IHttpRequest payload = null)
        {
            throw new System.NotImplementedException();
        }
    }
}