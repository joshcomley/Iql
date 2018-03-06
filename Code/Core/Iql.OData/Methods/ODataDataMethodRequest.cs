using System;
using System.Threading.Tasks;
using Iql.Queryable.Data.Methods;

namespace Iql.OData.Methods
{
    public class ODataDataMethodRequest<T> : ODataMethodRequestBase
    {
        public ODataDataMethodRequest(ODataDataStore dataStore, string uri, Func<Task<DataMethodResult<T>>> submitAsync)
            : base(dataStore, uri)
        {
            SubmitAsync = submitAsync;
        }

        public Func<Task<DataMethodResult<T>>> SubmitAsync { get; set; }
    }
}