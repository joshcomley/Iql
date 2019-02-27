using System;
using Iql.Data.Tracking;
using Iql.OData;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests.OData
{
    public class ODataTestsBase : TestsBase
    {
        [TestCleanup]
        public override void TestCleanUp()
        {
            base.TestCleanUp();
            PersistenceKeyGenerator.New = () => Guid.NewGuid();
        }

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