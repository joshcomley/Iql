using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class Md5Tests
    {
        [TestMethod]
        public void TestMd5()
        {
            Assert.AreEqual("7791bb51dbd8d117033a4cb39218da53", Md5.Hash("hey2"));
            Assert.AreEqual("6057f13c496ecf7fd777ceb9e79ae285", Md5.Hash("hey"));
        }
    }
}