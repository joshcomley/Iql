using System.Collections.Generic;
using System.Linq;

namespace Iql.Tests.Data.Context
{
    public class RequestStack
    {
        private List<FakeHttpRequest> _requests = new List<FakeHttpRequest>();
        public int Count => _requests.Count;

        public FakeHttpRequest Pop()
        {
            var r = _requests;
            _requests = new List<FakeHttpRequest>();
            for (var i = 0; i < r.Count - 1; i++)
            {
                _requests.Add(r[i]);
            }
            return r.LastOrDefault();
        }

        internal void Add(FakeHttpRequest request)
        {
            _requests.Add(request);
        }
    }
}