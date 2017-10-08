#if TypeScript
using Iql.JavaScript.QueryToJavaScript;
#else
#endif
using Iql.Queryable.Data.DataStores;

namespace Iql.TestBed
{
    public class AppDbContext : TunnelDataContextBase
    {
        public AppDbContext(IDataStore dataStore) : base(dataStore)
        {
            ODataConfiguration.ApiUriBase = @"http://localhost:28000/odata/";
        }
    }
}