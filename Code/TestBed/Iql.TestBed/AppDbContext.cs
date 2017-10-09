#if TypeScript
using Iql.JavaScript.QueryToJavaScript;
#else
#endif
using Iql.DotNet.Http;
using Iql.OData.Data;

namespace Iql.TestBed
{
    public class AppDbContext : TunnelDataContextBase
    {
        public AppDbContext() : base(new ODataDataStore())
        {
            ODataConfiguration.ApiUriBase = @"http://localhost:28000/odata/";
            ODataConfiguration.HttpProvider = new DotNetHttpProvider();
        }
    }
}