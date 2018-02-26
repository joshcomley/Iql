using System;
#if TypeScript
using Iql.JavaScript.JavaScriptExpressionToIql.Expressions.JavaScript;
#else
using Iql.DotNet;
#endif
using Iql.Queryable;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests
{
    public class TestsBase
    {
        static TestsBase()
        {
#if TypeScript
            IqlQueryableAdapter.ExpressionConverter = () => new JavaScriptExpressionToIqlConverter();
#else
            IqlQueryableAdapter.ExpressionConverter = () => new DotNetExpressionConverter();
#endif
            new HazceptionDataStore().GetData();
        }
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
                Assert.IsTrue(e is TException || e.InnerException is TException);
            }
            Assert.AreEqual(exceptionCount, 1);
        }
    }
}