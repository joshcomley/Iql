using Iql.OData;
using Iql.Tests.Context;

namespace Iql.Tests.Tests.OData
{
    public class ODataTestsBase : TestsBase
    {
        public HazceptionDataContext NewHazDb()
        {
            return new HazceptionDataContext(new ODataDataStore(Db.EntityConfigurationContext));
        }

        public AppDbContext NewDb()
        {
            return new AppDbContext(new ODataDataStore(Db.EntityConfigurationContext));
        }
    }
}