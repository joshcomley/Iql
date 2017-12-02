using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestSomething()
        {
            var x = 2;
            x = x * 2;
            Assert.AreEqual(4, x);
        }
    }
}