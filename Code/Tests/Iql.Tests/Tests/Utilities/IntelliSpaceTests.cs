using Iql.Extensions;
using Iql.Queryable.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests.Utilities
{
    [TestClass]
    public class IntelliSpaceTests
    {
        [TestMethod]
        public void TestIntelliSpace()
        {
            Assert.AreEqual("Some Name", IntelliSpace.Parse("SomeName"));
        }
    }
}