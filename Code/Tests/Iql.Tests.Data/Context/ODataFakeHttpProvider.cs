﻿using System;
using System.Threading.Tasks;
using Iql.Queryable.Data.Http;

namespace Iql.Tests.Context
{
    public class ODataFakeHttpProvider : IHttpProvider
    {
        public async Task<IHttpResult> Get(string uri, IHttpRequest payload = null)
        {
            var responseData = ODataFakeRequestResults.Get(uri);
            return new HttpResult(responseData, responseData != null);
        }

        public async Task<IHttpResult> Post(string uri, IHttpRequest payload = null)
        {
            RequestLog.Instance.Posts.Add(new FakeHttpRequest(uri, payload));
            return new HttpResult(payload.Body, true);
        }

        public async Task<IHttpResult> Put(string uri, IHttpRequest payload = null)
        {
            RequestLog.Instance.Patches.Add(new FakeHttpRequest(uri, payload));
            return new HttpResult("", true);
        }

        public async Task<IHttpResult> Delete(string uri, IHttpRequest payload = null)
        {
            RequestLog.Instance.Deletes.Add(new FakeHttpRequest(uri, payload));
            return new HttpResult("", true);
        }
    }
}