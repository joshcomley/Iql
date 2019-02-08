using Iql.OData;
using Iql.Tests.Context;

namespace Iql.Tests.Tests.OData
{
    public class ODataTestsBase : TestsBase
    {
        public HazceptionDataContext NewHazDb()
        {
            return new HazceptionDataContext(new ODataDataStore());
        }

        public AppDbContext NewDb()
        {
            return new AppDbContext(new ODataDataStore());
        }
    }
}