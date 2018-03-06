using System;
using System.Threading.Tasks;
using Iql.Queryable.Data.Methods;

namespace Iql.OData.Methods
{
    public class ODataMethodRequest : ODataMethodRequestBase
    {
        public Func<Task<MethodResult>> SubmitAsync { get; set; }

        public ODataMethodRequest(ODataDataStore dataStore, string uri, Func<Task<MethodResult>> submitAsync) : base(dataStore, uri)
        {
            SubmitAsync = submitAsync;
        }
    }
}