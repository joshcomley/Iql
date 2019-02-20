using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Iql.Data.Http;

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
            }
            var result = await httpClient.GetAsync(uri);
            var httpResult = GetHttpResult(result);
            return httpResult;
        }

        //public string AppedQueryStringValues(string uri, object obj)
        //{
        //    var uriBuilder = new UriBuilder(uri);
        //    var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        //    foreach (var property in obj.GetType().GetProperties())
        //    {
        //        var value = property.GetValue(obj, null);
        //        query[property.Name] = value?.ToString();
        //    }
        //    uriBuilder.Query = query.ToString();
        //    return uriBuilder.ToString();
        //}

        public async Task<IHttpResult> Post(string uri, IHttpRequest payload = null)
        {
            return await SendAsync(uri, payload, HttpMethod.Post);
        }

        private static async Task<IHttpResult> SendAsync(string uri, IHttpRequest payload, HttpMethod httpMethod)
        {
            var request = new HttpRequestMessage(httpMethod, uri);
            var http = new HttpClient();
            if (payload != null)
            {
                request.Content = new StringContent(payload.Body);
                if (payload.Headers != null)
                {
                    foreach (var header in payload.Headers)
                    {
                        http.DefaultRequestHeaders.Add(header.Name, header.Value);
                    }
                }
            }
            var result = await http.SendAsync(request);
            var httpResult = GetHttpResult(result);
            return httpResult;
        }

        private static HttpResult GetHttpResult(HttpResponseMessage result)
        {
            var httpResult = new HttpResult(
                async () => await result.Content.ReadAsStringAsync(),
                async () => await result.Content.ReadAsByteArrayAsync(),
                async () => await result.Content.ReadAsStreamAsync(),
                (HttpStatusCode)(int)result.StatusCode,
                result.Content.Headers.ContentType.MediaType,
                result.IsSuccessStatusCode);
            return httpResult;
        }

        public async Task<IHttpResult> Put(string uri, IHttpRequest payload = null)
        {
            return await SendAsync(uri, payload, HttpMethod.Put);
        }

        public async Task<IHttpResult> Delete(string uri, IHttpRequest payload = null)
        {
            var http = new HttpClient();
            if (payload != null)
            {
                if (payload.Headers != null)
                {
                    foreach (var header in payload.Headers)
                    {
                        http.DefaultRequestHeaders.Add(header.Name, header.Value);
                    }
                }
            }
            var result = await http.DeleteAsync(uri);
            var httpResult = GetHttpResult(result);
            return httpResult;
        }
    }
}