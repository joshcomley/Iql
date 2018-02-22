using System.Collections.Generic;

namespace Iql.Tests.Context
{
    public class RequestStack
    {
        private List<FakeHttpRequest> _requests = new List<FakeHttpRequest>();
        public int Count => _requests.Count;

        public List<FakeHttpRequest> Pop()
        {
            var r = _requests;
            _requests = new List<FakeHttpRequest>();
            return r;
        }

        internal void Add(FakeHttpRequest request)
        {
            _requests.Add(request);
        }
    }
}