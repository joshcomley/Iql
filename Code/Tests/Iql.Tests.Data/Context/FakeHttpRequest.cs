using Iql.Data.Http;

namespace Iql.Tests.Context
{
    public class FakeHttpRequest
    {
        public string Uri { get; set; }
        public IHttpRequest Body { get; set; }

        public FakeHttpRequest(string uri, IHttpRequest body)
        {
            Uri = uri;
            Body = body;
        }
    }
}