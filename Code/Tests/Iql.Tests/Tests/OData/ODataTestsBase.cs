using Iql.OData.Data;
using Iql.Tests.Context;

namespace Iql.Tests.Tests.OData
{
    public class ODataTestsBase : TestsBase
    {
        public HazceptionDataContext NewDb()
        {
            return new HazceptionDataContext(new ODataDataStore());
        }
    }
}