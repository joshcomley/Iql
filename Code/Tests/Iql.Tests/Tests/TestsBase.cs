using System;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests
{
    public class TestsBase
    {
        protected static AppDbContext Db => TestsBlock.Db;

        [ClassInitialize]
        public static void SetUp(TestContext textContext)
        {
        }

        [ClassCleanup]
        public static void CleanUp()
        {

        }

        [TestCleanup]
        public void TestCleanUp()
        {
            TestsBlock.TestCleanUp();
        }

        public virtual void ShouldThrowException<TException>(Action action)
        {
            var exceptionCount = 0;
            try
            {
                action();
            }
            catch (Exception e)
            {
                exceptionCount++;
                Assert.IsTrue(e is TException);
            }
            Assert.AreEqual(exceptionCount, 1);
        }
    }
}