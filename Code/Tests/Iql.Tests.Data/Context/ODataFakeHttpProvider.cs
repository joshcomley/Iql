using System;
using System.Threading.Tasks;
using Iql.Queryable.Data.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Iql.Tests.Context
{
    public class ODataFakeHttpProvider : IHttpProvider
    {
        public async Task<IHttpResult> Get(string uri, IHttpRequest payload = null)
        {
            IHttpResult interceptedResult;
            if (TryIntercept(HttpMethod.Get, uri, payload, out interceptedResult))
            {
                return interceptedResult;
            }
            var responseData = ODataFakeRequestResults.HttpGetResponse(uri);
            return new HttpResult(responseData, responseData != null);
        }

        public async Task<IHttpResult> Post(string uri, IHttpRequest payload = null)
        {
            RequestLog.Instance?.Posts.Add(new FakeHttpRequest(uri, payload));
            IHttpResult interceptedResult;
            if (TryIntercept(HttpMethod.Post, uri, payload, out interceptedResult))
            {
                return interceptedResult;
            }
            var jobj = JObject.Parse(payload.Body);
            jobj["Id"] = 0;
            return new HttpResult(jobj.ToString(), true);
        }

        private static bool TryIntercept(HttpMethod method, string uri, IHttpRequest payload, out IHttpResult post)
        {
            if (RequestLog.Instance?.Interceptor != null)
            {
                var result = RequestLog.Instance.Interceptor(method, uri, payload);
                if (result != null)
                {
                    post = result;
                    return true;
                }
            }

            post = null;
            return false;
        }

        public async Task<IHttpResult> Put(string uri, IHttpRequest payload = null)
        {
            RequestLog.Instance?.Patches.Add(new FakeHttpRequest(uri, payload));
            IHttpResult interceptedResult;
            if (TryIntercept(HttpMethod.Patch, uri, payload, out interceptedResult))
            {
                return interceptedResult;
            }
            return new HttpResult("", true);
        }

        public async Task<IHttpResult> Delete(string uri, IHttpRequest payload = null)
        {
            RequestLog.Instance?.Deletes.Add(new FakeHttpRequest(uri, payload));
            IHttpResult interceptedResult;
            if (TryIntercept(HttpMethod.Delete, uri, payload, out interceptedResult))
            {
                return interceptedResult;
            }
            return new HttpResult("", true);
        }
    }
}